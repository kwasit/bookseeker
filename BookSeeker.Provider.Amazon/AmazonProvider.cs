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
    public class AmazonProvider : IBookDataProvider
    {
        private readonly ILogger<AmazonProvider> _logger;
        private readonly IConfiguration _configuration;

        public AmazonProvider( ILogger<AmazonProvider> logger, IConfiguration configuration)
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
                return new List<ProviderBookSearchItem>();
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
                return new ProviderBookOffer();
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