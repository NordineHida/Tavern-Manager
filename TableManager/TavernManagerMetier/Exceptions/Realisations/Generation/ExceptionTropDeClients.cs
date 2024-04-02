using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations.Generation
{
    /// <summary>
    /// Exception levé quand trop de clients sont demandés dans la taverne
    /// </summary>
    public class ExceptionTropDeClients : ExceptionGeneration
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="nombreMax">Nombre max de clients</param>
        public ExceptionTropDeClients(int nombreMax) : base("Impossible de créer plus de " + nombreMax.ToString() + " clients")
        {
        }
    }
}
