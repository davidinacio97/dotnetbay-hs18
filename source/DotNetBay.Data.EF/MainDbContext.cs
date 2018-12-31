using System.Configuration;
using System.Data.Entity;
using DotNetBay.Data.Entity;

namespace DotNetBay.Data.EF
{
    public class MainDbContext : DbContext
    {
        public MainDbContext() : base("name=AuctionDbConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Auction> Auctions { get; set; }

        public virtual DbSet<Member> Members { get; set; }

        public virtual DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Add(new DateTime2Convention());

            modelBuilder.Entity<Auction>().HasRequired(a => a.Seller).WithMany(member => member.Auctions);
            modelBuilder.Entity<Auction>().HasMany(a => a.Bids).WithRequired(b => b.Auction);
        }
    }
}
