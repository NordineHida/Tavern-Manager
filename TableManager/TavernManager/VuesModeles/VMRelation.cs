using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManager.VuesModeles
{
    public class VMRelation
    {
        /// <summary>
        /// Premier client
        /// </summary>
        public VMClient Client1 => client1;
        private VMClient client1;

        /// <summary>
        /// Deuxième client
        /// </summary>
        public VMClient Client2 => client2;
        private VMClient client2;

        /// <summary>
        /// La relation est-elle amicale
        /// </summary>
        public bool IsAmical => isAmical;
        private bool isAmical;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="client1">Premier client</param>
        /// <param name="client2">Deuxième client</param>
        /// <param name="isAmical">La relation est-elle amicale</param>
        public VMRelation(VMClient client1, VMClient client2, bool isAmical)
        {
            this.client1 = client1;
            this.client2 = client2;
            this.isAmical = isAmical;
        }

    }
}
