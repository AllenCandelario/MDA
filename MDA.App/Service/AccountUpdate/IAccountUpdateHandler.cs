using MDA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDA.App.Service.AccountUpdate
{
    public interface IAccountUpdateHandler
    {
         Task HandleAsync(IBAccountUpdate accountUpdate, CancellationToken cancellationToken);
    }
}
