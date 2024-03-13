using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthSample.MocksInterfaces
{
    public interface ISessionServices
    {
        bool VerifySessionExpiration(DateTime sessionExpiration);
    }
}
