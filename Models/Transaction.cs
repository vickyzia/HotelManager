using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoinPaymentsDemo.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string TxnId { get; set; }
        public string Address { get; set; }
        public int ConfirmationsNeeded { get; set; }
        public int TimeOut { get; set; }
        public string StatusUrl { get; set; }
        public string QRcodeUrl { get; set; }
        public int Status { get; set; }
        public string StatusMessage { get; set; }
    }
}