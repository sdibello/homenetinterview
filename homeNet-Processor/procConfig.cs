using System.Configuration;

namespace homeNet_Processor
{
    public sealed class procConfig
    {
        public string baseUrl { get; set; }

        public void readConfig()
        {
            this.baseUrl =  ConfigurationManager.AppSettings["url.base"];
        }
    }
}
