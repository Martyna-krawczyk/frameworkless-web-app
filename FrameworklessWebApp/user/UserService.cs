using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;

namespace frameworkless_web_application_kata
{
    public class UserService
    {
        public readonly IRepository UserRepository;

        public UserService(IRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public void AddUserToList(string name)
        {
            var formattedName = Formatter.Capitalise(name);
            UserRepository.Add(formattedName);
        }

        public bool NameExists(string name)
        {
            return UserRepository.GetAll().Contains(name, StringComparer.CurrentCultureIgnoreCase);
        }

        public List<string> GetUserList()
        {
            return UserRepository.GetAll();
        }

        public void DeleteUser(string name)
        {
            var index = FindMatch(name);
            UserRepository.GetAll().RemoveAt(index);
        }

        public void UpdateUser(string name, string resource)
        {
            var index = FindMatch(resource);
            UserRepository.GetAll().RemoveAt(index);
            var formattedName = Formatter.Capitalise(name);
            UserRepository.GetAll().Insert(index, formattedName);
        }

        public int FindMatch(string resource)
        {
            var index = UserRepository.GetAll().FindIndex(n => n.ToLower().Equals(resource));
            return index;
        }
    }
}