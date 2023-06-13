using ApproACI.Models;
using ApproACI.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using ApproACI.Utils;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ApproACI.DTO;
using recipeMgtServer.Tools;

namespace ApproACI.Controllers
{

    public class UserController :  GenericController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        [Route("GetUserById/{Id}")]
       
        public async Task<IActionResult> getEmployeById(int Id)
        {
            try
            {
                string message = string.Empty;
                var data = await _unitOfWork.Users.Get(e=>e.Id==Id);
                ResponseObject<User> response = new ResponseObject<User>
                {
                    Data = data,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = message,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<User> response = new ResponseObject<User>
                {
                    Status = ResponseStatus.ERROR.ToString(),
                    Message = "Problème survenue lors du traitement de votre requête. Veuillez réessayer ultérieurement",
                    DevelopperMessage = ex.InnerException.ToString()
                };
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                string message = string.Empty;
                var data = await _unitOfWork.Users.GetAll();
                ResponseObject<List<User>> response = new ResponseObject<List<User>>
                {
                    Data = (List<User>) data,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = message,
                    DevelopperMessage = "SUCCES"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<List<User>> response = new ResponseObject<List<User>>
                {
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Problème survenue lors du traitement de votre requête. Veuillez réessayer ultérieurement",
                    DevelopperMessage = ex.InnerException.ToString()
                };
                return Ok(response);
            }

        }

        [HttpPost]
        [Route("saveUser")]
        public async Task<IActionResult> saveUser([FromBody] UserDTO userDTO)
        {
            try
            {
                var _user = _mapper.Map<User>(userDTO);
                _user.DateCreation= DateTime.Now;
                _user.MotDePasse = Cipher.Encrypt(userDTO.MotDePasse, Helpers.PASSWORD_FOR_ENCRYPTION_AND_DECRYPTION);
          
                  await _unitOfWork.Users.Insert(_user);    
                _unitOfWork.Save();
                ResponseObject<string> response = new ResponseObject<string>
                {
                    Data=null,
                    Status =  ResponseStatus.SUCCESSFUL.ToString() ,
                    Message = "Utilisateur créé avec succès",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<string> response = new ResponseObject<string>
                {
                    Status = ResponseStatus.ERROR.ToString(),
                    Message = "Problème survenue lors du traitement de votre requête. Veuillez réessayer ultérieurement",
                    DevelopperMessage = ex.InnerException.ToString()
                };
                return BadRequest(response);
            }

        }

        [HttpPut]
        [Route("updateUser")]
        public async Task<IActionResult> updateUser([FromBody] User user)
        {
            try
            {

                var _user = await _unitOfWork.Users.Get(u=>u.Id==user.Id);
                 _user.Nom=user.Nom;
                _user.Prenom=user.Prenom;
                _user.Email=user.Email; 
                _user.Fonction=user.Fonction;
                _user.Tel=user.Tel;
                _user.MotDePasse=Cipher.Encrypt(user.MotDePasse,Helpers.PASSWORD_FOR_ENCRYPTION_AND_DECRYPTION);
                _user.DroitAcces=user.DroitAcces;
                _user.Login=user.Login;
                _user.DateDerniereMaj=DateTime.Now;
                _unitOfWork.Users.Udapte(_user);
                _unitOfWork.Save();
                ResponseObject<string> response = new ResponseObject<string>
                {
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Utilisateur Modifié avec succès",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<string> response = new ResponseObject<string>
                {
                    Status = ResponseStatus.ERROR.ToString(),
                    Message = "Problème survenue lors du traitement de votre requête. Veuillez réessayer ultérieurement",
                    DevelopperMessage = ex.InnerException.ToString()
                };
                return BadRequest(response);
            }
        }

        //}

        [HttpPost("changeUserPassword")]
        public async Task<IActionResult> changeUserPassword([FromBody] ChangePassWordUserDTO pwdChangeModel)
        {
            try
            {
                string message = string.Empty;
                var user = await _unitOfWork.Users.Get(u => u.Id == pwdChangeModel.userId);
                user.MotDePasse = Cipher.Encrypt(pwdChangeModel.NewPassword, Helpers.PASSWORD_FOR_ENCRYPTION_AND_DECRYPTION) ;
                user.IsFirstConnection = false;
                _unitOfWork.Users.Udapte(user);
                _unitOfWork.Save();
                ResponseObject<User> response = new ResponseObject<User>
                {
                    Data = user,
                    Status = ResponseStatus.SUCCESSFUL.ToString(),
                    Message = "Mot de passe Changé avc succès",
                    DevelopperMessage = "SUCCES"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseObject<User> response = new ResponseObject<User>
                {
                    Status = ResponseStatus.ERROR.ToString(),
                    Message = "Problème survenue lors du traitement de votre requête. Veuillez réessayer ultérieurement",
                    DevelopperMessage = ex.InnerException.ToString()
                };
                return new BadRequestObjectResult(response);
            }

        }

        [HttpGet]
        [Route("GetAllDroitsAcces")]
        public async Task<IActionResult> GetAllDroitsAcces()
        {
            try
            {
                string message = string.Empty;
                 var data=await _unitOfWork.DroitsAcces.GetAll();
                ResponseObject<List<DroitsAcces>> response = new ResponseObject<List<DroitsAcces>>
                {
                    Data = (List<DroitsAcces>) data,
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
                return Ok(response);
            }

        }
    }
}
