using Kyrs.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kyrs
{
    public class ComponentRepository : Repository<Component>
    {
        public ComponentRepository(string filePath) : base(filePath)
        {
        }
        public void Delete(Component component)
        {
            List<Component> components = GetAll();
            Component componentToRemove = components.FirstOrDefault(c => c.Name == component.Name && c.ArticleNumber == component.ArticleNumber);
            if (componentToRemove != null)
            {
                components.Remove(componentToRemove);
                SaveChanges(components);
            }
        }

        public void Update(Component oldComponent, Component newComponent)
        {
            List<Component> components = GetAll();
            int index = components.FindIndex(c => c.Name == oldComponent.Name && c.ArticleNumber == oldComponent.ArticleNumber);
            if (index != -1)
            {
                components[index] = newComponent;
                SaveChanges(components);
            }
        }

        private void SaveChanges(List<Component> components)
        {
            string json = JsonConvert.SerializeObject(components, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}

