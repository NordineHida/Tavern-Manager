using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Exceptions.Realisations;
using TavernManagerMetier.Metier.Algorithmes.Graphes;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes.Realisations
{    
    /// <summary>
    /// Algorithme DSatur (utilisant des graphes)
    /// </summary>
    public class AlgorithmeDSatur : IAlgorithme
    {
        /// <summary>
        /// Nom de l'algorithme
        /// </summary>
        public string Nom => "DSatur";

        /// <summary>
        /// Temps d'execution de l'algorithme (initialiser a -1 dans le constructeur)
        /// </summary>
        private long tempsExecution;

        /// <summary>
        /// Renvoie le temps d'exécution de l'algorithme
        /// </summary>
        public long TempsExecution { get { return tempsExecution; } set { tempsExecution = value; } }


        /// <summary>
        /// Exécute l'algorithme sur la taverne donnée
        /// </summary>
        public void Executer(Taverne taverne)
        {
            //Debut de la mesure de la durée de l'algorithme
            Stopwatch stopwatch = Stopwatch.StartNew();



            //Creation du graphe a partir de la taverne ----------------------------------------
            Graphe graphe = new Graphe(taverne);

            //Dictionnaire qui associe une Table (int) à un client
            Dictionary<Sommet, int> SommetEtLeurCouleur = new Dictionary<Sommet, int>();

            // Dictionnaire de saturation pour chaque sommet
            Dictionary<Sommet, int> saturationSommet = new Dictionary<Sommet, int>();

            // Étape 1: Initialisation
            foreach (Sommet sommet in graphe.Sommets)
            {
                SommetEtLeurCouleur[sommet] = -1; // -1 indique que le sommet n'est pas encore coloré
                saturationSommet[sommet] = 0;
            }

            // Étape 2: Sélection du sommet de départ avec le plus haut degré
            Sommet sommetDepart = graphe.Sommets.OrderByDescending(s => s.Voisins.Count).First();

            // Étape 3: Coloration du sommet de départ
            SommetEtLeurCouleur[sommetDepart] = 0; // Assigner la couleur 0 au sommet de départ

            // Mise à jour de la saturation des voisins du sommet de départ
            foreach (Sommet voisin in sommetDepart.Voisins)
            {
                if (SommetEtLeurCouleur[voisin] != -1)
                    saturationSommet[voisin]++;
            }

            //On vérifie si la taverne remplis la condition (NbAmi < capacite table), sinon on renvoi une exceptio
            foreach (Sommet sommet in SommetEtLeurCouleur.Keys)
            {
                if (sommet.NbClients > taverne.CapactieTables) throw new ExceptionTaverneImpossible("Impossible d'utiliser cette taverne", "Le nombre d'amis est supérieur à la capacité des tables");

            }

            // Étape 6: Répéter jusqu'à ce que tous les sommets soient colorés
            while (SommetEtLeurCouleur.ContainsValue(-1)) // Vérifier s'il y a encore des sommets non colorés
            {
                // Étape 4: Sélection du prochain sommet à colorer
                Sommet sommetSuivant = null;
                int saturationMax = -1;

                foreach (Sommet sommet in graphe.Sommets)
                {
                    if (SommetEtLeurCouleur[sommet] == -1 && saturationSommet[sommet] > saturationMax)
                    {
                        sommetSuivant = sommet;
                        saturationMax = saturationSommet[sommet];
                    }
                }

                // Étape 5: Coloration du sommet sélectionné avec la plus petite valeur disponible
                SommetEtLeurCouleur[sommetSuivant] = GetCouleurDispo(sommetSuivant, SommetEtLeurCouleur,taverne) ; 

                // Mise à jour de la saturation des voisins du sommet sélectionné
                foreach (Sommet voisin in sommetSuivant.Voisins)
                {
                    if (SommetEtLeurCouleur[voisin] != -1)
                        saturationSommet[voisin]++;
                }
            }



            //On passe du graphe a la taverne ------------------------------------------------------------------------------

            //On créer le nombre de table qu'il faut
            for (int i = 0; i < SommetEtLeurCouleur.Values.Max() + 1; i++)
            {
                taverne.AjouterTable();
            }


            // Affecte les tables aux clients
            foreach (Client client in taverne.Clients)
            {
                taverne.AjouterClientTable(client.Numero, SommetEtLeurCouleur[graphe.GetSommetOfClient(client)]);
            }


            //Fin de la mesure de la durée de l'algorithme
            stopwatch.Stop();
            this.tempsExecution = stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Renvoi le nombre d'occurence d'une couleur dans un dictionnaire associant des un sommet a une couleur
        /// </summary>
        /// <param name="SommetEtLeurCouleur">dictionnaire associant des sommets à des couleurs dont on veut connaitre l'occurence d'une valeur</param>
        /// <param name="couleur">couleur dont on veut connaitre l'occurence</param>
        /// <returns>l'occurence de la couleur dans le dictionnaire</returns>
        private static int GetNombreOccurenceCouleur(Dictionary<Sommet, int> SommetEtLeurCouleur, int couleur)
        {
            int occurrence = 0;
            foreach (int numTable in SommetEtLeurCouleur.Values)
            {
                if (numTable == couleur)
                    occurrence++;
            }
            return occurrence;
        }


        /// <summary>
        /// Renvoi la plus petite couleur disponible d'un sommet
        /// </summary>
        /// <param name="graphe">Graphe dans lequel on travaille</param>
        /// <param name="sommet">Sommet dont on cherche la plus petite couleur</param>
        /// <param name="SommetEtLeurCouleur"> dictionnaire des sommets et de leur couleur pour trouver la couleur des voisins du sommet recherché</param>
        /// <param name="taverne">taverne dans laquelle on travaille</param>
        /// <returns>(int) la plus petite couleur disponible du sommet</returns>
        private static int GetCouleurDispo(Sommet sommet, Dictionary<Sommet, int> SommetEtLeurCouleur,Taverne taverne)
        {
            int couleur = 0;

            //les couleurs des sommets voisins
            List<int> CouleurVoisin = new List<int>();

            //on recupere les couleurs des voisins du sommet
            foreach (Sommet voisin in sommet.Voisins)
            {
                CouleurVoisin.Add(SommetEtLeurCouleur[voisin]);
            }

            while (CouleurVoisin.Contains(couleur) || GetNombreOccurenceCouleur(SommetEtLeurCouleur, couleur) >= taverne.CapactieTables)
            {
                couleur += 1;
            }
            return couleur;
        }
    }
}
