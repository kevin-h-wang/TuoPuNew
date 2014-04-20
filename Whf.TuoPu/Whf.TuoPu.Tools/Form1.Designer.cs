namespace Whf.TuoPu.Tools
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtOriginal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEncript = new System.Windows.Forms.TextBox();
            this.btnEncript = new System.Windows.Forms.Button();
            this.Decript = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDecript = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "加密前的值：";
            // 
            // txtOriginal
            // 
            this.txtOriginal.Location = new System.Drawing.Point(123, 43);
            this.txtOriginal.Multiline = true;
            this.txtOriginal.Name = "txtOriginal";
            this.txtOriginal.Size = new System.Drawing.Size(375, 59);
            this.txtOriginal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "加密后的值：";
            // 
            // txtEncript
            // 
            this.txtEncript.Location = new System.Drawing.Point(123, 123);
            this.txtEncript.Multiline = true;
            this.txtEncript.Name = "txtEncript";
            this.txtEncript.Size = new System.Drawing.Size(375, 61);
            this.txtEncript.TabIndex = 3;
            // 
            // btnEncript
            // 
            this.btnEncript.Location = new System.Drawing.Point(123, 312);
            this.btnEncript.Name = "btnEncript";
            this.btnEncript.Size = new System.Drawing.Size(75, 23);
            this.btnEncript.TabIndex = 4;
            this.btnEncript.Text = "加密";
            this.btnEncript.UseVisualStyleBackColor = true;
            this.btnEncript.Click += new System.EventHandler(this.btnEncript_Click);
            // 
            // Decript
            // 
            this.Decript.Location = new System.Drawing.Point(310, 312);
            this.Decript.Name = "Decript";
            this.Decript.Size = new System.Drawing.Size(75, 23);
            this.Decript.TabIndex = 5;
            this.Decript.Text = "解密";
            this.Decript.UseVisualStyleBackColor = true;
            this.Decript.Click += new System.EventHandler(this.Decript_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "解密后的值：";
            // 
            // txtDecript
            // 
            this.txtDecript.Location = new System.Drawing.Point(123, 216);
            this.txtDecript.Multiline = true;
            this.txtDecript.Name = "txtDecript";
            this.txtDecript.Size = new System.Drawing.Size(375, 57);
            this.txtDecript.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(443, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "测试数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 399);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtDecript);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Decript);
            this.Controls.Add(this.btnEncript);
            this.Controls.Add(this.txtEncript);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOriginal);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "加密解密";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOriginal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEncript;
        private System.Windows.Forms.Button btnEncript;
        private System.Windows.Forms.Button Decript;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDecript;
        private System.Windows.Forms.Button button1;
    }
}

