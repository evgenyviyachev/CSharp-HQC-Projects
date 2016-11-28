namespace PhotoShare.Client
{
    using Core;
    using Data.Server;
    using Data.Services;
    using Interfaces;
    using IO;

    public class Application
    {
        public static void Main()
        {
            var context = new PhotoShareContext();

            var unit = new UnitOfWork(context);
            var userService = new UserService(unit);
            var townService = new TownService(unit);
            var albumService = new AlbumService(unit);
            var albumRoleService = new AlbumRoleService(unit);
            var tagService = new TagService(unit);
            var pictureService = new PictureService(unit);

            ICommandDispatcher commandDispatcher = new CommandDispatcher(userService, albumService, pictureService, tagService, albumRoleService, townService);
            
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IRunnable engine = new Engine(commandDispatcher, reader, writer);

            engine.Run();
        }
    }
}
