using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client.Data
{
    public class AssistService
    {
        private Blazored.LocalStorage.ILocalStorageService _localStorage;

        public AssistService(Blazored.LocalStorage.ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        
        public async Task UpdateUserInfo(int id)
        {
            var userListLocal = await _localStorage.GetItemAsync<AssistFuncList>("userList");

            userListLocal.UpdateList(id);

            await _localStorage.SetItemAsync("userList", userListLocal);
        }

        public async Task<int> FuncHint()
        {
            var userVisitCount = await _localStorage.GetItemAsync<int?>("userVisitCount");

            if (userVisitCount >= 4)
            {
                await _localStorage.SetItemAsync("userVisitCount", 0);
                var userListLocal = await _localStorage.GetItemAsync<AssistFuncList>("userList");
                var func = userListLocal.List.Last();
                return func.Id;
            }
            else
            {
                return -1;
            }
        }
    }
}
