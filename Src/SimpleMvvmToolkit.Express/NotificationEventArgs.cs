using System;

namespace SimpleMvvmToolkit.Express
{
    /*
     * There are three levels of notifications:
     * NotificationEventArgs - with or without a string message
     * NotificationEventArgs<TOutgoing> - with outgoing data
     * NotificationEventArgs<TOutgoing, TIncoming> - with completion callback,
     * which can be <object> and null if no data is returned
     */

    /// <summary>
    /// Notification with or without a string message
    /// </summary>
    public class NotificationEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationEventArgs() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">String-based message.</param>
        public NotificationEventArgs(string message)
        {
            Message = message;
        }

        /// <summary>
        /// String-based message.
        /// </summary>
        public string Message { get; protected set; }
    }

    /// <summary>
    /// Notification with outgoing data
    /// </summary>
    /// <typeparam name="TOutgoing">Outgoing data type</typeparam>
    public class NotificationEventArgs<TOutgoing> : NotificationEventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationEventArgs() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">String-based message.</param>
        public NotificationEventArgs(string message) : base(message) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">String-based message.</param>
        /// <param name="data">Outgoing data.</param>
        public NotificationEventArgs(string message, TOutgoing data)
            : this(message)
        {
            Data = data;
        }

        /// <summary>
        /// Outgoing data.
        /// </summary>
        public TOutgoing Data { get; protected set; }
    }

    /// <summary>
    /// Notification with outgoing and incoming data
    /// </summary>
    /// <typeparam name="TOutgoing">Outgoing data type</typeparam>
    /// <typeparam name="TIncoming">Incoming data type</typeparam>
    public class NotificationEventArgs<TOutgoing, TIncoming> : NotificationEventArgs<TOutgoing>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificationEventArgs() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">String-based message.</param>
        public NotificationEventArgs(string message) : base(message) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">String-based message.</param>
        /// <param name="data">Outgoing data.</param>
        public NotificationEventArgs(string message, TOutgoing data)
            : base(message, data) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">String-based message.</param>
        /// <param name="data">Outgoing data.</param>
        /// <param name="completed">Callback to return incoming data.</param>
        public NotificationEventArgs(string message, TOutgoing data, Action<TIncoming> completed)
            : this(message, data)
        {
            Completed = completed;
        }

        /// <summary>
        /// Callback to return incoming data.
        /// </summary>
        public Action<TIncoming> Completed { get; protected set; }
    }
}
