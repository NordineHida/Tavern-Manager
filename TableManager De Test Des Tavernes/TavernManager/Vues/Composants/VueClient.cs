using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TavernManager.VuesModeles;

namespace TavernManager.Vues.Composants
{
    /// <summary>
    /// Vue d'un client dans l'affichage de la taverne
    /// </summary>
    public class VueClient
    {
        /// <summary>
        /// La forme à afficher
        /// </summary>
        public Grid Vue => this.vue;
        private Grid vue;

        /// <summary>
        /// Vue modèle du client
        /// </summary>
        public VMClient Metier => metier;
        private VMClient metier;

        //Cercle interieur
        private Ellipse ellipse;
        //Zone de texte avec le numéro de la table
        private TextBlock numeroTable;

        /// <summary>
        /// Fond du cercle
        /// </summary>
        public Brush Fill { get => this.ellipse.Fill; set => this.ellipse.Fill = value;}

        /// <summary>
        /// Bordure du cercle
        /// </summary>
        public Brush Stroke { get => this.ellipse.Stroke; set => this.ellipse.Stroke = value; }

        /// <summary>
        /// Opacité du cercle
        /// </summary>
        public double Opacity { get => this.ellipse.Opacity; set => this.ellipse.Opacity = value; }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="metier">Le métier</param>
        public VueClient(VMClient metier)
        {
            //Initialisation
            this.vue = new Grid();
            this.ellipse = new Ellipse();
            this.numeroTable = new TextBlock();
            this.metier = metier;

            //Observation
            this.metier.PropertyChanged += Metier_PropertyChanged;

            //Création du cercle central
            this.ellipse.Width = 50;
            this.ellipse.Height = 50;
            this.ellipse.Fill = Brushes.Black;
            this.ellipse.Stroke = this.ellipse.Fill;
            this.ellipse.StrokeThickness = 2;
            this.vue.Children.Add(this.ellipse);

            //Création de la zone de texte
            this.numeroTable.Text = this.metier.NumeroTable;
            this.numeroTable.IsHitTestVisible = false;
            this.numeroTable.Foreground = Brushes.White;
            this.numeroTable.FontSize = 20;
            this.numeroTable.VerticalAlignment = VerticalAlignment.Center;
            this.numeroTable.HorizontalAlignment = HorizontalAlignment.Center;
            this.numeroTable.TextAlignment = TextAlignment.Center;
            this.vue.Children.Add(this.numeroTable);

            //Tooltip
            ToolTip toolTip = new ToolTip();
            toolTip.Content = this.metier.Identite;
            ToolTipService.SetInitialShowDelay(this.ellipse, 0);
            this.ellipse.ToolTip = toolTip;
        }

        /// <summary>
        /// Changement du métier
        /// </summary>
        /// <param name="sender">Le métier</param>
        /// <param name="e">L'évènement</param>
        private void Metier_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "NumeroTable") this.numeroTable.Text = this.metier.NumeroTable;
        }
    }
}
