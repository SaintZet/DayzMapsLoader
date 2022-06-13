using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestsHub.Domain.Services.ConsoleServices
{
    internal static class Documentation
    {
        internal static StringBuilder GetDocAboutCommands()
        {
            StringBuilder help = new StringBuilder("usage:");
            help.AppendLine("RequestsHub.exe [options] ");
            return help;
        }
    }
}