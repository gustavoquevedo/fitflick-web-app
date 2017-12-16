using FitFlickWebApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FitFlickWebApp.DAL
{
    public class AdvertisementFormContext : IdentityDbContext<ApplicationUser>
    {     

        public AdvertisementFormContext()
            : base("FitFlickWebAppContext", throwIfV1Schema: false)
        {
        }

        public static AdvertisementFormContext Create()
        {
            return new AdvertisementFormContext();
        }

        public DbSet<Advertisement> Advertisements { get; set; }
    }
}