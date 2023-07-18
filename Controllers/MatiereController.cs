using ApproACI.DTO;
using ApproACI.Models;
using ApproACI.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using ApproACI.Repositories.Implementations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using ApproACI.Utils;

namespace ApproACI.Controllers
{
    
    public class MatiereController : GenericController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MatiereController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Matieres = await _unitOfWork.Matieres.GetAll(includes: new List<string>{"Commandes"});
          //  var _matiere = _mapper.Map<List<MatiereDTO>>(Matieres);
            ResponseObject<List<Matiere>> response = new ResponseObject<List<Matiere>>
            {
                Data = (List<Matiere>) Matieres,
                Status = ResponseStatus.SUCCESSFUL.ToString(),
                Message = "",
                DevelopperMessage = "SUCCES",
            };
            return Ok(response);
        }
       


        [HttpGet]
        [Route("GetMatiereWithStock/{Id}")]
        public async Task<IActionResult> GetMatiereByIdWithStock(int Id)
        {
            var Produit = await _unitOfWork.Matieres.Get(p => p.Id == Id, includes: new List<String> {"Stocks" });
            return Ok(Produit);
        }

        [HttpGet]
        [Route("GetMatiereById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var Matiere = await _unitOfWork.Matieres.Get(p => p.Id == Id,includes: new List<string> { "Commandes"});
            ResponseObject<Matiere> response = new ResponseObject<Matiere>
            {
                Data = (Matiere)Matiere,
                Status = ResponseStatus.SUCCESSFUL.ToString(),
                Message = "",
                DevelopperMessage = "SUCCES",
            };
            return Ok(response);
          
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatiere([FromBody] CreateMatiereDTO matiereDTO)
        {

            try
            {
                var _matiere = _mapper.Map<Matiere>(matiereDTO);
                _matiere.DateModification = DateTime.Now;
                _matiere.DateCreation = DateTime.Now;
                await _unitOfWork.Matieres.Insert(_matiere);
                _unitOfWork.Save();
                ResponseObject<List<Matiere>> response = new ResponseObject<List<Matiere>>
                {
                    Data = null,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Matière première créé avec succès",
                    DevelopperMessage = "SUCCES",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Une érreur s'est produite");
            }
        }


        [HttpPut]
        [Route("UpdateMatiere/{Id}")]
        public async Task<IActionResult> UpdateMatiere(int Id, [FromBody] UpdateMatiereDTO matiereDTO)
        {

            try
            {
                var Matiere = await _unitOfWork.Matieres.Get(q => q.Id == Id);
                Matiere.Reference = matiereDTO.Reference;
                Matiere.Designation = matiereDTO.Designation;
                Matiere.DateModification = DateTime.Now;
                Matiere.Origine= matiereDTO.Origine;
                Matiere.Colissage= matiereDTO.Colissage;
                Matiere.PrixUnitaire= matiereDTO.PrixUnitaire;
                Matiere.DelaisAppro= matiereDTO.DelaisAppro;
                
                _unitOfWork.Matieres.Udapte(Matiere);
                _unitOfWork.Save();
                ResponseObject<Matiere> response = new ResponseObject<Matiere>
                {
                    Data = null,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Matière première Modifiée avec succès",
                    DevelopperMessage = "SUCCES",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<Matiere> response = new ResponseObject<Matiere>
                {
                    Data = null,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Une erreur s'est produite",
                    DevelopperMessage = "ECHEC",
                };
                return BadRequest(response);
            }
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> DeleteMatiere(int Id)
        {

            try
            {
                
                await _unitOfWork.Matieres.Delete(Id);
                _unitOfWork.Save();
                return Ok("Matière première Supprimée avec succès");
            }
            catch (Exception ex)
            {
                return BadRequest("Une erreur s'est produite");
            }
        }


    }
}

