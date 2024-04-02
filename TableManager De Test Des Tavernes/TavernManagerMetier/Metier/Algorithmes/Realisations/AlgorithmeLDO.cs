using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    public class AlgorithmeLDO : IAlgorithme
    {
        /// <summary>
        /// Nom de l'algorithme
        /// </summary>
        public string Nom => "LDO";

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
        public string Executer(Taverne taverne)
        {
            //Debut de la mesure de la durée de l'algorithme
            Stopwatch stopwatch = Stopwatch.StartNew();



            //Creation du graphe a partir de la taverne ----------------------------------------
            Graphe graphe = new Graphe(taverne);

            //Dictionnaire qui associe une Table (int) à un client
            Dictionary<Sommet, int> SommetEtLeurCouleur = new Dictionary<Sommet, int>();

            // Etape 1: Initialisation
            foreach (Sommet sommet in graphe.Sommets)
            {
                SommetEtLeurCouleur[sommet] = -1; // -1 indique que le sommet n'est pas encore coloré
            }

            //On vérifie si la taverne remplis la condition (NbAmi < capacite table), sinon on renvoi une exceptio
            foreach (Sommet sommet in SommetEtLeurCouleur.Keys)
            {
                if (sommet.NbClients > taverne.CapactieTables) throw new ExceptionTaverneImpossible("Impossible d'utiliser cette taverne", "Le nombre d'amis est supérieur à la capacité des tables");
            }

            // Etape 2: On classe les sommets par odre de degrés décroissant

            // On creer un dictionnaire de sommet avec leur degres pour pouvoir le trier
            Dictionary<Sommet, int> SommetEtLeurDegres = new Dictionary<Sommet, int>();

            //On affecte le degres a chacun des sommets du dictionnaire de degres
            foreach (Sommet sommet in SommetEtLeurCouleur.Keys)
            {
                SommetEtLeurDegres[sommet] = GetDegres(sommet, taverne);
            }

            // Convertir le dictionnaire en une liste de paires clé-valeur
            List<KeyValuePair<Sommet, int>> listeSommetDegres = SommetEtLeurDegres.ToList();

            // Trier la liste dans l'ordre décroissant des degrés
            listeSommetDegres = listeSommetDegres.OrderByDescending(pair => pair.Value).ToList();

            //On reconverti la liste en Dictionnaire
            SommetEtLeurDegres = listeSommetDegres.ToDictionary(pair => pair.Key, pair => pair.Value);


            // Etape 3: On colorie les sommets dans cet ordre en donnant la plus petite couleur possible 
            foreach (Sommet sommet in SommetEtLeurDegres.Keys)
            {
                SommetEtLeurCouleur[sommet] = GetCouleurDispo(sommet, SommetEtLeurCouleur, taverne);
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

            return stopwatch.ElapsedMilliseconds.ToString() + "/" + (SommetEtLeurCouleur.Values.Max() + 1).ToString();
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
        private static int GetCouleurDispo(Sommet sommet, Dictionary<Sommet, int> SommetEtLeurCouleur, Taverne taverne)
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

        private int GetDegres(Sommet sommet, Taverne taverne)
        {
            int degres = 0;

            foreach(Sommet voisin in sommet.Voisins)
            {
                degres++;
            }

            return degres;
        }
    }
}
