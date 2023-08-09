using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace CrudProject
{

    public partial class Teachers : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        int i = 0;
        dbconnection dbconn = new dbconnection();

        public Teachers()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.dbconnect());
        }

        private void Teachers_Load(object sender, EventArgs e)
        {
            LoadRecord();
            dataGridView1.RowTemplate.Height = 25;
        }
        public void LoadRecord()
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new MySqlCommand("SELECT `teachid`, `teachname`, `class`, `dob`, `address`, `phone` FROM `tb_teacher`", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, dr["teachid"].ToString(), dr["teachname"], dr["class"].ToString(), dr["dob"], dr["address"].ToString(), dr["phone"].ToString());
            }
            dr.Close();
            conn.Close();
        }
        public void clear()
        {
            txt_teachid.Clear();
            txt_teachname.Clear();
            txt_teachaddress.Clear();
            txt_teachphone.Clear();
            cmb_teachclass.SelectedIndex = -1;
            dtp_teachdob.Value = DateTime.Now;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((txt_teachid.Text == String.Empty) || (txt_teachname.Text == String.Empty) || (cmb_teachclass.Text == String.Empty) || (dtp_teachdob.Text == String.Empty) || (txt_teachphone.Text == String.Empty) || (txt_teachaddress.Text == String.Empty))
            {
                MessageBox.Show("Warning: Required fill filled ?", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            else
            {
                string date1 = dtp_teachdob.Value.ToString("yyyy-MM-dd");
                conn.Open();
                cmd = new MySqlCommand("INSERT INTO `tb_teacher`(`teachid`, `teachname`, `class`, `dob`, `address`, `phone`) VALUES (@teachid,@teachname,@class,@dob,@address,@phone)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@teachid", txt_teachid.Text);
                cmd.Parameters.AddWithValue("@teachname", txt_teachname.Text);
                cmd.Parameters.AddWithValue("@class", cmb_teachclass.Text);
                cmd.Parameters.AddWithValue("@dob", date1);
                cmd.Parameters.AddWithValue("@phone", txt_teachphone.Text);
                cmd.Parameters.AddWithValue("@address", txt_teachaddress.Text);

                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Save Success !", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Save Failed !", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                conn.Close();
                LoadRecord();
                clear();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_teachid.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_teachname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cmb_teachclass.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dtp_teachdob.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txt_teachaddress.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txt_teachphone.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Update
            string date1 = dtp_teachdob.Value.ToString("yyyy-MM-dd");
            conn.Open();
            cmd = new MySqlCommand("UPDATE `tb_teacher` SET `teachname`=@teachname, `class`=@class, `dob`=@dob, `address`=@address, `phone`=@phone WHERE `teachid`=@teachid", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@teachname", txt_teachname.Text);
            cmd.Parameters.AddWithValue("@class", cmb_teachclass.Text);
            cmd.Parameters.AddWithValue("@dob", date1);
            cmd.Parameters.AddWithValue("@address", txt_teachaddress.Text);
            cmd.Parameters.AddWithValue("@phone", txt_teachphone.Text);
            cmd.Parameters.AddWithValue("@teachid", txt_teachid.Text);

            i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Update Success !", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Update Failed !", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            conn.Close();
            LoadRecord();
            clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var newForm = new Form1();
            newForm.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Delete
            conn.Open();
            cmd = new MySqlCommand("DELETE From `tb_teacher`  WHERE `teachid`=@teachid", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@teachid", txt_teachid.Text);

            i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Record Delete Success !", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Record Delete Failed !", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            conn.Close();
            LoadRecord();
            clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new MySqlCommand("SELECT teachid, teachname, class, dob, address, phone FROM tb_teacher WHERE teachid like '%" + txt_search.Text + "%' or teachname like '%" + txt_search.Text + "%' or class like '%" + txt_search.Text + "%' or dob like '%" + txt_search.Text + "%' or address like '%" + txt_search.Text + "%' or phone like '%" + txt_search.Text + "%'", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, dr["teachid"].ToString(), dr["teachname"].ToString(), dr["class"].ToString(), dr["dob"], dr["address"].ToString(), dr["phone"].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var newForm = new Subjects();
            newForm.Show();
            this.Hide();
        }
    }


}
