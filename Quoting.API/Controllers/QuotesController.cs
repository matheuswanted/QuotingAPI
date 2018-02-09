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
        public async Task<IActionResult> Quote([FromBody]Customer customer)
        {
            BadRequestObjectResult error = CheckConsistency(customer);
            if (error != null)
                return error;

            var c = await _customerRepo.GetBySSN(customer.SSN);

            if (c == null)
                _customerRepo.Add(customer);
            else
            {
                c.Gender = customer.Gender;
                c.Address = customer.Address;
                c.BirthDate = customer.BirthDate;
                c.Email = customer.Email;
                c.Phone = customer.Phone;
                c.Vehicle.Maker = customer.Vehicle.Maker;
                c.Vehicle.ManufacturingYear = customer.Vehicle.ManufacturingYear;
                c.Vehicle.Model = customer.Vehicle.Model;
                c.Vehicle.Type = customer.Vehicle.Type;
            }

            await _unitOfWork.SaveChangesAsync();

            return Ok(Guid.NewGuid());
        }

        private BadRequestObjectResult CheckConsistency(Customer customer)
        {
            if (customer == null)
                return BadRequest("Invalid parameter.");
            if (!customer.IsConsistent())
                return BadRequest(string.Join(Environment.NewLine, customer.ModelInconsistecies.Select(n => n.Notification)));
            return null;
        }
    }
}