using Microsoft.EntityFrameworkCore;

namespace ViewTransaction.Models
{
    public class ViewContext : DbContext
    {
        public ViewContext(DbContextOptions<ViewContext> options)
            :base(options)
        {

        }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ViewModel> ViewModels { get; set; }
    }
}
