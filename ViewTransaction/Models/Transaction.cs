using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewTransaction.Models
{
    [Table("tbl_Transactions")]
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        public string InvoiceId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
