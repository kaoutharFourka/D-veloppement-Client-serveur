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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //créer une connexion vers le serveur ainsi que la base de données
        SqlConnection cnx = new SqlConnection(@"Data Source=karim-vaio\sqlexpress;
   Initial Catalog=GestionCommandes;Integrated Security=True");

        //bouton ajouter
        private void button1_Click(object sender, EventArgs e)
        {
        
            // ouvrir la connexion cnx
              cnx.Open();

            //créer la requete pour ajouter le produit dans la base de données
     SqlCommand cmd = new SqlCommand("insert into produit values('" +
       textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text +
                  "','" + textBox4.Text + "')", cnx);

            //executer la requete
              cmd.ExecuteNonQuery();

            //fermer la   connexion
              cnx.Close();

              button2_Click(sender, e);
              Form1_Load(sender, e);
        }
        //suppression
            private void button3_Click(object sender, EventArgs e)
           {
            cnx.Open();
            SqlCommand cmd = new SqlCommand(" delete produit where numProd='" +textBox1.Text+"'",cnx);
            cmd.ExecuteNonQuery();
            cnx.Close();
            MessageBox.Show("Suppression terminée");

            button2_Click(sender, e);//pour annuler
            Form1_Load(sender, e); //pour actualiser
             }

          //au chargement du formulaire
            private void Form1_Load(object sender, EventArgs e)
            {
                cnx.Open();             
                SqlCommand cmd = new SqlCommand("select * from produit ", cnx);
             //stocker le resultat de selection (lA LISTE DES PRODUITS) dans la varaible dr
                SqlDataReader dr=   cmd.ExecuteReader();
                DataTable dt= new DataTable ();
                dt.Load (dr);
                dataGridView1.DataSource = dt;
                 cnx.Close();
            }
            private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
            {
         //i est l'indice de de la ligne contenant la cellule sélectionée
        int i= dataGridView1.SelectedCells[0].RowIndex ;
    textBox1 .Text =dataGridView1 .Rows[i].Cells[0].Value.ToString ();
    textBox2.Text=dataGridView1 .Rows[i].Cells[1].Value.ToString ();
    textBox3.Text=dataGridView1 .Rows[i].Cells[2].Value.ToString ();
    textBox4.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            }

            private void button2_Click(object sender, EventArgs e)
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("update produit set nomProd='" + textBox2.Text + "', prixUnitaire='" + textBox3.Text + "',qteStock='" + textBox4.Text + "' where numProd='" + textBox1.Text + "'", cnx);
                cmd.ExecuteNonQuery();
                cnx.Close();
                button2_Click(sender, e);
                Form1_Load(sender, e);
            }
        //premier produit
            private void button8_Click(object sender, EventArgs e)
            {
                afficher(0);
            }

            private void button5_Click(object sender, EventArgs e)
            {

                int c = dataGridView1.Rows.Count - 1;
                afficher(c);
            }
        //précédent
            private void button7_Click(object sender, EventArgs e)
            {int p=-1;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (textBox1.Text == dataGridView1.Rows[i].Cells[0].Value.ToString())
                        p = i;                        
                }
                if (p > 0)
                {
                    afficher(p - 1);
                }


            }
        //bouton suivant
            private void button6_Click(object sender, EventArgs e)
            {
                int p = -1;
                int c = dataGridView1.Rows.Count;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (textBox1.Text == dataGridView1.Rows[i].Cells[0].Value.ToString())
                        p = i;
                }
                if (p < c-1)
                {
                    afficher(p + 1);
               }
            }
            public void afficher(int x)
            {
                textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[x].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[x].Cells[3].Value.ToString();

            }

         

    }
}
