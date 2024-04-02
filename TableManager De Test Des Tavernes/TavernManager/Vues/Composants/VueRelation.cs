using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using TavernManager.VuesModeles;

namespace TavernManager.Vues.Composants
{
    /// <summary>
    /// Vue d'une relation entre deux clients
    /// </summary>
    public class VueRelation
    {
        /// <summary>
        /// Forme à afficher
        /// </summary>
        public Line Vue => this.vue;
        private Line vue;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="vueModele">La vue modèle de la relation</param>
        public VueRelation(VMRelation vueModele)
        {
            this.vue = new Line();
            this.vue.IsHitTestVisible = false;
            if (vueModele.IsAmical) this.vue.Stroke = Brushes.Green;
            else this.vue.Stroke = Brushes.Red;
        }
    }
}
