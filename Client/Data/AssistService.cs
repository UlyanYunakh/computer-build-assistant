using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Data
{
    public class AssistService
    {
        public List<AssistFuncInfo> AssistFuncInfos { get; set; }
        public AssistFuncList UserList { get; set; }

        public AssistService()
        {
            AssistFuncInfos = new List<AssistFuncInfo>()
            {
                new AssistFuncInfo() { Id = 0, Name = "Функция 1", Description = "Описание 1", Img = "../img/computer.png"},
                new AssistFuncInfo() { Id = 1, Name = "Функция 2", Description = "Описание 2", Img = "../img/computer.png"},
                new AssistFuncInfo() { Id = 2, Name = "Функция 3", Description = "Описание 3", Img = "../img/computer.png"},
                new AssistFuncInfo() { Id = 3, Name = "Функция 4", Description = "Описание 4", Img = "../img/computer.png"},
            };
        }
    }
}
