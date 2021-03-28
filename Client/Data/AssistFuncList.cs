using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Client.Data
{
    public class AssistFuncList
    {
        [Required]
        public List<AssistFunc> List { get; set; }

        public void UpdateList()
        {

        }
    }
}