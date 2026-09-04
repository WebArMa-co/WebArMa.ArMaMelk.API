using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.RealStates.Entities
{
    public class City : EntityBase
    {
        private readonly List<Neighborhood> _neighborhoods = [];

        private City()
        {
            Name = null!;
            Province = null!;
        }

        public string Name { get; private set; }
        public int ProvinceId { get; private set; }
        public Province Province { get; private set; }
        public IReadOnlyCollection<Neighborhood> Neighborhoods => _neighborhoods.AsReadOnly();
    }
}
