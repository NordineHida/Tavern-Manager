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
    public class AlgorithmeWelshPowell : IAlgorithme
    {
        /// <summary>
        /// Nom de l'algorithme
        /// </summary>
        public string Nom => "Welsh Powell";

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


            //Tri par degré décroissant----------------------------------------------
            List<Sommet> sommetsTries = graphe.Sommets.OrderByDescending(s => s.Voisins.Count()).ToList();

            // 

            Dictionary<Sommet, int> SommetEtLeurCouleur = new Dictionary<Sommet, int>();
            foreach (Sommet sommet in sommetsTries)
            {
                SommetEtLeurCouleur[sommet] = -1;
            }

            //On vérifie si la taverne remplis la condition (NbAmi < capacite table), sinon on renvoi une exceptio
            foreach (Sommet sommet in SommetEtLeurCouleur.Keys)
            {
                if (sommet.NbClients > taverne.CapactieTables) throw new ExceptionTaverneImpossible("Impossible d'utiliser cette taverne", "Le nombre d'amis est supérieur à la capacité des tables");
            }

            int couleur = 0;
            int SommetsTraites = 0;
            List<int> NbClientsTable = new List<int>();
            NbClientsTable.Add(0);

            // Parcourt les sommets triés --------------------------------------------------------------
            while (SommetsTraites < sommetsTries.Count())
            {
                foreach (Sommet sommet in sommetsTries)
                {
                    if (SommetEtLeurCouleur[sommet] == -1)
                    {
                        bool stop = false;
                        foreach (Sommet voisin in sommet.Voisins)
                        {
                            if (SommetEtLeurCouleur[voisin] == couleur)
                            {
                                stop = true;
                            }
                        }

                        if (!stop && (NbClientsTable[couleur] + sommet.NbClients < taverne.CapactieTables))
                        {
                            SommetEtLeurCouleur[sommet] = couleur;
                            NbClientsTable[couleur] += sommet.NbClients;
                            SommetsTraites++;
                        }
                    }
                }
                couleur++;
                NbClientsTable.Add(0);
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
        private int GetNombreOccurrence(Dictionary<Sommet, int> SommetEtLeurCouleur, int couleur)
        {
            int occurrence = 0;
            foreach (int numTable in SommetEtLeurCouleur.Values)
            {
                if (numTable == couleur)
                    occurrence++;
            }
            return occurrence;
        }

    }
}
