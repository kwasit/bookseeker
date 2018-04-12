using BookSeeker.Common.Extensions;
using BookSeeker.Provider.Apress.RestModels;
using BookSeeker.Providers.Common;
using BookSeeker.Providers.Common.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BookSeeker.Provider.Apress
{
    public class ApressProvider : IBookOffersDataProvider
    {
        private const string BaseUrl = "https://www.apress.com/";

        private readonly ILogger<ApressProvider> _logger;

        public string Name => "apress";

        public ApressProvider(ILogger<ApressProvider> logger)
        {
            _logger = logger;
        }

        public async Task<ProviderBookOffer> SearchOffersByIsbnAsync(string isbn)
        {
            try
            {
                var request = new RestRequest("gp/product-search/ajax/prices", Method.POST) { RequestFormat = DataFormat.Json };

                var serializeObject = JsonConvert.SerializeObject(new[]
                {
                    new BookItemRestModel
                    {
                        Id = isbn,
                        Type = "book"
                    }
                });

                request.AddParameter("application/json; charset=utf-8", serializeObject, ParameterType.RequestBody);

                //request.AddBody(serializeObject);

                var result = await RestClient.ExecuteTaskAsync<IEnumerable<BookOfferRestModel>>(request);

                var hasOffer = result.Data.Any();

                var offer = result.Data.First();

                return hasOffer ? new ProviderBookOffer
                {
                    CurrencyCode = CultureInfo.GetCultureInfo("de-DE").GetCurrencyCode(),
                    Isbn = isbn,
                    Price = offer.Price.BestPrice,
                    Provider = Name,
                    Url = $"https://www.apress.com/gp/book/{offer.Id}"
                } : new ProviderBookOffer();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{Name}: Search books failed.");
                return new ProviderBookOffer();
            }
        }

        private static RestClient RestClient => new RestClient(BaseUrl);
    }
}