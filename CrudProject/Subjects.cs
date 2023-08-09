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

namespace CrudProject
{
    public partial class Subjects : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        int i = 0;

        dbconnection dbconn = new dbconnection();
        public Subjects()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.dbconnect());
        }

        private void Subjects_Load(object sender, EventArgs e)
        {

        }

        private void Subjects_Load_1(object sender, EventArgs e)
        {
            LoadRecord();
            dataGridView1.RowTemplate.Height = 25;
        }
        public void LoadRecord()
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new MySqlCommand("SELECT `teachid`, `teachname`, `subject` FROM `tb_subject`", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, dr["teachid"].ToString(), dr["teachname"].ToString(), dr["subject"].ToString());
            }
            dr.Close();
            conn.Close();
        }
        public void clear()
        {
            txt_teacherid.Clear();
            txt_teachername.Clear();
            txt_subject.Clear();
         

        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            if ((txt_teacherid.Text == String.Empty) || (txt_teachername.Text == String.Empty) || (txt_subject.Text == String.Empty))
            {
                MessageBox.Show("Warning: Required fill filled ?", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            else
            {
                
                conn.Open();
                cmd = new MySqlCommand("INSERT INTO `tb_subject`(`teachid`, `teachname`, `subject`) VALUES (@teachid,@teachname,@subject)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@teachid", txt_teacherid.Text);
                cmd.Parameters.AddWithValue("@teachname", txt_teachername.Text);
                cmd.Parameters.AddWithValue("@subject", txt_subject.Text);
            

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

        private void btn_update_Click(object sender, EventArgs e)
        {
            //Update
            conn.Open();
            cmd = new MySqlCommand("UPDATE `tb_subject` SET `teachname`=@teachname, `subject`=@subject WHERE `teachid`=@teachid", conn);
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@teachid", txt_teacherid.Text);
            cmd.Parameters.AddWithValue("@teachname", txt_teachername.Text);
            cmd.Parameters.AddWithValue("@subject", txt_subject.Text);
            

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

        private void btn_delete_Click(object sender, EventArgs e)
        {
            //Delete
            conn.Open();
            cmd = new MySqlCommand("DELETE From `tb_subject`  WHERE `teachid`=@teachid", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@teachid", txt_teacherid.Text);

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_teacherid.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_teachername.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txt_subject.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
          
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void txt_searchfunc_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new MySqlCommand("SELECT teachid, teachname, subject FROM tb_subject WHERE teachid like '%" + txt_searchfunc.Text + "%' or teachname like '%" + txt_searchfunc.Text + "%' or subject like '%" + txt_searchfunc.Text + "%'", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, dr["teachid"].ToString(), dr["teachname"].ToString(), dr["subject"].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var newForm = new Form1();
            newForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var newForm = new Teachers();
            newForm.Show();
            this.Hide();
        }
    }
}
