using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using model;
using System.Data.Common; // IMPORTANT pour utiliser DbDataReader et autres
using MySql.Data.MySqlClient; // IMPORTANT pour utiliser MySQL

namespace controller
{
    public partial class Form2 : Form
    {
        private Utilisateur unUtilisateur;
        private Form1 fenetrePrecedente;

        public Utilisateur User
        {
            get { return this.unUtilisateur; }
            set
            {
                if (this.unUtilisateur != value)
                    this.unUtilisateur = value;
            }
        }

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Utilisateur unUtilisateur)
        {
            InitializeComponent();
            this.unUtilisateur = unUtilisateur;
        }
        public Form2(Utilisateur unUtilisateur, Form1 maPremiereForm)
        {
            InitializeComponent();
            this.unUtilisateur = unUtilisateur;
            this.fenetrePrecedente = maPremiereForm;
            this.CenterToScreen();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            
            //MessageBox.Show(unUtilisateur.Image, "Info2");
            this.txtId.Text = unUtilisateur.Id.ToString();
            this.txtUsername.Text = unUtilisateur.Username;
            this.txtPassword.Text = unUtilisateur.Password;
            this.txtId.ReadOnly = true;
                       
            // la ligne suivante est pour une image par défaut
            //this.pbxPhoto.Image = Image.FromFile(@"images\ares.jpg");// source de : http://stackoverflow.com/questions/17193825/loading-picturebox-image-from-resource-file-with-path-part-3
            // ATTENTION => Le dossier images doit se trouver dans nom_du_projet/bin/Debug !!!                        
            this.pbxPhoto.Image = Image.FromFile(unUtilisateur.Image);
            this.pbxPhoto.SizeMode = PictureBoxSizeMode.Zoom;
            this.pbxPhoto.Visible = true;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*Form1 monForm1 = new Form1();
            monForm1.Show();*/
            this.fenetrePrecedente.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // source de : http://www.c-sharpcorner.com/UploadFile/9582c9/insert-update-delete-display-data-in-mysql-using-C-Sharp/
            MySqlConnection cnx = new MySqlConnection("server=localhost;user=root;password=root;database=katnisseverdeen");
            cnx.Open();
            String query = "UPDATE user SET username = '" + txtUsername.Text + "', password = '" + txtPassword.Text + "' WHERE id = " + txtId.Text;
            MySqlCommand cmd = new MySqlCommand(query, cnx);
            int nombreDeLignesModifiees = cmd.ExecuteNonQuery();// source de : http://www.aymericlagier.com/2010/01/29/introduction-a-ado-net-en-c/
            if(nombreDeLignesModifiees > 0 )
                MessageBox.Show("BRAVO ! Vos informations ont été mises à jour.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
            else
                MessageBox.Show("ERREUR ! Je ne peux pas mettre à jour vos informations.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
            cnx.Close();
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "image seulement | *.jpg; *.jpeg; *.bmp";
            DialogResult res = openFileDialog1.ShowDialog();// source de : http://www.dotnetperls.com/openfiledialog
            if (res == DialogResult.OK)
            {
                String nomFichier = openFileDialog1.FileName;
                MessageBox.Show(nomFichier, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(nomFichier.Contains(".jpg")) 
                    MessageBox.Show("C'est une image !");
                else 
                    MessageBox.Show("Ce ne pas une image !");
                // copier-coller le fichier au dossier images 
                // source de :  https://msdn.microsoft.com/library/cc148994(v=vs.100).aspx
                String sourceFile = openFileDialog1.FileName;
                String destinationFile = unUtilisateur.Image;
                //System.IO.File.Move(sourceFile, destinationFile);
            }
        }
    }
}
