using CoinPaymentsDemo.Models;
using libCoinPaymentsNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CoinPaymentsDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PaymentContext context = new PaymentContext();
            return View(context.Transactions.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        [ActionName("CreateTransaction")]
        public JsonResult CreateTransaction(int amount) {
            PaymentContext context = new PaymentContext();
            var transaction = context.Transactions.Add(new Models.Transaction { Amount = amount, Status = -5, StatusMessage = "Pending" });
            context.SaveChanges();
            CoinPayments coinPayments = new CoinPayments("f73f21154c9f00A19DF62C3EA63d1f834aD42afa5c46DeB25f858Cf4F1576Fd9", "94850cce3d7bbd018077ecf3b8c89315c90836710f1ed4a16f64d1181567380d");
            var list = new SortedList<string, string>();
            list.Add("amount", amount.ToString());
            list.Add("currency1", "USD");
            list.Add("currency2", "LTCT");
            list.Add("invoice", transaction.Id.ToString());
            list.Add("ipn_url", GetBaseUrl() + "Home/Notification");
            var result = coinPayments.CallAPI("create_transaction", list);
            dynamic res;
            dynamic err;
            result.TryGetValue("error", out err);
            result.TryGetValue("result", out res);
            if ((string)err == "ok"){
                transaction.Amount = double.Parse(res["amount"]);//Amount in currency to send
                transaction.TxnId = res["txn_id"];
                transaction.ConfirmationsNeeded = int.Parse(res["confirms_needed"]);
                transaction.TimeOut = res["timeout"];
                transaction.StatusUrl = res["status_url"];
                transaction.QRcodeUrl = res["qrcode_url"];
                transaction.Address = res["address"];
                context.SaveChanges();
            }
            return Json(new { transaction= transaction , ipn_url= GetBaseUrl() + "Home/Notification" });
        }
        [HttpPost]
        [ActionName("Notification")]
        public JsonResult Notification(IPN ipn)
        {
            var invoiceNumber = int.Parse(ipn.invoice);
            PaymentContext context = new PaymentContext();
            var transaction = context.Transactions.FirstOrDefault(x => x.Id == invoiceNumber);
            transaction.Status = int.Parse(ipn.status);
            transaction.StatusMessage = ipn.status_text;
            context.SaveChanges();
            return Json("ok");
        }
        private string GetBaseUrl() {
            return string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.DnsSafeHost, Url.Content("~"));
        }
    }
}