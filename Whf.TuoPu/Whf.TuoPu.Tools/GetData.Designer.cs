namespace Whf.TuoPu.Tools
{
    partial class GetData
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.personaccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.personname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.personofficephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.personmobilephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.personaccount,
            this.personname,
            this.personofficephone,
            this.personmobilephone});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(719, 300);
            this.dataGridView1.TabIndex = 0;
            // 
            // personaccount
            // 
            this.personaccount.HeaderText = "人员账号";
            this.personaccount.Name = "personaccount";
            this.personaccount.Width = 150;
            // 
            // personname
            // 
            this.personname.HeaderText = "人员名称";
            this.personname.Name = "personname";
            this.personname.Width = 150;
            // 
            // personofficephone
            // 
            this.personofficephone.HeaderText = "办公电话";
            this.personofficephone.Name = "personofficephone";
            this.personofficephone.Width = 150;
            // 
            // personmobilephone
            // 
            this.personmobilephone.HeaderText = "移动电话";
            this.personmobilephone.Name = "personmobilephone";
            this.personmobilephone.Width = 200;
            // 
            // GetData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 324);
            this.Controls.Add(this.dataGridView1);
            this.Name = "GetData";
            this.Text = "GetData";
            this.Load += new System.EventHandler(this.GetData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn personaccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn personname;
        private System.Windows.Forms.DataGridViewTextBoxColumn personofficephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn personmobilephone;
    }
}