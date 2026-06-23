

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
