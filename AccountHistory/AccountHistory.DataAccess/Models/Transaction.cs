using System;

namespace AccountHistory.DataAccess.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public DateTime Date { get; set; }

        public int OperationId { get; set; }
        public Operation Operation { get; set; }
    }
}
