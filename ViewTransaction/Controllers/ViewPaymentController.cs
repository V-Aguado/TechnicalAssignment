using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewTransaction.Models;
using ViewTransaction.Services;

namespace ViewTransaction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewPaymentController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public ViewPaymentController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<ViewModel>> GetTransactions(string currency, string status, string dateFrom, string dateTo)
        {
            if (currency != null)
                return await _transactionRepository.ByCurrency(currency);
            else if (status != null)
                return await _transactionRepository.ByStatus(status);
            else if (dateFrom != null && dateTo != null)
                return await _transactionRepository.ByDateRange(dateFrom, dateTo);
            else
                return await _transactionRepository.All();
        }
    }
}