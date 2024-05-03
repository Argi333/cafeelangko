using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cafe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void gunaGradient2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        public void loadform(object Form)
        {
            if (this.coffeepanel.Controls.Count > 0)
                this.coffeepanel.Controls.Clear();

            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.coffeepanel.Controls.Add(f);
            this.coffeepanel.Tag = f;
            f.Show();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            loadform(new CoffeeManagement());
        }
    }
}
