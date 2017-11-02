namespace EFSettings.Concrete
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using EFSettings.Entities;

    public partial class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("name=Setting")
        {
        }

        public virtual DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
