using Common.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Manager.User
{
    class UserManager
    {
        private const int UpdateTimeSpan = 60;

        private Dictionary<User, int> UserList { get; set; }

        internal UserManager()
        {
            UserList = new Dictionary<User, int>();
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(UpdateTimeSpan * 1000);
                    UpdateAllUser();
                }

            }).Start();
        }

        private void UpdateAllUser()
        {
            lock (UserList)
            {
                foreach (User user in UserList.Keys)
                {
                    lock (user)
                    {
                        user.Update();
                    }
                }
            }
        }

        internal bool Signin(string userName, string password)
        {
            string passwordHash = password.MD5();
            string savedHash = Settings.GetStringValue(string.Format("{0}.{1}", userName, "password"));
            if (passwordHash != null && passwordHash == savedHash)
            {
                User user = FindUserByName(userName);

                lock (UserList)
                {
                    if (user == null)
                    {
                        user = new User(userName);
                        UserList.Add(user, 0);
                    }
                    UserList[user]++;
                }
                return true;
            }
            return false;
        }

        internal void SignOut(string userName)
        {
            User user = FindUserByName(userName);
            if (user != null)
            {
                lock (UserList)
                {
                    if (--UserList[user] == 0)
                    {
                        UserList.Remove(user);
                    }
                }
            }
        }

        internal string GetToken(string userName)
        {
            User user = FindUserByName(userName);
            if (user != null)
            {
                return user.CurrentToken.ToString();
            }
            return null;
        }

        internal bool Check(string userName, string token)
        {
            User user = FindUserByName(userName);
            if (user != null)
            {
                return user.Check(token);
            }
            return false;
        }

        private User FindUserByName(string userName)
        {
            return UserList.Keys.ToList().Find(x => x.UserName == userName);
        }
    }
}
