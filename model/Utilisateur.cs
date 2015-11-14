using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    public class Utilisateur
    {
            // attribut(s)
        private int id;
        private String username;
        private String password;
        private String image;
            // methode(s)
        // constructeur(s)
        public Utilisateur() : this(1, "no username", "no password", "no image") { }
        public Utilisateur(int id, String username, String password, String image)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.image = image;
        }
        // propriétés en lecture et en écriture
        public int Id
        {
            get { return this.id; }
            set
            {
                if(this.id != value)
                    this.id = value;
            }
        }
        public String Username
        {
            get { return this.username; }
            set
            {
                if (this.username != value)
                    this.username = value;
            }
        }
        public String Password
        {
            get { return this.password; }
            set
            {
                if (this.password != value)
                    this.password = value;
            }
        }
        public String Image
        {
            get { return this.image; }
            set
            {
                if (this.image != value)
                    this.image = value;
            }
        }
    }
}
