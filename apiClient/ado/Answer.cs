using System;
using System.Collections.Generic;
using System.Text;

namespace apiClient.ado
{
    class Answer {
        public Dealers[] dealers;
    }

    class Dealers {
        public int DealerId { get; set; }
        public string Name { get; set; }
        public Vehicles[] vehicles;
    }
    
    public class Vehicles {
        public int VechileId { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

    }
}
