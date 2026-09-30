# MiniPluginWinForms

一个基于 **C# WinForms + 插件化 + 配置驱动** 的 Mini 自动化测试框架 Demo。

项目用于学习和演示非标自动化上位机软件中常见的：

- 插件接口设计
- Factory 工厂模式
- 测试流程动态配置
- 测试参数动态编辑
- TreeView 流程展示
- PropertyGrid 参数配置
- JSON 工程保存与加载
- 测试流程执行引擎
- 插件运行结果统一处理

---

## 1. 项目目标

本项目模拟一套简化版工业测试软件。

用户可以在程序运行过程中创建不同的测试流程，例如：

```text
产品A电压测试
│
├─ 前提条件
│  ├─ PLC写入
│  └─ 延时
│
├─ 操作步骤
│  └─ 读取电压
│
└─ 期望结果
   └─ 电压判断
```

每一个测试步骤都由独立插件实现。

执行引擎并不知道 PLC、仪器或者视觉的具体实现，只通过统一的 `IPlugin` 接口执行：

```csharp
child.Plugin.Run(child.TestParam);
```

因此可以在不修改执行引擎的情况下持续增加新的测试插件。

---

## 2. 项目结构

```text
MiniPluginWinForms
│
├─ Interfaces
│  └─ PluginInterfaces.cs
│
├─ Models
│  ├─ FlowModels.cs
│  └─ ProjectDto.cs
│
├─ Plugins
│  └─ BasicPlugins.cs
│
├─ Services
│  ├─ PluginFactory.cs
│  ├─ TestEngine.cs
│  └─ ProjectJsonService.cs
│
├─ RuntimeContext.cs
│
├─ MainForm.cs
├─ MainForm.Designer.cs
└─ Program.cs
```

---

## 3. 核心架构

整个框架的数据流如下：

```text
WinForms配置界面
        ↓
TestFlow
        ↓
RootNodeData
        ↓
ChildNodeData
        ↓
┌──────────────────┐
│ Plugin           │
│ TestParam        │
└──────────────────┘
        ↓
TestEngine
        ↓
IPlugin.Run()
        ↓
具体设备 / 算法
        ↓
Dictionary<string, object>
        ↓
PASS / FAIL
```

---

## 4. IPlugin

所有插件都实现统一接口：

```csharp
public interface IPlugin
{
    string PluginId { get; }

    ITestParam CreateDefaultParam();

    Dictionary<string, object> Run(
        ITestParam param);
}
```

例如：

```text
PlcWritePlugin
DelayPlugin
ReadVoltagePlugin
VoltageCheckPlugin
```

虽然插件功能完全不同，但执行引擎只依赖 `IPlugin`。

---

## 5. ITestParam

不同插件拥有不同参数。

例如 PLC：

```csharp
public class PlcWriteParam : ITestParam
{
    public string Address { get; set; }

    public int Value { get; set; }
}
```

延时：

```csharp
public class DelayParam : ITestParam
{
    public int Milliseconds { get; set; }
}
```

电压判断：

```csharp
public class VoltageCheckParam : ITestParam
{
    public double Min { get; set; }

    public double Max { get; set; }
}
```

统一实现 `ITestParam` 后，流程节点可以统一保存：

```csharp
public ITestParam TestParam { get; set; }
```

---

## 6. PluginFactory

插件 Factory 负责：

```text
PluginId
↓
Type
↓
创建插件实例
```

例如：

```csharp
_factory.Register<PlcWritePlugin>();
_factory.Register<DelayPlugin>();
```

Factory 内部保存：

```text
PLC写入
→ typeof(PlcWritePlugin)

延时
→ typeof(DelayPlugin)
```

创建插件时：

```csharp
IPlugin plugin =
    _factory.Create("PLC写入");
```

内部通过：

```csharp
Activator.CreateInstance(type);
```

动态创建插件。

---

## 7. TestFlow

一个完整测试流程包含三部分：

```text
PreConditions
前提条件

Actions
操作步骤

ExpectedResults
期望结果
```

例如：

```text
电压测试
│
├─ 前提条件
│  ├─ 打开继电器
│  └─ 等待500ms
│
├─ 操作步骤
│  └─ 读取电压
│
└─ 期望结果
   └─ 判断电压10~15V
```

每一部分内部保存：

```csharp
List<ChildNodeData>
```

---

## 8. ChildNodeData

一个执行节点由三部分组成：

```text
Name
Plugin
TestParam
```

例如：

```text
Name:
打开测试继电器

Plugin:
PlcWritePlugin

TestParam:
Address = D100
Value = 1
```

可以理解为：

```text
Name
= 这一步叫什么

Plugin
= 谁来执行

TestParam
= 怎么执行
```

---

## 9. TestEngine

TestEngine 是整个测试流程的执行引擎。

核心代码：

```csharp
var result =
    child.Plugin.Run(
        child.TestParam);
```

TestEngine 不需要知道当前插件到底是：

```text
PLC
CAN
串口
相机
视觉
仪器
```

它只认 `IPlugin`。

因此添加新的插件时不需要修改 TestEngine。

---

## 10. PropertyGrid

