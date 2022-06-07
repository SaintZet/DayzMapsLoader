using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestsHub.Domain.Contracts
{
    internal interface IMap
    {
        NameMap Name { get; }
        int Height { get; set; }
        int Width { get; set; }
        string Version { get; set; }
    }
}