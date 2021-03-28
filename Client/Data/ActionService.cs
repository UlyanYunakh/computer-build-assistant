using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Data
{
    public class AssistService
    {
        public List<AssistFunc> AssistFuncs { get; set; }

        public AssistService()
        {
            AssistFuncs = new List<AssistFunc>()
            {
                new AssistFunc() { Name = "Функция 1", Description = "Описание 1", Img = "../img/computer.png"},
                new AssistFunc() { Name = "Функция 2", Description = "Описание 2", Img = "../img/computer.png"},
                new AssistFunc() { Name = "Функция 3", Description = "Описание 3", Img = "../img/computer.png"},
                new AssistFunc() { Name = "Функция 4", Description = "Описание 4", Img = "../img/computer.png"},
            };
        }
    }
}
