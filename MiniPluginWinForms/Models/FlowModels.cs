using MiniPluginWinForms.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MiniPluginWinForms.Models
{
    public class ChildNodeData
    {
        public string Name { get; set; }

        /// <summary>
        /// 用于重新创建插件
        /// </summary>
        public string PluginKey { get; set; }

        /// <summary>
        /// 运行时插件对象，不直接保存到JSON
        /// </summary>
        [JsonIgnore]
        public IPlugin Plugin { get; set; }

        public ITestParam TestParam { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class RootNodeData
    {
        public List<ChildNodeData> Children { get; set; }
            = new List<ChildNodeData>();
    }

    public class TestFlow
    {
        public string Name { get; set; }

        public bool IsUse { get; set; } = true;

        public RootNodeData PreConditions { get; set; }
            = new RootNodeData();

        public RootNodeData Actions { get; set; }
            = new RootNodeData();

        public RootNodeData ExpectedResults { get; set; }
            = new RootNodeData();

        public override string ToString()
        {
            return Name;
        }
    }
}