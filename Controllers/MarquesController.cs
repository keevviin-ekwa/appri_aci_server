using ApproACI.Models;
using ApproACI.Repositories.Interfaces;
using ApproACI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApproACI.Controllers
{
    
    public class MarquesController : GenericController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MarquesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<MarquesController>
        [HttpGet]
        public async Task<IActionResult> GetAllMarques()
        {
            var Marques = await _unitOfWork.Marques.GetAll();
            string message= string.Empty;
            ResponseObject<List<Marque>> response = new ResponseObject<List<Marque>>
            {
                Data = (List<Marque>)Marques,
                Status = ResponseStatus.SUCCESSFUL.ToString(),
                Message = message,
                DevelopperMessage = "SUCCES",
            };

            return Ok(response);
        }

        // GET api/<MarquesController>/5
        [HttpGet("{id}")]
        public string GetMarqueById(int id)
        {
            return "value";
        }

        // POST api/<MarquesController>
        [HttpPost]
        public void CreateMarque([FromBody] string value)
        {
        }

        // PUT api/<MarquesController>/5
        [HttpPut("{id}")]
        public void UpdateMarque(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MarquesController>/5
        [HttpDelete("{id}")]
        public void DeleteMarque(int id)
        {
        }
    }
}
