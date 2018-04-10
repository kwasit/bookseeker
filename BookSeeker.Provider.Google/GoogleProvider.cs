using BookSeeker.Providers.Common;
using BookSeeker.Providers.Common.Models;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSeeker.Provider.Google
{
    public class GoogleProvider : IBookDataProvider
    {
        private readonly IConfiguration _configuration;

        public GoogleProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Name => "google";

        public async Task<IEnumerable<ProviderBookSearchItem>> SearchByTitleAsync(string title)
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
            });
        }

        public async Task<ProviderBookOffer> SearchOffersByIsbnAsync(string isbn)
        {
            var request = BookService.Volumes.List($"isbn:{isbn}");

            var volumes = await request.ExecuteAsync();

            var saleInfo = volumes.Items.FirstOrDefault()?.SaleInfo;

            if (saleInfo == null)
            {
                return null;
            }

            return new ProviderBookOffer
            {
                Provider = Name,
                Price = (decimal) saleInfo.ListPrice.Amount.Value,
                Url = saleInfo.BuyLink
            };
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

