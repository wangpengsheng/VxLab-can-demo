namespace Extended_Robots_Can_Demo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnScan = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_Start = new System.Windows.Forms.Button();
            this.button_Stop = new System.Windows.Forms.Button();
            this.listView_left_robots = new System.Windows.Forms.ListView();
            this.listView_right_robots = new System.Windows.Forms.ListView();
            this.label_left = new System.Windows.Forms.Label();
            this.label_right = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listView1.Enabled = false;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 41);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(574, 145);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "";
            this.columnHeader1.Text = "IP";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Tag = "";
            this.columnHeader3.Text = "Availablility";
            this.columnHeader3.Width = 84;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Tag = "";
            this.columnHeader4.Text = "Virtual";
            this.columnHeader4.Width = 49;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Tag = "";
            this.columnHeader5.Text = "System name";
            this.columnHeader5.Width = 111;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Tag = "";
            this.columnHeader6.Text = "RobotWare";
            this.columnHeader6.Width = 105;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Tag = "";
            this.columnHeader7.Text = "Controller Name";
            this.columnHeader7.Width = 143;
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(217, 12);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(141, 23);
            this.btnScan.TabIndex = 2;
            this.btnScan.Text = "Scan Whole Network";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // button_Start
            // 
            this.button_Start.Enabled = false;
            this.button_Start.Location = new System.Drawing.Point(83, 414);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(182, 36);
            this.button_Start.TabIndex = 7;
            this.button_Start.Tag = "";
            this.button_Start.Text = "Run Robots from start";
            this.toolTip1.SetToolTip(this.button_Start, "This starts the Demo from the Beginning");
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_coordMove_StartRobot_Click);
            // 
            // button_Stop
            // 
            this.button_Stop.Enabled = false;
            this.button_Stop.Location = new System.Drawing.Point(352, 414);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(182, 36);
            this.button_Stop.TabIndex = 12;
            this.button_Stop.Tag = "";
            this.button_Stop.Text = "Stop Robots";
            this.toolTip1.SetToolTip(this.button_Stop, "Send Stop signal to the robots");
            this.button_Stop.UseVisualStyleBackColor = true;
            this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // listView_left_robots
            // 
            this.listView_left_robots.Location = new System.Drawing.Point(83, 244);
            this.listView_left_robots.Name = "listView_left_robots";
            this.listView_left_robots.Size = new System.Drawing.Size(121, 97);
            this.listView_left_robots.TabIndex = 8;
            this.listView_left_robots.UseCompatibleStateImageBehavior = false;
            this.listView_left_robots.View = System.Windows.Forms.View.List;
            this.listView_left_robots.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_left_robots_ItemSelectionChanged);
            // 
            // listView_right_robots
            // 
            this.listView_right_robots.Location = new System.Drawing.Point(413, 244);
            this.listView_right_robots.Name = "listView_right_robots";
            this.listView_right_robots.Size = new System.Drawing.Size(121, 97);
            this.listView_right_robots.TabIndex = 9;
            this.listView_right_robots.UseCompatibleStateImageBehavior = false;
            this.listView_right_robots.View = System.Windows.Forms.View.List;
            this.listView_right_robots.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_right_robots_ItemSelectionChanged);
            // 
            // label_left
            // 
            this.label_left.AutoSize = true;
            this.label_left.Location = new System.Drawing.Point(80, 216);
            this.label_left.Name = "label_left";
            this.label_left.Size = new System.Drawing.Size(111, 13);
            this.label_left.TabIndex = 10;
            this.label_left.Text = "Choose Left Controller";
            // 
            // label_right
            // 
            this.label_right.AutoSize = true;
            this.label_right.Location = new System.Drawing.Point(410, 216);
            this.label_right.Name = "label_right";
            this.label_right.Size = new System.Drawing.Size(118, 13);
            this.label_right.TabIndex = 11;
            this.label_right.Text = "Choose Right Controller";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 505);
            this.Controls.Add(this.button_Stop);
            this.Controls.Add(this.label_right);
            this.Controls.Add(this.label_left);
            this.Controls.Add(this.listView_right_robots);
            this.Controls.Add(this.listView_left_robots);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.listView1);
            this.Name = "Form1";
            this.Text = "Robot Synchronise Application v0.6";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.ListView listView_left_robots;
        private System.Windows.Forms.ListView listView_right_robots;
        private System.Windows.Forms.Label label_left;
        private System.Windows.Forms.Label label_right;
        private System.Windows.Forms.Button button_Stop;
    }
}

