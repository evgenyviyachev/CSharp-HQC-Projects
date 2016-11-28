namespace PhotoShare.Test
{
    using Data.Contracts;
    using Data.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Setup;
    using System.Linq;
    using System;

    [TestClass]
    public class AddTagToTest
    {
        private IUnitOfWork unitOfWork;
        private Tag tag;
        private AlbumService albumService;

        [TestInitialize]
        public void Initialize()
        {
            Tag tag = new Tag()
            {
                Name = "#summer"
            };

            this.tag = tag;
            this.unitOfWork = new CustomUnitOfWork();
            this.unitOfWork.Albums.Add(new Album() { IsPublic = true, Name = "Album One" });
            this.unitOfWork.Albums.Add(new Album() { IsPublic = true, Name = "Album Two" });
            this.unitOfWork.Albums.Add(new Album() { IsPublic = true, Name = "Album Two" });
            this.albumService = new AlbumService(unitOfWork);
        }

        [TestMethod]
        public void ShouldAddTagToAnExistingAlbum()
        {
            var album = this.unitOfWork.Albums.FindById(1);
            var countOfTagsBeforeAdd = album.Tags.Count();
            this.albumService.AddTagToAlbum(this.tag, "Album One");
            var countOfTagsAfterAdd = album.Tags.Count();

            Assert.AreEqual(countOfTagsBeforeAdd + 1, countOfTagsAfterAdd);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ShouldThrowIfThereIsNoSuchAlbum()
        {
            this.albumService.AddTagToAlbum(this.tag, "Album Three");
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ShouldThrowIfThereAreMoreThanOneAlbumsWithThisName()
        {
            this.albumService.AddTagToAlbum(this.tag, "Album Two");
        }
    }
}
