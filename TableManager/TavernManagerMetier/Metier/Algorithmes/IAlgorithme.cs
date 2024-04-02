using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManagerMetier.Metier.Algorithmes
{
    public interface IAlgorithme
    {
        /// <summary>
        /// Nom de l'algorithme
        /// </summary>
        string Nom { get; }

        /// <summary>
        /// Exécute l'algorithme sur la taverne donnée
        /// </summary>
        /// <param name="taverne">La taverne</param>
        void Executer(Taverne taverne);

        /// <summary>
        /// Renvoie le temps d'exécution de l'algorithme (-1 si le temps n'est pas calculé)
        /// </summary>
        long TempsExecution { get; }
    }
}
