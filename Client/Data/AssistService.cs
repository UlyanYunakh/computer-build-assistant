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

        public async Task<AssistFuncList> GetLocalUserList()
        {
            var userListLocal = await _localStorage.GetItemAsync<AssistFuncList>("userList");

            if (userListLocal == null)
            {
                userListLocal = new AssistFuncList();
                await SetLocalUserList(userListLocal);
            }

            return userListLocal;
        }

        private async Task SetLocalUserList(AssistFuncList userListLocal)
            => await _localStorage.SetItemAsync("userList", userListLocal);

        public async Task UpdateLocalUserList(AssistFunc func)
        {
            var userList = await GetLocalUserList();

            userList.UpdateAssistFunc(func);

            await SetLocalUserList(userList);
        }

        public async Task UpdateLocalUserListOrder(int id)
        {
            var userList = await GetLocalUserList();

            userList.UpdateListOrder(id);

            await SetLocalUserList(userList);
        }

        public async Task<AssistFunc> GetAssistFunc(int id)
        {
            var userList = await GetLocalUserList();
            return userList.List.FirstOrDefault(a => a.Id == id);
        }

        private async Task<int> GetFuncFastCount(int id)
            => await _localStorage.GetItemAsync<int>($"func{id}FastCount");

        private async Task<int> SetFuncFastCount(int id)
        {
            var count = await _localStorage.GetItemAsync<int>($"func{id}FastCount");

            if (count == 0)
                await _localStorage.SetItemAsync($"func{id}FastCount", 1);
            else
                await _localStorage.SetItemAsync($"func{id}FastCount", ++count);

            return count;
        }

        private async Task SetFuncFastCountToZero(int id)
            => await _localStorage.SetItemAsync($"func{id}FastCount", 0);

        public async Task SetUserVisitCount()
        {
            var userVisitCount = await _localStorage.GetItemAsync<int>("userVisitCount");

            if (userVisitCount == 0)
            {
                await _localStorage.SetItemAsync("userVisitCount", 1);
            }
            else
                await _localStorage.SetItemAsync("userVisitCount", ++userVisitCount);
        }

        public async Task UpdateAssistFunc(AssistFunc func, bool isFast)
        {
            if (func.UserLevel == UserLevel.New)
            {
                func.UserLevel = UserLevel.Beginner;
            }
            else if (isFast)
            {
                if (func.UserLevel == UserLevel.Beginner)
                    if (await SetFuncFastCount(func.Id) >= 3)
                        func.UserLevel = UserLevel.Pro;
            }
            else if (!isFast)
            {
                await SetFuncFastCountToZero(func.Id);
            }

            if (await GetFuncFastCount(func.Id) == 0)
                func.UserLevel = UserLevel.Beginner;

            await UpdateLocalUserList(func);

            await UpdateLocalUserListOrder(func.Id);
        }

        public async Task UpdateAssistFuncLevel(AssistFunc func, UserLevel level)
        {
            func.UserLevel = level;

            await UpdateLocalUserList(func);
        }

        public async Task<int> FuncHint()
        {
            var userVisitCount = await _localStorage.GetItemAsync<int>("userVisitCount");

            if (userVisitCount >= 6)
            {
                await _localStorage.SetItemAsync("userVisitCount", 0);
                var userListLocal = await GetLocalUserList();
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
