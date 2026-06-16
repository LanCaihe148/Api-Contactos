using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Domain
{
    public class Company : ValueObject
    {

        public string? Name { get; private set; } 

        public string? CatchPhrase { get; private set; }

        public string? Bs { get; private set; }

        public Company(string? name, string? catchPhrase, string? bs)
        {
            Name = name;
            CatchPhrase = catchPhrase;
            Bs = bs;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return CatchPhrase;
            yield return Bs;
        }
    }
}
