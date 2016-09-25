using Manager.Files;
using Manager.User;

namespace Manager
{
    public class ActorGroup
    {
        private static ActorGroup instance = new ActorGroup();

        internal static ActorGroup Instance { get { return instance; } }

        private ActorGroup()
        {
            UserManager = new UserManager();
            FileManager = new FileManager();
        }

        internal UserManager UserManager { get; private set; }

        internal FileManager FileManager { get; private set; }
    }
}
