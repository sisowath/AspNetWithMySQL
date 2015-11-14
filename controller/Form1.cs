using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;// IMPORTANT pour utiliser DbProviderFactory
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using model;
using MySql.Data.MySqlClient; // IMPORTANT pour indiquer à DbProviderFactories quelle base de données sera utilisée.
                                // ou pour établir une connexion à une BD MySQL

namespace controller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();// source de : http://stackoverflow.com/questions/4601827/how-do-i-center-a-window-onscreen-in-c
        }

        private void btnConnecter_Click(object sender, EventArgs e)
        {
            // source de : http://stackoverflow.com/questions/1216626/how-can-i-use-ado-net-dbproviderfactory-with-mysql
            /*DbProviderFactory dbpf = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");         
            using (DbConnection dbcn = dbpf.CreateConnection())
            {
                dbcn.ConnectionString = "Server=localhost;Database=katnisseverdeen;Uid=root;Pwd=root;";
                dbcn.Open();
                using (DbCommand dbcmd = dbcn.CreateCommand())
                {
                    dbcmd.CommandType = CommandType.Text;
                    dbcmd.CommandText = "SELECT * FROM user WHERE username LIKE " + txtUsername.Text;
                    using (DbDataReader dbrdr = dbcmd.ExecuteReader())
                    {
                        if (dbrdr.Read())
                        {
                            Utilisateur unUtilisateur = new Utilisateur();
                            int id = 0;
                            int.TryParse(dbrdr["id"].ToString(), out id);
                            unUtilisateur.Id = id;
                            unUtilisateur.Username = dbrdr["username"].ToString();
                            unUtilisateur.Password = dbrdr["password"].ToString();
                            if(unUtilisateur.Password.Equals(txtPassword.Text))
                                MessageBox.Show("Message", "BRAVO ! Vous êtes connecté.");
                            else
                                MessageBox.Show("Message", "ERREUR ! Le mot de passe est incorrect.");
                        } else {
                            MessageBox.Show("Message", "ERREUR ! Il n'existe aucun utilisateur { " + txtUsername.Text + " } ");
                        }
                    }
                }
            }*/
            
            // source de : https://www.youtube.com/watch?v=FPb9B7eoa9k
            // website name : http://www.babycenter.com/popular-baby-girl-names-2014
            MySqlConnection cnx = new MySqlConnection("server=localhost;user=root;password=root;database=katnisseverdeen");
            cnx.Open();
            MySqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM user WHERE username LIKE '" + txtUsername.Text.Trim() + "'";
            using (DbDataReader dbrdr = cmd.ExecuteReader())
            {
                if(dbrdr.Read())
                {
                    Utilisateur unUtilisateur = new Utilisateur();
                    int id = 0;
                    int.TryParse(dbrdr["id"].ToString(), out id);
                    unUtilisateur.Id = id;
                    unUtilisateur.Username = dbrdr["username"].ToString();
                    unUtilisateur.Password = dbrdr["password"].ToString();
                    unUtilisateur.Image = dbrdr["image"].ToString();
                    //MessageBox.Show(unUtilisateur.Image, "Info");
                    if (unUtilisateur.Password.Equals(txtPassword.Text.Trim()))
                    {
                        DialogResult res = MessageBox.Show("BRAVO ! Vous êtes connecté.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (res.Equals(DialogResult.OK))
                        {
                            // source de : https://msdn.microsoft.com/en-us/library/ws1btzy8(v=vs.90).aspx 
                            Form2 monForm2 = new Form2(unUtilisateur, this);
                            /*monForm2.User.Id = unUtilisateur.Id;
                            monForm2.User.Username = unUtilisateur.Username;
                            monForm2.User.Password = unUtilisateur.Password;*/
                            monForm2.Show();                            
                        }                        
                    }
                    else
                        MessageBox.Show("ERREUR ! Le mot de passe est incorrect.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("ERREUR ! Il n'existe aucun utilisateur { " + txtUsername.Text + " } ","Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            cnx.Close();
        }
    }
}
