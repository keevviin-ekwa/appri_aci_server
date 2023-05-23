using ApproACI.Models;
using ApproACI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ApproACI.Utils;
using ApproACI.DTO;

namespace ApproACI.Controllers
{

    public class DroitAccesController : GenericController
    {
        private readonly IUnitOfWork _unitOfWork;

        public DroitAccesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                string message = string.Empty;
                var data = await _unitOfWork.DroitsAcces.GetAll();
                ResponseObject<List<DroitsAcces>> response = new ResponseObject<List<DroitsAcces>>
                {
                    Data = (List<DroitsAcces>)data,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = message,
                    DevelopperMessage = "SUCCES"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<List<DroitsAcces>> response = new ResponseObject<List<DroitsAcces>>
                {
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Problème survenue lors du traitement de votre requête. Veuillez réessayer ultérieurement",
                    DevelopperMessage = ex.InnerException.ToString()
                };
                return BadRequest(response);
            }

        }


        [HttpPost]
        public async Task<IActionResult> Save([FromBody] DroitAccesDTO droitAccesDTO)
        {
            try
            {

                DroitsAcces droit = new DroitsAcces
                {
                    DateCreation = DateTime.Now,
                    DateDerniereMaj = DateTime.Now,
                    Description = droitAccesDTO.Description,
                    DesignationTechnique = droitAccesDTO.DesignationTechnique,

                };

               await  _unitOfWork.DroitsAcces.Insert(droit);
                _unitOfWork.Save();


                 
                ResponseObject<DroitsAcces> response = new ResponseObject<DroitsAcces>
                {
                    Data = null,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Droit ajouté avec succès",
                    DevelopperMessage = "SUCCES"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<DroitsAcces> response = new ResponseObject<DroitsAcces>
                {
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Problème survenue lors du traitement de votre requête. Veuillez réessayer ultérieurement",
                    DevelopperMessage = ex.InnerException.ToString()
                };
                return BadRequest(response);
            }

        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                string message = string.Empty;
                var data = await _unitOfWork.DroitsAcces.Get(d => d.Id == Id);
                ResponseObject<DroitsAcces> response = new ResponseObject<DroitsAcces>
                {
                    Data = (DroitsAcces)data,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = message,
                    DevelopperMessage = "SUCCES"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<DroitsAcces> response = new ResponseObject<DroitsAcces>
                {
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Problème survenue lors du traitement de votre requête. Veuillez réessayer ultérieurement",
                    DevelopperMessage = ex.InnerException.ToString()
                };
                return BadRequest(response);
            }

        }

        [HttpGet]
        [Route("GetDroitByName/{Name}")]
        public async Task<IActionResult> GetByName(string Name)
        {
            try
            {
                string message = string.Empty;
                var data = await _unitOfWork.DroitsAcces.Get(d=>d.DesignationTechnique==Name);
                ResponseObject<DroitsAcces> response = new ResponseObject<DroitsAcces>
                {
                    Data = (DroitsAcces)data,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = message,
                    DevelopperMessage = "SUCCES"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<DroitsAcces> response = new ResponseObject<DroitsAcces>
                {
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Problème survenue lors du traitement de votre requête. Veuillez réessayer ultérieurement",
                    DevelopperMessage = ex.InnerException.ToString()
                };
                return BadRequest(response);
            }

        }
    }
}

