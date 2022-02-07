using System.ComponentModel.DataAnnotations;

namespace API.Database
{
    public class AccountType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AccountSubclassId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public char Charateristic { get; set; }

        public AccountSubclass AccountSubclass { get; set; }
    }
}
