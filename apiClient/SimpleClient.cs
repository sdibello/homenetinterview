using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using apiClient.rdo;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace apiClient
{

    public class simpleClient 
    {
        static HttpClient _client;

        public simpleClient()
        {
            _client = new HttpClient(new HttpClientHandler { UseProxy = false });
        }

        public async Task<int?> PostAnswer(string datasetId, dto.Answer answer)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(answer);
            var stringContent = new StringContent(JsonConvert.SerializeObject(answer), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(string.Format("{0}/api/{1}/answer", _client.BaseAddress, datasetId), stringContent);
            string result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            { 
                return (int)response.StatusCode;
            }
            return null;
        }


        public async Task<Tuple<string, int>> GetDataset()
        {
            HttpResponseMessage response = await _client.GetAsync(string.Format("{0}/api/datasetId", _client.BaseAddress));
            rdo.DataSetResponse datasetobj = null;
            if (response.IsSuccessStatusCode)
            {
                datasetobj = await response.Content.ReadAsAsync<rdo.DataSetResponse>();
                return new Tuple<string, int>(datasetobj.DatasetId, (int)response.StatusCode);
            }
            return null;
        }

        public async Task<Tuple<int[], int>> Getvehicles(string datasetId)
        {
            HttpResponseMessage response = await _client.GetAsync(string.Format("{0}/api/{1}/vehicles ", _client.BaseAddress, datasetId));
            rdo.vehiclesReponse vehicleobj = null;
            if (response.IsSuccessStatusCode)
            {
                vehicleobj = await response.Content.ReadAsAsync<rdo.vehiclesReponse>();
                return new Tuple<int[], int>(vehicleobj.vehicleIds, (int)response.StatusCode);
            }
            return null;
        }

        public async Task<Tuple<VehicleDetailsRepsonse, int>> GetvehiclesDetails(string datasetId, int vechileId)
        {
            HttpResponseMessage response = await _client.GetAsync(string.Format("{0}/api/{1}/vehicles/{2} ", _client.BaseAddress, datasetId, vechileId.ToString()));
            rdo.VehicleDetailsRepsonse vehicleDetailsobj = null;
            if (response.IsSuccessStatusCode)
            {
                vehicleDetailsobj = await response.Content.ReadAsAsync<rdo.VehicleDetailsRepsonse> ();
                return new Tuple<rdo.VehicleDetailsRepsonse, int>(vehicleDetailsobj, (int)response.StatusCode);
            }
            return null;
        }

        public async Task<Tuple<DealerDetailResponse, int>> GetDealerDetail(string datasetId, int dealerId)
        {
            HttpResponseMessage response = await _client.GetAsync(string.Format("{0}/api/{1}/dealers/{2} ", _client.BaseAddress, datasetId, dealerId));
            rdo.DealerDetailResponse dealerobj = null;
            if (response.IsSuccessStatusCode)
            {
                dealerobj = await response.Content.ReadAsAsync<rdo.DealerDetailResponse>();
                return new Tuple<rdo.DealerDetailResponse, int>(dealerobj, (int)response.StatusCode);
            }
            return null;
        }

        public void setBaseAddress(string baseAddress)
        {
            _client.BaseAddress = new Uri(baseAddress);             //http://vautointerview.azurewebsites.net
        }


    }
}
