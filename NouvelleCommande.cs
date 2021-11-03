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
namespace demoTDI202
{
    public partial class NouvelleCommande : Form
    {
        public NouvelleCommande()
        {
            InitializeComponent();
        }
        SqlConnection cnx = new SqlConnection(@"Data Source=karim-vaio\sqlexpress;
             Initial Catalog=GestionCommandes;Integrated Security=True");
        int d = 0;
        private void NouvelleCommande_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            cnx.Open();
            SqlCommand cmd = new SqlCommand(" select * from client  ", cnx);
         SqlDataReader  dr=   cmd.ExecuteReader();
       DataTable dt= new DataTable ();
            dt.Load (dr);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "CIN";//la colonne à afficher
            cnx.Close();
            comboBox1.Text = "";


            cnx.Open();
            SqlCommand cmd2 = new SqlCommand(" select * from produit  ", cnx);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(dr2);
            comboBox2.DataSource = dt2;
            comboBox2.DisplayMember = "nomProd";//la colonne à afficher  combobox2.Text
            comboBox2.ValueMember = "numProd";//valeur de chaque produit   combobox2.SelectedValue
            cnx.Close();
            comboBox2.Text = "";


            d = 1;//on a terminé le remplissage du combobox          
        }
        //lorsqu'on choisit un CIN différent
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (d == 1)
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("select *from client where CIN='" + comboBox1.Text +
                    "'", cnx);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();             
                label4.Text = dr[1].ToString() + " " + dr[2].ToString() + "  " + dr[4].ToString();
          cnx.Close();     }
        }
         private void button1_Click(object sender, EventArgs e)
        {
            cnx.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("insert into commande values( '" + textBox1.Text + 
                    "','" + dateTimePicker1.Value + "','" + comboBox1.Text + "')", cnx);
                cmd.ExecuteNonQuery();
 MessageBox.Show("commande ajoutée avec succès");
            }
            catch (Exception ex)
            {                MessageBox.Show(ex.Message);     }
                cnx.Close();
            }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
           

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (d == 1)
            {  cnx.Open();
 SqlCommand cmd = new SqlCommand("select  prixUnitaire from produit where nomProd='" + 
     comboBox2.Text +
                    "'", cnx);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                textBox2.Text = dr[0].ToString();
                cnx.Close();
            }
        }
        float totalCom = 0;
        private void button2_Click(object sender, EventArgs e)
        { cnx.Open();
            try
            {
 SqlCommand cmd = new SqlCommand("insert into ligneCommande values('" + textBox1.Text + "','" +
     comboBox2.SelectedValue + "','" + numericUpDown1.Value + "')", cnx);
                cmd.ExecuteNonQuery();
                MessageBox.Show("produit ajouté à la commande");
dataGridView1.Rows.Add(comboBox2.SelectedValue, comboBox2.Text, textBox2.Text, numericUpDown1.Value,
    int.Parse(textBox2.Text) * numericUpDown1.Value);
    totalCom = totalCom + float.Parse((int.Parse(textBox2.Text) * numericUpDown1.Value).ToString());
                textBox3.Text = totalCom.ToString();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
           cnx.Close();          
        }
    }
}
