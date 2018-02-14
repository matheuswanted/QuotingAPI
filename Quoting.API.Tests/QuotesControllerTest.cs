using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Quoting.API.Controllers;
using Quoting.API.Tests.DataGenerator;
using Quoting.Domain.Models;
using Quoting.Domain.Queries;
using Quoting.Domain.Repositories;
using Quoting.Domain.Repositories.Queryable;
using Quoting.Domain.Seedworking;
using Quoting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Quoting.API.Tests
{
    public class QuotesControllerTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ICustomerRepository> _customerRepoMock;
        private Mock<IQuoteRepository> _quoteRepoMock;

        public QuotesControllerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(m => m.SaveChangesAsync(default(System.Threading.CancellationToken))).ReturnsAsync(() => 1);
            _customerRepoMock = new Mock<ICustomerRepository>();
            _customerRepoMock.Setup(m => m.Put(It.IsAny<Customer>())).ReturnsAsync(() => new Customer() { Id = 1 }.From(ControllerGenerator.OkCustomer()));
            _quoteRepoMock = new Mock<IQuoteRepository>();
            _quoteRepoMock.Setup(m => m.Add(It.IsAny<Quote>()));
        }
        private QuotesController New()
        {
            return new QuotesController(_customerRepoMock.Object, _quoteRepoMock.Object, null, _unitOfWorkMock.Object, null);
        }
        [Fact]

        public void QuotesQuote_ShouldReturnIdOfTheNewQuote_WhenPostedACustomerAndVehicle()
        {

            var controller = New();
            var result = controller.Quote(ControllerGenerator.OkQuote()).Result;
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.IsType<int>(okResult.Value);
            Assert.NotEqual(default(int), okResult.Value);
        }
        [Theory]
        [MemberData(nameof(ControllerGenerator.BadCustomers), MemberType = typeof(ControllerGenerator))]
        public void QuotesQuote_ShouldReturnBadRequest(QuoteRequest customer)
        {
            var controller = New();
            var result = controller.Quote(customer).Result;
            var badResult = result as BadRequestObjectResult;
            Assert.NotNull(badResult);
        }
    }
}
