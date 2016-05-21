namespace SimpleMvvmToolkit.Express
{
    /// <summary>
    /// Facilitates communication among view-models.
    /// To prevent memory leaks weak references are used.
    /// </summary>
    public class MessageBus
    {
        private static MessageBus _instance;
        private static readonly object StaticLock = new object();

        /// <summary>
        /// Singleton of MessageBus.
        /// </summary>
        public static MessageBus Default
        {
            get
            {
                if (_instance == null)
                {
                    CreateInstance();
                }
                return _instance;
            }
        }

        private static void CreateInstance()
        {
            lock (StaticLock)
            {
                if (_instance == null)
                {
                    _instance = new MessageBus();
                }
            }
        }
    }
}
