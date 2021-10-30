using System.Collections.Generic;
using System.Threading.Tasks;
using ViewTransaction.Models;

namespace ViewTransaction.Services
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<ViewModel>> All();
        Task<IEnumerable<ViewModel>> ByCurrency(string cur);
        Task<IEnumerable<ViewModel>> ByDateRange(string dateFrom, string dateTo);
        Task<IEnumerable<ViewModel>> ByStatus(string status);
    }
}
