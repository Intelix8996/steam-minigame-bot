namespace SteamAim
{
    partial class Form1
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
            this.StatusLabel = new System.Windows.Forms.Label();
            this.XPos = new System.Windows.Forms.TextBox();
            this.YPos = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(12, 19);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(83, 13);
            this.StatusLabel.TabIndex = 0;
            this.StatusLabel.Text = "Status: Stopped";
            // 
            // XPos
            // 
            this.XPos.Location = new System.Drawing.Point(26, 94);
            this.XPos.Name = "XPos";
            this.XPos.Size = new System.Drawing.Size(59, 20);
            this.XPos.TabIndex = 1;
            // 
            // YPos
            // 
            this.YPos.Location = new System.Drawing.Point(126, 94);
            this.YPos.Name = "YPos";
            this.YPos.Size = new System.Drawing.Size(59, 20);
            this.YPos.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 141);
            this.Controls.Add(this.YPos);
            this.Controls.Add(this.XPos);
            this.Controls.Add(this.StatusLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.TextBox XPos;
        private System.Windows.Forms.TextBox YPos;
    }
}

