using System;
using System.Collections.Generic;
using System.Linq;

namespace frameworkless_web_application_kata
{
    public static class Formatter
    {
        public static string PrintGreetingMessage(IList<string> users)
        {
            return $"Hello {PrintFormattedNamesForGreeting(users)} - the time on the server is {DateTime.Now:%h:mm tt} " +
                   $"on {DateTime.Now:%d MMMM yyyy}";
        }

        private static string PrintFormattedNamesForGreeting(IList<string> users)
        {
            if (users.Count == 1)
            {
                return users.FirstOrDefault();
            }

            if (users.Count == 2 )
            {
                return string.Join(" and ", users);
            }

            var lastNameInList = users[users.Count - 1];
            users.RemoveAt(users.Count - 1); 
            return PrintNames(users) + " and " + lastNameInList;
        }
        
        public static string PrintNames(IEnumerable<string> users)
        {
            var userNameList = string.Join(", ", users);
            return userNameList;
        }

        public static string Capitalise(string name)
        {
            return name.First().ToString().ToUpper() + name.Substring(1);
        }
    }
}