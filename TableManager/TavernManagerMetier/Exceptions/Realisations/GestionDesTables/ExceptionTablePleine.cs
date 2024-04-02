using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations.GestionDesTables
{
    /// <summary>
    /// Exception levée si une table est déjà pleine
    /// </summary>
    public class ExceptionTablePleine : ExceptionGestionDesTables
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public ExceptionTablePleine() : base("La table est pleine")
        {
        }
    }
}
