namespace DemEx
{
    partial class Aautorization
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.txtlog_us = new System.Windows.Forms.TextBox();
            this.txtpass_us = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(75, 298);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(240, 102);
            this.button1.TabIndex = 0;
            this.button1.Text = "Войти";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtlog_us
            // 
            this.txtlog_us.Location = new System.Drawing.Point(75, 65);
            this.txtlog_us.Name = "txtlog_us";
            this.txtlog_us.Size = new System.Drawing.Size(240, 26);
            this.txtlog_us.TabIndex = 1;
            // 
            // txtpass_us
            // 
            this.txtpass_us.Location = new System.Drawing.Point(75, 161);
            this.txtpass_us.Name = "txtpass_us";
            this.txtpass_us.Size = new System.Drawing.Size(240, 26);
            this.txtpass_us.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Введите логин:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Введите пароль:";
            // 
            // Aautorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtpass_us);
            this.Controls.Add(this.txtlog_us);
            this.Controls.Add(this.button1);
            this.Name = "Aautorization";
            this.Text = "Авторизация";
            this.Load += new System.EventHandler(this.Aautorization_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtlog_us;
        private System.Windows.Forms.TextBox txtpass_us;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

