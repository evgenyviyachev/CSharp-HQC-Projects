namespace PhotoShare.Client.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using Commands;
    using Data.Server;
    using Interfaces;
    using System.Data.Entity;
    using Models;
    using Data.Services;

    public class CommandDispatcher : ICommandDispatcher
    {
        private const string CommandsNamespacePath = "PhotoShare.Client.Core.Commands.";
        private const string CommandSuffix = "Command";
        
        private UserService userService;
        private AlbumService albumService;
        private PictureService pictureService;
        private TagService tagService;
        private AlbumRoleService albumRoleService;
        private TownService townService;

        public CommandDispatcher(
            UserService userService,
            AlbumService albumService,
            PictureService pictureService,
            TagService tagService,
            AlbumRoleService albumRoleService,
            TownService townService)
        {
            this.userService = userService;
            this.albumService = albumService;
            this.pictureService = pictureService;
            this.tagService = tagService;
            this.albumRoleService = albumRoleService;
            this.townService = townService;
        }

        public IExecutable DispatchCommand(string commandName, string[] commandParameters)
        {
            string commandFullName =
                CommandsNamespacePath +
                commandName +
                CommandSuffix;

            object[] parameters = new object[] { commandParameters };

            IExecutable command = null;
            try
            {
                command = (Command)Activator.CreateInstance(Type.GetType(commandFullName), parameters);
            }
            catch
            {
                throw new InvalidOperationException("Invalid command!");
            }

            command = this.InjectDependencies(command);
            return command;
        }

        private IExecutable InjectDependencies(IExecutable command)
        {
            FieldInfo[] commandFields = command.GetType()
                                              .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            FieldInfo[] dispatcherFields = typeof(CommandDispatcher)
                                              .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var field in commandFields)
            {
                var fieldAttribute = field.GetCustomAttribute(typeof(InjectAttribute));
                if (fieldAttribute != null)
                {
                    if (dispatcherFields.Any(x => x.FieldType == field.FieldType))
                    {
                        field.SetValue(command,
                            dispatcherFields.First(x => x.FieldType == field.FieldType)
                            .GetValue(this));
                    }
                }
            }

            return command;
        }
    }
}
