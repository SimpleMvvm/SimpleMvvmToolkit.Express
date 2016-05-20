namespace SimpleMvvmToolkit.Express
{
    /// <summary>
    /// Implemented by MessageBusProxy so that it can send notifications in a leak-proof manner.
    /// </summary>
    public interface INotifyable
    {
        /// <summary>
        /// Notify registered callbacks.
        /// </summary>
        /// <param name="token">String identifying a message token</param>
        /// <param name="sender">Sender of notification</param>
        /// <param name="e">Event args carrying message</param>
        void Notify(string token, object sender, NotificationEventArgs e);
    }
}
