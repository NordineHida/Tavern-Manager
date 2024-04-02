using System.Collections.Generic;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManager.VuesModeles
{
    /// <summary>
    /// Vue modèle d'une table
    /// </summary>
    public class VMTable
    {
        /// <summary>
        /// Liste des clients
        /// </summary>        
        public VMClient[] VMClients => vmClients.ToArray();
        private List<VMClient> vmClients;

        /// <summary>
        /// Nom de la table
        /// </summary>
        public string Nom => "Table n°"+this.metier.Numero.ToString();

        /// <summary>
        /// La table est-elle valide
        /// </summary>
        public bool EstValide => this.metier.EstValide;

        //Métier
        private Table metier;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="metier">Couche métier</param>
        public VMTable(Table metier)
        {
            this.metier = metier;
            this.vmClients = new List<VMClient>();
        }

        /// <summary>
        /// Ajoute un VMClient
        /// </summary>
        /// <param name="vmClient">VMClient à ajouter</param>
        public void AjouterVMClient(VMClient vmClient)
        {
            this.vmClients.Add(vmClient);
        }

        /// <summary>
        /// Affichage
        /// </summary>
        /// <returns>Le nom de la table</returns>
        public override string ToString()
        {
            return this.Nom;
        }
    }
}
