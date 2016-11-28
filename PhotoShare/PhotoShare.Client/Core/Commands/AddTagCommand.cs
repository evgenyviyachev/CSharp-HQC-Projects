namespace PhotoShare.Client.Core.Commands
{
    using Attributes;
    using Data.Server;
    using Data.Services;
    using Models;
    using System;

    public class AddTagCommand : Command
    {
        [Inject]
        private TagService tagService;

        public AddTagCommand(string[] data) 
            : base(data)
        {
        }

        //AddTag <tag>
        public override string Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            string tag = this.Data[1].ValidateOrTransform();

            this.tagService.AddTag(new Tag
            {
                Name = tag
            });
            
            return tag + " was added sucessfully to database";
        }
    }
}
