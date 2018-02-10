using Microsoft.AspNetCore.Mvc;
using Moq;
using Quoting.API.Controllers;
using Quoting.API.Tests.DataGenerator;
using Quoting.Domain.Models;
using Quoting.Domain.Repositories;
using Quoting.Domain.Seedworking;
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

        public QuotesControllerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(m => m.SaveChangesAsync(default(System.Threading.CancellationToken))).Returns(()=>Async(()=>1));
            _customerRepoMock = new Mock<ICustomerRepository>();
            _customerRepoMock.Setup(m => m.GetBySSN(It.IsAny<string>())).Returns(() => Async<Customer>(() => null));
            _customerRepoMock.Setup(m => m.Add(It.IsAny<Customer>()));
        }
        private Task<T> Async<T>(Func<T> factory)
        {
            return Task.Factory.StartNew(factory);
        }
        private QuotesController New()
        {
            return new QuotesController(_customerRepoMock.Object, _unitOfWorkMock.Object);
        }
        [Fact]

        public void QuotesQuote_ShouldReturnIdOfTheNewQuote_WhenPostedACustomerAndVehicle()
        {

            var controller = New();
            var result = controller.Quote(ControllerGenerator.OkCustomer()).Result;
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.IsType<Guid>(okResult.Value);
            Assert.NotEqual(new Guid(), okResult.Value);
        }
        [Theory]
        [MemberData(nameof(ControllerGenerator.BadCustomers),MemberType = typeof(ControllerGenerator))]
        public void QuotesQuote_ShouldReturnBadRequest(Customer customer)
        {
            var controller = New();
            var result = controller.Quote(customer).Result;
            var badResult = result as BadRequestObjectResult;
            Assert.NotNull(badResult);
        }
    }
}
