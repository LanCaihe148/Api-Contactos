

namespace Contactos.Domain
{
    public class Email : ValueObject
    {
        public string Direction { get; private set; }

        private Email() { }

        public Email(string direction)
        {
            Direction = direction;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Direction;
        }
    }
}
