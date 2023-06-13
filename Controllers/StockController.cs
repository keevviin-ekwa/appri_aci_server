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
                var consommation = objectif.ObjectifGF * nom.ContributionMatiereGF + objectif.ObjectifPF * nom.contributionMatierePF;
                var stockFin = stockDebut - consommation;

                for (var a = currentMonthNumber; a <=12; a++)
                {
                    //si le stock de fin est négatif ,
                    //on doit passer une commande
                    if (stockFin < 0)
                    {
                        var colisage = nom.Matiere.Colissage;
                        //on recherche le jour pour lequel si la commande était passée alors , le produit serait livré aujourd'hui
                        var dateCommande = currentMonth.AddDays(-colisage);
                        Calendar cal = new CultureInfo("en-US").Calendar;

                        var _colisage = nom.Matiere.Colissage;


                        var commande = new Commande
                        {
                            DateCommande = dateCommande,
                            DateLivraison = currentMonth,
                            MatiereId = nom.MatiereId,
                            Quantite = _colisage,
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
                    if(objectif == null)
                    {
                        continue;
                    }
                    consommation = objectif.ObjectifGF * nom.ContributionMatiereGF  + objectif.ObjectifPF * nom.contributionMatierePF ;
                    stockDebut = stockFin;
                    stockFin = stockDebut - consommation;
                    //currentMonthNumber++;

                }
                


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

        [HttpPut]
        public async Task<IActionResult> UpdteStock([FromBody] UpdateStockDTO UpdatedStock)
        {

            try
            {

                var Produits = await _unitOfWork.Produits.GetAll();

                var produit = Produits[0];

                var nom = await _unitOfWork.MatiereProduits.Get(mp => mp.MatiereId == UpdatedStock.MatiereId && mp.ProduitId == produit.Id, includes: new List<string> { "Matiere" });
                var splitedDate = UpdatedStock.Mois.Split("/");
                var currentMonth = int.Parse(splitedDate[0]);
                var currentYear = int.Parse(splitedDate[1]);
                DateTime currentDate = new DateTime(currentYear, currentMonth, 1); // premier jour du mois
                var objectif = await _unitOfWork.Objectifs.Get(o => o.Mois == UpdatedStock.Mois);

                //calcul de la consommation objectif * contribution par matière premiere
                var consommation = objectif.ObjectifGF * nom.ContributionMatiereGF / 100 + objectif.ObjectifPF * nom.contributionMatierePF / 100;
                var stockDebut = UpdatedStock.StockDebut;
                var stockFin= UpdatedStock.StockFin;
                var stock = await _unitOfWork.Stocks.Get(s => s.Id == UpdatedStock.Id);

                stock.StockDebut = UpdatedStock.StockDebut;
                stock.StockFin = UpdatedStock.StockFin;
                stock.Consommation = UpdatedStock.StockDebut - UpdatedStock.StockFin;
                var NextMonth = UpdatedStock.Mois;



                do
                {

                    //si le stock de fin est négatif ,

                    //on doit passer une commande
                    var Commandes = _unitOfWork.Commandes.Get(c => c.DateLivraison.ToString("MM/yyyy") == NextMonth);
                    if(Commandes != null)
                    {
                        await _unitOfWork.Commandes.Delete(Commandes.Id);
                        _unitOfWork.Save();

                    }

                    if (stock.StockFin < 0)
                    {
                        var colisage = nom.Matiere.Colissage;
                        //on recherche le jour pour lequel si la commande était passée alors , le produit serait livré aujourd'hui
                      
                        var dateCommande = currentDate.AddDays(-colisage);
                        Calendar cal = new CultureInfo("en-US").Calendar;

                        var _colisage = nom.Matiere.Colissage;
                    
                       

                        var commande = new Commande
                        {
                            DateCommande = dateCommande,
                            DateLivraison = currentDate,
                            MatiereId = nom.MatiereId,
                            Quantite = _colisage,
                            SemaineCommande = Helpers.GetWeekNumberOfMonth(dateCommande),
                        };

                        await _unitOfWork.Commandes.Insert(commande);
                        _unitOfWork.Save();
                        stock.StockDebut += commande.Quantite;
                        stock.StockFin = stock.StockDebut - consommation;
                    }



                    //update 
                    _unitOfWork.Stocks.Udapte(stock);
                    _unitOfWork.Save();
                    //sauvarge de l'ancienne valeur du stock
                    var temp = stock;
                    //incremente le mois
                    currentMonth = currentMonth + 1;
                    NextMonth = currentMonth.ToString("D2") + "/" + currentYear;

                    //recupere l'objectif du mois suivant
                    objectif = await _unitOfWork.Objectifs.Get(o => o.Mois == NextMonth);

                    if (objectif == null)
                    {
                        continue;
                    }
                    //calcul de la cosommation
                    consommation = objectif.ObjectifGF * nom.ContributionMatiereGF / 100 + objectif.ObjectifPF * nom.contributionMatierePF / 100;

                    //recupere le stock du mois suivant
                    stock= await _unitOfWork.Stocks.Get(s=>s.Mois== NextMonth);

                    stock.StockDebut = temp.StockFin;// le stock de debut est égale au stock de fin du mois précédent
                    stock.Consommation = consommation;
                    ///stock fin précedent pris comme stock debut suivant
                    stock.StockFin = temp.StockFin - consommation; 

                    

                }

                while (currentMonth<=12);


                ResponseObject<Stock> response = new ResponseObject<Stock>
                {
                    Data = null,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Mise a jour reussie",
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
