using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class OutcomeNotFoundException : Exception
    {
        public OutcomeNotFoundException(string message) : base(message) { }
    }
}
