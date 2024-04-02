using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Data;
using TavernManagerMetier.Metier.Algorithmes;
using TavernManagerMetier.Metier.Algorithmes.Realisations;
using TavernManagerMetier.Metier.Tavernes;
using TavernManagerMetier.Metier.Tavernes.Fabriques;

namespace TavernManager.VuesModeles
{
    /// <summary>
    /// Vue modele de la fenetre principale
    /// </summary>
    public class VMFenetrePrincipale : INotifyPropertyChanged
    {
        //La taverne selectionnée
        private Taverne taverne;

        /// <summary>
        /// Liste des VM des clients
        /// </summary>
        public VMClient[] VMClients => vmClients.Values.ToArray();
        private Dictionary<Client,VMClient> vmClients;

        /// <summary>
        /// Liste des algorithmes
        /// </summary>
        public List<IAlgorithme> Algorithmes => algorithmes;
        private List<IAlgorithme> algorithmes;

        /// <summary>
        /// Liste des tables pour la treeView
        /// </summary>
        public List<VMTable> VMTables => vmTables;
        private List<VMTable> vmTables;

        /// <summary>
        /// Algorithme sélectionné
        /// </summary>
        public IAlgorithme AlgorithmeSelectionne
        {
            get => algorithmeSelectionne;
            set
            {
                algorithmeSelectionne = value;
            }
        }
        private IAlgorithme algorithmeSelectionne;

        /// <summary>
        /// Temps d'exécution de l'algorithme
        /// </summary>
        public long TempsExecution => this.algorithmeSelectionne.TempsExecution;

        /// <summary>
        /// Liste des tavernes
        /// </summary>
        public string[] Tavernes
        {
            get
            {
                string[] files = Directory.GetFiles("Tavernes/", "*.tvn");
                for (int i=0;i<files.Length;i++) files[i] = files[i].Substring(9, files[i].Length-4-9);
                return files;
            }
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VMFenetrePrincipale()
        {

            //Initialisation
            this.vmClients = new Dictionary<Client, VMClient>();
            this.algorithmes = new List<IAlgorithme>();
            this.vmTables = new List<VMTable>();

            //Liste les algorithmes
            this.ListerLesAlgorithmes();
        }

        /// <summary>
        /// Liste les algorithmes
        /// </summary>
        public void ListerLesAlgorithmes()
        {
            this.algorithmes.AddRange(new AlgorithmeManager().ListeDesAlgorithmes());
        }

        /// <summary>
        /// Chargement de la taverne
        /// </summary>
        /// <param name="nomFichier">Nom du fichier de la taverne à charger</param>
        public void ChargementTaverne(string nomFichier)
        {
            this.ClearTaverne();
            this.taverne = new TaverneDAO().Charger(nomFichier);

            //Création des VM des clients
            foreach (Client client in this.taverne.Clients) this.vmClients[client] = new VMClient(client);

            //Création des VM des relations
            foreach (Client client in this.taverne.Clients)
            {
                foreach (Client amis in client.Amis) vmClients[client].AddRelation(new VMRelation(vmClients[client], vmClients[amis], true));
                foreach (Client ennemis in client.Ennemis) vmClients[client].AddRelation(new VMRelation(vmClients[client], vmClients[ennemis], false));
            }
        }

        /// <summary>
        /// Update la liste des tavernes
        /// </summary>
        public void UpdateListeTavernes()
        {
            this.NotifyPropertyChanged("Tavernes");
        }

        /// <summary>
        /// Supprime la taverne en cours
        /// </summary>
        public void ClearTaverne()
        {
            this.taverne = null;
            this.vmClients.Clear();
            this.vmTables.Clear();
            this.NotifyPropertyChanged("VMTables");
        }

        /// <summary>
        /// Lance l'algorithme sélectionné sur la taverne sélectionnée
        /// </summary>
        public void LancerAlgorithme()
        {
            if (this.algorithmeSelectionne != null && this.taverne != null)
            {
                //On lance l'algo
                this.algorithmeSelectionne.Executer(this.taverne);

                //On met à jour les VMTables
                this.vmTables.Clear();
                foreach(Table table in taverne.Tables)
                {
                    //Création de la table
                    VMTable vmTable = new VMTable(table);
                    //Ajout des clients
                    foreach(Client client in table.Clients) vmTable.AjouterVMClient(this.vmClients[client]);
                    //Ajout de la table à la liste
                    this.vmTables.Add(vmTable);
                }
                this.NotifyPropertyChanged("VMTables");
                this.NotifyPropertyChanged("TempsExecution");
            }
        }


        //Pattern d'observation
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
