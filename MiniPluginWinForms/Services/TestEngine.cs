using MiniPluginWinForms.Models;
using System;
using System.Collections.Generic;

namespace MiniPluginWinForms.Services
{
    public class TestEngine
    {
        public bool Run(
            TestFlow flow,
            Action<string> log)
        {
            log("");
            log($"========== 开始：{flow.Name} ==========");

            bool pass = true;

            pass &= RunGroup(
                "前提条件",
                flow.PreConditions,
                log);

            pass &= RunGroup(
                "操作步骤",
                flow.Actions,
                log);

            pass &= RunGroup(
                "期望结果",
                flow.ExpectedResults,
                log);

            log(
                $"========== {(pass ? "PASS" : "FAIL")} ==========");

            return pass;
        }

        private bool RunGroup(
            string groupName,
            RootNodeData group,
            Action<string> log)
        {
            log("");
            log($"--- {groupName} ---");

            bool groupResult = true;

            foreach (var child in group.Children)
            {
                log($"执行：{child.Name}");

                try
                {
                    Dictionary<string, object> result =
                        child.Plugin.Run(
                            child.TestParam);

                    bool pass = true;

                    if (result.TryGetValue(
                        "结果",
                        out object resultValue))
                    {
                        pass =
                            Convert.ToBoolean(
                                resultValue);
                    }

                    foreach (var kv in result)
                    {
                        log(
                            $"    {kv.Key}：{kv.Value}");
                    }

                    if (!pass)
                    {
                        groupResult = false;
                    }
                }
                catch (Exception ex)
                {
                    log($"    异常：{ex.Message}");

                    groupResult = false;
                }
            }

            return groupResult;
        }
    }
}