using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TavernManagerMetier.Metier.Tavernes;

namespace TavernManager.VuesModeles
{
    /// <summary>
    /// Vue modèle d'un client
    /// </summary>
    public class VMClient : INotifyPropertyChanged
    {
        //Client métier
        private Client metier;

        /// <summary>
        /// Liste des relations
        /// </summary>
        public VMRelation[] VMRelations => vmRelations.ToArray();
        private List<VMRelation> vmRelations;

        /// <summary>
        /// Le client a-t-il une table
        /// </summary>
        public bool AsUneTable => this.metier.AsUneTable;
        /// <summary>
        /// Le client est-il avec ces amis
        /// </summary>
        public bool EstAvecCesAmis => this.metier.EstAvecCesAmis;
        /// <summary>
        /// Le client est-il avec ces ennemis
        /// </summary>
        public bool EstAvecUnEnnemis => this.metier.EstAvecUnEnnemis;

        /// <summary>
        /// Numéro de la table du client
        /// </summary>
        public string NumeroTable {
            get
            {
                string res = "";
                if (this.metier.Table != null) res = this.metier.Table.Numero.ToString();
                return res;
            }
        }

        /// <summary>
        /// Identité complète du client
        /// </summary>
        public String Identite => this.metier.ToString();

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="metier">Couche métier</param>
        public VMClient(Client metier)
        {
            this.metier = metier;
            this.metier.PropertyChanged += Metier_PropertyChanged;
            this.vmRelations = new List<VMRelation>();
        }

        private void Metier_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Table") this.NotifyPropertyChanged("NumeroTable");
        }

        /// <summary>
        /// Ajoute une relation
        /// </summary>
        /// <param name="vmRelation">La relation</param>
        public void AddRelation(VMRelation vmRelation)
        {
            this.vmRelations.Add(vmRelation);
        }

        /// <summary>
        /// Affichage
        /// </summary>
        /// <returns>L'identité du client</returns>
        public override string ToString()
        {
            return this.Identite;
        }

        //Pattern d'observation
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
