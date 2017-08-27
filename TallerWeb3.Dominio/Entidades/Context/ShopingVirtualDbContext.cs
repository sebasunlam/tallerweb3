using Microsoft.AspNet.Identity.EntityFramework;

namespace Nedo.ShopingVirtual.Dominio.Entidades.Context
{
    public class ShopingVirtualDbContext  : IdentityDbContext<ApplicationUser>
    {
        public ShopingVirtualDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ShopingVirtualDbContext Create()
        {
            return new ShopingVirtualDbContext();
        }
    }
}