using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Exceptions.Realisations.GestionDesTables;

namespace TavernManagerMetier.Metier.Tavernes
{
    /// <summary>
    /// Une table pouvant accueillir des clients
    /// </summary>
    public class Table
    {
        /// <summary>
        /// Ensemble des clients assis à cette table
        /// </summary>
        public Client[] Clients => clients.ToArray();
        private List<Client> clients;

        /// <summary>
        /// Nombre de places à la table
        /// </summary>
        public int Capacite => capacite;
        private int capacite;

        /// <summary>
        /// Nombre de clients assis à la table
        /// </summary>
        public int NombreClients => this.clients.Count;

        /// <summary>
        /// La table est-elle pleine
        /// </summary>
        public bool EstPleine => this.NombreClients == capacite;

        /// <summary>
        /// Numéro de la table
        /// </summary>
        public int Numero => numero;
        private int numero;

        /// <summary>
        /// La table est-elle valide (pas deux ennemis ensemble)
        /// </summary>
        public bool EstValide
        {
            get
            {
                bool valide = true;
                foreach(Client client1 in this.clients)
                {
                    if(valide)
                    {
                        foreach (Client client2 in this.clients)
                        {
                            if (valide)
                            {
                                if (client1.EstEnnemisAvec(client2)) valide = false;
                            }
                        }
                    }
                }
                return valide;
            }
        }

        /// <summary>
        /// Constructeur par défaut (table vide)
        /// </summary>
        /// <param name="capacite">Capacité de la table</param>
        public Table(int capacite,int numero)
        {
            this.clients = new List<Client>();
            this.capacite = capacite;
            this.numero = numero;
        }

        /// <summary>
        /// Ajoute si possible un client à la table
        /// </summary>
        /// <param name="client">Client à ajouter à la table</param>
        /// <exception cref="ExceptionTablePleine">Exception levée si la table est déjà pleine</exception>
        public void AjouterClient(Client client)
        {
            //Si la table est pleine on lève une exception
            if (this.EstPleine) throw new ExceptionTablePleine();
            //On ajoute le client à la liste des clients
            this.clients.Add(client);
        }

        /// <summary>
        /// Enlève un client de la table
        /// </summary>
        /// <param name="client">Le client à enlever</param>
        /// <exception cref="ExceptionClientNonPresentTable">Si le client n'est pas à cette table</exception>
        public void EnleverClient(Client client)
        {
            //Si le client n'est pas à cette table on lève une exception
            if (!this.clients.Contains(client)) throw new ExceptionClientNonPresentTable(client);
            //On enlève le client de la table
            this.clients.Remove(client);
        }

    }
}
