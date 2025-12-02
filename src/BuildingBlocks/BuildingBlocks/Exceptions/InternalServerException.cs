using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException(string mensagem) : base(mensagem) { }

        public InternalServerException(string mensagem, string details)
            : base(mensagem) 
        {
            Details = details;
        }

        public string Details { get; }
    }
}
