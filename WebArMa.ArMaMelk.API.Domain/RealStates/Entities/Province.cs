using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.RealStates.Entities
{
    public sealed class Province : EntityBase
    {
        private readonly List<City> _cities = [];

        private Province()
        {
            Name = null!;
        }

        public string Name { get; private set; }
        public IReadOnlyCollection<City> Cities => _cities.AsReadOnly();
    }
}
