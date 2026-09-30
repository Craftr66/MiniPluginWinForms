using MiniPluginWinForms.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniPluginWinForms.Services
{
    public class PluginFactory
    {
        private readonly Dictionary<string, Type>
            _types = new Dictionary<string, Type>();

        public void Register<T>()
            where T : IPlugin, new()
        {
            T temp = new T();

            _types[temp.PluginId] =
                typeof(T);
        }

        public IPlugin Create(string pluginId)
        {
            if (!_types.TryGetValue(
                pluginId,
                out Type type))
            {
                throw new Exception(
                    $"插件未注册：{pluginId}");
            }

            return (IPlugin)Activator.CreateInstance(type);
        }

        public List<string> GetPluginNames()
        {
            return _types.Keys.ToList();
        }
    }
}