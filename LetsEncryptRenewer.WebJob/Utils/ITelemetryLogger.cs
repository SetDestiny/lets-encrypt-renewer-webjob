using System;

namespace LetsEncryptRenewer.WebJob.Utils
{
    public interface ITelemetryLogger
    {
        void TrackTrace(string message);
        void TrackException(Exception ex);
    }
}
