using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewTransaction.Models;

namespace ViewTransaction.Services
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ViewContext _context;

        public TransactionRepository(ViewContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ViewModel>> All()
        {
            return await _context.Transactions
            .Select(x => new ViewModel
            {
                Id = x.Id,
                InvoiceId = x.InvoiceId,
                Payment = x.Amount + " " + x.Currency,
                Status = (x.Status == "Finished" ? "D" :
                          x.Status == "Done" ? "D" :
                          x.Status == "Rejected" ? "R" :
                          x.Status == "Failed" ? "R" : "A")
            }).ToListAsync();

        }
        public async Task<IEnumerable<ViewModel>> ByCurrency(string cur)
        {
            return await _context.Transactions.Where(p => p.Currency == cur)
            .Select(x => new ViewModel
            {
                Id = x.Id,
                InvoiceId = x.InvoiceId,
                Payment = x.Amount + " " + x.Currency,
                Status = (x.Status == "Finished" ? "D" :
                          x.Status == "Done" ? "D" :
                          x.Status == "Rejected" ? "R" :
                          x.Status == "Failed" ? "R" : "A")
            }).ToListAsync();


        }

        public async Task<IEnumerable<ViewModel>> ByDateRange(string dateFrom, string dateTo)
        {
            return await _context.Transactions.Where(p => p.TransactionDate >= Convert.ToDateTime(dateFrom) && p.TransactionDate <= Convert.ToDateTime(dateTo))
            .Select(x => new ViewModel
            {
                Id = x.Id,
                InvoiceId = x.InvoiceId,
                Payment = x.Amount + " " + x.Currency,
                Status = (x.Status == "Finished" ? "D" :
                          x.Status == "Done" ? "D" :
                          x.Status == "Rejected" ? "R" :
                          x.Status == "Failed" ? "R" : "A")
            }).ToListAsync();
        }

        public async Task<IEnumerable<ViewModel>> ByStatus(string status)
        {
            return await _context.Transactions.Where(p => p.Status == status)
            .Select(x => new ViewModel
            {
                Id = x.Id,
                InvoiceId = x.InvoiceId,
                Payment = x.Amount + " " + x.Currency,
                Status = (x.Status == "Finished" ? "D" :
                          x.Status == "Done" ? "D" :
                          x.Status == "Rejected" ? "R" :
                          x.Status == "Failed" ? "R" : "A")
            }).ToListAsync();
        }
    }
}
