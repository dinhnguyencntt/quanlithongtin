using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Text;
namespace quanlithongtin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAB1-MAY01\MISASME2022;Initial Catalog=quanlithongtin;Integrated Security=True;Encrypt=False");
        private void opencon()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        private void closecon()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        private Boolean Exe(string cmd)
        {
            opencon();
            Boolean check;
            try
            {
                SqlCommand sc = new SqlCommand(cmd, con);
                sc.ExecuteNonQuery();
                check = true;
            }
            catch (Exception)
            {
                check = false;
                throw;
            }
            closecon();
            return check;
        }
        private DataTable Red(string cmd)
        {
            opencon();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand sc = new SqlCommand(cmd, con);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                sda.Fill(dt);
            }
            catch (Exception)
            {
                dt = null;
                throw;
            }
            closecon();
            return dt;

        }
        private void load()
        {
            DataTable dt = Red("SELECT * FROM quanlithongtin");
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }    
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
        load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            maten.ResetText();
            hoten.ResetText();
            namsinh.ResetText();
            quequan.ResetText();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Exe ("INSERT INTO quanlithongtin(hoten,maten,namsinh,quequan) VALUES(N*"+maten.Text+ ",N*"+hoten.Text+ ",N*"+namsinh.Text+ ",N*"+quequan.Text+")");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Exe("UPDATE quanlithongtin SET maten=N'" + maten.Text + ",hoten=N'" + hoten.Text + ",N*" + namsinh.Text + ",N*" + quequan.Text + ")");
        }
    }
}
