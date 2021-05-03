using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Client.Data
{
    public class AssistFuncList
    {
        [Required]
        public List<AssistFunc> List { get; set; }

        public AssistFuncList()
        {
            List = new List<AssistFunc>();

            List.Add(new AssistFunc()
            {
                Id = 0,
                UserLevel = UserLevel.New
            });
            List.Add(new AssistFunc()
            {
                Id = 1,
                UserLevel = UserLevel.New
            });
            List.Add(new AssistFunc()
            {
                Id = 2,
                UserLevel = UserLevel.New
            });
            List.Add(new AssistFunc()
            {
                Id = 3,
                UserLevel = UserLevel.New
            });
        }

        public void UpdateListOrder(int id)
        {
            for (int i = 0; i < List.Count - 1; i++)
            {
                if (List[i + 1] != null && List[i + 1].Id == id)
                {
                    var temp = List[i];
                    List[i] = List[i + 1];
                    List[i + 1] = temp;
                    break;
                }
            }
        }

        public void UpdateAssistFunc(AssistFunc func)
        {
            for (int i = 0; i < List.Count; i++)
                if (func.Id == List[i].Id)
                {
                    List[i] = func;
                    break;
                }
        }
    }
}