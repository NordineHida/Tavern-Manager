using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations
{
    /// <summary>
    /// Taverne impossible à utiliser car nombre ami > capacite table
    /// </summary>
    public class ExceptionTaverneImpossible : ExceptionTavernManager
    {
        /// <summary>
        /// Titre de l'exception
        /// </summary>
        public string Titre => titre;
        private string titre;

        /// <summary>
        /// Constructeur de l'exception
        /// </summary>
        /// <param name="titre">Titre de l'exception</param>
        /// <param name="message">Message de l'exception</param>
        public ExceptionTaverneImpossible(string titre, string message) : base(titre, message)
        {
            this.titre = titre;
        }


    }
}
