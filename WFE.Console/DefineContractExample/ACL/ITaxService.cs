using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFE.Console.DefineContractExample.ACL
{
    public interface ITaxService
    {
        string Get(string nationalCode);
    }

}
