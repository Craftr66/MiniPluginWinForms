using MiniPluginWinForms.Interfaces;
using MiniPluginWinForms.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace MiniPluginWinForms.Services
{
    public class ProjectJsonService
    {
        private readonly PluginFactory _factory;

        public ProjectJsonService(PluginFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// 保存整个测试工程
        /// </summary>
        public void Save(
            string filePath,
            List<TestFlow> flows)
        {
            ProjectDto project =
                new ProjectDto();

            foreach (TestFlow flow in flows)
            {
                TestFlowDto flowDto =
                    new TestFlowDto
                    {
                        Name = flow.Name,
                        IsUse = flow.IsUse
                    };

                ConvertGroupToDto(
                    flow.PreConditions,
                    flowDto.PreConditions);

                ConvertGroupToDto(
                    flow.Actions,
                    flowDto.Actions);

                ConvertGroupToDto(
                    flow.ExpectedResults,
                    flowDto.ExpectedResults);

                project.Flows.Add(flowDto);
            }

            string json =
                JsonConvert.SerializeObject(
                    project,
                    Formatting.Indented);

            File.WriteAllText(
                filePath,
                json);
        }

        /// <summary>
        /// RootNodeData -> JSON DTO
        /// </summary>
        private void ConvertGroupToDto(
            RootNodeData group,
            List<PluginNodeDto> target)
        {
            foreach (ChildNodeData child
                     in group.Children)
            {
                PluginNodeDto dto =
                    new PluginNodeDto
                    {
                        Name = child.Name,

                        PluginKey =
                            child.PluginKey,

                        ParamData =
                            child.TestParam == null
                                ? new JObject()
                                : JObject.FromObject(
                                    child.TestParam)
                    };

                target.Add(dto);
            }
        }

        /// <summary>
        /// 加载整个测试工程
        /// </summary>
        public List<TestFlow> Load(
            string filePath)
        {
            string json =
                File.ReadAllText(filePath);

            ProjectDto project =
                JsonConvert.DeserializeObject<ProjectDto>(
                    json);

            List<TestFlow> flows =
                new List<TestFlow>();

            if (project == null)
                return flows;

            foreach (TestFlowDto flowDto
                     in project.Flows)
            {
                TestFlow flow =
                    new TestFlow
                    {
                        Name = flowDto.Name,
                        IsUse = flowDto.IsUse
                    };

                RestoreGroup(
                    flowDto.PreConditions,
                    flow.PreConditions);

                RestoreGroup(
                    flowDto.Actions,
                    flow.Actions);

                RestoreGroup(
                    flowDto.ExpectedResults,
                    flow.ExpectedResults);

                flows.Add(flow);
            }

            return flows;
        }

        /// <summary>
        /// JSON DTO -> 真正运行时插件节点
        /// </summary>
        private void RestoreGroup(
            List<PluginNodeDto> source,
            RootNodeData target)
        {
            foreach (PluginNodeDto dto
                     in source)
            {
                // 1. 根据PluginKey重新创建插件
                IPlugin plugin =
                    _factory.Create(
                        dto.PluginKey);

                // 2. 插件告诉我们它需要什么参数类型
                ITestParam defaultParam =
                    plugin.CreateDefaultParam();

                Type paramType =
                    defaultParam.GetType();

                // 3. JSON -> 具体参数类型
                ITestParam param;

                if (dto.ParamData == null)
                {
                    param = defaultParam;
                }
                else
                {
                    param =
                        (ITestParam)
                        dto.ParamData.ToObject(
                            paramType);
                }

                // 4. 恢复真正的ChildNodeData
                ChildNodeData child =
                    new ChildNodeData
                    {
                        Name = dto.Name,

                        PluginKey =
                            dto.PluginKey,

                        Plugin =
                            plugin,

                        TestParam =
                            param
                    };

                target.Children.Add(child);
            }
        }
    }
}