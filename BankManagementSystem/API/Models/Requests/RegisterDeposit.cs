using System.ComponentModel.DataAnnotations;

namespace API.Models.Requests
{
    public class RegisterCredit
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CreditTypeId { get; set;}
        [Required]
        public int CreditCode { get; set; }
        [Required]
        public int CreditAmount { get; set;}
        [Required]
        public int CurrencyId { get; set;}
        [Required]
        public decimal Percent { get; set;}
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int ContractTerm { get; set; }
        [Required]
        public bool PrematureRepayment { get; set; }

    }
}
