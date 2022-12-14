namespace AuthenticationAuthorization.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "mihai", EmailAddress = "mihai.admin@email.com", Password = "pass", GivenName = "Mihai", Surname = "Mustuc", Role = "Administrator" },
            new UserModel() { Username = "valerian", EmailAddress = "valerian.seller@email.com", Password = "pass", GivenName = "Valerian", Surname = "cucu", Role = "Seller" },
        };
    }
}
