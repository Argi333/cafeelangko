using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace cafe
{
    
    public partial class CoffeeManagement : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='cafeturedb';username=root;password=");
        public CoffeeManagement()
        {
            InitializeComponent();
            FillDGV();
        }

      
       
        public void FillDGV()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM cafelang", connection);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.RowTemplate.Height = 100;

            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.DataSource = dt;

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol = (DataGridViewImageColumn)dataGridView1.Columns[4];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;


        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Filter = "Choose Image (*.JPG;*.PNG;*.GIF)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                guna2PictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
           

        }

        public void ExecMyQuery(MySqlCommand mcod, string myMsg)
        {
            try
            {
                connection.Open();
                if (mcod.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show(myMsg);
                }
                else
                {
                    MessageBox.Show("Query Not Executed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            FillDGV();

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        private void ClearData()
        {
            txtId.Text = "";
            txtFood.Text = "";
            txtPrice.Text = "";
            txtDesc.Text = "";

            guna2PictureBox1.Image = null;
        }

        private void txtFood_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {

            MemoryStream ms = new MemoryStream();
            guna2PictureBox1.Image.Save(ms, guna2PictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();

            MySqlCommand cmd = new MySqlCommand("INSERT INTO cafelang(ID, Name, Price, Description, Coffee_img) VALUES (@ID, @Name, @Price, @Description, @Coffee_img) ", connection);

            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = txtId.Text;
            cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = txtFood.Text;
            cmd.Parameters.Add("@Price", MySqlDbType.VarChar).Value = txtPrice.Text;
            cmd.Parameters.Add("@Description", MySqlDbType.VarChar).Value = txtDesc.Text;
            cmd.Parameters.Add("@Coffee_img", MySqlDbType.Blob).Value = img;

            ExecMyQuery(cmd, "Data Inserted");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            MemoryStream ms = new MemoryStream();
            guna2PictureBox1.Image.Save(ms, guna2PictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();

            MySqlCommand cmd = new MySqlCommand("UPDATE INTO cafelang(ID, Name, Price, Description, Coffee_img) VALUES (@ID, @Name, @Price, @Description, @Coffee_img) ", connection);

            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = txtId.Text;
            cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = txtFood.Text;
            cmd.Parameters.Add("@Price", MySqlDbType.VarChar).Value = txtPrice.Text;
            cmd.Parameters.Add("@Description", MySqlDbType.VarChar).Value = txtDesc.Text;
            cmd.Parameters.Add("@Coffee_img", MySqlDbType.Blob).Value = img;

            ExecMyQuery(cmd, "Data Updated");
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM cafelang WHERE ID=@ID", connection);

            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = txtId.Text;

            ExecMyQuery(cmd, "Deleted");
        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDisplay.Text))
            {
                int searchId;
                if (int.TryParse(txtDisplay.Text, out searchId))
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM cafelang WHERE ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", searchId);
                    DataTable dt = new DataTable();

                    try
                    {
                        connection.Open();
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connection.Close();
                    }

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Record with ID " + searchId + " not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid ID.", "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter an ID to search.", "No ID Entered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CoffeeManagement_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Byte[] img = (Byte[])dataGridView1.CurrentRow.Cells[4].Value;

            MemoryStream ms = new MemoryStream(img);

            guna2PictureBox1.Image = Image.FromStream(ms);

            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtFood.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtPrice.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtDesc.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

        }
    }
}
