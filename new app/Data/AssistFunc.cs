using System.ComponentModel.DataAnnotations;

namespace new_app.Data
{
    public class AssistFunc
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public UserLevel UserLevel { get; set; }
    }
}