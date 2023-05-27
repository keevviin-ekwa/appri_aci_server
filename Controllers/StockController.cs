using ApproACI.DTO;
using ApproACI.Models;
using ApproACI.Repositories.Interfaces;
using ApproACI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace ApproACI.Controllers
{
    public class StockController : GenericController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StockController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet("Month")]
       
        public async Task<IActionResult> GetAll(string Month)
        {
           
            var message = string.Empty;
            var stocks = await _unitOfWork.Stocks.GetAll(s=>s.Mois==Month ,includes: new List<String> {"Matiere"});
            ResponseObject<List<Stock>> response = new ResponseObject<List<Stock>>
            {
                Data = (List<Stock>)stocks,
                Status = ResponseStatus.SUCCESSFUL.ToString(),
                Message = message,
                DevelopperMessage = "SUCCES",
            };
            return Ok(response);
        }


        [HttpGet("Id")]
        public async Task<IActionResult> GetById(int Id)
        {
            var Produit = await _unitOfWork.Stocks.Get(s => s.Id == Id, includes: new List<String> { "Matiere" }) ;
            return Ok(Produit);
        }

       



        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] StockDTO stockDTO)
        {

            try
            {
                //var _stock = _mapper.Map<Stock>(stockDTO);
                //_stock.DateModification = DateTime.Now;
                //_stock.DateCreation = DateTime.Now;
                var Produits= await _unitOfWork.Produits.GetAll();
              
                var produit = Produits[0];

                var nom = await _unitOfWork.MatiereProduits.Get(mp => mp.MatiereId == stockDTO.MatiereId && mp.ProduitId == produit.Id, includes:new List<string> { "Matiere"});
                
                var LastdayOfYear = new DateTime(DateTime.Now.Year, 12, 31);
                var numberOfMonht = ((LastdayOfYear.Year - stockDTO.date.Year) * 12) + LastdayOfYear.Month - stockDTO.date.Month;
                var currentMonthNumber = int.Parse(stockDTO.date.ToString("MM"));
                var currentMonth = stockDTO.date;
                var i = currentMonthNumber;
                var stockDebut = stockDTO.StockDebut;
                var objectif = await _unitOfWork.Objectifs.Get(o => o.Mois == stockDTO.date.ToString("MM/yyyy"));
                var consommation = objectif.ObjectifGF * nom.ContributionMatiereGF / 100 + objectif.ObjectifPF * nom.contributionMatierePF / 100;
                var stockFin = stockDebut - consommation;
                do
                {
                    //si le stock de fin est négatif ,
                    //on doit passer une commande
                    if (stockFin < 0)
                    {
                        var colisage = nom.Matiere.Colissage;
                        //on recherche le jour pour lequel si la commande était passée alors , le produit serait livré aujourd'hui
                        var dateCommande = currentMonth.AddDays(-colisage);
                        Calendar cal = new CultureInfo("en-US").Calendar;

                        var _colisage= nom.Matiere.Colissage;
                        var Quantite = _colisage;
                        var unite = 1;
                        while (_colisage <= consommation)
                        {
                            Quantite += _colisage;
                            unite++;
                        }

                        var commande = new Commande
                        {
                            DateCommande = dateCommande,
                            DateLivraison = currentMonth,
                            MatiereId = nom.MatiereId,
                            Quantite = Quantite,
                            SemaineCommande = Helpers.GetWeekNumberOfMonth(dateCommande),
                        };

                         await _unitOfWork.Commandes.Insert(commande);
                        _unitOfWork.Save();
                        stockDebut += commande.Quantite;
                        stockFin = stockDebut - consommation;
                    }

                  
                    var Stock = new Stock
                    {
                        MatiereId = stockDTO.MatiereId,
      
                        Consommation = consommation,
                        StockDebut = stockDebut,
                        StockFin = stockFin,
                        Mois = currentMonth.ToString("MM/yyyy"),
                    };



                    await _unitOfWork.Stocks.Insert(Stock);
                    _unitOfWork.Save();

                    currentMonth = currentMonth.AddMonths(1);
                    objectif = await _unitOfWork.Objectifs.Get(o => o.Mois == currentMonth.ToString("MM/yyyy"));
                    consommation = objectif.ObjectifGF * nom.ContributionMatiereGF / 100 + objectif.ObjectifPF * nom.contributionMatierePF / 100;
                    stockDebut = stockFin;
                    stockFin = stockDebut - consommation;
                    currentMonthNumber++;
                }

                while (currentMonthNumber<12);



                ResponseObject<Stock> response = new ResponseObject<Stock>
                {
                    Data =null,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Stock ajouté avec succès",
                    DevelopperMessage = "SUCCES",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<Stock> response = new ResponseObject<Stock>
                {
                    Data = null,
                    Status = ResponseStatus.FAILED.ToString(),
                    Message = "Une erreur s'est produite",
                    DevelopperMessage = "ECHEC",
                };
                return BadRequest(response);
            }
        }

        [HttpPut("Id")]
        public async Task<IActionResult> UpdteStock(int Id, [FromBody] StockDTO stockDTO)
        {

            try
            {
                var _stock = await _unitOfWork.Stocks.Get(q => q.Id == Id);
                _stock.StockDebut = stockDTO.StockDebut;
                _stock.StockFin = stockDTO.StockFin;
                 //_stock.Mois = stockDTO.Mois;
                _stock.MatiereId = stockDTO.MatiereId;
                _stock.DateModification = DateTime.Now;
                _unitOfWork.Stocks.Udapte(_stock);
                _unitOfWork.Save();
                return Ok("Stock Modifié avec succès");
            }
            catch (Exception ex)
            {
                return BadRequest("Une erreur s'est produite");
            }
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> DeleteStock(int Id)
        {

            try
            {

                await _unitOfWork.Stocks.Delete(Id);
                _unitOfWork.Save();
                return Ok("Stock Supprimé avec succès");
            }
            catch (Exception ex)
            {
                return BadRequest("Une erreur s'est produite");
            }
        }

        //}
    }

}
