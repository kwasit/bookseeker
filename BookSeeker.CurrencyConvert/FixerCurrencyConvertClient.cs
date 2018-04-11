using BookSeeker.CurrencyConvert.RestModels;
using RestSharp;

namespace BookSeeker.CurrencyConvert
{
    internal class FixerCurrencyConvertClient : ICurrencyConvertClient
    {
        public decimal Convert(string baseCode, string targetCode, decimal amount)
        {
            var client = new RestClient("data.fixer.io");
            var request = new RestRequest("api/latest", Method.GET);
            request.AddParameter("access_key", "f32629394d40c41bfc6e7608a5a0d31c");
            request.AddParameter("base", baseCode);
            request.AddParameter("symbols", targetCode);

            var queryResult = client.Execute<LatestRatesRestModel>(request).Data;

            return 0;
        }
    }
}