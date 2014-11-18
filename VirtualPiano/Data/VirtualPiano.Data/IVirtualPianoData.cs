namespace VirtualPiano.Data
{
    using System.Data.Entity;

    using VirtualPiano.Data.Common.Repository;
    using VirtualPiano.Models;

    public interface IVirtualPianoData
    {
        DbContext Context { get; }

        IDeletableEntityRepository<MusicAd> MusicAds { get; }

        IDeletableEntityRepository<MusicSheet> MusicSheets { get; }

        IDeletableEntityRepository<Teacher> Teachers { get; }

        IRepository<MusicSheetsCategory> MusicSheetsCategories { get; }

        IRepository<Rating> Ratings { get; }

        IRepository<ApplicationUser> Users { get; }

        IRepository<Artist> Artists { get; }

        IRepository<AdCategory> AdCategories { get; }

        int SaveChanges();
    }
}
