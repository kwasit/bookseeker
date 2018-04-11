using BookSeeker.Providers.Common;
using BookSeeker.Providers.Common.Models;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSeeker.Provider.Google
{
    public class GoogleProvider : IBookDataProvider
    {
        private readonly ILogger<GoogleProvider> _logger;
        private readonly IConfiguration _configuration;

        public GoogleProvider(IConfiguration configuration, ILogger<GoogleProvider> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public string Name => "google";

        public async Task<IEnumerable<ProviderBookSearchItem>> SearchByTitleAsync(string title)
        {
            try
            {
                var request = BookService.Volumes.List(title);

                var volumes = await request.ExecuteAsync();

                return volumes.Items.Select(x => new ProviderBookSearchItem
                {
                    Provider = Name,
                    Title = x.VolumeInfo.Title,
                    Authors = x.VolumeInfo.Authors,
                    Isbn = new IsbnData
                    {
                        Id13Digits = x.VolumeInfo.IndustryIdentifiers.FirstOrDefault(i => i.Type == "ISBN_13")?.Identifier,
                        Id10Digits = x.VolumeInfo.IndustryIdentifiers.FirstOrDefault(i => i.Type == "ISBN_10")?.Identifier,
                    }
                }).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Google: Search books failed.");
                return Enumerable.Empty<ProviderBookSearchItem>();
            }
        }

        public async Task<ProviderBookOffer> SearchOffersByIsbnAsync(string isbn)
        {
            try
            {
                var request = BookService.Volumes.List($"isbn:{isbn}");

                var volumes = await request.ExecuteAsync();

                var volume = volumes.Items.FirstOrDefault();
                var saleInfo = volume?.SaleInfo;

                if (saleInfo == null)
                {
                    return null;
                }

                return new ProviderBookOffer
                {
                    Title = volume.VolumeInfo.Title,
                    Isbn = volume.VolumeInfo.IndustryIdentifiers.FirstOrDefault(i => i.Type == "ISBN_10")?.Identifier,
                    Provider = Name,
                    Price =  (decimal?) saleInfo.ListPrice?.Amount,
                    CurrencyCode = saleInfo.ListPrice?.CurrencyCode,
                    Url = saleInfo.BuyLink
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Google: Search offers failed.");
                return new ProviderBookOffer();
            }
        }

        private BooksService BookService
        {
            get
            {
                var auth = _configuration.GetSection("GoogleAuthentication");
                return new BooksService(new BaseClientService.Initializer
                {
                    ApplicationName = auth.GetSection("ApplicationName").Value,
                    ApiKey = auth.GetSection("ApiKey").Value,
                });
            }
        }
    }
}

