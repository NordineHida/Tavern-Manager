using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations
{
    /// <summary>
    /// Exception générique de gestion de la taverne
    /// </summary>
    public abstract class ExceptionGestionTaverne : ExceptionTavernManager
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="message">Message de l'exception</param>
        protected ExceptionGestionTaverne(string message) : base("Erreur de gestion de la taverne", message)
        {
        }
    }
}
