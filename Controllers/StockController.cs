using ApproACI.DTO;
using ApproACI.Models;
using ApproACI.Repositories.Interfaces;
using ApproACI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
                var _stock = _mapper.Map<Stock>(stockDTO);
                _stock.DateModification = DateTime.Now;
                _stock.DateCreation = DateTime.Now;
                await _unitOfWork.Stocks.Insert(_stock);
                _unitOfWork.Save();
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
