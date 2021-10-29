using Microsoft.AspNetCore.Http;
using Payment.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Payment.Services
{
    public class DataUploadService : IDataUploadService
    {
        private readonly PaymentContext _paymentContext;

        public DataUploadService(PaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }
        public bool UploadData(IFormFile file)
        {
            try
            {
                decimal amount = 0;
                DateTime transDate;

                if (file is null)
                    return false;

                if (file.Length > 1048576)
                    return false;

                using (StreamReader sr = new StreamReader(file.OpenReadStream()))
                {
                    while (sr.Peek() > 0)
                    {
                        if (Path.GetExtension(file.FileName).ToLower() == ".csv")
                        {
                            string[] line = sr.ReadLine().Split(",");
                            if (line.Length != 5) return false;
                            else
                            {
                                foreach (string item in line)
                                    if (item.Trim() == "") return false;

                                if (!decimal.TryParse(line[1], out amount) || !DateTime.TryParse(line[3], new CultureInfo("fr-FR"), DateTimeStyles.AllowWhiteSpaces, out transDate))
                                    return false;

                                _paymentContext.Transactions.Add(new Transaction()
                                {
                                    InvoiceId = line[0],
                                    Amount = amount,
                                    Currency = line[2],
                                    TransactionDate = transDate,
                                    Status = line[4]
                                });

                            }
                        }
                        else if (Path.GetExtension(file.FileName).ToLower() == ".xml")
                        {
                            XDocument doc = XDocument.Parse(sr.ReadToEnd());
                            var transactions = doc.Descendants("Transactions").Elements("Transaction").Select(x => new
                            {
                                InvoiceId = x.Attribute("id").Value,
                                TransactionDate = x.Element("TransactionDate").Value,
                                Currency = x.Element("PaymentDetails").Element("CurrencyCode").Value,
                                Amount = x.Element("PaymentDetails").Element("Amount").Value,
                                Status = x.Element("Status").Value
                            });
                            foreach (var trans in transactions)
                            {
                                if (trans.InvoiceId.Length == 0 || !decimal.TryParse(trans.Amount, out amount) ||
                                    trans.Currency.Length == 0 || !DateTime.TryParse(trans.TransactionDate, new CultureInfo("fr-FR"), DateTimeStyles.AllowWhiteSpaces, out transDate) ||
                                    trans.Status.Length == 0)
                                    return false;

                                _paymentContext.Transactions.Add(new Transaction()
                                {
                                    InvoiceId = trans.InvoiceId,
                                    Amount = amount,
                                    Currency = trans.Currency,
                                    TransactionDate = transDate,
                                    Status = trans.Status
                                });
                            }
                        }
                        else return false;
                    }
                }
                return _paymentContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
