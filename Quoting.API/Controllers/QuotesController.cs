using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quoting.Domain.Models;
using Quoting.Domain.Queries;
using Quoting.Domain.Repositories;
using Quoting.Domain.Repositories.Queryable;
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
        private readonly IQuoteQueryableRepository _quoteQueries;

        public QuotesController(ICustomerRepository customerRepo, IQuoteRepository quoteRepo, IQuoteQueryableRepository quoteQueries, IUnitOfWork unitOfWork)
        {
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
            _quoteRepo = quoteRepo;
            _quoteQueries = quoteQueries;
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

        [Route("Information")]
        [HttpGet]
        public async Task<IActionResult> Information(int id)
        {
            var result = await _quoteQueries.Query<IQuoteInformationRequestQuery>().Run(id);
            if (result == null)
                return NotFound();

            return Ok(new
            {
                Customer = new 
                {
                    result.Customer.SSN,
                    result.Customer.Phone,
                    result.Customer.Address,
                    result.Customer.Email,
                    result.Customer.Gender,
                    BirthDate = result.Customer.BirthDate.ToShortDateString(),
                },
                Vehicle = new {
                    result.Vehicle.Make,
                    result.Vehicle.ManufacturingYear,
                    result.Vehicle.Model,
                    result.Vehicle.Type
                }
            });
        }

        [Route("Status")]
        [HttpGet]
        public async Task<IActionResult> Status(int id)
        {
            var result = await _quoteQueries.Query<IQuoteStatusRequestQuery>().Run(id);
            if (result == null)
                return NotFound();

            return Ok(new
            {
                Status = result.Status.ToString(),
                Value = result.Value.ToString("#0.00")
            });
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