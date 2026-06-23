

namespace Contactos.Domain
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }

        public string Suite { get; private set; }

        public string City { get; private set; }

        public string ZipCode { get; private set; }

        public Geo? Geo { get; private set; }


        private Address() { }
        public Address(string street, string suite, string city, string zipCode, Geo? geo)
        {
            Street = street;
            Suite = suite;
            City = city;
            ZipCode = zipCode;
            Geo = geo;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return Suite;
            yield return City;
            yield return ZipCode;
            yield return Geo;
        }

    }
}
