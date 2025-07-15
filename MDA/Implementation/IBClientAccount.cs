using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;

namespace MDA.Implementation
{
    public partial class IBClient : EWrapper
    {
        void EWrapper.managedAccounts(string accountsList)
        {
            Console.WriteLine($"accountsList: {accountsList}");
        }
    }
}
