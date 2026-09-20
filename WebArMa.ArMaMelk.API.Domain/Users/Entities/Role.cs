using WebArMa.ArMaMelk.API.Domain._Shared.Entities;

namespace WebArMa.ArMaMelk.API.Domain.Users.Entities
{
    public class Role : EntityBase
    {
        public static Role Create(string name, List<long> accessCodes)
        {
            return new Role
            {
                Name = name,
                AccessCodes = accessCodes
            };
        }
        public void Update(string name, List<long> accessCodes)
        {
            Name = name;
            AccessCodes = accessCodes;
        }

        public Role()
        {
            Name = string.Empty;
            AccessCodes = [];
            Users = [];
        }

        public string Name { get; private set; }
        public List<long> AccessCodes { get; private set; }
        public virtual ICollection<User> Users { get; private set; }
    }
}
