using System.Collections.Generic;

namespace MiniPluginWinForms.Interfaces
{
    /// <summary>
    /// 所有插件参数统一实现这个接口
    /// </summary>
    public interface ITestParam
    {
    }

    /// <summary>
    /// 所有插件统一实现这个接口
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 插件唯一名称
        /// </summary>
        string PluginId { get; }

        /// <summary>
        /// 创建这个插件默认的参数对象
        /// </summary>
        ITestParam CreateDefaultParam();

        /// <summary>
        /// 执行插件
        /// </summary>
        Dictionary<string, object> Run(ITestParam param);
    }
}