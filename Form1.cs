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


namespace Win
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-OI29NR8\SQLEXPRESS;Initial Catalog=cabinetMedecin;Integrated Security=True");
        SqlDataAdapter Da;
        DataTable Dt = new DataTable();
        CurrencyManager cm;
        SqlCommandBuilder cmdb;
        public Form1()
        {
            InitializeComponent();
            Da = new SqlDataAdapter("Select * From medecin", cn);
            Da.Fill(Dt);
            txtid.DataBindings.Add("Text", Dt, "id");
            txtnom.DataBindings.Add("Text", Dt, "nom");
            txtpre.DataBindings.Add("Text", Dt, "prenom");
            txtemail.DataBindings.Add("Text", Dt, "email");
            txttel.DataBindings.Add("Text", Dt, "telephone");
            DGVmedecin.Rows.Clear();
            //DGVmedecin.CurrentRow.Cells[0].Value = txtnom.Text;
            //DGVmedecin.CurrentRow.Cells[1].Value = txtpre.Text;
            //DGVmedecin.CurrentRow.Cells[2].Value = txtemail.Text;
            //DGVmedecin.CurrentRow.Cells[3].Value = txttel.Text;
            cm = (CurrencyManager)BindingContext[Dt];
            //cm = (CurrencyManager)BindingContext[DGVmedecin.DataSource];
            //cm.SuspendBinding();
            //cm.ResumeBinding();

            label1.Text = (cm.Position + 1) + " / " + (Dt.Rows.Count);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            foreach (DataRow dr in Dt.Rows)
            {
                DGVmedecin.Rows.Add(dr[1], dr[2], dr[3], dr[4]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cm.Position = 0;
            label1.Text = (cm.Position + 1) + " / " + (Dt.Rows.Count);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cm.Position -= 1;
            label1.Text = (cm.Position + 1) + " / " + (Dt.Rows.Count);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            cm.Position += 1;
            label1.Text = (cm.Position + 1) + " / " + (Dt.Rows.Count);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            cm.Position = Dt.Rows.Count;
            label1.Text = (cm.Position + 1) + " / " + (Dt.Rows.Count);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cm.AddNew();
            txtid.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cm.EndCurrentEdit();
            cmdb = new SqlCommandBuilder(Da);
            Da.Update(Dt);
            MessageBox.Show("Asseccufuly");
            label1.Text = (cm.Position + 1) + " / " + (Dt.Rows.Count);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cm.RemoveAt(cm.Position);
            cm.EndCurrentEdit();
            cmdb = new SqlCommandBuilder(Da);
            Da.Update(Dt);

            label1.Text = (cm.Position + 1) + " / " + (Dt.Rows.Count);
            MessageBox.Show("Delete");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cm.EndCurrentEdit();
            cmdb = new SqlCommandBuilder(Da);
            Da.Update(Dt);
            cm.Refresh();
            MessageBox.Show("Edit");
        }
    }
}
