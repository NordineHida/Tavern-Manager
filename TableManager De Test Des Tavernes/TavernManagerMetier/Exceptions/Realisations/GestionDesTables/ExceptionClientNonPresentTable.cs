using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Exceptions.Realisations.GestionDesTables
{
    /// <summary>
    /// Exception levée quand un client n'est pas à la table manipulée
    /// </summary>
    public class ExceptionClientNonPresentTable : ExceptionGestionDesTables
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="client">Le client concerné (non null)</param>
        public ExceptionClientNonPresentTable(Client client) : base("Le client "+client.ToString()+" n'est pas à cette table.")
        {
        }
    }
}
