using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations
{
    /// <summary>
    /// Exception générique de gestion des tables
    /// </summary>
    public abstract class ExceptionGestionDesTables : ExceptionTavernManager
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="message">Message de l'exception</param>
        public ExceptionGestionDesTables(string message) : base("Erreur de gestion des tables", message)
        {
        }
    }
}
