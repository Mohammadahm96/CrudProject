namespace CrudProject
    
{
    using Microsoft.Win32.SafeHandles;
    using MySql.Data.MySqlClient;
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        int i = 0;

        dbconnection dbconn = new dbconnection();
        public Form1()
        {
            InitializeComponent();
            conn = new MySqlConnection(dbconn.dbconnect());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRecord();
            dataGridView1.RowTemplate.Height = 25;
        }
        public void LoadRecord()
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new MySqlCommand("SELECT `stuid`, `stuname`, `fathername`, `class`, `dob`, `address`, `phone` FROM `tb_students`", conn);
            dr= cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, dr["stuid"].ToString(), dr["stuname"].ToString(), dr["fathername"].ToString(), dr["class"].ToString(), dr["dob"], dr["address"].ToString(), dr["phone"].ToString());
            }
            dr.Close();
            conn.Close();
        }

        public void clear()
        {
            txt_Address.Clear();
            txt_FatherName.Clear();
            txt_phone.Clear();
            txt_studID.Clear();
            txt_stuName.Clear();
            cbo_Class.SelectedIndex = -1;
            dtb_DOB.Value = DateTime.Now;

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if ((txt_studID.Text == String.Empty) || (txt_stuName.Text == String.Empty) || (txt_FatherName.Text == String.Empty) || (txt_Address.Text == String.Empty) || (txt_phone.Text == String.Empty) || (cbo_Class.Text == String.Empty))
            {
                MessageBox.Show("Warning: Required fill filled ?","CRUD",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;

            }
            else
            {
                string date1 = dtb_DOB.Value.ToString("yyyy-MM-dd");
                conn.Open();
                cmd = new MySqlCommand("INSERT INTO `tb_students`(`stuid`, `stuname`, `fathername`, `class`, `dob`, `address`, `phone`) VALUES (@stuid,@stuname,@fathername,@class,@dob,@address,@phone)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@stuid", txt_studID.Text);
                cmd.Parameters.AddWithValue("@stuname", txt_stuName.Text);
                cmd.Parameters.AddWithValue("@fathername", txt_FatherName.Text);
                cmd.Parameters.AddWithValue("@class", cbo_Class.Text);
                cmd.Parameters.AddWithValue("@dob", date1);
                cmd.Parameters.AddWithValue("@address", txt_Address.Text);
                cmd.Parameters.AddWithValue("@phone", txt_phone.Text);

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

        private void btn_Update_Click(object sender, EventArgs e)
        {
            //Update
            string date1 = dtb_DOB.Value.ToString("yyyy-MM-dd");
            conn.Open();
            cmd = new MySqlCommand("UPDATE `tb_students` SET `stuname`=@stuname, `fathername`=@fathername, `class`=@class, `dob`=@dob, `address`=@address, `phone`=@phone WHERE `stuid`=@stuid", conn);
            cmd.Parameters.Clear();
      
            cmd.Parameters.AddWithValue("@stuname", txt_stuName.Text);
            cmd.Parameters.AddWithValue("@fathername", txt_FatherName.Text);
            cmd.Parameters.AddWithValue("@class", cbo_Class.Text);
            cmd.Parameters.AddWithValue("@dob", date1);
            cmd.Parameters.AddWithValue("@address", txt_Address.Text);
            cmd.Parameters.AddWithValue("@phone", txt_phone.Text);
            cmd.Parameters.AddWithValue("@stuid", txt_studID.Text);

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

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            //Delete
            conn.Open();
            cmd = new MySqlCommand("DELETE From `tb_students`  WHERE `stuid`=@stuid", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@stuid", txt_studID.Text);

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_studID.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_stuName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txt_FatherName.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            cbo_Class.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dtb_DOB.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txt_Address.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txt_phone.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new MySqlCommand("SELECT stuid, stuname, fathername, class, dob, address, phone FROM tb_students WHERE stuid like '%" + txt_search.Text + "%' or stuname like '%" + txt_search.Text + "%' or fathername like '%" + txt_search.Text + "%' or class like '%" + txt_search.Text + "%' or dob like '%" + txt_search.Text + "%' or address like '%" + txt_search.Text + "%' or phone like '%" + txt_search.Text + "%'", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, dr["stuid"].ToString(), dr["stuname"].ToString(), dr["fathername"].ToString(), dr["class"].ToString(), dr["dob"], dr["address"].ToString(), dr["phone"].ToString());
            }
            dr.Close();
            conn.Close();
        }
    

        private void button2_Click(object sender, EventArgs e)
        {
            var newForm = new Teachers();
            newForm.Show();
            this.Hide();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var newForm = new Subjects();
            newForm.Show();
            this.Hide();
        }

    }
}