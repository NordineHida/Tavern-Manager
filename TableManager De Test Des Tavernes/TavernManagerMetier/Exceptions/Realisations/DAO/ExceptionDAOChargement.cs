using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations.DAO
{
    /// <summary>
    /// Exception levé lors d'un problème de chargement de taverne
    /// </summary>
    public class ExceptionDAOChargement : ExceptionDAO
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="nomFichier">Nom du fichier de la taverne</param>
        public ExceptionDAOChargement(string nomFichier) : base("Impossible de charger la taverne "+nomFichier+".")
        {
        }
    }
}
