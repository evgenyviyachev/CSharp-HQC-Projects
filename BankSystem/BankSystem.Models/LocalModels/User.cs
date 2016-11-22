namespace BankSystem.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
    {
        [NotMapped]
        private static bool isLoggedIn = false;

        [NotMapped]
        private static int loggedUserId = 0;

        [NotMapped]
        public static bool IsLoggedIn
        {
            get
            {
                return isLoggedIn;
            }
            set
            {
                isLoggedIn = value;
            }
        }

        [NotMapped]
        public static int LoggedUserId
        {
            get
            {
                return loggedUserId;
            }
            set
            {
                loggedUserId = value;
            }
        }
    }
}
