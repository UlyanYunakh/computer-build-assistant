using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace new_app.Data
{
    public class User
    {
        private Blazored.LocalStorage.ILocalStorageService _localStorage;
        public AssistFuncList UserList { get; set; }
        public List<int> FastCount { get; set; }
        public int? VisitCount { get; set; }
        public List<Selection> SavedCompanents { get; set; }
        public List<Selection> CompareComponents { get; set; }

        public User(Blazored.LocalStorage.ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            InitProperties();
        }

        private async void InitProperties()
        {
            await InitUserList();
            await InitVisitCount();
            await InitFastCount();
            await InitSavedComponents();
            await InitCompareComponents();
        }

        private async Task InitCompareComponents()
        {
            CompareComponents = await _localStorage.GetItemAsync<List<Selection>>("userCompareComponents");
        
            if(CompareComponents == null)
            {
                CompareComponents = new List<Selection>(2);
                CompareComponents.Add(null);
                CompareComponents.Add(null);
                await SetCompareComponents(CompareComponents);
            }
        }

        public async Task SetCompareComponents(List<Selection> components)
        {
            CompareComponents = components;
            await _localStorage.SetItemAsync("userCompareComponents", components);
        }

        private async Task InitSavedComponents()
        {
            SavedCompanents = await _localStorage.GetItemAsync<List<Selection>>("userSavedComponents");

            if (SavedCompanents == null)
            {
                SavedCompanents = new List<Selection>();
                await SetSavedComponents(SavedCompanents);
            }
        }

        public async Task SetSavedComponents(List<Selection> components)
        {
            SavedCompanents = components;
            await _localStorage.SetItemAsync("userSavedComponents", components);
        }

        private async Task InitFastCount()
        {
            FastCount = await _localStorage.GetItemAsync<List<int>>("userFastCount");

            if (FastCount == null)
            {
                FastCount = new List<int>();
                foreach (AssistFunc assistFunc in UserList.List)
                    FastCount.Add(0);
                await SetFastCount(FastCount);
            }
        }

        public async Task SetFastCount(List<int> fastCount)
        {
            FastCount = fastCount;
            await _localStorage.SetItemAsync("userFastCount", fastCount);
        }

        private async Task InitVisitCount()
        {
            VisitCount = await _localStorage.GetItemAsync<int?>("userVisitCount");

            if (VisitCount == null)
            {
                VisitCount = 0;
                await SetVisitCount(VisitCount);
            }
        }

        public async Task SetVisitCount(int? visitCount)
        {
            VisitCount = visitCount;
            await _localStorage.SetItemAsync("userVisitCount", visitCount);
        }

        private async Task InitUserList()
        {
            UserList = await _localStorage.GetItemAsync<AssistFuncList>("userList");

            if (UserList == null)
                await SetLocalUserList(new AssistFuncList());
        }

        public async Task SetLocalUserList(AssistFuncList userListLocal)
        {
            UserList = userListLocal;
            await _localStorage.SetItemAsync("userList", userListLocal);
        }
    }
}