﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Quoting.Domain.Models;
using Quoting.Domain.Queries;
using Quoting.Domain.Repositories;
using Quoting.Domain.Repositories.Queryable;
using Quoting.Domain.Seedworking;
using Quoting.Domain.ValueObjects;

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
        private readonly IMemoryCache _cache;

        public QuotesController(ICustomerRepository customerRepo, IQuoteRepository quoteRepo, IQuoteQueryableRepository quoteQueries, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
            _quoteRepo = quoteRepo;
            _quoteQueries = quoteQueries;
            _cache = cache;
        }
        [Route("Quote")]
        [HttpPost]
        public async Task<IActionResult> Quote([FromBody]QuoteRequest request)
        {
            var quote = request.ToQuote();

            BadRequestObjectResult error = CheckConsistency(quote);
            if (error != null)
                return error;

            _quoteRepo.Add(quote);

            quote.SetCustomer(await _customerRepo.Put(quote.Customer));

            quote.SetStatusAsRequested();

            await _unitOfWork.SaveChangesAsync();

            CacheRequest(quote);

            return Ok(quote.Id);
        }

        private void CacheRequest(Quote quote)
            => _cache?.Set(QuoteRequestKey(quote.Id), quote.Request, DateTimeOffset.Now.AddHours(1));

        private string QuoteRequestKey(int id)
        {
            return $"quote_request_{id}";
        }

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 3600)]
        [Route("Information")]
        [HttpGet]
        public async Task<IActionResult> Information(int id)
        {
            var cached = _cache.Get(QuoteRequestKey(id));

            if (cached != null)
                return Ok(cached);

            var result = await _quoteQueries.Query<IQuoteInformationRequestQuery>().Run(id);

            if (result == null)
                return NotFound();

            CacheRequest(result);

            return Ok(result.Request);
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
                return BadRequest(string.Join(Environment.NewLine, quote.ModelInconsistecies.Select(i => i.Message)));
            return null;
        }
    }
}