using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations
{
    /// <summary>
    /// Exception générique de génération
    /// </summary>
    public abstract class ExceptionGeneration : ExceptionTavernManager
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="message">Message de l'exception</param>
        public ExceptionGeneration(string message) : base("Erreur de génération", message)
        {
        }
    }
}
