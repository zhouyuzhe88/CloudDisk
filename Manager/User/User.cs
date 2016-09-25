using System;

namespace Manager.User
{
    class User
    {
        private Guid PreviousToken { get; set; }

        internal Guid CurrentToken { get; private set; }

        internal string UserName { get; set; }

        internal User(string userName)
        {
            UserName = userName;
            CurrentToken = Guid.NewGuid();
        }

        internal void Update()
        {
            lock (this)
            {
                PreviousToken = CurrentToken;
                CurrentToken = Guid.NewGuid();
                Console.WriteLine("Update Token {0}\t to \t{1}", PreviousToken, CurrentToken);
            }
        }

        internal bool Check(string token)
        {
            lock (this)
            {
                if (PreviousToken != null && PreviousToken.ToString() == token)
                {
                    return true;
                }
                return CurrentToken.ToString() == token;
            }
        }
    }
}
