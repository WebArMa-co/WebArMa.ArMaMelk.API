using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Locations.Entities
{
    public class Village : EntityBase
    {
        public Village()
        {
            Name = string.Empty;
            County = null!;
        }

        public string Name { get; private set; }
        public int CountyId { get; private set; }
        public virtual County County { get; private set; }
    }
}
