namespace PhotoShare.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Data.Contracts;
    using System.Linq;
    using Data.Services;
    using Setup;

    [TestClass]
    public class AddTagTest
    {
        private IUnitOfWork unitOfWork;
        private Tag tag;
        private TagService tagService;

        [TestInitialize]
        public void Initialize()
        {
            Tag tag = new Tag()
            {
                Name = "#summer"
            };

            this.tag = tag;
            this.unitOfWork = new CustomUnitOfWork();
            this.tagService = new TagService(unitOfWork);
        }

        [TestMethod]
        public void ShouldAddTag()
        {
            var countOfTagsBeforeAdd = this.unitOfWork.Tags.FindAll(t => true).Count();
            this.tagService.AddTag(this.tag);
            var countOfTagsAfterAdd = this.unitOfWork.Tags.FindAll(t => true).Count();

            Assert.AreEqual(countOfTagsBeforeAdd + 1, countOfTagsAfterAdd);
        }
    }
}
