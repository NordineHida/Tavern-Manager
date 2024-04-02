using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Metier.Tavernes.Fabriques
{
    /// <summary>
    /// Fabrique de taverne aléatoire
    /// </summary>
    public class FabriqueTaverne
    {
        /// <summary>
        /// Création d'une taverne
        /// </summary>
        /// <param name="capaciteTables">Capacité des tables</param>
        /// <param name="nombreClients">Nombre de clients</param>
        /// <param name="densite">Densité des relations</param>
        /// <param name="proportionAmis">Proportion de relations amicales</param>
        /// <returns></returns>
        public Taverne CreerTaverne(int capaciteTables, int nombreClients, double densite, double proportionAmis)
        {
            //Création de la liste des clients
            List<Client> listeClients = (new FabriqueClient()).CreerListeClients(nombreClients, densite,proportionAmis);

            //Création de la taverne
            Taverne taverne = new Taverne(capaciteTables, listeClients);

            return taverne;
        }


    }
}
