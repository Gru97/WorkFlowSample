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

    public class TaxService : ITaxService
    {
        public string Get(string nationalCode)
        {
            return "";
        }
    }

}
