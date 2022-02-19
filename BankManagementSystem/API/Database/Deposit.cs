using System.ComponentModel.DataAnnotations;

namespace API.Database
{
    public class Deposit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DepositTypeId { get; set; }
        [Required]
        public int DepositNumber { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int ContractTerm { get; set; }
        [Required]
        public int DepositAmount { get; set; }
        [Required]
        public int DepositPercent { get; set; }
        [Required]
        public int MainAccountId { get; set; }
        [Required]
        public int PercentAccountId { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public DateTime LastPercentEvaluationDate { get; set; }

        public DepositType DepositType { get; set; }
        public Currency Currency { get; set; }
        public Client Client { get; set; }
        public Account MainAccount { get; set; }
        public Account PercentAccount { get; set; }
    }
}
