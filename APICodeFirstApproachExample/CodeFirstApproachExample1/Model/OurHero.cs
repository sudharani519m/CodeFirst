using System.ComponentModel.DataAnnotations;

namespace CodeFirstApproachExample1.Model
{
    public class OurHero
    {


        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
    }
}
