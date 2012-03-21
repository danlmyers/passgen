using System;
using System.Windows.Forms;

namespace PassGen
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            int pwlength = int.Parse(PWLengthN.Text);
            bool pwusespecial = PWUseSpecialCharacters.Checked;
            password pw = new password();
            PWResultBox.Text = pw.genpass(pwlength, pwusespecial);
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.Show();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            PWResultBox.Text = "";
        }

    }
}