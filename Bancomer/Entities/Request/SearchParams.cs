using System;

namespace Bancomer.Entities.Request
{
    public class SearchParams
    {
        public SearchParams() {
            Offset = 0;
            Limit = 10;
        }

        public int Offset { get; set; }

        public int Limit { get; set; }

        public Decimal Amount { get; set; }

        public Decimal AmountGte { get; set; }

        public Decimal AmountLte { get; set; }

        public DateTime Creation { get; set; }

        public DateTime CreationGte { get; set; }

        public DateTime CreationLte { get; set; }

		public String OrderId { set; get; }

		public TransactionStatus? Status { set; get; }

        public void Between(DateTime start, DateTime end)
        {
            CreationGte = start;
            CreationLte = end;
        }

    }
}
