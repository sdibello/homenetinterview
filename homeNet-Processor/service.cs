﻿using apiClient;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace homeNetProcessor
{
    public class Service
    {
        private simpleClient _client;
        public string datasetid;
        public int[] vehiclesWorkList;
        public List<apiClient.rdo.VehicleDetailsRepsonse> vechileDetails;
        public List<apiClient.rdo.DealerDetailResponse> dealerDetails;

        public Service(simpleClient client)
        {
            this._client = client;
            LoadData();
        }

        public string GetDataSet()
        {
            try
            {
                Task<Tuple<string, int>> task = Task.Run(async () => await _client.GetDataset());
                task.Wait();
                Tuple<string, int> result = task.Result;
                return result.Item1;
            }
            catch (Exception ex) {
                throw new ApplicationException(string.Format("GetDataSet failed : {0}", ex.Message));
            }

        }

        public int[] GetvehicleList(string datasetId)
        {
            try
            {
                Task<Tuple<int[], int>> vehicleTask = Task.Run(async () => await _client.Getvehicles(datasetId));
                vehicleTask.Wait();
                Tuple<int[], int> result = vehicleTask.Result;
                return result.Item1;
            }
            catch (Exception ex) {
                throw new ApplicationException(string.Format("GetvehicleList failed : {0}", ex.Message));
            }
        }

        public Tuple<apiClient.rdo.VehicleDetailsRepsonse, int> GetvehicleDetails(int vehcileID)
        {
            try
            {
                Task<Tuple<apiClient.rdo.VehicleDetailsRepsonse, int>> vehicleDetailTask = Task.Run(async () => await _client.GetvehiclesDetails(datasetid, vehcileID));
                vehicleDetailTask.Wait();
                Tuple<apiClient.rdo.VehicleDetailsRepsonse, int> result = vehicleDetailTask.Result;
                return result;
            }
            catch (Exception ex) {
                throw new ApplicationException(string.Format("GetvehicleDetails failed : {0}", ex.Message));
            }
        }

        public Tuple<apiClient.rdo.DealerDetailResponse, int> GetDealerDetails(int dealerId)
        {
            try
            {
                Task<Tuple<apiClient.rdo.DealerDetailResponse, int>> DealerDetailTask = Task.Run(async () => await _client.GetDealerDetail(datasetid, dealerId));
                DealerDetailTask.Wait();
                Tuple<apiClient.rdo.DealerDetailResponse, int> result = DealerDetailTask.Result;
                return result;
            }
            catch (Exception ex) {
                throw new ApplicationException(string.Format("GetDealerDetails failed : {0}", ex.Message));
            }

        }

        public Tuple<int?, string> PostAnswer(apiClient.dto.Answer answer)
        {
            try
            {
                Task<Tuple<int?, string>> PostAnswerTask = Task.Run(async () => await _client.PostAnswer(datasetid, answer));
                PostAnswerTask.Wait();
                return PostAnswerTask.Result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("PostAnswer failed : {0}", ex.Message));
            }

        }

        public Boolean LoadData()
        {
            this.datasetid = this.GetDataSet();
            this.vehiclesWorkList = this.GetvehicleList(datasetid);
            this.vechileDetails = new List<apiClient.rdo.VehicleDetailsRepsonse>();
            this.dealerDetails = new List<apiClient.rdo.DealerDetailResponse>();

            Parallel.ForEach(vehiclesWorkList, (id) => {
                var apiresponse = this.GetvehicleDetails(id);
                if (apiresponse.Item2 == 200)
                {
                    vechileDetails.Add(apiresponse.Item1);
                }
            });

            Parallel.ForEach(vechileDetails, (vdetails) => {
                var apiresponse = this.GetDealerDetails(vdetails.dealerId);
                if (apiresponse.Item2 == 200)
                {
                    dealerDetails.Add(apiresponse.Item1);
                }
            });

            return true;
        }

    }
}

