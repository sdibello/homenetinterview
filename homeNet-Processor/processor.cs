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
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

        /// <summary>
        /// constructor.  
        /// </summary>
        /// <param name="config">config settings</param>
        public processor(procConfig config)
        {
            watch.Start();
            this._config = config;
            _client = new simpleClient();
            _client.setBaseAddress(_config.baseUrl);
            InitiateProcessAsync();
        }

        private void InitiateProcessAsync()
        {
            var service = new homeNetProcessor.Service(_client);

            Answer answer = new Answer();

            foreach (var dealer in service.dealerDetails)
            {
                var dealerItem = new Dealers() {Name = dealer.name, DealerId = dealer.dealerId };
                var check =  answer.dealers.SingleOrDefault(x => x.Name == dealerItem.Name);
                if (check == null) {
                    answer.dealers.Add(dealerItem);
                }
            }

            foreach (var item in service.vechileDetails) {
                var finddealer = answer.dealers.Where(x => x.DealerId == item.dealerId).Single();
                var car = new Vehicles() { Make = item.make, Model = item.model, Year = item.year, vehicleId  = item.vehicleId };
                var check = finddealer.vehicles.SingleOrDefault(x => x.vehicleId == car.vehicleId);
                if (check == null) {
                    finddealer.vehicles.Add(car);
                }
            }

            Tuple<int?, string> result = service.PostAnswer(answer);
            watch.Stop();
            if (result.Item1 == 200)
            {
                Console.WriteLine(string.Format("answer posted, total elasped seconds {0}", watch.ElapsedMilliseconds / 1000));
                Console.Write(result.Item2);
            }
        }

    }
}
