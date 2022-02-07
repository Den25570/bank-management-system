using System.ComponentModel.DataAnnotations;

namespace API.Database
{
    public class AccountClass
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
