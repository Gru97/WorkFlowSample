using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFE.Console.DefineContractExample.Domain
{
    public class Contract
    {
        public string ContractNo { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthData { get; set; }
        public string NationalCode { get; set; }
        public string AccountNo { get; set; }
        public string TaxCode { get; set; }
        public State State { get; set; }
        public string Description { get; set; }
    }
    public enum State
    {
        Imported,
        Reject,
        WaitingForPSP,
        Accepted
    }
}
