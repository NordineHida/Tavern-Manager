using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Exceptions.Realisations.DAO;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Data
{
    /// <summary>
    /// DAO pour la taverne
    /// </summary>
    public class TaverneDAO
    {
        /// <summary>
        /// Sauve une taverne
        /// </summary>
        /// <param name="taverne">Taverne à sauver</param>
        /// <param name="nomFichier">Nom du fichier</param>
        public void Sauver(Taverne taverne, string nomFichier)
        {
            try
            {
                StreamWriter sw = new StreamWriter("Tavernes/" + nomFichier + ".tvn");
                //Capacité
                sw.WriteLine("[Capacite]");
                sw.WriteLine(taverne.CapactieTables);
                //Clients
                sw.WriteLine("[Clients]");
                foreach (Client client in taverne.Clients)
                {
                    sw.WriteLine(client.Prenom + " " + client.Nom);
                }
                sw.WriteLine("[Relations]");
                //Relations
                foreach (Client client in taverne.Clients)
                {
                    foreach (Client client2 in client.Amis)
                    {
                        sw.WriteLine(client.Numero + "/" + client2.Numero + "/" + "True");
                    }
                    foreach (Client client2 in client.Ennemis)
                    {
                        sw.WriteLine(client.Numero + "/" + client2.Numero + "/" + "False");
                    }
                }
                sw.Close();
            }

            catch (Exception)
            {
                throw new ExceptionDAOSauvegarde(nomFichier);
            }
        }

        /// <summary>
        /// Charge une taverne
        /// </summary>
        /// <param name="nomFichier">Nom du fichier</param>
        /// <returns>La taverne</returns>
        public Taverne Charger(string nomFichier)
        {
            try
            {
                StreamReader sr = new StreamReader("Tavernes/" + nomFichier + ".tvn");
                string mode = "";
                int numeroClient = 0;
                int capacite = 0;
                List<Client> listeClients = new List<Client>();

                string ligne = sr.ReadLine();
                while (ligne != null)
                {
                    if (ligne == "[Capacite]") mode = "Capacite";
                    else if (ligne == "[Clients]") mode = "Client";
                    else if (ligne == "[Relations]") mode = "Relation";
                    else if (mode == "Capacite")
                    {
                        capacite = Convert.ToInt32(ligne);
                    }
                    else if (mode == "Client")
                    {
                        string[] morceaux = ligne.Split(' ');
                        Client client = new Client(morceaux[0], morceaux[1], numeroClient);
                        listeClients.Add(client);
                        numeroClient++;
                    }
                    else if (mode == "Relation")
                    {
                        string[] morceaux = ligne.Split('/');
                        int numClient1 = Convert.ToInt32(morceaux[0]);
                        int numClient2 = Convert.ToInt32(morceaux[1]);
                        bool estAmical = Convert.ToBoolean(morceaux[2]);
                        if (estAmical) listeClients[numClient1].AjouterAmis(listeClients[numClient2]);
                        else listeClients[numClient1].AjouterEnnemis(listeClients[numClient2]);
                    }
                    ligne = sr.ReadLine();
                }
                return new Taverne(capacite, listeClients);
            }
            catch(Exception)
            {
                throw new ExceptionDAOChargement(nomFichier);
            }
            
        }
    }
}
