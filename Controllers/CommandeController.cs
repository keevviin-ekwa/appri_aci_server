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

