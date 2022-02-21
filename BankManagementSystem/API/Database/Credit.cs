using System.ComponentModel.DataAnnotations;

namespace API.Database
{
    public class Credit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CreditTypeId { get; set; }
        [Required]
        public int CreditNumber { get; set; }
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
        public int CreditAmount { get; set; }
        [Required]
        public decimal CreditPercent { get; set; }
        [Required]
        public int MainAccountId { get; set; }
        [Required]
        public int PercentAccountId { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public bool PrematureRepayment { get; set; }
        [Required]
        public decimal PayedToDate { get; set; } = 0;
        [Required]
        public DateTime LastPercentEvaluationDate { get; set; }

        public int DaysPassed { get; set; } = 0;

        public CreditType CreditType { get; set; }
        public Currency Currency { get; set; }
        public Client Client { get; set; }
        public Account MainAccount { get; set; }
        public Account PercentAccount { get; set; }
    }
}
