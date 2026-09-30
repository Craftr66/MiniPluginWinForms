using MiniPluginWinForms.Interfaces;
using MiniPluginWinForms.Models;
using MiniPluginWinForms.Plugins;
using MiniPluginWinForms.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniPluginWinForms
{
    public partial class MainForm : Form
    {
        private readonly PluginFactory _factory = new PluginFactory();
        private ProjectJsonService _jsonService;
        private readonly TestEngine _engine = new TestEngine();

        /// <summary>
        /// 当前程序里配置的所有测试流程
        /// </summary>
        private readonly List<TestFlow> _flows = new List<TestFlow>();

        public MainForm()
        {
            InitializeComponent();

            InitPlugins();
            _jsonService = new ProjectJsonService(_factory);
            InitUi();
        }

        /// <summary>
        /// 注册所有插件
        /// </summary>
        private void InitPlugins()
        {
            _factory.Register<PlcWritePlugin>();
            _factory.Register<DelayPlugin>();
            _factory.Register<ReadVoltagePlugin>();
            _factory.Register<VoltageCheckPlugin>();
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        private void InitUi()
        {
            // 加载插件列表
            comboPlugins.DataSource = _factory.GetPluginNames();

            // TreeView选中节点事件
            treeFlow.AfterSelect += TreeFlow_AfterSelect;

            // PropertyGrid参数变化后刷新TreeView显示（可选）
            propertyGridParam.PropertyValueChanged += PropertyGridParam_PropertyValueChanged;
        }

        #region 流程管理

        /// <summary>
        /// 新建测试流程
        /// </summary>
        private void btnNewFlow_Click(object sender, EventArgs e)
        {
            string name = txtFlowName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(
                    "请输入流程名称",
                    "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            TestFlow flow = new TestFlow
            {
                Name = name,
                IsUse = true
            };

            _flows.Add(flow);

            RefreshFlowList();

            listBoxFlows.SelectedItem = flow;

            txtFlowName.Clear();

            RefreshTree();
        }

        /// <summary>
        /// 删除测试流程
        /// </summary>
        private void btnDeleteFlow_Click(object sender, EventArgs e)
        {
            TestFlow flow = GetCurrentFlow();

            if (flow == null)
            {
                MessageBox.Show("请选择要删除的流程");
                return;
            }

            DialogResult result = MessageBox.Show(
                $"确定删除流程【{flow.Name}】吗？",
                "提示",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            _flows.Remove(flow);

            RefreshFlowList();

            treeFlow.Nodes.Clear();

            propertyGridParam.SelectedObject = null;
        }

        /// <summary>
        /// 刷新左边流程列表
        /// </summary>
        private void RefreshFlowList()
        {
            listBoxFlows.DataSource = null;
            listBoxFlows.DataSource = _flows;
        }

        /// <summary>
        /// 当前选中的流程
        /// </summary>
        private TestFlow GetCurrentFlow()
        {
            return listBoxFlows.SelectedItem as TestFlow;
        }

        /// <summary>
        /// 切换测试流程
        /// </summary>
        private void listBoxFlows_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridParam.SelectedObject = null;

            RefreshTree();
        }

        #endregion


        #region 插件添加

        /// <summary>
        /// 添加到前提条件
        /// </summary>
        private void btnAddPre_Click(object sender, EventArgs e)
        {
            TestFlow flow = GetCurrentFlow();

            if (flow == null)
            {
                MessageBox.Show("请先新建或者选择一个测试流程");
                return;
            }

            AddPlugin(flow.PreConditions);
        }

        /// <summary>
        /// 添加到操作步骤
        /// </summary>
        private void btnAddAction_Click(object sender, EventArgs e)
        {
            TestFlow flow = GetCurrentFlow();

            if (flow == null)
            {
                MessageBox.Show("请先新建或者选择一个测试流程");
                return;
            }

            AddPlugin(flow.Actions);
        }

        /// <summary>
        /// 添加到期望结果
        /// </summary>
        private void btnAddExpected_Click(object sender, EventArgs e)
        {
            TestFlow flow = GetCurrentFlow();

            if (flow == null)
            {
                MessageBox.Show("请先新建或者选择一个测试流程");
                return;
            }

            AddPlugin(flow.ExpectedResults);
        }

        /// <summary>
        /// 真正执行插件添加
        /// </summary>
        private void AddPlugin(RootNodeData targetGroup)
        {
            string pluginName = comboPlugins.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(pluginName))
            {
                MessageBox.Show("请选择插件");
                return;
            }

            try
            {
                // 通过Factory创建插件实例
                IPlugin plugin = _factory.Create(pluginName);

                // 让插件自己创建默认参数
                ITestParam param = plugin.CreateDefaultParam();

                ChildNodeData child = new ChildNodeData
                {
                    Name = pluginName,
                    PluginKey = pluginName,
                    Plugin = plugin,
                    TestParam = param
                };

                targetGroup.Children.Add(child);

                RefreshTree();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "插件创建失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion


        #region TreeView

        /// <summary>
        /// 刷新测试流程树
        /// </summary>
        private void RefreshTree()
        {
            treeFlow.BeginUpdate();

            treeFlow.Nodes.Clear();

            TestFlow flow = GetCurrentFlow();

            if (flow == null)
            {
                treeFlow.EndUpdate();
                return;
            }

            TreeNode preNode = new TreeNode("前提条件");
            TreeNode actionNode = new TreeNode("操作步骤");
            TreeNode expectedNode = new TreeNode("期望结果");

            // Tag里放RootNodeData
            // 后面可以用于判断节点属于哪个组
            preNode.Tag = flow.PreConditions;
            actionNode.Tag = flow.Actions;
            expectedNode.Tag = flow.ExpectedResults;

            AddChildrenToTree(
                preNode,
                flow.PreConditions);

            AddChildrenToTree(
                actionNode,
                flow.Actions);

            AddChildrenToTree(
                expectedNode,
                flow.ExpectedResults);

            treeFlow.Nodes.Add(preNode);
            treeFlow.Nodes.Add(actionNode);
            treeFlow.Nodes.Add(expectedNode);

            treeFlow.ExpandAll();

            treeFlow.EndUpdate();
        }

        /// <summary>
        /// 把ChildNodeData显示到TreeView
        /// </summary>
        private void AddChildrenToTree(
            TreeNode parent,
            RootNodeData group)
        {
            for (int i = 0; i < group.Children.Count; i++)
            {
                ChildNodeData child = group.Children[i];

                TreeNode node = new TreeNode
                {
                    Text = $"{i + 1}. {child.Name}",
                    Tag = child
                };

                parent.Nodes.Add(node);
            }
        }

        /// <summary>
        /// 点击TreeView节点
        /// </summary>
        private void TreeFlow_AfterSelect(
            object sender,
            TreeViewEventArgs e)
        {
            if (e.Node.Tag is ChildNodeData child)
            {
                // PropertyGrid直接绑定参数对象
                propertyGridParam.SelectedObject =
                    child.TestParam;

                lblSelectedPlugin.Text =
                    $"当前插件：{child.PluginKey}";
            }
            else
            {
                propertyGridParam.SelectedObject = null;

                lblSelectedPlugin.Text = "当前插件：未选择";
            }
        }

        /// <summary>
        /// 修改参数以后
        /// </summary>
        private void PropertyGridParam_PropertyValueChanged(
            object s,
            PropertyValueChangedEventArgs e)
        {
            propertyGridParam.Refresh();
        }

        #endregion


        #region 删除节点

        private void btnDeleteNode_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode =
                treeFlow.SelectedNode;

            if (selectedNode == null)
            {
                MessageBox.Show("请选择要删除的插件节点");
                return;
            }

            if (!(selectedNode.Tag is ChildNodeData child))
            {
                MessageBox.Show("根节点不能删除");
                return;
            }

            TestFlow flow = GetCurrentFlow();

            if (flow == null)
                return;

            bool removed = false;

            removed |=
                flow.PreConditions.Children.Remove(child);

            removed |=
                flow.Actions.Children.Remove(child);

            removed |=
                flow.ExpectedResults.Children.Remove(child);

            if (removed)
            {
                propertyGridParam.SelectedObject = null;

                RefreshTree();
            }
        }

        #endregion


        #region 上移下移

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            MoveSelectedNode(-1);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            MoveSelectedNode(1);
        }

        /// <summary>
        /// direction:
        /// -1 上移
        ///  1 下移
        /// </summary>
        private void MoveSelectedNode(int direction)
        {
            TreeNode selectedNode =
                treeFlow.SelectedNode;

            if (selectedNode == null)
                return;

            if (!(selectedNode.Tag is ChildNodeData child))
                return;

            TestFlow flow = GetCurrentFlow();

            if (flow == null)
                return;

            RootNodeData group =
                FindGroup(flow, child);

            if (group == null)
                return;

            int oldIndex =
                group.Children.IndexOf(child);

            int newIndex =
                oldIndex + direction;

            if (newIndex < 0 ||
                newIndex >= group.Children.Count)
            {
                return;
            }

            group.Children.RemoveAt(oldIndex);

            group.Children.Insert(
                newIndex,
                child);

            RefreshTree();
        }

        /// <summary>
        /// 找到某个Child属于哪个RootNodeData
        /// </summary>
        private RootNodeData FindGroup(
            TestFlow flow,
            ChildNodeData child)
        {
            if (flow.PreConditions.Children.Contains(child))
                return flow.PreConditions;

            if (flow.Actions.Children.Contains(child))
                return flow.Actions;

            if (flow.ExpectedResults.Children.Contains(child))
                return flow.ExpectedResults;

            return null;
        }

        #endregion


        #region 运行

        private async void btnRun_Click(object sender, EventArgs e)
        {
            TestFlow flow =
                GetCurrentFlow();

            if (flow == null)
            {
                MessageBox.Show("请选择测试流程");
                return;
            }

            txtLog.Clear();

            btnRun.Enabled = false;

            try
            {
                bool result =
                    await Task.Run(() =>
                    {
                        return _engine.Run(
                            flow,
                            AddLog);
                    });

                if (result)
                {
                    AddLog("");
                    AddLog("最终测试结果：PASS");
                }
                else
                {
                    AddLog("");
                    AddLog("最终测试结果：FAIL");
                }
            }
            catch (Exception ex)
            {
                AddLog($"执行异常：{ex}");
            }
            finally
            {
                btnRun.Enabled = true;
            }
        }

        private void btnSave_Click(object sender,EventArgs e)
        {
            if (_flows.Count == 0)
            {
                MessageBox.Show(
                    "当前没有测试流程可以保存");

                return;
            }

            using SaveFileDialog dialog =
                new SaveFileDialog();

            dialog.Filter =
                "JSON工程文件 (*.json)|*.json";

            dialog.DefaultExt = "json";

            dialog.FileName =
                "TestProject.json";

            if (dialog.ShowDialog()
                != DialogResult.OK)
            {
                return;
            }

            try
            {
                _jsonService.Save(
                    dialog.FileName,
                    _flows);

                AddLog(
                    $"工程保存成功：{dialog.FileName}");

                MessageBox.Show(
                    "保存成功",
                    "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"保存失败：{ex.Message}",
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender,EventArgs e)
        {
            using OpenFileDialog dialog =
                new OpenFileDialog();

            dialog.Filter =
                "JSON工程文件 (*.json)|*.json";

            if (dialog.ShowDialog()
                != DialogResult.OK)
            {
                return;
            }

            try
            {
                List<TestFlow> loadedFlows =
                    _jsonService.Load(
                        dialog.FileName);

                _flows.Clear();

                _flows.AddRange(
                    loadedFlows);

                RefreshFlowList();

                propertyGridParam.SelectedObject =
                    null;

                if (_flows.Count > 0)
                {
                    listBoxFlows.SelectedIndex = 0;
                }

                RefreshTree();

                AddLog(
                    $"工程加载成功：{dialog.FileName}");

                MessageBox.Show(
                    $"加载完成，共 {_flows.Count} 个测试流程",
                    "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"加载失败：{ex.Message}",
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 线程安全日志
        /// </summary>
        private void AddLog(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(
                    new Action<string>(AddLog),
                    message);

                return;
            }

            txtLog.AppendText(
                $"[{DateTime.Now:HH:mm:ss.fff}] {message}" +
                Environment.NewLine);

            txtLog.SelectionStart =
                txtLog.TextLength;

            txtLog.ScrollToCaret();
        }

        #endregion
    }
}