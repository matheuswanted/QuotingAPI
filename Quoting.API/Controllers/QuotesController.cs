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
        private readonly ICustomerRepository _customerRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuoteRepository _quoteRepo;

        public QuotesController(ICustomerRepository customerRepo, IQuoteRepository quoteRepo, IUnitOfWork unitOfWork)
        {
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
            _quoteRepo = quoteRepo;
        }
        [Route("Quote")]
        [HttpPost]
        public async Task<IActionResult> Quote([FromBody]Quote quote)
        {
            BadRequestObjectResult error = CheckConsistency(quote);
            if (error != null)
                return error;

            quote.Customer.AddVehicle(quote.Vehicle);

            quote.SetCustomer(await _customerRepo.Put(quote.Customer));

            _quoteRepo.Add(quote);

            await _unitOfWork.SaveChangesAsync();

            return Ok(quote.Id);
        }

        private BadRequestObjectResult CheckConsistency(Quote quote)
        {
            if (quote == null)
                return BadRequest("Invalid parameter.");
            if (!quote.IsConsistent())
                return BadRequest(string.Join(Environment.NewLine, quote.ModelInconsistecies.Select(i=>i.Message)));
            return null;
        }
    }
}