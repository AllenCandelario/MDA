using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDA.Config
{
    public sealed class IBKRConfigOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public int ClientId { get; set; }
    }
}
