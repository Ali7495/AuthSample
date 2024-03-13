using AuthSample.MocksInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthSample.MockServices
{
    public class SessionServices : ISessionServices
    {
        public bool VerifySessionExpiration(DateTime sessionExpiration)
        {
            return sessionExpiration > DateTime.Now;
        }
    }
}
