using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations.GestionTaverne
{
    /// <summary>
    /// Exception levé si le numéro de client utilisé n'existe pas
    /// </summary>
    public class ExceptionNumeroClientInconnu : ExceptionGestionTaverne
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="numero">Numéro du client</param>
        public ExceptionNumeroClientInconnu(int numero) : base("Le client n°" + numero.ToString() + " n'existe pas !")
        {
        }
    }
}
