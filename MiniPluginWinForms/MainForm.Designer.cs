namespace MiniPluginWinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnDeleteFlow = new System.Windows.Forms.Button();
            this.btnNewFlow = new System.Windows.Forms.Button();
            this.txtFlowName = new System.Windows.Forms.TextBox();
            this.lblFlowName = new System.Windows.Forms.Label();

            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.groupFlows = new System.Windows.Forms.GroupBox();
            this.listBoxFlows = new System.Windows.Forms.ListBox();

            this.splitRight = new System.Windows.Forms.SplitContainer();

            this.panelCenter = new System.Windows.Forms.Panel();
            this.treeFlow = new System.Windows.Forms.TreeView();
            this.panelPluginToolbar = new System.Windows.Forms.Panel();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnDeleteNode = new System.Windows.Forms.Button();
            this.btnAddExpected = new System.Windows.Forms.Button();
            this.btnAddAction = new System.Windows.Forms.Button();
            this.btnAddPre = new System.Windows.Forms.Button();
            this.comboPlugins = new System.Windows.Forms.ComboBox();
            this.lblPlugin = new System.Windows.Forms.Label();

            this.panelRight = new System.Windows.Forms.Panel();
            this.propertyGridParam = new System.Windows.Forms.PropertyGrid();
            this.lblSelectedPlugin = new System.Windows.Forms.Label();
            //
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.panelRun = new System.Windows.Forms.Panel();
            this.btnRun = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();

            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();

            this.groupFlows.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).BeginInit();
            this.splitRight.Panel1.SuspendLayout();
            this.splitRight.Panel2.SuspendLayout();
            this.splitRight.SuspendLayout();

            this.panelCenter.SuspendLayout();
            this.panelPluginToolbar.SuspendLayout();

            this.panelRight.SuspendLayout();

            this.panelBottom.SuspendLayout();
            this.panelRun.SuspendLayout();

            this.SuspendLayout();

            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnDeleteFlow);
            this.panelTop.Controls.Add(this.btnNewFlow);
            this.panelTop.Controls.Add(this.txtFlowName);
            this.panelTop.Controls.Add(this.lblFlowName);
            this.panelTop.Controls.Add(this.btnSave);
            this.panelTop.Controls.Add(this.btnLoad);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(10);
            this.panelTop.Size = new System.Drawing.Size(1400, 60);
            this.panelTop.TabIndex = 0;

            // 
            // lblFlowName
            // 
            this.lblFlowName.AutoSize = true;
            this.lblFlowName.Location = new System.Drawing.Point(15, 20);
            this.lblFlowName.Name = "lblFlowName";
            this.lblFlowName.Size = new System.Drawing.Size(68, 17);
            this.lblFlowName.TabIndex = 0;
            this.lblFlowName.Text = "流程名称：";

            // 
            // txtFlowName
            // 
            this.txtFlowName.Location = new System.Drawing.Point(90, 16);
            this.txtFlowName.Name = "txtFlowName";
            this.txtFlowName.Size = new System.Drawing.Size(250, 23);
            this.txtFlowName.TabIndex = 1;

            // 
            // btnNewFlow
            // 
            this.btnNewFlow.Location = new System.Drawing.Point(350, 14);
            this.btnNewFlow.Name = "btnNewFlow";
            this.btnNewFlow.Size = new System.Drawing.Size(100, 30);
            this.btnNewFlow.TabIndex = 2;
            this.btnNewFlow.Text = "新建流程";
            this.btnNewFlow.UseVisualStyleBackColor = true;
            this.btnNewFlow.Click += new System.EventHandler(this.btnNewFlow_Click);

            // 
            // btnDeleteFlow
            // 
            this.btnDeleteFlow.Location = new System.Drawing.Point(460, 14);
            this.btnDeleteFlow.Name = "btnDeleteFlow";
            this.btnDeleteFlow.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteFlow.TabIndex = 3;
            this.btnDeleteFlow.Text = "删除流程";
            this.btnDeleteFlow.UseVisualStyleBackColor = true;
            this.btnDeleteFlow.Click += new System.EventHandler(this.btnDeleteFlow_Click);

            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(580, 14);
            this.btnSave.Name =  "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存工程";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click +=new System.EventHandler(this.btnSave_Click);

            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(690, 14);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(100, 30);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "加载工程";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click +=  new System.EventHandler(this.btnLoad_Click);
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 60);
            this.splitMain.Name = "splitMain";

            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.groupFlows);

            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.splitRight);

            this.splitMain.Size = new System.Drawing.Size(1400, 590);
            this.splitMain.SplitterDistance = 220;
            this.splitMain.TabIndex = 1;

            // 
            // groupFlows
            // 
            this.groupFlows.Controls.Add(this.listBoxFlows);
            this.groupFlows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupFlows.Location = new System.Drawing.Point(0, 0);
            this.groupFlows.Name = "groupFlows";
            this.groupFlows.Padding = new System.Windows.Forms.Padding(8);
            this.groupFlows.Size = new System.Drawing.Size(220, 590);
            this.groupFlows.TabIndex = 0;
            this.groupFlows.TabStop = false;
            this.groupFlows.Text = "测试流程";

            // 
            // listBoxFlows
            // 
            this.listBoxFlows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFlows.FormattingEnabled = true;
            this.listBoxFlows.ItemHeight = 17;
            this.listBoxFlows.Location = new System.Drawing.Point(8, 24);
            this.listBoxFlows.Name = "listBoxFlows";
            this.listBoxFlows.Size = new System.Drawing.Size(204, 558);
            this.listBoxFlows.TabIndex = 0;
            this.listBoxFlows.SelectedIndexChanged +=
                new System.EventHandler(this.listBoxFlows_SelectedIndexChanged);

            // 
            // splitRight
            // 
            this.splitRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRight.Location = new System.Drawing.Point(0, 0);
            this.splitRight.Name = "splitRight";

            // 
            // splitRight.Panel1
            // 
            this.splitRight.Panel1.Controls.Add(this.panelCenter);

            // 
            // splitRight.Panel2
            // 
            this.splitRight.Panel2.Controls.Add(this.panelRight);

            this.splitRight.Size = new System.Drawing.Size(1176, 590);
            this.splitRight.SplitterDistance = 750;
            this.splitRight.TabIndex = 0;

            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.treeFlow);
            this.panelCenter.Controls.Add(this.panelPluginToolbar);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 0);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(750, 590);
            this.panelCenter.TabIndex = 0;

            // 
            // panelPluginToolbar
            // 
            this.panelPluginToolbar.Controls.Add(this.btnMoveDown);
            this.panelPluginToolbar.Controls.Add(this.btnMoveUp);
            this.panelPluginToolbar.Controls.Add(this.btnDeleteNode);
            this.panelPluginToolbar.Controls.Add(this.btnAddExpected);
            this.panelPluginToolbar.Controls.Add(this.btnAddAction);
            this.panelPluginToolbar.Controls.Add(this.btnAddPre);
            this.panelPluginToolbar.Controls.Add(this.comboPlugins);
            this.panelPluginToolbar.Controls.Add(this.lblPlugin);

            this.panelPluginToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPluginToolbar.Location = new System.Drawing.Point(0, 0);
            this.panelPluginToolbar.Name = "panelPluginToolbar";
            this.panelPluginToolbar.Padding = new System.Windows.Forms.Padding(8);
            this.panelPluginToolbar.Size = new System.Drawing.Size(750, 110);
            this.panelPluginToolbar.TabIndex = 0;

            // 
            // lblPlugin
            // 
            this.lblPlugin.AutoSize = true;
            this.lblPlugin.Location = new System.Drawing.Point(10, 15);
            this.lblPlugin.Name = "lblPlugin";
            this.lblPlugin.Size = new System.Drawing.Size(68, 17);
            this.lblPlugin.Text = "选择插件：";

            // 
            // comboPlugins
            // 
            this.comboPlugins.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPlugins.FormattingEnabled = true;
            this.comboPlugins.Location = new System.Drawing.Point(85, 11);
            this.comboPlugins.Name = "comboPlugins";
            this.comboPlugins.Size = new System.Drawing.Size(220, 25);
            this.comboPlugins.TabIndex = 1;

            // 
            // btnAddPre
            // 
            this.btnAddPre.Location = new System.Drawing.Point(320, 8);
            this.btnAddPre.Name = "btnAddPre";
            this.btnAddPre.Size = new System.Drawing.Size(120, 32);
            this.btnAddPre.TabIndex = 2;
            this.btnAddPre.Text = "加入前提条件";
            this.btnAddPre.UseVisualStyleBackColor = true;
            this.btnAddPre.Click +=
                new System.EventHandler(this.btnAddPre_Click);

            // 
            // btnAddAction
            // 
            this.btnAddAction.Location = new System.Drawing.Point(450, 8);
            this.btnAddAction.Name = "btnAddAction";
            this.btnAddAction.Size = new System.Drawing.Size(120, 32);
            this.btnAddAction.TabIndex = 3;
            this.btnAddAction.Text = "加入操作步骤";
            this.btnAddAction.UseVisualStyleBackColor = true;
            this.btnAddAction.Click +=
                new System.EventHandler(this.btnAddAction_Click);

            // 
            // btnAddExpected
            // 
            this.btnAddExpected.Location = new System.Drawing.Point(580, 8);
            this.btnAddExpected.Name = "btnAddExpected";
            this.btnAddExpected.Size = new System.Drawing.Size(120, 32);
            this.btnAddExpected.TabIndex = 4;
            this.btnAddExpected.Text = "加入期望结果";
            this.btnAddExpected.UseVisualStyleBackColor = true;
            this.btnAddExpected.Click +=
                new System.EventHandler(this.btnAddExpected_Click);

            // 
            // btnDeleteNode
            // 
            this.btnDeleteNode.Location = new System.Drawing.Point(13, 55);
            this.btnDeleteNode.Name = "btnDeleteNode";
            this.btnDeleteNode.Size = new System.Drawing.Size(100, 32);
            this.btnDeleteNode.TabIndex = 5;
            this.btnDeleteNode.Text = "删除节点";
            this.btnDeleteNode.UseVisualStyleBackColor = true;
            this.btnDeleteNode.Click +=
                new System.EventHandler(this.btnDeleteNode_Click);

            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(125, 55);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(100, 32);
            this.btnMoveUp.TabIndex = 6;
            this.btnMoveUp.Text = "上移";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click +=
                new System.EventHandler(this.btnMoveUp_Click);

            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(237, 55);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(100, 32);
            this.btnMoveDown.TabIndex = 7;
            this.btnMoveDown.Text = "下移";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click +=
                new System.EventHandler(this.btnMoveDown_Click);

            // 
            // treeFlow
            // 
            this.treeFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFlow.HideSelection = false;
            this.treeFlow.Location = new System.Drawing.Point(0, 110);
            this.treeFlow.Name = "treeFlow";
            this.treeFlow.Size = new System.Drawing.Size(750, 480);
            this.treeFlow.TabIndex = 1;

            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.propertyGridParam);
            this.panelRight.Controls.Add(this.lblSelectedPlugin);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(422, 590);
            this.panelRight.TabIndex = 0;

            // 
            // lblSelectedPlugin
            // 
            this.lblSelectedPlugin.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSelectedPlugin.Font =
                new System.Drawing.Font(
                    "Microsoft YaHei UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblSelectedPlugin.Location =
                new System.Drawing.Point(0, 0);

            this.lblSelectedPlugin.Name =
                "lblSelectedPlugin";

            this.lblSelectedPlugin.Padding =
                new System.Windows.Forms.Padding(10);

            this.lblSelectedPlugin.Size =
                new System.Drawing.Size(422, 45);

            this.lblSelectedPlugin.Text =
                "当前插件：未选择";

            this.lblSelectedPlugin.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // propertyGridParam
            // 
            this.propertyGridParam.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.propertyGridParam.Location =
                new System.Drawing.Point(0, 45);

            this.propertyGridParam.Name =
                "propertyGridParam";

            this.propertyGridParam.Size =
                new System.Drawing.Size(422, 545);

            this.propertyGridParam.TabIndex = 1;

            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.txtLog);
            this.panelBottom.Controls.Add(this.panelRun);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 650);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1400, 250);
            this.panelBottom.TabIndex = 2;

            // 
            // panelRun
            // 
            this.panelRun.Controls.Add(this.btnRun);
            this.panelRun.Controls.Add(this.lblLog);
            this.panelRun.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRun.Location = new System.Drawing.Point(0, 0);
            this.panelRun.Name = "panelRun";
            this.panelRun.Size = new System.Drawing.Size(1400, 50);
            this.panelRun.TabIndex = 0;

            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(12, 17);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(68, 17);
            this.lblLog.Text = "运行日志：";

            // 
            // btnRun
            // 
            this.btnRun.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                ((System.Windows.Forms.AnchorStyles.Top |
                  System.Windows.Forms.AnchorStyles.Right)));

            this.btnRun.Font =
                new System.Drawing.Font(
                    "Microsoft YaHei UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.btnRun.Location =
                new System.Drawing.Point(1265, 7);

            this.btnRun.Name =
                "btnRun";

            this.btnRun.Size =
                new System.Drawing.Size(120, 36);

            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "运行流程";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click +=
                new System.EventHandler(this.btnRun_Click);

            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font =
                new System.Drawing.Font(
                    "Consolas",
                    10F);

            this.txtLog.Location =
                new System.Drawing.Point(0, 50);

            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars =
                System.Windows.Forms.ScrollBars.Vertical;

            this.txtLog.Size =
                new System.Drawing.Size(1400, 200);

            this.txtLog.TabIndex = 1;

            // 
            // MainForm
            // 
            this.AutoScaleDimensions =
                new System.Drawing.SizeF(7F, 17F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(1400, 900);

            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);

            this.Font =
                new System.Drawing.Font(
                    "Microsoft YaHei UI",
                    9F);

            this.MinimumSize =
                new System.Drawing.Size(1100, 700);

            this.Name = "MainForm";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text = "Mini Plugin Test Framework";

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();

            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(this.splitMain))
                .EndInit();

            this.splitMain.ResumeLayout(false);

            this.groupFlows.ResumeLayout(false);

            this.splitRight.Panel1.ResumeLayout(false);
            this.splitRight.Panel2.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(this.splitRight))
                .EndInit();

            this.splitRight.ResumeLayout(false);

            this.panelCenter.ResumeLayout(false);

            this.panelPluginToolbar.ResumeLayout(false);
            this.panelPluginToolbar.PerformLayout();

            this.panelRight.ResumeLayout(false);

            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();

            this.panelRun.ResumeLayout(false);
            this.panelRun.PerformLayout();

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;

        private System.Windows.Forms.Label lblFlowName;
        private System.Windows.Forms.TextBox txtFlowName;

        private System.Windows.Forms.Button btnNewFlow;
        private System.Windows.Forms.Button btnDeleteFlow;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;

        private System.Windows.Forms.SplitContainer splitMain;

        private System.Windows.Forms.GroupBox groupFlows;
        private System.Windows.Forms.ListBox listBoxFlows;

        private System.Windows.Forms.SplitContainer splitRight;

        private System.Windows.Forms.Panel panelCenter;

        private System.Windows.Forms.Panel panelPluginToolbar;

        private System.Windows.Forms.Label lblPlugin;
        private System.Windows.Forms.ComboBox comboPlugins;

        private System.Windows.Forms.Button btnAddPre;
        private System.Windows.Forms.Button btnAddAction;
        private System.Windows.Forms.Button btnAddExpected;

        private System.Windows.Forms.Button btnDeleteNode;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;

        private System.Windows.Forms.TreeView treeFlow;

        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblSelectedPlugin;
        private System.Windows.Forms.PropertyGrid propertyGridParam;

        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelRun;

        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Button btnRun;

        private System.Windows.Forms.TextBox txtLog;
    }
}