using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoinPaymentsDemo.Models
{
    public class IPN
    {
        public string ipn_version { get; set; }
        public string ipn_type { get; set; } 
        public string ipn_mode { get; set; }
        public string ipn_id { get; set; }
        public string merchant { get; set; }
        public string status { get; set; }
        public string status_text { get; set; }
        public string txn_id { get; set; }
        public string currency1 { get; set; }
        public string currency2 { get; set; }
        public string amount1 { get; set; }
        public string amount2 { get; set; }
        public string fee { get; set; }
        public string invoice { get; set; }
        public string received_amount { get; set; }
        public string received_confirms { get; set; }
    }
}