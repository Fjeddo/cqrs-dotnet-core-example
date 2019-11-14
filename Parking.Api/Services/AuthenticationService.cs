using System;

namespace Parking.Api.Services
{
    public class AuthenticationService
    {
        public string UserId { get; }

        public AuthenticationService()
        {
            UserId = Guid.NewGuid().ToString();
        }
    }
}
