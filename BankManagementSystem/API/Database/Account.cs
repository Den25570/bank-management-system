using System.ComponentModel.DataAnnotations;

namespace API.Database
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int IndividualNumber { get; set; }
        [Required]
        public int AccountTypeId { get; set; }
        [Required]
        public int AccountActivityId { get; set; }
        [Required]
        public decimal Debit { get; set; }
        [Required]
        public decimal Credit { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        public int CurrencyId { get; set; }

        public int? OwnerId { get; set; }

        public Client Owner { get; set; }
        public AccountActivity AccountActivity { get; set; }
        public AccountType AccountType { get; set; }
    }
}
