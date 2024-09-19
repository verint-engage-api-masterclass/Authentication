using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication1;

internal interface ISettings
{
    string APIKeyID { get; }
    string APIKey { get; }

    string Protocol { get; }
    string Hostname { get; }
}
