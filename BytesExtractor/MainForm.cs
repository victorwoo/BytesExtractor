using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BytesExtractor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            var bytes = Calculator.HexStringToByteArray(this.tbData.Text.Trim());
            var pattern = Calculator.ReadPattern(this.tbPattern.Text);

            this.tbData.Text = Calculator.Extract(bytes, pattern);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbPattern_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void Calculate()
        {
            try
            {
                var bytes = Calculator.HexStringToByteArray(this.tbData.Text.Trim());
                var pattern = Calculator.ReadPattern(this.tbPattern.Text);

                this.tbData.Text = Calculator.Extract(bytes, pattern);
            }
            catch (Exception e)
            {

            }
        }
    }
}
