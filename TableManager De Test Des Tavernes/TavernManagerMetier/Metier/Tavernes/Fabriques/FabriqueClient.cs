using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TavernManagerMetier.Exceptions.Realisations.Generation;

namespace TavernManagerMetier.Metier.Tavernes.Fabriques
{
    /// <summary>
    /// Fabrique de client aléatoire
    /// </summary>
    public class FabriqueClient
    {
        //Liste des noms possibles
        private static string[] listeNoms;
        //Liste des prenoms possibles
        private static string[] listePrenoms;


        /// <summary>
        /// Création d'une liste aléatoire de client
        /// </summary>
        /// <param name="nombre">Nombre de clients voulus</param>
        /// <param name="densite">Densité des relations</param>
        /// <param name="proportionAmis">Proportion de relations amicales</param>
        /// <returns>Une liste de clients</returns>
        public List<Client> CreerListeClients(int nombre, double densite, double proportionAmis)
        {
            //On charge la liste des noms et des prénoms en la mélangeant
            this.ChargeListes();
            //Si le nombre de clients demandés est trop grand, on lève une exception
            if (nombre > listeNoms.Length * listePrenoms.Length) throw new ExceptionTropDeClients(listeNoms.Length * listePrenoms.Length);

            //Création de la liste des clients sans aucun lien
            List<Client> clients = CreerListeClientsSansLien(nombre);

            //Création des liens entre les clients
            this.CreerLiensEntreClients(clients, densite, proportionAmis);

            return clients;
        }

        /// <summary>
        /// Charge (au besoin) les listes de noms et prénoms et les mélange
        /// </summary>
        private void ChargeListes()
        {
            if(listeNoms == null || listePrenoms == null)
            {
                //Charges les deux listes (prénoms et noms)
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("TavernManagerMetier.Metier.Ressources.Prenom.txt"))
                using (StreamReader reader = new StreamReader(stream))
                {
                    listePrenoms = reader.ReadToEnd().Replace("\r\n", "\n").Split('\n') ;
                }

                using (Stream stream = assembly.GetManifestResourceStream("TavernManagerMetier.Metier.Ressources.Nom.txt"))
                using (StreamReader reader = new StreamReader(stream))
                {
                    listeNoms = reader.ReadToEnd().Replace("\r\n", "\n").Split('\n');
                }
            }

            //Mélange les deux listes
            Random gen = new Random();
            for (int i = 0; i < listePrenoms.Length * listeNoms.Length; i++)
            {
                int indice1 = gen.Next(listePrenoms.Length);
                int indice2 = gen.Next(listePrenoms.Length);
                string mem = listePrenoms[indice1];
                listePrenoms[indice1] = listePrenoms[indice2];
                listePrenoms[indice2] = mem;

                indice1 = gen.Next(listeNoms.Length);
                indice2 = gen.Next(listeNoms.Length);
                mem = listeNoms[indice1];
                listeNoms[indice1] = listeNoms[indice2];
                listeNoms[indice2] = mem;
            }
        }

        /// <summary>
        /// Création d'une liste de clients sans doublon et sans lien entre les clients
        /// </summary>
        /// <param name="nombre">Nombre de clients souhaités</param>
        /// <returns>La liste des clients</returns>
        private List<Client> CreerListeClientsSansLien(int nombre)
        {
            List<Client> clients = new List<Client>();
            //Génération aléatoire des clients sans doublon
            for (int i = 0; i < nombre; i++)
            {
                int offset = i / listePrenoms.Length;
                int numPrenom = i % listePrenoms.Length;
                Client nouveauClient = new Client(listePrenoms[numPrenom], listeNoms[(offset + numPrenom) % (listeNoms.Length)],i);
                clients.Add(nouveauClient);
            }
            return clients;
        }

        /// <summary>
        /// Génère les liens entre les clients
        /// </summary>
        /// <param name="listeClients">Liste des clients</param>
        /// <param name="densite">Densité des relations</param>
        /// <param name="proportionAmis">Proportion de relations amicales</param>
        private void CreerLiensEntreClients(List<Client> listeClients,double densite, double proportionAmis)
        {
            //Création de la liste des liens possibles
            List<(int, int)> listeLienPossible = new List<(int, int)>();
            for(int i=0;i< listeClients.Count;i++)
            {
                for(int j=i+1;j<listeClients.Count;j++)
                {
                    listeLienPossible.Add((i, j));
                }
            }

            //Mélange de la liste des liens possible
            Random gen = new Random();
            for (int i=0;i< listeLienPossible.Count;i++)
            {
                int indice2 = gen.Next(listeLienPossible.Count);
                (int,int) mem = listeLienPossible[i];
                listeLienPossible[i] = listeLienPossible[indice2];
                listeLienPossible[indice2] = mem;
            }

            //Détermine le nombre de relation (ennemis et amis)
            int nbRelations = (int)(densite * listeLienPossible.Count);
            int nbRelationsAmis = (int)(proportionAmis * nbRelations);

            //Création des liens amis/ennemis
            for(int i=0;i<nbRelations;i++)
            {
                if (i < nbRelationsAmis)
                {
                    listeClients[listeLienPossible[i].Item1].AjouterAmis(listeClients[listeLienPossible[i].Item2]);
                    listeClients[listeLienPossible[i].Item2].AjouterAmis(listeClients[listeLienPossible[i].Item1]);
                }
                else
                {
                    listeClients[listeLienPossible[i].Item1].AjouterEnnemis(listeClients[listeLienPossible[i].Item2]);
                    listeClients[listeLienPossible[i].Item2].AjouterEnnemis(listeClients[listeLienPossible[i].Item1]);
                }
            }
        }
    }
}
