using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MiniPluginWinForms.Models
{
    /// <summary>
    /// 整个工程文件
    /// </summary>
    public class ProjectDto
    {
        public List<TestFlowDto> Flows { get; set; }
            = new List<TestFlowDto>();
    }

    /// <summary>
    /// 一个测试流程
    /// </summary>
    public class TestFlowDto
    {
        public string Name { get; set; }

        public bool IsUse { get; set; }

        public List<PluginNodeDto> PreConditions { get; set; }
            = new List<PluginNodeDto>();

        public List<PluginNodeDto> Actions { get; set; }
            = new List<PluginNodeDto>();

        public List<PluginNodeDto> ExpectedResults { get; set; }
            = new List<PluginNodeDto>();
    }

    /// <summary>
    /// 一个插件节点
    /// </summary>
    public class PluginNodeDto
    {
        public string Name { get; set; }

        /// <summary>
        /// 例如：
        /// PLC写入
        /// 延时
        /// 读取电压
        /// </summary>
        public string PluginKey { get; set; }

        /// <summary>
        /// 插件参数JSON
        /// </summary>
        public JObject ParamData { get; set; }
    }
}