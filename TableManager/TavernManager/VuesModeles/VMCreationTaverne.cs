using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TavernManagerMetier.Data;
using TavernManagerMetier.Metier.Tavernes;
using TavernManagerMetier.Metier.Tavernes.Fabriques;

namespace TavernManager.VuesModeles
{
    /// <summary>
    /// Vue modèle de l'écran de création de taverne
    /// </summary>
    public class VMCreationTaverne
    {
        /// <summary>
        /// Nom de la taverne à créer
        /// </summary>
        public string Nom { get => nom; set => nom = value; }
        private string nom;
        /// <summary>
        /// Nombre de clients de la taverne à créer
        /// </summary>
        public string NombreClient { get => nombreClient; set => nombreClient = value; }
        private string nombreClient;
        /// <summary>
        /// Densité de relations de la taverne à créer
        /// </summary>
        public int Densite { get => densite; set => densite = value; }
        private int densite;

        /// <summary>
        /// Proporition relations amicales dans la taverne à créer
        /// </summary>
        public int Proportion { get => proportion; set => proportion = value; }

        private int proportion;

        /// <summary>
        /// Capacité des tables
        /// </summary>
        public string Capacite { get => capacite; set => capacite = value; }
        private string capacite;


        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VMCreationTaverne()
        {
            this.nom = "";
            this.nombreClient = "20";
            this.Densite = 25;
            this.proportion = 0;
            this.capacite = "10";
        }

        /// <summary>
        /// Création de la taverne
        /// </summary>
        public void CreationTaverne()
        {
            //Création
            Taverne taverne = new FabriqueTaverne().CreerTaverne(Convert.ToInt32(capacite), Convert.ToInt32(nombreClient), densite / 100.0, proportion / 100.0);
            //Sauvegarde
            new TaverneDAO().Sauver(taverne, nom);
        }
    }
}
