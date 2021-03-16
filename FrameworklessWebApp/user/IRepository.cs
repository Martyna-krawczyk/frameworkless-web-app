using System.Collections.Generic;

namespace frameworkless_web_application_kata
{
    public interface IRepository
    {
        List<string> GetAll();
        void Add(string name);
        void Remove(string name);
    }
}