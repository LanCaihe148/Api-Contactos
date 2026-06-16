using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Domain
{
    public class Phone : ValueObject
    {
        public string Number { get; private set; }

        private Phone() { }
        public Phone(string number)
        {
            Number = number;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
