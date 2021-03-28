using System.ComponentModel.DataAnnotations;

namespace Client.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public AssistFuncList UserList { get; set; }

        [Required]
        public int VisitCount { get; set; }
    }
}