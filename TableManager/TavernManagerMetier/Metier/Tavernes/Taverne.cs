using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Exceptions.Realisations.GestionTaverne;

namespace TavernManagerMetier.Metier.Tavernes
{

    public class Taverne
    {
        /// <summary>
        /// Liste des clients de la taverne
        /// </summary>
        public Client[] Clients => clients.ToArray();
        private List<Client> clients;

        /// <summary>
        /// Liste des tables de la taverne
        /// </summary>
        public Table[] Tables => tables.ToArray();
        private List<Table> tables;

        //Capacite des tables
        public int CapactieTables => capaciteTables;
        private int capaciteTables;

        /// <summary>
        /// Nombre de table de la taverne
        /// </summary>
        public int NombreTables => tables.Count;

        /// <summary>
        /// Constructeur d'une taverne vide avec des tables
        /// </summary>
        /// <param name="capaciteTable">Capacité des tables de la taverne</param>
        public Taverne(int capaciteTable, List<Client> listeClients)
        {
            this.clients = listeClients;
            this.capaciteTables = capaciteTable;
            this.tables = new List<Table>();
        }

        /// <summary>
        /// Ajoute une nouvelle table
        /// </summary>
        public void AjouterTable()
        {
            this.tables.Add(new Table(this.capaciteTables,this.tables.Count));
        }

        /// <summary>
        /// Ajoute un client à une table
        /// </summary>
        /// <param name="numeroClient">Numéro du client</param>
        /// <param name="numeroTable">Numéro de la table</param>
        /// <exception cref="ExceptionNumeroClientInconnu">Levée si le client n'existe pas</exception>
        /// <exception cref="ExceptionNumeroTableInconnu">Levée si la table n'existe pas</exception>
        public void AjouterClientTable(int numeroClient, int numeroTable)
        {
            //On lève une exception si le numéro client n'a pas de sens
            if (numeroClient < 0 || numeroClient > this.clients.Count || this.clients[numeroClient] == null) throw new ExceptionNumeroClientInconnu(numeroClient);
            //On lève une exception si le numéro de table n'a pas de sens
            if (numeroTable < 0 || numeroTable > this.tables.Count || this.tables[numeroTable] == null) throw new ExceptionNumeroTableInconnu(numeroTable);

            //On change le client de table
            this.clients[numeroClient].ChangerTable(this.tables[numeroTable]);
        }


        /// <summary>
        /// Ajoute un ensemble de clients à une table
        /// </summary>
        /// <param name="clients">Ensemble de client (d'amis) qu'on mets à la même table</param>
        /// <param name="numeroTable">table à laquelle on mets tous les amis</param>
        /// <exception cref="ExceptionNumeroClientInconnu">Levée si le client n'existe pas</exception>
        /// <exception cref="ExceptionNumeroTableInconnu">Levée si la table n'existe pas</exception>
        public void AjouterClientTable( List<Client> clients, int numeroTable)
        {
            foreach (Client client in clients)
            {
                //On lève une exception si le numéro client n'a pas de sens
                if (client.Numero < 0 || client.Numero > this.clients.Count || this.clients[client.Numero] == null) throw new ExceptionNumeroClientInconnu(client.Numero);
                //On lève une exception si le numéro de table n'a pas de sens
                if (numeroTable < 0 || numeroTable > this.tables.Count || this.tables[numeroTable] == null) throw new ExceptionNumeroTableInconnu(numeroTable);

                //On change le client de table
                this.clients[client.Numero].ChangerTable(this.tables[numeroTable]);
            }

        }

    }
}
