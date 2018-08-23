using apiClient;
using apiClient.dto;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homeNet_Processor
{
    public class processor
    {
        procConfig _config;
        simpleClient _client;

        /// <summary>
        /// constructor.  
        /// </summary>
        /// <param name="config">config settings</param>
        public processor(procConfig config)
        {
            this._config = config;
            _client = new simpleClient();
            _client.setBaseAddress(_config.baseUrl);
            InitiateProcessAsync();
        }

        private void InitiateProcessAsync()
        {
            var service = new homeNetProcessor.Service(_client);

            Answer answer = new Answer();

            foreach (var item in service.dealerDetails)
            {
                var dealer = new Dealers() {Name = item.name, DealerId = item.dealerId };
                var check =  answer.dealers.SingleOrDefault(x => x.Name == dealer.Name);
                if (check == null) {
                    answer.dealers.Add(dealer);
                }
            }

            foreach (var item in service.vechileDetails) {
                var finddealer = answer.dealers.Where(x => x.DealerId == item.dealerId).Single();
                var car = new Vehicles() { Make = item.make, Model = item.model, Year = item.year, VechileId = item.vehicleId };
                var check = finddealer.vehicles.SingleOrDefault(x => x.VechileId == item.vehicleId);
                if (check == null) {
                    finddealer.vehicles.Add(car);
                }
            }

            //should be done.
            Console.Write(answer.dealers.First().DealerId);

            if (service.PostAnswer(answer))
                Console.WriteLine("done");

        }

    }
}
