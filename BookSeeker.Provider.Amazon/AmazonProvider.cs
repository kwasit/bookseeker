using BookSeeker.Providers.Common;
using BookSeeker.Providers.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nager.AmazonProductAdvertising;
using Nager.AmazonProductAdvertising.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSeeker.Provider.Amazon
{
    public class AmazonProvider : IBookOffersDataProvider, IBookSearchDataProvider
    {
        private readonly ILogger<AmazonProvider> _logger;
        private readonly IConfiguration _configuration;

        public AmazonProvider(ILogger<AmazonProvider> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public string Name => "amazon";

        public async Task<IEnumerable<ProviderBookSearchItem>> SearchByTitleAsync(string title)
        {
            try
            {
                var response = await AmazonWrapper.SearchAsync(title, AmazonSearchIndex.Books, AmazonResponseGroup.Small);

                return response.Items.Item
                    .Select(x => new ProviderBookSearchItem
                    {
                        Title = x.ItemAttributes.Title,
                        Isbn = new IsbnData
                        {
                            Id10Digits = x.ItemAttributes.ISBN
                        },
                        Authors = x.ItemAttributes.Author,
                        Provider = Name
                    })
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Amazon: Search books failed.");
                return Enumerable.Empty<ProviderBookSearchItem>();
            }
        }

        public async Task<ProviderBookOffer> SearchOffersByIsbnAsync(string isbn)
        {
            try
            {
                var response = await AmazonWrapper.SearchAsync(isbn, AmazonSearchIndex.Books, AmazonResponseGroup.Offers);

                var item = response.Items.Item.FirstOrDefault();
                var itemOffer = item.Offers.Offer.FirstOrDefault();
                var itemOfferListing = itemOffer.OfferListing.FirstOrDefault();

                return new ProviderBookOffer
                {
                    Title = item.ItemAttributes.Title,
                    Isbn = item.ItemAttributes.ISBN,
                    Provider = Name,
                    Price = decimal.Parse(itemOfferListing.Price.Amount),
                    CurrencyCode = itemOfferListing.Price.CurrencyCode,
                    Url = itemOfferListing.OfferListingId
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Amazon: Search offers failed.");
                return new ProviderBookOffer();
            }
        }

        private IAmazonWrapper AmazonWrapper
        {
            get
            {
                var auth = _configuration.GetSection("AmazonAuthentication");
                return new AmazonWrapper(new AmazonAuthentication
                {
                    AccessKey = auth.GetSection("AccessKey").Value,
                    SecretKey = auth.GetSection("SecretKey").Value
                }, AmazonEndpoint.US);
            }
        }
    }
}