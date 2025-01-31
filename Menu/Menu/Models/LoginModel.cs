namespace menu.Models
{
    public class LoginModel
    {
        // In a real application, this might check against a database or API
        private readonly Dictionary<string, string> _validUsers;

        public LoginModel()
        {
            // Mock users for example purposes
            _validUsers = new Dictionary<string, string>
            {
                { "admin", "password" },
                { "user1", "1234" }
            };
        }

        /// <summary>
        /// Validates the user credentials.
        /// </summary>
        /// <param name="username">The entered username</param>
        /// <param name="password">The entered password</param>
        /// <returns>True if credentials are valid; otherwise false</returns>
        public bool Authenticate(string username, string password)
        {
            return _validUsers.TryGetValue(username, out var storedPassword) && storedPassword == password;
        }
    }
}
