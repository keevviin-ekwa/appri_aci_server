using ApproACI.DTO;
using ApproACI.Models;
using ApproACI.Repositories.Interfaces;
using ApproACI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApproACI.Controllers
{
  
    public class ObjectifController : GenericController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObjectifController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll() { 

            var Objectifs= await _unitOfWork.Objectifs.GetAll(includes: new List<string>{"Produit"});
            ResponseObject<List<Objectif>> response = new ResponseObject<List<Objectif>>
            {
                Data = (List<Objectif>)Objectifs,
                Status = ResponseStatus.SUCCESSFUL.ToString(),
                Message = "",
                DevelopperMessage = "SUCCES",
            };
            return Ok(response);

        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetById(int Id)
        {
            var Objectif = await _unitOfWork.Objectifs.Get(p => p.Id == Id, includes:new List<string> { "Produit"});
            ResponseObject<Objectif> response = new ResponseObject<Objectif>
            {
                Data = (Objectif)Objectif,
                Status = ResponseStatus.SUCCESSFUL.ToString(),
                Message = "",
                DevelopperMessage = "SUCCES",
            };
            return Ok(response);
        }

        [HttpGet("Month")]
        public async Task<IActionResult> GetByMonth(string Month)
        {
            var Objectif = await _unitOfWork.Objectifs.Get(p => p.Mois == Month);
            ResponseObject<Objectif> response = new ResponseObject<Objectif>
            {
                Data = (Objectif)Objectif,
                Status = ResponseStatus.SUCCESSFUL.ToString(),
                Message = "",
                DevelopperMessage = "SUCCES",
            };
            return Ok(response);
        }



        [HttpGet]
        [Route("getObjectifByyear/{Year}")]
        public async Task<IActionResult> GetByYear(string Year)
        {
            var Objectif = await _unitOfWork.Objectifs.GetAll(p => p.Mois.Contains(Year));
            ResponseObject<List<Objectif>> response = new ResponseObject<List<Objectif>>
            {
                Data = (List<Objectif>)Objectif,
                Status = ResponseStatus.SUCCESSFUL.ToString(),
                Message = "",
                DevelopperMessage = "SUCCES",
            };
            return Ok(response);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateObjectif([FromBody] ObjectifDTO  objectifDTO)
        {

            try
            {

                var temp = await _unitOfWork.Objectifs.Get(o => o.Mois == objectifDTO.Mois);

               


                

                     temp.ObjectifPF=objectifDTO.ObjectifPF;
                     temp.ObjectifGF=objectifDTO.ObjectifGF;
                     _unitOfWork.Objectifs.Udapte(temp);
                     _unitOfWork.Save();
                    ResponseObject<Objectif> response = new ResponseObject<Objectif>
                    {
                        Data = null,
                        Status = ResponseStatus.SUCCESSFUL.ToString(),
                        Message = "Objectif Modifié avec succès",
                        DevelopperMessage = "SUCCES",
                    };
                    return Ok(response);
                
                
            }
            catch (Exception ex)
            {
                ResponseObject<Objectif> response = new ResponseObject<Objectif>
                {
                    Data = null,
                    Status = ResponseStatus.FAILED.ToString(),
                    Message = "Une erreur s'est  produite",
                    DevelopperMessage = "SUCCES",
                };
                return Ok(response);
            }

        }


        [HttpPost]
        public async Task<IActionResult> CreateObjectif([FromBody] ObjectifDTO objectifDTO)
        {

            try
            {

                var temp = await _unitOfWork.Objectifs.Get(o => o.Mois == objectifDTO.Mois);

                if (temp == null)
                {

                    var _objectif = _mapper.Map<Objectif>(objectifDTO);

                    await _unitOfWork.Objectifs.Insert(_objectif);
                    _unitOfWork.Save();
                    ResponseObject<Objectif> response = new ResponseObject<Objectif>
                    {
                        Data = null,
                        Status = ResponseStatus.SUCCESSFUL.ToString(),
                        Message = "Objectif ajouté avec succès",
                        DevelopperMessage = "SUCCES",
                    };
                    return Ok(response);
                }
                else
                {
                    ResponseObject<Objectif> response = new ResponseObject<Objectif>
                    {
                        Data = null,
                        Status = ResponseStatus.SUCCESSFUL.ToString(),
                        Message = "Objectif ce mois est deja défini",
                        DevelopperMessage = "SUCCES",
                    };
                    return BadRequest(response);
                }
                }
            catch (Exception ex)
            {
                return BadRequest("Une érreur s'est produite");
            }
        
        }

        [HttpDelete]
        [Route("deleteObjectif/{Id}")]
        public async Task<IActionResult> DeleteObjectif(int Id)
        {

            try
            {

                await _unitOfWork.Objectifs.Delete(Id);
                _unitOfWork.Save();
                return Ok("Objectif supprimé avec succès");
            }
            catch (Exception ex)
            {
                return BadRequest("Une erreur s'est produite");
            }
        }



    }
}
