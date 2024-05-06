using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace Kyrs
{
    public abstract class Repository<T>
    {
        protected readonly string _filePath;

        public Repository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
        }

        public virtual void Add(T item)
        {
            List<T> items = GetAll();
            items.Add(item);
            string json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public virtual List<T> GetAll()
        {
            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }
    }
}
