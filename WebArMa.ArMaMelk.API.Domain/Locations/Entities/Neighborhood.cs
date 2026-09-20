using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Locations.Entities
{
    public class Neighborhood : EntityBase
    {
        private Neighborhood()
        {
            Name = null!;
            City = null!;
        }

        public string Name { get; private set; }
        public int CityId { get; private set; }
        public virtual City City { get; private set; }
    }
}
