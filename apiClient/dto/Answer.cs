using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace apiClient.dto
{
    public class Answer {
        public List<Dealers> dealers;

        public Answer()
        {
            dealers = new List<Dealers>();
        }
    }

    public class Dealers {

        public int DealerId { get; set; }
        public string Name { get; set; }

        public List<Vehicles> vehicles;

        public Dealers()
        {
            vehicles = new List<Vehicles>();
        }
    }
    
    public class Vehicles {
        public int vehicleId { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

    }
}
