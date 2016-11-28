namespace PhotoShare.Data.Contracts
{
    using PhotoShare.Models;

    public interface IUnitOfWork
    {
        IRepository<Tag> Tags { get; }

        IRepository<Town> Towns { get; }

        IRepository<Album> Albums { get; }

        IRepository<AlbumRole> AlbumRoles { get; }

        IRepository<User> Users { get; }

        IRepository<Picture> Pictures { get; }

        void Save();
    }
}
