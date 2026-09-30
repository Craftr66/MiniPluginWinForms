using MiniPluginWinForms.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace MiniPluginWinForms.Plugins
{
    public class DelayParam : ITestParam
    {
        [DisplayName("延时时间(ms)")]
        public int Milliseconds { get; set; } = 500;
    }

    public class DelayPlugin : IPlugin
    {
        public string PluginId => "延时";

        public ITestParam CreateDefaultParam()
        {
            return new DelayParam();
        }

        public Dictionary<string, object> Run(
            ITestParam param)
        {
            var p = (DelayParam)param;

            Thread.Sleep(p.Milliseconds);

            return new Dictionary<string, object>
            {
                ["结果"] = true,
                ["描述"] = $"等待 {p.Milliseconds} ms"
            };
        }
    }


    public class PlcWriteParam : ITestParam
    {
        [DisplayName("PLC地址")]
        public string Address { get; set; } = "D100";

        [DisplayName("写入值")]
        public int Value { get; set; } = 1;
    }

    public class PlcWritePlugin : IPlugin
    {
        public string PluginId => "PLC写入";

        public ITestParam CreateDefaultParam()
        {
            return new PlcWriteParam();
        }

        public Dictionary<string, object> Run(
            ITestParam param)
        {
            var p = (PlcWriteParam)param;

            // 暂时模拟
            // 以后这里换成：
            // inovanceH5UTcpTool.Write(p.Address, p.Value);

            return new Dictionary<string, object>
            {
                ["结果"] = true,
                ["描述"] = $"PLC模拟写入 {p.Address} = {p.Value}"
            };
        }
    }

    public class ReadVoltageParam : ITestParam
    {
        [DisplayName("模拟电压")]
        public double SimulatedVoltage { get; set; } = 12.3;
    }

    public class ReadVoltagePlugin : IPlugin
    {
        public string PluginId => "读取电压";

        public ITestParam CreateDefaultParam()
        {
            return new ReadVoltageParam();
        }

        public Dictionary<string, object> Run(
            ITestParam param)
        {
            var p = (ReadVoltageParam)param;

            // 模拟仪器读取
            double voltage = p.SimulatedVoltage;

            RuntimeContext.Instance.Voltage =
                voltage;

            return new Dictionary<string, object>
            {
                ["结果"] = true,
                ["电压"] = voltage
            };
        }
    }


    public class VoltageCheckParam : ITestParam
    {
        [DisplayName("最小电压")]
        public double Min { get; set; } = 10;

        [DisplayName("最大电压")]
        public double Max { get; set; } = 15;
    }

    public class VoltageCheckPlugin : IPlugin
    {
        public string PluginId => "电压判断";

        public ITestParam CreateDefaultParam()
        {
            return new VoltageCheckParam();
        }

        public Dictionary<string, object> Run(
            ITestParam param)
        {
            var p = (VoltageCheckParam)param;

            double voltage =
                RuntimeContext.Instance.Voltage;

            bool pass =
                voltage >= p.Min &&
                voltage <= p.Max;

            return new Dictionary<string, object>
            {
                ["结果"] = pass,
                ["实际电压"] = voltage,
                ["范围"] = $"{p.Min} ~ {p.Max}"
            };
        }
    }
}