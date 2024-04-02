using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using System.IO;
using TavernManager.VuesModeles;
using TavernManagerMetier.Exceptions;

namespace TavernManager.Vues
{
    /// <summary>
    /// Logique d'interaction pour FenetreCreationTaverne.xaml
    /// </summary>
    public partial class FenetreCreationTaverne : Window
    {

        //Vue modèle
        private VMCreationTaverne vueModel;
        //La fenetre est-elle chargée
        private bool loaded;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public FenetreCreationTaverne()
        {
            this.loaded = false;
            this.vueModel = new VMCreationTaverne();
            this.DataContext = this.vueModel;
            InitializeComponent();
            this.loaded = true;
            this.CheckValidation();
        }

        /// <summary>
        /// Check des conditions de validation
        /// </summary>
        private void CheckValidation()
        {
            if(loaded)
            {
                bool validable = true;
                string nom = this.TextNom.Text;
                string nombre = this.TextNombreClients.Text;
                string capacite = this.TextCapacite.Text;
                string message = "";


                //Le nom ne doit pas être vide
                if(nom == "")
                {
                    validable = false;
                    if (message != "") message += "\n";
                    message += "Le nom ne doit pas être vide.";
                }

                //Le nom ne doit contenir que des caractères valides
                if(!nom.All(char.IsLetterOrDigit))
                {
                    validable = false;
                    if (message != "") message += "\n";
                    message += "Le nom de la taverne ne doit contenir que des lettres et/ou chiffres.";
                }

                //Le nom ne doit pas déjà être pris
                var chem = System.IO.Path.Combine("Tavernes/", nom+".tvn");
                if (File.Exists(chem)) {
                    validable = false;
                    if (message != "") message += "\n";
                    message += "Le nom de la taverne est déjà utilisé.";
                }

                //le nombre de clients doit être un entier
                int n;
                bool isNumeric = int.TryParse(nombre, out n);
                if (!isNumeric)
                {
                    validable = false;
                    if (message != "") message += "\n";
                    message += "Le nombre de client n'est pas valide.";
                }
                //le nombre de clients entre 1 et 5000
                else if(n<1 || n >5000)
                {
                    validable = false;
                    if (message != "") message += "\n";
                    message += "Le nombre de client doit être entre 1 et 5000.";
                }

                //La capacité doit être un entier
                isNumeric = int.TryParse(capacite, out n);
                if (!isNumeric)
                {
                    validable = false;
                    if (message != "") message += "\n";
                    message += "Le capacité des tables n'est pas valide.";
                }
                //le nombre de clients entre 1 et 5000
                else if (n < 1 || n > 5000)
                {
                    validable = false;
                    if (message != "") message += "\n";
                    message += "Le capacité des tables doit être entre 1 et 5000.";
                }
                //Modification du bouton
                this.BoutonValider.IsEnabled = validable;
                this.BoutonValider.ToolTip = null;
                if (!validable)
                {
                    ToolTip tp = new ToolTip();
                    tp.Content = message;
                    this.BoutonValider.ToolTip = tp;
                }
            }
        }

        /// <summary>
        /// Changement du nom
        /// </summary>
        /// <param name="sender">Le composant</param>
        /// <param name="e">L'évènement</param>
        private void TextNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.CheckValidation();
        }

        /// <summary>
        /// Changement du nombre de clients
        /// </summary>
        /// <param name="sender">Le composant</param>
        /// <param name="e">L'évènement</param>
        private void TextNombreClients_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.CheckValidation();
        }

        /// <summary>
        /// Changement de la densité
        /// </summary>
        /// <param name="sender">Le composant</param>
        /// <param name="e">L'évènement</param>
        private void SliderDensite_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.CheckValidation();
        }

        /// <summary>
        /// Changement de la proportion de relations amicales
        /// </summary>
        /// <param name="sender">Le composant</param>
        /// <param name="e">L'évènement</param>
        private void SliderProportion_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.CheckValidation();
        }

        /// <summary>
        /// Changement de la capacité
        /// </summary>
        /// <param name="sender">Le composant</param>
        /// <param name="e">L'évènement</param>
        private void TextCapacite_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.CheckValidation();
        }

        /// <summary>
        /// Validation
        /// </summary>
        /// <param name="sender">Le composant</param>
        /// <param name="e">L'évènement</param>
        private void BoutonValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.vueModel.CreationTaverne();
            }
            catch(ExceptionTavernManager ex)
            {
                MessageBox.Show(ex.Message,ex.Titre,MessageBoxButton.OK,MessageBoxImage.Error);
            }
            this.Close();
        }
    }
}
