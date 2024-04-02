using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TavernManager.Vues.Composants;
using TavernManager.VuesModeles;
using TavernManagerMetier.Data;
using TavernManagerMetier.Exceptions;
using TavernManagerMetier.Metier.Algorithmes.Realisations;
using TavernManagerMetier.Metier.Tavernes;
using TavernManagerMetier.Metier.Tavernes.Fabriques;

namespace TavernManager.Vues
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FenetrePrincipale : RatioWindow
    {
        //Vue modèle
        private VMFenetrePrincipale vueModele;

        /// <summary>
        /// Client selectionné
        /// </summary>
        private VueClient VueClientSelectionnee
        {
            get => vueClientSelectionnee;
            set
            {
                vueClientSelectionnee = value;
                this.AffichageClientSelectionne();
            }
        }
        private VueClient vueClientSelectionnee;

        /// <summary>
        /// Nombre de clients par ligne
        /// </summary>
        private int NbClientParLigne
        {
            get { return nbClientParLigne; }
            set { 
                if(value != nbClientParLigne)
                {
                    nbClientParLigne = value;
                    RafraichirAffichageClients();
                }
            }
        }
        private int nbClientParLigne = 5;

        //Liste des relations affichées
        private List<VueRelation> vueRelations;
        //Dictionnaire des vues des clients
        private Dictionary<VMClient, VueClient> vueClients;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public FenetrePrincipale()
        {
            //Initialisation
            this.vueClients = new Dictionary<VMClient, VueClient>();
            this.vueRelations = new List<VueRelation>();
            //Création du vue-modele
            this.vueModele = new VMFenetrePrincipale();
            this.DataContext = this.vueModele;
            //Initialisation des composants graphiques
            InitializeComponent();

            this.vueModele.PropertyChanged += VueModele_PropertyChanged;
            //this.AffichageClients();


            TaverneDAO dao = new TaverneDAO();
            FabriqueTaverne fabrique = new FabriqueTaverne();

            AlgorithmeColorationCroissante algorithmeColorationCroissante = new AlgorithmeColorationCroissante();
            AlgorithmeDSatur algorithmeDSatur = new AlgorithmeDSatur();
            AlgorithmeLDO algorithmeLDO = new AlgorithmeLDO();
            AlgorithmeWelshPowell algorithmeWelshPowell = new AlgorithmeWelshPowell();


            //Changer le i < nombre de taverne
            for (int i = 0; i < 100; i++)
            {
                //On creer i taverne avec les caractéristique que l'
                dao.Sauver(fabrique.CreerTaverne(29, 30, 0.06, 0.02), "Num" + i.ToString());
            }

            string SmoyenneTempsColo;
            string SmoyenneTableColo ;
            string SmoyenneTempsDSatur;
            string SmoyenneTableDSatur;
            string SmoyenneTempsLDO;
            string SmoyenneTableLDO;
            string SmoyenneTempsWelshPowell;
            string SmoyenneTableWelshPowell;

            int moyenneTempsColo = 0;
            int moyenneTableColo = 0;
            int moyenneTempsDSatur = 0;
            int moyenneTableDSatur = 0;
            int moyenneTempsLDO = 0;
            int moyenneTableLDO = 0;
            int moyenneTempsWelshPowell = 0;
            int moyenneTableWelshPowell = 0;


            //Changer le i < nombre de taverne
            for (int i = 0; i <100; i++)
            {
                Taverne taverne = dao.Charger("Num" + i.ToString());
                string resultatColo = algorithmeColorationCroissante.Executer(taverne);

                taverne = dao.Charger("Num" + i.ToString());
                string resultatDSatur = algorithmeDSatur.Executer(taverne);

                taverne = dao.Charger("Num" + i.ToString());
                string resultatLDO = algorithmeLDO.Executer(taverne);

                taverne = dao.Charger("Num" + i.ToString());
                string resultatWelshPowell = algorithmeWelshPowell.Executer(taverne);


                string[] elementsColo        = resultatColo.Split('/');
                string[] elementsDSatur      = resultatDSatur.Split('/');
                string[] elementsLDO         = resultatLDO.Split('/');
                string[] elementsWelshPowell = resultatWelshPowell.Split('/');


                SmoyenneTempsColo         = elementsColo[0]                     ;
                SmoyenneTableColo         = elementsColo[1]             ;
                SmoyenneTempsDSatur       = elementsDSatur[0]                 ;
                SmoyenneTableDSatur       = elementsDSatur[1]                 ;
                SmoyenneTempsLDO          = elementsLDO[0]                       ;
                SmoyenneTableLDO          = elementsLDO[1]                       ;
                SmoyenneTempsWelshPowell  = elementsWelshPowell[0]       ;
                SmoyenneTableWelshPowell  = elementsWelshPowell[1];


                moyenneTempsColo        += int.Parse(SmoyenneTempsColo);
                moyenneTableColo        += int.Parse(SmoyenneTableColo) ;
                moyenneTempsDSatur      += int.Parse(SmoyenneTempsDSatur);
                moyenneTableDSatur      += int.Parse(SmoyenneTableDSatur);
                moyenneTempsLDO         += int.Parse(SmoyenneTempsLDO);
                moyenneTableLDO         += int.Parse(SmoyenneTableLDO);
                moyenneTempsWelshPowell += int.Parse(SmoyenneTempsWelshPowell);
                moyenneTableWelshPowell += int.Parse(SmoyenneTableWelshPowell);

            }


            moyenneTempsColo           /= 100;
            moyenneTableColo           /= 100;
                                          
            moyenneTempsDSatur         /= 100;
            moyenneTableDSatur         /= 100;
                                          
            moyenneTempsLDO            /= 100;
            moyenneTableLDO            /= 100;
                                          
            moyenneTempsWelshPowell    /= 100;
            moyenneTableWelshPowell    /= 100;



            //Mettre un point d'arrete ici et observer (via espion ou non) les valeurs moyennes du test ci-dessus.
            moyenneTempsColo +=00;



        }

        /// <summary>
        /// Affichage des clients
        /// </summary>
        private void AffichageClients()
        {
            this.CanvasTaverne.Children.Clear();
            this.vueClients.Clear();
            for (int i=0; i < this.vueModele.VMClients.Length; i++)
            {
                VMClient vmclient = this.vueModele.VMClients[i];
                VueClient vueClient = new VueClient(vmclient);
                vueClient.Vue.MouseDown += (s,e) => this.MouseDown_VueClient(vueClient);
                this.vueClients[vmclient] = vueClient;
                this.CanvasTaverne.Children.Add(vueClient.Vue);
            }
            this.RafraichirAffichageClients();
            this.CanvasTaverne.Height = (this.vueModele.VMClients.Length / NbClientParLigne) * 100 + 50;
            if(this.vueModele.VMClients.Length % NbClientParLigne != 0) this.CanvasTaverne.Height += 100;
        }

        /// <summary>
        /// Raffraichissement des positions des clients en cas de redimensionnement
        /// </summary>
        private void RafraichirAffichageClients()
        {
            for (int i = 0; i < this.vueModele.VMClients.Length; i++)
            {
                VMClient vmclient = this.vueModele.VMClients[i];
                VueClient vueClient = this.vueClients[vmclient];
                Canvas.SetLeft(vueClient.Vue, 50 + (i % NbClientParLigne) * 100);
                Canvas.SetTop(vueClient.Vue, 50 + (i / NbClientParLigne) * 100);
            }
        }

        /// <summary>
        /// Click sur un client
        /// </summary>
        /// <param name="vueClient">Le client sélectionné</param>
        private void MouseDown_VueClient(VueClient vueClient)
        {
            if (vueClient == vueClientSelectionnee) VueClientSelectionnee = null;
            else VueClientSelectionnee = vueClient;
        }

        /// <summary>
        /// Mise à jour de l'affichage en cas de sélection d'un client
        /// </summary>
        private void AffichageClientSelectionne()
        {
            //Supprimes les relations affichées
            foreach(VueRelation vueRelation in this.vueRelations) this.CanvasTaverne.Children.Remove(vueRelation.Vue);
            this.vueRelations.Clear();

            //Déselection
            if(vueClientSelectionnee == null)
            {
                foreach (VueClient vue in this.vueClients.Values)
                {
                    vue.Opacity = 1;
                    vue.Stroke = vue.Fill;
                }
            }
            //Sélection
            else
            {
                //Met tous les clients en gris
                foreach (VueClient vue in this.vueClients.Values)
                {
                    vue.Opacity = 0.2;
                    vue.Stroke = vue.Fill;
                }
                //Sauf celui sélectionné
                vueClientSelectionnee.Opacity = 1;
                //Affiche les relations
                foreach (VMRelation vmRelation in vueClientSelectionnee.Metier.VMRelations)
                {
                    //Dessine la relation
                    VueRelation vueRelation = new VueRelation(vmRelation);
                    vueRelation.Vue.X1 = Canvas.GetLeft(this.vueClients[vmRelation.Client1].Vue) + this.vueClients[vmRelation.Client1].Vue.ActualWidth / 2;
                    vueRelation.Vue.Y1 = Canvas.GetTop(this.vueClients[vmRelation.Client1].Vue) + this.vueClients[vmRelation.Client1].Vue.ActualHeight / 2;
                    vueRelation.Vue.X2 = Canvas.GetLeft(this.vueClients[vmRelation.Client2].Vue) + this.vueClients[vmRelation.Client2].Vue.ActualWidth / 2;
                    vueRelation.Vue.Y2 = Canvas.GetTop(this.vueClients[vmRelation.Client2].Vue) + this.vueClients[vmRelation.Client2].Vue.ActualHeight / 2;
                    Canvas.SetZIndex(vueRelation.Vue, -1);
                    //Met le sommet en valeur
                    this.vueClients[vmRelation.Client2].Stroke = vueRelation.Vue.Stroke;
                    this.vueClients[vmRelation.Client2].Opacity = 0.75;
                    this.CanvasTaverne.Children.Add(vueRelation.Vue);
                    this.vueRelations.Add(vueRelation);
                }
            }
        }

        /// <summary>
        /// Redimensionnement de la zone d'affichage
        /// </summary>
        /// <param name="sender">Fenetre</param>
        /// <param name="e">Evenement</param>
        private void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.NbClientParLigne = (int) ((this.ScrollViewerTaverne.ActualWidth - 20) / 100);
        }

        /// <summary>
        /// Changement de selection dans la combobox des algorithmes
        /// </summary>
        /// <param name="sender">La combobox</param>
        /// <param name="e">L'évènement</param>
        private void ComboAlgorithmes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.CheckBoutonLancement();
        }

        /// <summary>
        /// Changement de selection dans la combobox des tavernes
        /// </summary>
        /// <param name="sender">La combobox</param>
        /// <param name="e">L'évènement</param>
        private void ComboTavernes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboTavernes.SelectedIndex == this.ComboTavernes.Items.Count - 1)
            {
                this.ComboTavernes.SelectedIndex = -1;
                new FenetreCreationTaverne().ShowDialog();
                this.vueModele.UpdateListeTavernes();
            }
            else if(this.ComboTavernes.SelectedItem != null)
            {
                try
                {
                    this.vueModele.ChargementTaverne(this.ComboTavernes.SelectedItem.ToString());
                    this.AffichageClients();
                    this.CheckBoutonLancement();
                }
                catch(ExceptionTavernManager ex)
                {
                    MessageBox.Show(ex.Message, ex.Titre, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                this.vueModele.ClearTaverne();
                this.AffichageClients();
            }
        }

        /// <summary>
        /// Check de validation des conditions de lancement
        /// </summary>
        private void CheckBoutonLancement()
        {
            this.BoutonLancement.IsEnabled = this.ComboTavernes.SelectedIndex >= 0 && this.ComboAlgorithmes.SelectedIndex >= 0;
        }

        /// <summary>
        /// Lancement de l'algorithme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonLancement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.vueModele.LancerAlgorithme();
            }
            catch(ExceptionTavernManager ex)
            {
                MessageBox.Show(ex.Message, ex.Titre, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        /// <summary>
        /// Modification manuelle de la vm
        /// </summary>
        /// <param name="sender">la vm</param>
        /// <param name="e">le changement</param>
        private void VueModele_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //Mise à jour des tables => treeview
            if (e.PropertyName == "VMTables")
            {
                this.TreeViewTable.Items.Clear();
                this.Log.Text = "";
                foreach(VMTable vmTable in this.vueModele.VMTables)
                {
                    //Table
                    TreeViewItem itemTable = new TreeViewItem();
                    itemTable.Header = vmTable;

                    if(vmTable.VMClients.Length>0)
                    {
                        itemTable.Foreground = Brushes.Green;
                        itemTable.FontWeight = FontWeights.Bold;
                    }
                    else
                    {
                        itemTable.Foreground = Brushes.LightGray;
                    }


                    if (!vmTable.EstValide)
                    {
                        itemTable.Foreground = Brushes.Red;
                        itemTable.FontWeight = FontWeights.Bold;
                    }
                    //Clients
                    foreach(VMClient vmClient in vmTable.VMClients)
                    {
                        TreeViewItem itemClient = new TreeViewItem();
                        string message = "";
                        if (!vmClient.EstAvecCesAmis) message = vmClient.Identite + " n'est pas à la même table que ces amis";
                        if (vmClient.EstAvecUnEnnemis)
                        {
                            if (message != "") message += "\n";
                            message += vmClient.Identite+" est à la même table qu'un de ces ennemis";
                        }
                        if(message != "")
                        {
                            this.Log.Text += message + "\n\n";
                            itemTable.Foreground = Brushes.Red;
                            itemTable.FontWeight = FontWeights.Bold;
                            itemClient.Foreground = Brushes.Red;
                            ToolTip tp = new ToolTip();
                            tp.Content = message;
                            itemClient.ToolTip = tp;
                            ToolTipService.SetInitialShowDelay(itemClient, 0);
                        }
                        itemClient.Header = vmClient;
                        itemTable.Items.Add(itemClient);
                    }
                    this.TreeViewTable.Items.Add(itemTable);
                    this.TextNombreTable.Text = this.vueModele.VMTables.Count.ToString();
                }
            }
            else if(e.PropertyName == "TempsExecution")
            {
                //Affichage du temps d'exécution
                if(this.vueModele.TempsExecution != -1) MessageBox.Show("Temps d'exécution : " + this.vueModele.TempsExecution + " milisecondes", "Temps d'exécution", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
