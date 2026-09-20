using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Locations.Entities
{
    public class County : EntityBase
    {
        private County()
        {
            Name = string.Empty;
            City = null!;
            Villages = [];
        }

        public string Name { get; private set; }
        public int CityId { get; private set; }
        public virtual City City { get; private set; }
        public virtual ICollection<Village> Villages { get; set; }
    }
}
