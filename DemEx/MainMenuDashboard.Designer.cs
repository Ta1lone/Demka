﻿namespace DemEx
{
    partial class MainMenuDashboard
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.валидацияФИОToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетОЗагруженностиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.гостиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.валидацияФИОToolStripMenuItem,
            this.отчетОЗагруженностиToolStripMenuItem,
            this.гостиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // валидацияФИОToolStripMenuItem
            // 
            this.валидацияФИОToolStripMenuItem.Name = "валидацияФИОToolStripMenuItem";
            this.валидацияФИОToolStripMenuItem.Size = new System.Drawing.Size(160, 29);
            this.валидацияФИОToolStripMenuItem.Text = "Валидация ФИО";
            this.валидацияФИОToolStripMenuItem.Click += new System.EventHandler(this.валидацияФИОToolStripMenuItem_Click);
            // 
            // отчетОЗагруженностиToolStripMenuItem
            // 
            this.отчетОЗагруженностиToolStripMenuItem.Name = "отчетОЗагруженностиToolStripMenuItem";
            this.отчетОЗагруженностиToolStripMenuItem.Size = new System.Drawing.Size(218, 29);
            this.отчетОЗагруженностиToolStripMenuItem.Text = "Отчет о загруженности";
            this.отчетОЗагруженностиToolStripMenuItem.Click += new System.EventHandler(this.отчетОЗагруженностиToolStripMenuItem_Click);
            // 
            // гостиToolStripMenuItem
            // 
            this.гостиToolStripMenuItem.Name = "гостиToolStripMenuItem";
            this.гостиToolStripMenuItem.Size = new System.Drawing.Size(72, 29);
            this.гостиToolStripMenuItem.Text = "Гости";
            this.гостиToolStripMenuItem.Click += new System.EventHandler(this.гостиToolStripMenuItem_Click);
            // 
            // MainMenuDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenuDashboard";
            this.Text = "Главное меню";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem валидацияФИОToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетОЗагруженностиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem гостиToolStripMenuItem;
    }
}