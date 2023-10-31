using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBot.API
{
    public class API_Model
    {
        public class Root
        {
            public List<Result> result { get; set; }
        }

        public class Result
        {
            public string key { get; set; }
            public string name { get; set; }
            public string name_en { get; set; }
            public double? dominance { get; set; }
            public double? market_cap { get; set; }
            public double price { get; set; }
            public double? price_change_24h { get; set; }
        }
    }
}
