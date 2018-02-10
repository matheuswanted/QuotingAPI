using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quoting.Domain.Models;
using Quoting.Domain.Repositories;
using Quoting.Domain.Seedworking;

namespace Quoting.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Quotes")]
    public class QuotesController : Controller
    {
        private ICustomerRepository _customerRepo;
        private IUnitOfWork _unitOfWork;

        public QuotesController(ICustomerRepository customerRepo, IUnitOfWork unitOfWork)
        {
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
        }
        [Route("Quote")]
        [HttpPost]
        public async Task<IActionResult> Quote([FromBody]Quote quote)
        {
            BadRequestObjectResult error = CheckConsistency(quote);
            if (error != null)
                return error;

            quote.Customer.AddVehicle(quote.Vehicle);

            await _customerRepo.Put(quote.Customer);

            await _unitOfWork.SaveChangesAsync();

            return Ok(Guid.NewGuid());
        }

        private BadRequestObjectResult CheckConsistency(Quote quote)
        {
            if (quote == null)
                return BadRequest("Invalid parameter.");
            if (!quote.IsConsistent())
                return BadRequest(string.Join(Environment.NewLine, quote.ModelInconsistecies));
            return null;
        }
    }
}