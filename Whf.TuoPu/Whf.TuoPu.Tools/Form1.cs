using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using Whf.TuoPu.Common;


namespace Whf.TuoPu.Tools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncript_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOriginal.Text.Trim()))
            {
                txtEncript.Text = WhfEncryption.DESEnCrypt(txtOriginal.Text.Trim());
            }
        }

        private void Decript_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOriginal.Text.Trim()))
            {
                txtDecript.Text = WhfEncryption.DESDeCrypt(txtEncript.Text.Trim());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form getData = new GetData();
            getData.Show();
        }
    }
}
