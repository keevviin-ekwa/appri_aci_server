using ApproACI.DTO;
using ApproACI.Models;
using ApproACI.Repositories.Interfaces;
using ApproACI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApproACI.Controllers
{
 
    public class ProduitController : GenericController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProduitController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string message = string.Empty;
            var Produits= await _unitOfWork.Produits.GetAll(includes:new List<string> { "Marque" });
            ResponseObject<List<Produit>> response = new ResponseObject<List<Produit>>
            {
                Data = (List<Produit>)Produits,
                Status = ResponseStatus.SUCCESSFUL.ToString(),
                Message = message,
                DevelopperMessage = "SUCCES",
            };
            return Ok(response);
        }


        [HttpGet("Id")]
        public async Task<IActionResult> GetById(int Id)
        {
            var Produit = await _unitOfWork.Produits.Get(p=>p.Id==Id);
            return Ok(Produit);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduit([FromBody] CreateProductDTO productDTO)
        {

            try
            {
                var _produit = _mapper.Map<Produit>(productDTO);
                _produit.DateModification = DateTime.Now;
                _produit.DateCreation   = DateTime.Now;
                await _unitOfWork.Produits.Insert(_produit);
                _unitOfWork.Save();
                ResponseObject<List<Produit>> response = new ResponseObject<List<Produit>>
                {
                    Data =null,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Produit créé avec succès",
                    DevelopperMessage = "SUCCES",
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                ResponseObject<List<Produit>> response = new ResponseObject<List<Produit>>
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
        [Route("updateProduct/{Id}")]
        public async Task<IActionResult> UpdateProduit(int Id,[FromBody] UpdateProductDTO productDTO)
        {

            try
            { 
                var Produit = await _unitOfWork.Produits.Get(q=>q.Id==Id);
                Produit.Reference = productDTO.Reference;
                Produit.Designation = productDTO.Designation;
                Produit.DateModification = DateTime.Now;
                _unitOfWork.Produits.Udapte(Produit);
                _unitOfWork.Save();

                ResponseObject<Produit> response = new ResponseObject<Produit>
                {
                    Data = null,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Produit créé avec succès",
                    DevelopperMessage = "SUCCES",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<Produit> response = new ResponseObject<Produit>
                {
                    Data = null,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Une erreur s'est produite",
                    DevelopperMessage = "SUCCES",
                };
                return BadRequest(response);
            }
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> DeleteProduit(int Id)
        {

            try
            {
               
                await _unitOfWork.Produits.Delete(Id);
                _unitOfWork.Save();
                return Ok("produit Supprimé avec succès");
            }
            catch (Exception ex)
            {
                return BadRequest("Une erreur s'est produite");
            }
        }


    }
}
