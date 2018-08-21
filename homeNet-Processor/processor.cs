using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace homeNet_Processor
{
    public class processor
    {
        public processor()
        {
            //this._client = new IO.Swagger.Client.ApiClient("http://vautointerview.azurewebsites.net");
            InitiateProcessAsync();
        }

        private void InitiateProcessAsync()
        {
            var simpleclient = new apiClient.simpleClient();
            var datasetid = simpleclient.GetDataSet();

            var vehicles = simpleclient.GetvehicleList(datasetid);



            //var formParams = new Dictionary<string, string>();
            //var headerParams = new Dictionary<string, string>() {{ "Accept", "application/json" }};
            //var qParams = new List<KeyValuePair<string, string>>();
            //var fileParams = new Dictionary<string, FileParameter>();
            //var pathParams = new Dictionary<string, string>();
            //var contentType = "application/json; charset=utf-8";

            //try
            //{
            //    var result = _client.CallApi("api/datasetId", Method.GET, qParams, null, headerParams, formParams, fileParams, pathParams, contentType);
            //    Console.WriteLine(result);
            //}
            //catch (Exception) {

            //    throw;
            //}

        }


    }
}
