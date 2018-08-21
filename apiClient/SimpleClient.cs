using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace apiClient
{

    public class simpleClient
    {
        static HttpClient _client;

        public simpleClient()
        {
            _client = new HttpClient(new HttpClientHandler { UseProxy = false });
            _client.BaseAddress = new Uri("http://vautointerview.azurewebsites.net");
        }

        public string GetDataSet()
        {
            Task<Tuple<string, int>> task = Task.Run(async () => await GetDatasetAsync());
            task.Wait();
            Tuple<string, int> result = task.Result;

            if ( result == null) {
                //log error
            }
            return result.Item1;
        }

        private async Task<Tuple<string, int>> GetDatasetAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(string.Format("{0}/api/datasetId", _client.BaseAddress));
            rdo.DataSetResponse datasetobj = null;
            if (response.IsSuccessStatusCode) {
                datasetobj = await response.Content.ReadAsAsync<rdo.DataSetResponse>();
                return new Tuple<string, int>(datasetobj.DatasetId, (int)response.StatusCode);
            }
            return null;
        }





        public int[] GetvehicleList(string datasetId)
        {
            Task<Tuple<int[], int>> vehicleTask = Task.Run(async () => await Getvehicles(datasetId));
            vehicleTask.Wait();
            Tuple<int[], int> result = vehicleTask.Result;

            if (result == null)
            {
                //log error
            }
            return result.Item1;
        }

        private async Task<Tuple<int[], int>> Getvehicles(string datasetId)
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





        private async Task<Tuple<rdo.VehicleDetailsRepsonse, int>> GetvehicleDetails(string datasetId, string vehicleId)
        {
            HttpResponseMessage response = await _client.GetAsync(string.Format("{0}/api/{1}/vehicles/{2} ", _client.BaseAddress, datasetId, vehicleId));
            rdo.VehicleDetailsRepsonse vehicleobj = null;
            if (response.IsSuccessStatusCode)
            {
                vehicleobj = await response.Content.ReadAsAsync<rdo.VehicleDetailsRepsonse>();
                return new Tuple<rdo.VehicleDetailsRepsonse, int>(vehicleobj, (int)response.StatusCode);
            }
            return null;
        }

        private async Task<Tuple<rdo.DealerDetailResponse, int>> GetDealerDetail(string datasetId, string dealerId)
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

    }
}
