using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.API.Models
{
    public class StockDetails
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string CompanyCode { get; set; }
    }
}
