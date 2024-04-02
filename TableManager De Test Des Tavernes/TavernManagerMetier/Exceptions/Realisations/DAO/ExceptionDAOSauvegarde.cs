using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations.DAO
{
    /// <summary>
    /// Exception levé lors d'un problème de sauvegarde de taverne
    /// </summary>
    public class ExceptionDAOSauvegarde : ExceptionDAO
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="nomFichier">Nom du fichier de la taverne</param>
        public ExceptionDAOSauvegarde(string nomFichier) : base("Impossible de sauver la taverne " + nomFichier + ".")
        {
        }
    }
}
