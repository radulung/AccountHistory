using System;

namespace AccountHistory.Common.Domain.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public DateTime Date { get; set; }
        public string Operation { get; set; }
    }
}
