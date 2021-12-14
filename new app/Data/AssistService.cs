using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace new_app.Data
{
    public class AssistService
    {
        private User _user;

        public AssistService(User user)
        {
            _user = user;
        }

        public async Task UpdateLocalUserList(AssistFunc func)
        {
            _user.UserList.UpdateAssistFunc(func);
            await _user.SetLocalUserList(_user.UserList);
        }

        public async Task UpdateLocalUserListOrder(int id)
        {
            _user.UserList.UpdateListOrder(id);
            await _user.SetLocalUserList(_user.UserList);
        }

        public AssistFunc GetAssistFunc(int id)
            => _user.UserList.List.FirstOrDefault(a => a.Id == id);

        public async Task IncreaseUserVisitCount()
            => await _user.SetVisitCount(++_user.VisitCount);

        public async Task IncreaseAssistFuncLevel(AssistFunc func, bool isFast)
        {
            if (func.UserLevel == UserLevel.New)
                func.UserLevel = UserLevel.Beginner;
            else if (isFast && func.UserLevel == UserLevel.Beginner)
            {
                ++_user.FastCount[func.Id];
                await _user.SetFastCount(_user.FastCount);
                if (_user.FastCount[func.Id] >= 3)
                    func.UserLevel = UserLevel.Pro;
            }
            else if (!isFast)
            {
                _user.FastCount[func.Id] = 0;
                await _user.SetFastCount(_user.FastCount);
            }
            if (_user.FastCount[func.Id] == 0)
                func.UserLevel = UserLevel.Beginner;

            await UpdateLocalUserList(func);

            await UpdateLocalUserListOrder(func.Id);
        }

        public async Task UpdateAssistFuncLevel(AssistFunc func, UserLevel level)
        {
            func.UserLevel = level;
            if (level == UserLevel.Beginner)
            {
                _user.FastCount[func.Id] = 0;
                await _user.SetFastCount(_user.FastCount);
            }
            await UpdateLocalUserList(func);
        }

        public async Task<int> GetHintId()
        {
            if (_user.VisitCount > 6)
            {
                await _user.SetVisitCount(0);
                return _user.UserList.List.Last().Id;
            }
            else
                return -1;
        }
    }
}
