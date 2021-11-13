﻿
namespace MarxDev.Tasks.MultithreadingSql
{
    partial class MainForm
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
            this.btnStart = new System.Windows.Forms.Button();
            this.prgrsBar = new System.Windows.Forms.ProgressBar();
            this.nmrcUpDwnThreadCounter = new System.Windows.Forms.NumericUpDown();
            this.lblThreadsQuantity = new System.Windows.Forms.Label();
            this.lblEntriesQuantity = new System.Windows.Forms.Label();
            this.txtBxEntriesQuantity = new System.Windows.Forms.TextBox();
            this.sttsStrp = new System.Windows.Forms.StatusStrip();
            this.mainStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnStop = new System.Windows.Forms.Button();
            this.mainGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcUpDwnThreadCounter)).BeginInit();
            this.sttsStrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(434, 16);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStartClick);
            // 
            // prgrsBar
            // 
            this.prgrsBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgrsBar.Location = new System.Drawing.Point(12, 45);
            this.prgrsBar.Name = "prgrsBar";
            this.prgrsBar.Size = new System.Drawing.Size(580, 23);
            this.prgrsBar.TabIndex = 1;
            // 
            // nmrcUpDwnThreadCounter
            // 
            this.nmrcUpDwnThreadCounter.Location = new System.Drawing.Point(86, 18);
            this.nmrcUpDwnThreadCounter.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmrcUpDwnThreadCounter.Name = "nmrcUpDwnThreadCounter";
            this.nmrcUpDwnThreadCounter.Size = new System.Drawing.Size(78, 20);
            this.nmrcUpDwnThreadCounter.TabIndex = 2;
            this.nmrcUpDwnThreadCounter.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblThreadsQuantity
            // 
            this.lblThreadsQuantity.AutoSize = true;
            this.lblThreadsQuantity.Location = new System.Drawing.Point(14, 20);
            this.lblThreadsQuantity.Name = "lblThreadsQuantity";
            this.lblThreadsQuantity.Size = new System.Drawing.Size(66, 13);
            this.lblThreadsQuantity.TabIndex = 3;
            this.lblThreadsQuantity.Text = "Gijų skaičius";
            // 
            // lblEntriesQuantity
            // 
            this.lblEntriesQuantity.AutoSize = true;
            this.lblEntriesQuantity.Location = new System.Drawing.Point(179, 20);
            this.lblEntriesQuantity.Name = "lblEntriesQuantity";
            this.lblEntriesQuantity.Size = new System.Drawing.Size(125, 13);
            this.lblEntriesQuantity.TabIndex = 4;
            this.lblEntriesQuantity.Text = "Generuojamų įrašų kiekis";
            // 
            // txtBxEntriesQuantity
            // 
            this.txtBxEntriesQuantity.Location = new System.Drawing.Point(310, 18);
            this.txtBxEntriesQuantity.Name = "txtBxEntriesQuantity";
            this.txtBxEntriesQuantity.Size = new System.Drawing.Size(100, 20);
            this.txtBxEntriesQuantity.TabIndex = 5;
            this.txtBxEntriesQuantity.Text = "2000";
            this.txtBxEntriesQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBxEntriesQuantityKeyPress);
            // 
            // sttsStrp
            // 
            this.sttsStrp.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.sttsStrp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainStatus});
            this.sttsStrp.Location = new System.Drawing.Point(0, 199);
            this.sttsStrp.Name = "sttsStrp";
            this.sttsStrp.Size = new System.Drawing.Size(604, 22);
            this.sttsStrp.TabIndex = 6;
            // 
            // mainStatus
            // 
            this.mainStatus.Name = "mainStatus";
            this.mainStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(517, 16);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // mainGrid
            // 
            this.mainGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainGrid.Location = new System.Drawing.Point(12, 75);
            this.mainGrid.Name = "mainGrid";
            this.mainGrid.Size = new System.Drawing.Size(580, 94);
            this.mainGrid.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 221);
            this.Controls.Add(this.mainGrid);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.sttsStrp);
            this.Controls.Add(this.txtBxEntriesQuantity);
            this.Controls.Add(this.lblEntriesQuantity);
            this.Controls.Add(this.lblThreadsQuantity);
            this.Controls.Add(this.nmrcUpDwnThreadCounter);
            this.Controls.Add(this.prgrsBar);
            this.Controls.Add(this.btnStart);
            this.MinimumSize = new System.Drawing.Size(620, 260);
            this.Name = "MainForm";
            this.Text = "Multithreading SQL";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmrcUpDwnThreadCounter)).EndInit();
            this.sttsStrp.ResumeLayout(false);
            this.sttsStrp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ProgressBar prgrsBar;
        private System.Windows.Forms.NumericUpDown nmrcUpDwnThreadCounter;
        private System.Windows.Forms.Label lblThreadsQuantity;
        private System.Windows.Forms.Label lblEntriesQuantity;
        private System.Windows.Forms.TextBox txtBxEntriesQuantity;
        private System.Windows.Forms.StatusStrip sttsStrp;
        private System.Windows.Forms.ToolStripStatusLabel mainStatus;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.DataGridView mainGrid;
    }
}

