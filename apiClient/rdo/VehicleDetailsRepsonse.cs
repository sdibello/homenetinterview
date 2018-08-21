using System;
using System.Collections.Generic;
using System.Text;

namespace apiClient.rdo
{
    public class VehicleDetailsRepsonse
    {
        public int vehicleId { get; set; }
        public int year { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public int dealerId { get; set; }
    }
}
