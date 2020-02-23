using System;

namespace TrailTracker.API.Models
{
    public class ServiceError
    {
        public string Description { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }
    }
}
