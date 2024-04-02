using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes.Graphes
{
    /// <summary>
    /// Classe des graphes
    /// </summary>
    public class Graphe
    {
        /// <summary>
        /// propriété renvoyant la liste des sommets du graphe
        /// </summary>
        public List<Sommet> Sommets => this.sommets.Values.Distinct().ToList<Sommet>();

        /// <summary>
        /// Dictionnaire des sommet indexé par client
        /// </summary>
        private Dictionary<Client, Sommet> sommets;

        /// <summary>
        /// Constructeur de graphe
        /// </summary>
        /// <param name="taverne">taverne associés</param>
        public Graphe(Taverne taverne)
        {
            //Initialisation du dictionnaire
            this.sommets = new Dictionary<Client, Sommet>();

            //Pour chaque client de la taverne, ajoutez un sommet à votre graphe
            foreach (Client client in taverne.Clients)
            {
                this.AjouterSommet(client, new Sommet());
            }

            //Pour chaque client, pour chacun de ses ennemis, ajoutez une arête entre le sommet du client et le sommet de l’ennemi.
            foreach (Client client in taverne.Clients)
            {
                foreach (Client ennemi in client.Ennemis)
                {
                    AjouterArete(client, ennemi);
                }
            }
        }

        /// <summary>
        /// Ajoute un sommet au graphe et ajoute un groupe d'amis au sommet
        /// </summary>
        /// <param name="client">client indexant le sommet ajouté</param>
        private void AjouterSommet(Client client, Sommet sommet)
        {
            if (!this.sommets.ContainsKey(client))
            {
                this.sommets[client] = sommet;
                sommet.NbClients++;
                foreach(Client ami in client.Amis) this.AjouterSommet(ami,sommet);      
            }           
        }

        /// <summary>
        /// Ajoute une arete entre deux clients(sommets) (on ajoute un voisin)
        /// </summary>
        /// <param name="client1">Premier client (sommet)</param>
        /// <param name="client2">Deuxieme client (sommet)</param>
        private void AjouterArete(Client client1, Client client2)
        {
            this.sommets[client1].AjouterVoisin(sommets[client2]);
        }

        /// <summary>
        /// Renvoi le sommet associer au client (en entrée)
        /// </summary>
        /// <param name="client">client dont on cherche le sommet</param>
        /// <returns>le sommet du client donnée</returns>
        public Sommet GetSommetOfClient(Client client)
        {
            return (this.sommets[client]);
        }
    }
}
