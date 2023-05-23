using ApproACI.DTO;
using ApproACI.Models;
using ApproACI.Repositories.Interfaces;
using ApproACI.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipeMgtServer.Tools;
using System.Threading.Tasks;

namespace ApproACI.Controllers
{

    public class LoginController : GenericController
    {



        private readonly IUnitOfWork _unitOfWork;

        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("connectAndGetCredentials")]
        public async Task<IActionResult> Post([FromBody] LoginUserDTO user)
        {
            if (user != null && user.UserName != null && user.Password != null)
            {
                string message = string.Empty;

                string encryptedPassword = Cipher.Encrypt(user.Password, Helpers.PASSWORD_FOR_ENCRYPTION_AND_DECRYPTION);
                User _user = await _unitOfWork.Users.Get(u => u.Login == user.UserName && u.MotDePasse == encryptedPassword);
                

                if (_user != null)
                {

                    ResponseObject<User> response = new ResponseObject<User>
                    {
                        Data = _user,
                        Status = ResponseStatus.SUCCESSFUL.ToString(),
                        Message = message,
                    };
                    return Ok(response);

                    //return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {

                    ResponseObject<User> response = new ResponseObject<User>
                    {
                        Data = _user,
                        Status = ResponseStatus.FAILED.ToString(),
                        Message = "Une erreur s'est produite",
                    };
                    return BadRequest(response);
                }
            }
            else
            {
                return BadRequest();
            }
        }

      

    }
}

