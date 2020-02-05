using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWorkASP.Models;

namespace TestWorkASP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Records = LoadRecords();
            return View();
        }

        [HttpGet]
        public ActionResult SelectedCustomer(int? id)
        {
            DataBaseOrdersContext db = new DataBaseOrdersContext();
            var orders = db.Orders.Where(orderItem => orderItem.Customer.Id == id);
            List<Record> records = new List<Record>();
            foreach (var item in orders)
            {
                Record record = new Record
                {
                    OrderId = item.Id,
                    Date = item.Date,
                    Sum = item.OrderLines.Where(i => i.Order.Id == item.Id).Sum(i => i.Sum),
                    Quantity = item.OrderLines.Where(i => i.Order.Id == item.Id).Count()
                };
                records.Add(record);
            }
            ViewBag.Records = records;
            ViewBag.CustomerName = db.Customers.FirstOrDefault(item => item.Id == id).Name;
            return View();
        }

        [HttpGet]
        public ActionResult SelectedOrder(int? id)
        {
            DataBaseOrdersContext db = new DataBaseOrdersContext();
            ViewBag.OrderLines = db.OrderLines.Where(item => item.Order.Id == id);
            ViewBag.OrderNumber = id;
            return View();
        }

        [HttpGet]
        public ActionResult Statistics()
        {
            DataBaseOrdersContext db = new DataBaseOrdersContext();
            List<Record> records = new List<Record>();
            var statistics = db.Customers.GroupBy(item => item.Category).Select(item => new
            {
                CategoriName = item.Key,
                Quantity = item.Count(),
                Sum = item.Select(s => s.Orders.Select(g => g.OrderLines.Sum(f => f.Sum)).Sum()).Sum()
            });
            foreach (var item in statistics)
            {
                Record record = new Record
                {
                    Name = item.CategoriName,
                    Quantity = item.Quantity,
                    Sum = item.Sum
                };
                records.Add(record);
            }
            ViewBag.Records = records;
            return View();
        }

        private List<Record> LoadRecords()
        {
            List<Record> records = new List<Record>();
            using (DataBaseOrdersContext db = new DataBaseOrdersContext())
            {
                var customers = db.Customers.Select(c => new
                {
                    CustomerId = c.Id,
                    c.Name,
                    c.Adress,
                    Sum = c.Orders.GroupBy(o => o.Customer.Id).Select(
                        r => r.Select(or => or.OrderLines.Sum(s => s.Sum)).Sum()).Sum()
                });
                foreach (var item in customers)
                {
                    Record record = new Record
                    {
                        CustomerId = item.CustomerId,
                        Name = item.Name,
                        Adress = item.Adress,
                        Sum = item.Sum
                    };
                    records.Add(record);
                }
            }
            return records;
        }
    }
}