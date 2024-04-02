using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions
{
    /// <summary>
    /// Exception générique de TavernManager
    /// </summary>
    public abstract class ExceptionTavernManager : Exception
    {
        /// <summary>
        /// Titre de l'exception
        /// </summary>
        public string Titre => titre;
        private string titre;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="titre">Titre de l'exception</param>
        /// <param name="message">Message de l'exception</param>
        public ExceptionTavernManager(string titre, string message) : base(message)
        { 
            this.titre = titre;
        }
    }
}
