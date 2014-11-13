namespace VirtualPiano.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using VirtualPiano.Data.Common.Models;
    using VirtualPiano.Data.Common.Repository;
    using VirtualPiano.Models;

    public class VirtualPianoData : IVirtualPianoData
    {
        private readonly DbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public VirtualPianoData(DbContext context)
        {
            this.context = context;
        }

        public DbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IDeletableEntityRepository<MusicAd> MusicAds
        {
            get { return this.GetDeletableEntityRepository<MusicAd>(); }
        }

        public IDeletableEntityRepository<MusicSheet> MusicSheets
        {
            get { return this.GetDeletableEntityRepository<MusicSheet>(); }
        }

        public IDeletableEntityRepository<Teacher> Teachers
        {
            get { return this.GetDeletableEntityRepository<Teacher>(); }
        }

        public IRepository<MusicSheetsCategory> MusicSheetsCategories
        {
            get { return this.GetRepository<MusicSheetsCategory>(); }
        }

        public IRepository<Rating> Ratings
        {
            get { return this.GetRepository<Rating>(); }
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        public IRepository<Artist> Artists
        {
            get { return this.GetRepository<Artist>(); }
        }

        public IRepository<AdCategory> AdCategories
        {
            get { return this.GetRepository<AdCategory>(); }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
