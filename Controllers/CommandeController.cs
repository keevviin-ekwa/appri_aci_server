using ApproACI.Models;
using ApproACI.Repositories.Interfaces;
using ApproACI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace ApproACI.Controllers
{
    
    public class CommandeController : GenericController 
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommandeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // GET: api/<MarquesController>
        [HttpGet]
        public async Task<IActionResult> GetAllCommandes()
        {
            var Commandes = await _unitOfWork.Commandes.GetAll(includes: new List<string> {"Matiere"});
            string message = string.Empty;
            ResponseObject<List<Commande>> response = new ResponseObject<List<Commande>>
            {
                Data = (List<Commande>)Commandes,
                Status = ResponseStatus.SUCCESSFUL.ToString(),
                Message = message,
                DevelopperMessage = "SUCCES",
            };

            return Ok(response);
        }


        [HttpPut]
        [Route("ReceiveCommand/{Id}")]
        public async Task<IActionResult> ReceiveCommande(int Id, [FromBody] double QteRecu)
        {
            try
            {
                var Commandes = await _unitOfWork.Commandes.Get(c => c.Id == Id, includes: new List<string> { "Matiere" });
                var Matiere = Commandes.Matiere;
                var ContributionParMP = await _unitOfWork.MatiereProduits.Get(mp => mp.MatiereId == Matiere.Id);
                var CurrentMonth = Commandes.DateLivraison;
                var stock = await _unitOfWork.Stocks.Get(s => s.MatiereId == Commandes.MatiereId && s.Mois == Commandes.DateLivraison.ToString("MM/yyyy"));
                var objectif = await _unitOfWork.Objectifs.Get(o => o.Mois == CurrentMonth.ToString("MM/yyyy"));
                stock.StockDebut += QteRecu;
                var consommation = ContributionParMP.ContributionMatiereGF * objectif.ObjectifGF + ContributionParMP.contributionMatierePF * objectif.ObjectifPF;
                stock.Consommation = consommation;
               
                stock.StockFin = stock.StockDebut - consommation;
                Commandes.QuantiteLivre += QteRecu;
                Commandes.status = Commandes.Quantite== Commandes.QuantiteLivre ? true:false;
               

                if (stock.StockFin < 0 || stock.StockDebut == 0)
                {
                    stock.Consommation = stock.StockDebut;
                    stock.StockFin = stock.StockDebut - stock.Consommation;
                }


                _unitOfWork.Commandes.Udapte(Commandes);
                var PreviousStock = stock;
                _unitOfWork.Stocks.Udapte(stock);
                _unitOfWork.Save();


                for (int i = CurrentMonth.Month + 1; i <= 12; i++)
                {
                    CurrentMonth = CurrentMonth.AddMonths(1);
                    objectif = await _unitOfWork.Objectifs.Get(o => o.Mois == CurrentMonth.ToString("MM/yyyy"));
                    var NextStock = await _unitOfWork.Stocks.Get(s => s.MatiereId == Matiere.Id && s.Mois == CurrentMonth.ToString("MM/yyyy"));
                    NextStock.StockDebut = Math.Round(PreviousStock.StockFin,1);
                    NextStock.Consommation = ContributionParMP.ContributionMatiereGF * objectif.ObjectifGF + ContributionParMP.contributionMatierePF * objectif.ObjectifPF;
                    NextStock.StockFin = NextStock.StockDebut - NextStock.Consommation;
                    PreviousStock = NextStock;

                    if (NextStock.StockFin < 0 || NextStock.StockDebut == 0)
                    {
                        NextStock.Consommation =Math.Round(NextStock.StockDebut,1);
                        NextStock.StockFin =Math.Round(NextStock.StockDebut - NextStock.Consommation,1);
                    }

                    if (NextStock.StockFin <= 0)
                    {

                        var IfCommande = await _unitOfWork.Commandes.Get(c => c.MatiereId == Matiere.Id && c.status == false);
                        if (IfCommande == null)
                        {
                            var DelaisLivraison = Matiere.DelaisAppro;
                            //on recherche le jour pour lequel si la commande était passée alors , le produit serait livré aujourd'hui
                            var dateCommande = CurrentMonth.AddDays(-DelaisLivraison);
                            Calendar cal = new CultureInfo("en-US").Calendar;

                            var _colisage = Matiere.Colissage;


                            var commande = new Commande
                            {
                                DateCommande = dateCommande,
                                DateLivraison = CurrentMonth,
                                MatiereId = Matiere.Id,
                                Quantite = _colisage,
                                status = false,
                                SemaineCommande = Helpers.GetWeekNumberOfMonth(dateCommande),
                            };

                            await _unitOfWork.Commandes.Insert(commande);
                            _unitOfWork.Save();
                            // stockDebut += commande.Quantite;
                            //S  stockFin = stockDebut - consommation;
                        }

                    }

                    _unitOfWork.Stocks.Udapte(NextStock);
                    _unitOfWork.Save();


                }

                ResponseObject<Commande> response = new ResponseObject<Commande>
                {
                    Data = null,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Commande receptionnée avec succès",
                    DevelopperMessage = "SUCCES",
                };

                return Ok(response);

            }
            catch (Exception ex)
            {

                ResponseObject<Commande> response = new ResponseObject<Commande>
                {
                    Data = null,
                    Status = ResponseStatus.FAILED.ToString(),
                    Message = "Une érreur s'est produite",
                    DevelopperMessage = "ECHEC",
                };

                return BadRequest(response);

            }



        }

        [HttpDelete]
        [Route("deleteCommande/{Id}")]
        public async Task<IActionResult> DeleteCommande(int Id)
        {

            try
            {

                await _unitOfWork.Commandes.Delete(Id);
                _unitOfWork.Save();
                ResponseObject<Commande> response = new ResponseObject<Commande>
                {
                    Data = null,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Commande supprimée avec succès",
                    DevelopperMessage = "SUCCES",
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<Commande> response = new ResponseObject<Commande>
                {
                    Data = null,
                    Status = ResponseStatus.FAILED.ToString(),
                    Message = "Une erreur s'est produite",
                    DevelopperMessage = ex.Message,
                };

                return BadRequest(response);
            }
        }


    }




}

