using AutoMapper;
using BookSeeker.Common;
using BookSeeker.Common.Extensions;
using BookSeeker.CurrencyConvert;
using BookSeeker.Engine.Models;
using BookSeeker.Providers.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BookSeeker.Engine.Services
{
    public class BookSearchService : IBookSearchService
    {
        private readonly ILogger<BookSearchService> _logger;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IBookDataProvider> _bookDataProviders;
        private readonly ICurrencyConvertClient _currencyConvertClient;


        public BookSearchService(IEnumerable<IBookDataProvider> bookDataProviders,
            ILogger<BookSearchService> logger,
            IMapper mapper,
            ICurrencyConvertClient currencyConvertClient)
        {
            _bookDataProviders = bookDataProviders;
            _logger = logger;
            _mapper = mapper;
            _currencyConvertClient = currencyConvertClient;
        }

        public async Task<ServiceResult<IEnumerable<BookSearchItem>>> SearchByTitleAsync(string title)
        {
            try
            {
                var tasks = _bookDataProviders
                    .Select(x => x.SearchByTitleAsync(title));

                var searchResults = await Task.WhenAll(tasks);

                var searchItems = searchResults
                    .SelectMany(x => x)
                    .GroupBy(x => x.Isbn?.Id10Digits, (id, items) =>
                    {
                        var itemsList = items.ToList();
                        return new BookSearchItem
                        {
                            Isbn = id,
                            Title = itemsList.First().Title,
                            Authors = itemsList.First().Authors,
                            Providers = itemsList.Select(p => p.Provider)
                        };
                    })
                    .ToList();

                return ServiceResult<IEnumerable<BookSearchItem>>.Success(searchItems);
            }
            catch (Exception e)
            {
                var msg = $"Search by title ({title}) failed.";
                _logger.LogError(e, msg);
                return ServiceResult<IEnumerable<BookSearchItem>>.Fail(msg);
            }
        }

        public async Task<ServiceResult<IEnumerable<BookOffer>>> SearchBookOffersAsync(string isbn)
        {
            try
            {
                var tasks = _bookDataProviders
                    .Select(x => x.SearchOffersByIsbnAsync(isbn));

                var offerResults = await Task.WhenAll(tasks);

                var offersWithPrices = offerResults.Where(x => x.Price.HasValue).ToList();

                var offers = _mapper.Map<IEnumerable<BookOffer>>(offersWithPrices).ToList();

                var localCurrencyCode = CultureInfo.CurrentCulture.GetCurrencyCode();

                foreach (var offer in offers)
                {
                    if (offer.OriginalPrice.CurrencyCode == localCurrencyCode)
                    {
                        offer.LocalPrice = offer.OriginalPrice;
                    }
                    else
                    {
                        var convertedAmount = _currencyConvertClient.Convert(offer.OriginalPrice.CurrencyCode,
                            localCurrencyCode, offer.OriginalPrice.Amount);

                        offer.LocalPrice = convertedAmount.HasValue
                            ? new Money(localCurrencyCode, convertedAmount.Value)
                            : null;
                    }
                }

                return ServiceResult<IEnumerable<BookOffer>>.Success(offers);
            }
            catch (Exception e)
            {
                var msg = $"Search offers ({isbn}) failed.";
                _logger.LogError(e, msg);
                return ServiceResult<IEnumerable<BookOffer>>.Fail(msg);
            }
        }
    }
}