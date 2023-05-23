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
    public class MatiereProduitController: GenericController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MatiereProduitController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var message= string.Empty;
            var MatiereProduit = await _unitOfWork.MatiereProduits.GetAll(includes: new List<string> { "Produit","Matiere"});
            ResponseObject<List<MatiereProduit>> response = new ResponseObject<List<MatiereProduit>>
            {
                Data = (List<MatiereProduit>)MatiereProduit,
                Status = ResponseStatus.SUCCESSFUL.ToString(),
                Message = message,
                DevelopperMessage = "SUCCES",
            };
            return Ok(response);
           
        }

        [HttpGet]
        [Route("GetMatiereProduitByIds/{MatiereId}/{ProduitId}")]
        public async Task<IActionResult> GetById(int MatiereId,int ProduitId)
        {
            var message= string.Empty;
            var matiereProduit = await _unitOfWork.MatiereProduits.Get(s => s.MatiereId == MatiereId && s.ProduitId==ProduitId, includes: new List<String> { "Matiere", "Produit" });

            ResponseObject<MatiereProduit> response = new ResponseObject<MatiereProduit>
            {
                Data = (MatiereProduit)matiereProduit,
                Status = ResponseStatus.SUCCESSFUL.ToString(),
                Message = message,
                DevelopperMessage = "SUCCES",
            };
            return Ok(response);

          
        }

        [HttpGet]
        [Route("GetMatiereByProductId/{ProduitId}")]
        public async Task<IActionResult> GetMatiereByProductId(int ProduitId)
        {
            var message = string.Empty;
            var matiereProduit = await _unitOfWork.MatiereProduits.GetAll(s => s.ProduitId == ProduitId, includes: new List<String> { "Matiere","Produit" });

            ResponseObject<List<MatiereProduit>> response = new ResponseObject<List<MatiereProduit>>
            {
                Data = (List<MatiereProduit>)matiereProduit,
                Status = ResponseStatus.SUCCESSFUL.ToString(),
                Message = message,
                DevelopperMessage = "SUCCES",
            };
            return Ok(response);


        }


        [HttpPost]
        public async Task<IActionResult> CreateMatiereProduit([FromBody] MatiereProduitDTO matiereProduitDTO)
        {

            try
            {
                var _matiereProduit = _mapper.Map<MatiereProduit>(matiereProduitDTO);
                
                await _unitOfWork.MatiereProduits.Insert(_matiereProduit);
                _unitOfWork.Save();

                ResponseObject<MatiereProduit> response = new ResponseObject<MatiereProduit>
                {
                    Data = null,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Creation reussie",
                    DevelopperMessage = "SUCCES",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<MatiereProduit> response = new ResponseObject<MatiereProduit>
                {
                    Data = null,
                    Status = ResponseStatus.FAILED.ToString(),
                    Message = "Une érreur s'est produite",
                    DevelopperMessage = "ECHEC",
                };
                return Ok(response);
            }
        }
    }
}
