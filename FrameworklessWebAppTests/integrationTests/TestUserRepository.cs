using System.Collections.Generic;
using frameworkless_web_application_kata;

namespace FrameworklessWebAppTests
{
    public class TestUserRepository : IRepository
    {
        private readonly List<string> _users;
        
        public TestUserRepository()
        {
            _users = new List<string>() {"Martyna", "Sunshine", "Peter", "George", "Malcolm"};
        }

        public List<string> GetAll()
        {
            return _users;
        }

        public void Add(string name)
        {
            _users.Add(name);
        }

        public void Remove(string name)
        {
            _users.Remove(name);
        }
    }
}