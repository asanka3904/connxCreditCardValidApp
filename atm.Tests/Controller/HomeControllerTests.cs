using atm.Controllers;
using atm.Enum;
using atm.Models;
using atm.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace atm.Tests.Controller
{
    public class HomeControllerTests
    {
        private readonly Mock<ICreditCard> _mockRepo;
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _mockRepo = new Mock<ICreditCard>();
            _controller = new HomeController(Mock.Of<ILogger<HomeController>>(),_mockRepo.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

  

        [Fact]
        public void ValidateCard_InvalidModelState_ReturnsView()
        {
           

            var nos = new List<string>(){ "456666"};


            var result = _controller.ValidateCard(nos);

            var viewResult = Assert.IsType<ViewResult>(result);
            var testResult = Assert.IsType<List<CreditCardModel>>(viewResult.Model);

            Assert.Equal(nos[0], testResult[0].cardNo);
            Assert.Equal(CardType.UNKNOWN, testResult[0].cardType);
        }


        [Fact]
        public void ValidateCard_ModelStateValid_ValidateCardCalledOnce()
        {
            List<CreditCardModel>? creditcards = null;

            _mockRepo.Setup(r => r.checkCard(It.IsAny<List<string>>()))
                .Callback<List<CreditCardModel>>(x => creditcards = x);

            var carList = new List<string>()
            {
                "123-5435789603-21",
                "4111111111111111"
            };

            _controller.ValidateCard(carList);
            _mockRepo.Verify(x => x.checkCard(It.IsAny<List<string>>()), Times.Once);

            Assert.Equal(creditcards.Count, 2);
            Assert.Equal(creditcards.First().cardStatus,false );
            Assert.Equal(creditcards.First().cardType, CardType.UNKNOWN);
        }

        [Fact]
        public void ValidateCard_ActionExecuted_RedirectsToIndexAction()
        {

            var result = _controller.ValidateCard(new List<string>() { "45555547899" });

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
