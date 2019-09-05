using System.Collections.Generic;

namespace AccountHistory.DataAccess.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
