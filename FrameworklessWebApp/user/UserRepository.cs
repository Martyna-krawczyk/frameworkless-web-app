using System.Collections.Generic;

namespace frameworkless_web_application_kata
{
    public class UserRepository : IRepository
    {
        public readonly List<string> Users;
        
        public UserRepository()
        {
            Users = new List<string>();
            Add("Martyna");
        }

        public List<string> GetAll()
        {
            return Users;
        }

        public void Add(string name)
        {
            Users.Add(name);
        }

        public void Remove(string name)
        {
            Users.Remove(name);
        }
    }
}