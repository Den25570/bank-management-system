using API.Database;

namespace API.Models.Request
{
    public class Deposit
    {
        public int Id { get; set; }
        public int DepositTypeId { get; set; }
        public int DepositNumber { get; set; }
        public int CurrencyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClientId { get; set; }

        public int ContractTerm { get; set; }
        public int DepositAmount { get; set; }
        public int DepositPercent { get; set; }

        public Account MainAccount { get; set; }
        public Account PercentAccountId { get; set; }
    }
}
