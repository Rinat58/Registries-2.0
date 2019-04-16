using System;

namespace Logic.Model
{
    /// <summary>
    /// Учётная запись.
    /// </summary>
    [Serializable]
    public class Account
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Account(string name, string email, string pass)
        {
            Name = name;
            Email = email;
            Password = pass;
        }
        public Account() { }
    }
}
