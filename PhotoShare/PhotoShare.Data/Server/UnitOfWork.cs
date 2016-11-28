namespace PhotoShare.Data.Server
{
    using Contracts;
    using Database;
    using Models;

    public class UnitOfWork : IUnitOfWork
    {
        private PhotoShareContext context;

        private IRepository<Album> albums;
        private IRepository<AlbumRole> albumRoles;
        private IRepository<Picture> pictures;
        private IRepository<Tag> tags;
        private IRepository<Town> towns;
        private IRepository<User> users;

        public UnitOfWork(PhotoShareContext context)
        {
            this.context = context;
        }

        public IRepository<Album> Albums
        {
            get
            {
                return this.albums ?? (this.albums = new Repository<Album>(this.context));
            }
        }

        public IRepository<AlbumRole> AlbumRoles
        {
            get
            {
                return this.albumRoles ?? (this.albumRoles = new Repository<AlbumRole>(this.context));
            }
        }

        public IRepository<Picture> Pictures
        {
            get
            {
                return this.pictures ?? (this.pictures = new Repository<Picture>(this.context));
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                return this.tags ?? (this.tags = new Repository<Tag>(this.context));
            }
        }

        public IRepository<Town> Towns
        {
            get
            {
                return this.towns ?? (this.towns = new Repository<Town>(this.context));
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return this.users ?? (this.users = new Repository<User>(this.context));
            }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
