using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations.GestionTaverne
{
    /// <summary>
    /// Exception levé si le numéro de table utilisé n'existe pas
    /// </summary>
    public class ExceptionNumeroTableInconnu : ExceptionGestionTaverne
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="numero">Numéro de la table</param>
        public ExceptionNumeroTableInconnu(int numero) : base("La table n°" + numero.ToString() + " n'existe pas !")
        {

        }
    }
}
