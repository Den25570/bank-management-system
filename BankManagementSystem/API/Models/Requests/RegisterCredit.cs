using System.ComponentModel.DataAnnotations;

namespace API.Models.Requests
{
    public class RegisterDeposit
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int DepositTypeId { get; set;}
        [Required]
        public int DepositAmount { get; set;}
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

    }
}
