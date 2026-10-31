using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Classes
{
    internal enum AlertLevel
    {
        Info,
        Warning,
        Error
    }

    internal class AlertEventArgs(AlertLevel level, string message) : EventArgs
    {
        internal AlertLevel Level { get; } = level;
        internal string Message { get; } = message;
    }

    internal class AlertSystem
    {
        internal event EventHandler<AlertEventArgs>? AlertRaised;
        internal void RaiseAlert(AlertEventArgs e)
        {
            AlertRaised?.Invoke(this, e);
        }
    }
}
