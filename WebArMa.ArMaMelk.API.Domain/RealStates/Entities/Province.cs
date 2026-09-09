using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.RealStates.Entities
{
    public class Province : EntityBase
    {
        private Province()
        {
            Name = null!;
            Cities = [];
        }

        public string Name { get; private set; }
        public virtual IReadOnlyCollection<City> Cities { get; private set; }
    }
}
