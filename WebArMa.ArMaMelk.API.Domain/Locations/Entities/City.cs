using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Locations.Entities
{
    public class City : EntityBase
    {
        private City()
        {
            Name = null!;
            Province = null!;
            Neighborhoods = [];
        }

        public string Name { get; private set; }
        public int ProvinceId { get; private set; }
        public Province Province { get; private set; }
        public virtual ICollection<Neighborhood> Neighborhoods { get; private set; }
    }
}
