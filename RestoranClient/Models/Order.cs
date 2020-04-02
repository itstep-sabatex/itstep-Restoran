using System;
using System.Collections.Generic;
using System.Text;

namespace RestoranClient.Models
{
    public class Order
    {
        public int id { get; set; }
        public int? waiter_id { get; set; }
        public int? abonent_id { get; set; }
        public DateTime time_order { get; set; }
        public decimal? bill { get; set; }
        public int? source_id { get; set; }
        public DateTime? end_order { get; set; }
        public List<Detail> Details { get; set; }

    }
}
