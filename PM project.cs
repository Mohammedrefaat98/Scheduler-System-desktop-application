using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp
{
    public partial class PM_project : UserControl
    {
        public string conconnect = "Data Source=196.221.70.59,10668;Initial Catalog=PMProject;Persist Security Info=True;User ID=GUCBoys;Password=nis123";
        public PM_project()
        {
            InitializeComponent();
            label5.Visible = false;
            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
            delete.FlatStyle = FlatStyle.Popup;
            delete.HeaderText = "Delete";
            delete.Name = "Delete";
            delete.UseColumnTextForButtonValue = true;
            delete.Text = "Delete";
            if (dataGridView1.Columns.Contains(delete.Name = "Delete"))
            {

            }
            else
            {
                dataGridView1.Columns.Add(delete);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void loadgrid()
        {
            using (SqlConnection con = new SqlConnection(conconnect))
            {
                con.Open();
                SqlDataAdapter com = new SqlDataAdapter("Select * from PMPlans", con);
                DataTable dtbl = new DataTable();
                com.Fill(dtbl);
                dataGridView1.DataSource = dtbl;

            }
        }

        private void PM_project_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = DateTime.Today.AddDays(1);
            loadgrid();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conconnect);
            con.Open();
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "Select SiteName from SiteCount where SiteName like '" + textBox1.Text + "%' order by SiteName";
            SqlDataReader dr = com.ExecuteReader();
            listBox1.Items.Clear();
            while (dr.Read())
            {
                listBox1.Items.Add(dr.GetString(0));

            }
            con.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected =listBox1.SelectedItem.ToString();
            SqlConnection con = new SqlConnection(conconnect);
            con.Open();
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "Select * from SiteCount where SiteName = '" + selected + "'";
            SqlDataReader dr = com.ExecuteReader();
            listBox2.Items.Clear();
            dr.Read();
            for(int i = 0; i < 19; i++) {
                listBox2.Items.Add(dr.GetName(i) + ": " + dr.GetValue(i).ToString());
            }
            con.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int s = (int)dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                SqlConnection con = new SqlConnection(conconnect);
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "delete from PMPlans where id = '" + s + "' ";
                com.ExecuteNonQuery();
                loadgrid();
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime d = Convert.ToDateTime(dateTimePicker1.Value.ToString());
            if (listBox1.SelectedItem==null)
            {
                label5.Visible = true;
            }
            else
            {
                string x = listBox1.SelectedItem.ToString();
                SqlConnection con = new SqlConnection(conconnect);
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "insert into PMPlans values ('" + x + "','" + d + "')";
                com.ExecuteNonQuery();
                loadgrid();
                label5.Visible = false;
                con.Close();
            }
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }
    }
}
