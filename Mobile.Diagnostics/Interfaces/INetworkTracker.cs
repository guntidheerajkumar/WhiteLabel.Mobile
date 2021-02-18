using System;
using System.Net.Http;

namespace Mobile.Diagnostics.Interfaces
{
    public interface INetworkTracker
    {
        void TrackResponse(HttpResponseMessage response);

        void TrackResponse(Exception exception);
    }
}