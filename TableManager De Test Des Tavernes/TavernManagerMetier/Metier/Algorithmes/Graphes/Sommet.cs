using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes.Graphes
{
    /// <summary>
    /// Classe sommet
    /// </summary>
    public class Sommet
    {
        /// <summary>
        /// nombre de client
        /// </summary>
        private int nbClient;

        /// <summary>
        /// Nombre de client (Get/set)
        /// </summary>
        public int NbClients
        {
            get { return nbClient; }
            set { nbClient = value; }
        }

        /// <summary>
        /// liste de voisins du sommet
        /// </summary>
        private List<Sommet> voisins;


        /// <summary>
        /// liste de voisins du sommet (get/set)
        /// </summary>
        public List<Sommet> Voisins
        { 
            get { return voisins; } 
            set { voisins = value; }
        }
        
        /// <summary>
        /// Constructeur de sommet
        /// </summary>
        public Sommet()
        {
            voisins = new List<Sommet>();
            nbClient = 0;
        }

        /// <summary>
        /// Permet d'ajouter un voisin a un sommet
        /// </summary>
        /// <param name="sommet">Sommet à ajouter</param>
        public void AjouterVoisin(Sommet sommet)
        {
            voisins.Add(sommet);
        }

    }
}
