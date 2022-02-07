using System.ComponentModel.DataAnnotations;

namespace API.Database
{
    public class AccountSubclass
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AccountClassId { get; set; }
        [Required]
        public string Description { get; set; }

        public AccountClass AccountClass { get; set; }
    }
}
