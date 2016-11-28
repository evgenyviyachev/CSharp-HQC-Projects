namespace PhotoShare.Test.Setup
{
    using Data.Contracts;
    using Models;

    public class CustomUnitOfWork : IUnitOfWork
    {
        private IRepository<Album> albums;
        private IRepository<AlbumRole> albumRoles;
        private IRepository<Picture> pictures;
        private IRepository<Tag> tags;
        private IRepository<Town> towns;
        private IRepository<User> users;
        
        public IRepository<Album> Albums
        {
            get
            {
                return this.albums ?? (this.albums = new CustomRepository<Album>());
            }
        }

        public IRepository<AlbumRole> AlbumRoles
        {
            get
            {
                return this.albumRoles ?? (this.albumRoles = new CustomRepository<AlbumRole>());
            }
        }

        public IRepository<Picture> Pictures
        {
            get
            {
                return this.pictures ?? (this.pictures = new CustomRepository<Picture>());
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                return this.tags ?? (this.tags = new CustomRepository<Tag>());
            }
        }

        public IRepository<Town> Towns
        {
            get
            {
                return this.towns ?? (this.towns = new CustomRepository<Town>());
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return this.users ?? (this.users = new CustomRepository<User>());
            }
        }

        public void Save()
        {            
        }
    }
}
