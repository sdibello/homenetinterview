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
            var vechileDetails = new List<apiClient.rdo.VehicleDetailsRepsonse>();
            var dealerDetails = new List<apiClient.rdo.DealerDetailResponse>();

            var vehiclesWorkList = simpleclient.GetvehicleList(datasetid);

            Parallel.ForEach(vehiclesWorkList, (id) => {
                var apiresponse = simpleclient.GetvehicleDetails(datasetid, id);
                if (apiresponse.Item2 == 200)
                {
                    vechileDetails.Add(apiresponse.Item1);
                }
            });

            Parallel.ForEach(vechileDetails, (vdetails) => {
                var apiresponse = simpleclient.GetDealerDetails(datasetid, vdetails.dealerId);
                if (apiresponse.Item2 == 200)
                {
                    dealerDetails.Add(apiresponse.Item1);
                }
            });

            foreach (var item in vechileDetails)
            {
                Console.WriteLine("{0}{1}{2}{3}", item.make, item.model, item.vehicleId, item.year);            
            }

            foreach (var item in dealerDetails)
            {
                Console.WriteLine("{0}{1}", item.dealerId, item.name);
            }

        }

    }
}
