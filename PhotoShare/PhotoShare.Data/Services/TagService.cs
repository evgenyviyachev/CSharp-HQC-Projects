namespace PhotoShare.Data.Services
{
    using Contracts;
    using Models;
    using System;
    using System.Linq;

    public class TagService
    {
        private IUnitOfWork unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddTag(Tag tag)
        {
            this.unitOfWork.Tags.Add(tag);
            this.unitOfWork.Save();
        }
    }
}
