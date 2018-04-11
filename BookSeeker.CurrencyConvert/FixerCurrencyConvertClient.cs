using BookSeeker.CurrencyConvert.RestModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;

namespace BookSeeker.CurrencyConvert
{
    internal class FixerCurrencyConvertClient : ICurrencyConvertClient
    {
        private const string BaseUrl = "http://data.fixer.io";

        private readonly ILogger<FixerCurrencyConvertClient> _logger;
        private readonly IConfiguration _configuration;

        public FixerCurrencyConvertClient(IConfiguration configuration, ILogger<FixerCurrencyConvertClient> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public decimal? Convert(string baseCode, string targetCode, decimal amount)
        {
            try
            {
                var request = new RestRequest("api/convert", Method.GET);
                request.AddParameter("access_key", ApiKey);
                request.AddParameter("from", baseCode);
                request.AddParameter("to", targetCode);
                request.AddParameter("amount", amount);

                var result = RestClient.Execute<ConvertRestModel>(request);

                return result.Data.Result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Cannot convert amount from {baseCode} to {targetCode}.");
                return null;
            }
        }
               
        private static RestClient RestClient => new RestClient(BaseUrl);
        private string ApiKey => _configuration.GetSection("FixerAuthentication").GetSection("ApiKey").Value;
    }
}