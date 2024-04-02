using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Metier.Tavernes
{
    /// <summary>
    /// Un client de la taverne devant être placé sur une table
    /// </summary>
    public class Client : INotifyPropertyChanged
    {
        /// <summary>
        /// Nom du client
        /// </summary>
        public string Nom => nom;
        private string nom;

        /// <summary>
        /// Prénom du client
        /// </summary>
        public string Prenom => prenom;
        private string prenom;

        /// <summary>
        /// Est-ce qu'il est dans un groupe d'amis
        /// </summary>
        public bool EstDansUnGroupeAmis
        {
            get { return estDansUnGroupeAmis; }
            set { estDansUnGroupeAmis = value; }
        }
        private bool estDansUnGroupeAmis;

        /// <summary>
        /// Liste des amis du client
        /// </summary>
        /// 
        public List<Client> Amis => amis.Keys.ToList();
        private Dictionary<Client,bool> amis;

        /// <summary>
        /// Liste des ennemis du client
        /// </summary>
        public List<Client> Ennemis => ennemis.Keys.ToList();
        private Dictionary<Client,bool> ennemis;

        /// <summary>
        /// Numéro du client dans la taverne
        /// </summary>
        public int Numero => numero;
        private int numero;


        /// <summary>
        /// Table à laquelle le client est assis
        /// </summary>
        public Table Table => table;
        private Table table;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="prenom">Prénom du client</param>
        /// <param name="nom">Nom du client</param>
        /// <param name="numero">numero du client</param>
        public Client(string prenom,string nom,int numero)
        {
            this.prenom = prenom;
            this.nom = nom;
            this.table = null;
            this.numero = numero;
            this.amis = new Dictionary<Client, bool>();
            this.ennemis = new Dictionary<Client, bool>();
            this.estDansUnGroupeAmis = false;
        }

        /// <summary>
        /// Change le client de table
        /// </summary>
        /// <param name="nouvelleTable">Nouvelle table pour le client</param>
        public void ChangerTable(Table nouvelleTable)
        {
            //Si le client était déjà à une table on l'enlève de celle-ci
            if (this.table != null) this.table.EnleverClient(this);
            //On enlève le client de la table de départ (important en cas d'impossibilité de l'ajouter à la table d'arrivée)
            this.table = null;
            //Si le client a une table, alors on l'ajoute à celle-ci
            if(nouvelleTable != null) nouvelleTable.AjouterClient(this);
            //On change sa table
            this.table = nouvelleTable;
            this.NotifyPropertyChanged("Table");
        }

        /// <summary>
        /// Affichage de la personne
        /// </summary>
        /// <returns>prenom nom</returns>
        public override string ToString()
        {
            return this.prenom + " " + this.nom;
        }

        /// <summary>
        /// Egalité pour les clients : même nom et même prénom
        /// </summary>
        /// <param name="obj">Objet comparé</param>
        /// <returns>Est ce que obj est égal au client en cours</returns>
        public override bool Equals(object obj)
        {
            return obj is Client client &&
                   nom == client.nom &&
                   prenom == client.prenom;
        }

        /// <summary>
        /// Hashcode de la classe
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode()
        {
            int hashCode = -442594267;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(nom);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(prenom);
            return hashCode;
        }

        /// <summary>
        /// Ajoute un ami au client
        /// </summary>
        /// <param name="ami">L'ami du client</param>
        public void AjouterAmis(Client ami)
        {
            this.amis[ami] = true; ;
        }

        /// <summary>
        /// Ajoute un ennemis au client
        /// </summary>
        /// <param name="ami">L'ennemis du client</param>
        public void AjouterEnnemis(Client ennemis)
        {
            this.ennemis[ennemis] = true;
        }

        /// <summary>
        /// Est-il ami avec le client 
        /// </summary>
        /// <param name="client">le client</param>
        /// <returns>Oui ou non</returns>
        public bool EstAmisAvec(Client client)
        {
            return this.amis.ContainsKey(client);
        }


        /// <summary>
        /// Est-il ennemis avec le client 
        /// </summary>
        /// <param name="client">le client</param>
        /// <returns>Oui ou non</returns>
        public bool EstEnnemisAvec(Client client)
        {
            return this.ennemis.ContainsKey(client);
        }

        /// <summary>
        /// renvoi s'il est a la table d'un de ses ennemies
        /// </summary>
        public bool EstAvecUnEnnemis
        {
            get
            {
                bool valide = false;
                if (this.AsUneTable)
                {
                    foreach (Client client in this.Ennemis)
                    {
                        if (client.table == this.table) valide = true;
                    }
                }
                return valide;
            }
        }


        /// <summary>
        /// Le client est-il avec ces amis (et a une table)
        /// </summary>
        public bool EstAvecCesAmis
        {
            get
            {
                bool valide = this.Amis.Count == 0;
                if(this.AsUneTable)
                {
                    valide = true;
                    foreach (Client client in this.Amis)
                    {
                        if (client.table != this.table) valide = false;
                    }
                }
                return valide;
            }
        }

        /// <summary>
        /// Le client a-t-il une table 
        /// </summary>
        public bool AsUneTable
        {
            get => this.table != null;
        }

        //Pattern d'observation
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