WinForms 中使用 `PropertyGrid` 动态编辑插件参数。

TreeView 节点保存：

```csharp
node.Tag = child;
```

用户选择节点以后：

```csharp
propertyGridParam.SelectedObject =
    child.TestParam;
```

PropertyGrid 会根据实际参数类型自动显示不同的编辑项。

---

## 11. JSON 保存

工程保存时不会直接序列化 `IPlugin`。

只保存：

```text
PluginKey
+
ParamData
```

例如：

```json
{
  "Name": "PLC写入",
  "PluginKey": "PLC写入",
  "ParamData": {
    "Address": "D100",
    "Value": 1
  }
}
```

这样 JSON 只负责保存配置数据，不保存运行时插件对象。

---

## 12. JSON 加载

加载过程：

```text
读取JSON
↓
获得PluginKey
↓
PluginFactory.Create()
↓
重新创建Plugin
↓
Plugin.CreateDefaultParam()
↓
得到具体参数Type
↓
ParamData.ToObject(Type)
↓
恢复TestParam
↓
重新构建ChildNodeData
```

例如：

```text
PluginKey = PLC写入
```

Factory 创建：

```csharp
new PlcWritePlugin();
```

插件返回默认参数：

```csharp
new PlcWriteParam();
```

因此系统知道 JSON 参数应该反序列化为：

```csharp
PlcWriteParam
```

最终恢复完整运行时节点。

---

## 13. 当前提供的插件

### PLC写入

模拟 PLC 地址写入。

参数：

```text
Address
Value
```

后续可以替换为真实汇川 PLC：

```csharp
inovanceH5UTcpTool.Write(
    p.Address,
    p.Value);
```

### 延时

执行：

```csharp
Thread.Sleep(...)
```

参数：

```text
Milliseconds
```

### 读取电压

目前使用模拟电压。

后续可以替换为：

```text
串口仪器
VISA仪器
Modbus仪器
数据采集卡
```

### 电压判断

读取 RuntimeContext 中的电压结果，并按照：

```text
Min <= Voltage <= Max
```

执行 PASS / FAIL 判断。

---

## 14. RuntimeContext

不同插件之间可能需要共享数据。

例如：

```text
读取电压插件
↓
Voltage = 12.3

电压判断插件
↓
读取 Voltage
```

因此 Demo 中提供：

```csharp
RuntimeContext.Instance.Voltage
```

用于模拟测试过程中产生的共享数据。

真实项目中可以进一步扩展为：

```text
SN
Current
Voltage
Temperature
Barcode
VisionResult
MeasurementResult
```

---

## 15. 使用方法

### 第一步：创建流程

输入：

```text
产品A电压测试
```

点击：

```text
新建流程
```

### 第二步：添加前提条件

选择：

```text
PLC写入
```

点击：

```text
加入前提条件
```

再添加：

```text
延时
```

### 第三步：添加操作步骤

添加：

```text
读取电压
```

### 第四步：添加期望结果

添加：

```text
电压判断
```

### 第五步：配置参数

选择 TreeView 中的插件节点。

通过右侧 PropertyGrid 修改：

```text
PLC地址
PLC写入值
延时时间
模拟电压
电压上下限
```

### 第六步：运行

点击：

```text
运行流程
```

TestEngine 会按照配置顺序执行。

### 第七步：保存工程

点击：

```text
保存工程
```

生成：

```text
.json
```

配置文件。

### 第八步：重新加载

点击：

```text
加载工程
```

程序重新创建：

```text
TestFlow
Plugin
TestParam
```

然后可以继续运行。

---

## 16. 当前设计模式

本 Demo 涉及：

### 接口抽象

```text
IPlugin
ITestParam
```

### 工厂模式

```text
PluginFactory
```

### 配置驱动

测试流程不直接写死在代码中，而由用户动态配置。

### 多态

TestEngine 只依赖：

```csharp
IPlugin.Run()
```

不依赖具体插件类型。

### DTO

运行时对象与 JSON 数据模型分离。

---

## 17. 推荐后续扩展

可以继续加入：

```text
节点复制
节点启用/禁用
拖拽调整顺序
流程复制
工程名称
自动保存
最近工程
插件描述
插件分类
执行耗时
异常重试
超时控制
CancellationToken
运行中停止
进度条
DataGridView结果记录
CSV保存
真实PLC
真实串口
CAN
VISA仪器
Halcon视觉
相机SDK
```

进一步可以将：

```csharp
Dictionary<string, object>
```

升级为强类型：

```csharp
PluginResult
```

例如：

```csharp
public class PluginResult
{
    public bool Success { get; set; }

    public string Message { get; set; }

    public object Data { get; set; }
}
```

提高类型安全性与可维护性。

---

## 18. 学习重点

本项目最值得理解的不是 WinForms 控件本身，而是下面这条链：

```text
用户配置
↓
数据模型
↓
PluginKey
↓
Factory
↓
IPlugin
↓
TestParam
↓
Run()
↓
Result
↓
TestEngine
```

掌握这一条链以后，就可以逐渐从：

```text
按钮事件直接调用设备
```

升级到：

```text
配置驱动的工业自动化测试框架
```