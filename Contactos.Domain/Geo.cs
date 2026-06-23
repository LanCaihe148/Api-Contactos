
namespace Contactos.Domain
{
    public class Geo : ValueObject
    {
        public string Lat { get; private set; }

        public string Lng { get; private set; }

        private Geo() { }

        public Geo(string lat, string lng)
        {
            if (string.IsNullOrWhiteSpace(lat))
                throw new ArgumentException("Lat can`t be empty");

            if (string.IsNullOrWhiteSpace(lng))
                throw new ArgumentException("Long can`t be empty");


            Lat = lat;
            Lng = lng;

        }

         protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Lat;
            yield return Lng;

        }
    }
}
