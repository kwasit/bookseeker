using AutoMapper;
using BookSeeker.Common.Extensions;
using BookSeeker.CurrencyConvert;
using BookSeeker.Engine.Services;
using BookSeeker.Providers.Common;
using BookSeeker.Providers.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BookSeeker.Engine.UnitTests.Services
{
    [TestClass]
    public class BookSearchServiceTests
    {
        private BookSearchService _target;
        private Mock<IBookDataProvider> _firstBookDataProviderMock;
        private Mock<IBookDataProvider> _secondBookDataProviderMock;
        private Mock<ICurrencyConvertClient> _currencyConvertClientMock;
        private IMapper _mapper;

        [TestInitialize]
        public void TestSetUp()
        {
            _currencyConvertClientMock = new Mock<ICurrencyConvertClient>();
            _mapper = new Mapper(new MapperConfiguration(e => e.AddProfile<EngineProfile>()));

            _firstBookDataProviderMock = new Mock<IBookDataProvider>();
            _secondBookDataProviderMock = new Mock<IBookDataProvider>();

            _firstBookDataProviderMock.SetupGet(x => x.Name).Returns("firstProvider");
            _secondBookDataProviderMock.SetupGet(x => x.Name).Returns("secondProvider");

            var bookDataProviders = new[]
            {
                _firstBookDataProviderMock.Object,
                _secondBookDataProviderMock.Object
            };

            _target = new BookSearchService(bookDataProviders, null, _mapper, _currencyConvertClientMock.Object);
        }

        [TestMethod]
        public async Task SearchByTitleAsync_ForSomeText_ShouldSucceed()
        {
            // Arrange
            _firstBookDataProviderMock.Setup(x => x.SearchByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new List<ProviderBookSearchItem>
                {
                    new ProviderBookSearchItem()
                });


            _secondBookDataProviderMock.Setup(x => x.SearchByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new List<ProviderBookSearchItem>
                {
                    new ProviderBookSearchItem()
                });


            // Act
            var actual = await _target.SearchByTitleAsync("foo");

            // Assert
            Assert.IsTrue(actual.Succeeded);
        }

        [TestMethod]
        public async Task SearchByTitleAsync_ForSomeText_ShouldGroupResultsWithSameIsbn()
        {
            // Arrange
            _firstBookDataProviderMock.Setup(x => x.SearchByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new List<ProviderBookSearchItem>
                {
                    new ProviderBookSearchItem
                    {
                        Isbn = new IsbnData{Id10Digits = "xxx"},
                        Provider = "first"
                    }
                });


            _secondBookDataProviderMock.Setup(x => x.SearchByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new List<ProviderBookSearchItem>
                {
                    new ProviderBookSearchItem
                    {
                        Isbn = new IsbnData{Id10Digits = "xxx"},
                        Provider = "second"
                    }
                });


            // Act
            var actual = await _target.SearchByTitleAsync("foo");

            // Assert
            CollectionAssert.AreEqual(new[] { "first", "second" }, actual.Data.First().Providers.ToList());
        }

        [TestMethod]
        public async Task SearchBookOffersAsync_ForSomeIsbn_ShouldSucceed()
        {
            // Arrange
            _firstBookDataProviderMock.Setup(x => x.SearchOffersByIsbnAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new ProviderBookOffer());

            _secondBookDataProviderMock.Setup(x => x.SearchOffersByIsbnAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new ProviderBookOffer());

            // Act
            var actual = await _target.SearchBookOffersAsync("xxx");

            // Assert
            Assert.IsTrue(actual.Succeeded);
        }

        [TestMethod]
        public async Task SearchBookOffersAsync_ForSomeIsbn_ShouldFilterOffersWithoutPrices()
        {
            // Arrange
            _firstBookDataProviderMock.Setup(x => x.SearchOffersByIsbnAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new ProviderBookOffer
                {
                    Price = 123
                });

            _secondBookDataProviderMock.Setup(x => x.SearchOffersByIsbnAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new ProviderBookOffer
                {
                    Price = null
                });

            // Act
            var actual = await _target.SearchBookOffersAsync("xxx");

            // Assert
            Assert.IsTrue(actual.Data.Count() == 1);
        }

        [TestMethod]
        public async Task SearchBookOffersAsync_ForSameOriginalCurrencyAsLocal_ShouldAssignLocalPrice()
        {
            // Arrange
            _firstBookDataProviderMock.Setup(x => x.SearchOffersByIsbnAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new ProviderBookOffer
                {
                    Price = 123,
                    CurrencyCode = CultureInfo.CurrentCulture.GetCurrencyCode()
                });

            _secondBookDataProviderMock.Setup(x => x.SearchOffersByIsbnAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new ProviderBookOffer());

            // Act
            var actual = await _target.SearchBookOffersAsync("xxx");

            // Assert
            Assert.AreEqual(actual.Data.First().LocalPrice.Amount, 123);
        }

        [TestMethod]
        public async Task SearchBookOffersAsync_ForDifferentOriginalCurrencyAsLocal_ShouldCallCurrencyConvert()
        {
            // Arrange
            _firstBookDataProviderMock.Setup(x => x.SearchOffersByIsbnAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new ProviderBookOffer
                {
                    Price = 123,
                    CurrencyCode = "QWE"
                });

            _secondBookDataProviderMock.Setup(x => x.SearchOffersByIsbnAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new ProviderBookOffer());

            // Act
            await _target.SearchBookOffersAsync("xxx");

            // Assert
            _currencyConvertClientMock.Verify(x => x.Convert("QWE", CultureInfo.CurrentCulture.GetCurrencyCode(), It.IsAny<decimal>()));
        }
    }
}