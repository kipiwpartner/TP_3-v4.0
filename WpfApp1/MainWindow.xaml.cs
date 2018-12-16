using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected List<Horaire> lst_Horaire = new List<Horaire>();
        protected List<Horaire> lst_Horaire_copy = new List<Horaire>();
        private static string dataDate;
        private static string dataHeures;
        public MainWindow()
        {
            InitializeComponent(); 
        }


        //Обработчик кнопки фильтрации
        private void Filtrer_Click(object sender, RoutedEventArgs e)
        {
            
            dataDate = cmbAns.Text + "-" + cmbMois.Text + "-" + cmbJour.Text;
            dataHeures = cmbHeures.Text;

            switch (true)
            {
                case bool y when y = (chkNoCours.IsChecked == true):
                    SearchByNoCours(); break;

                case bool y when y = (chkNomCours.IsChecked == true):
                    SearchByNom(); break;

                case bool y when y = (chkDateDebut.IsChecked == true):
                    SearchByDate(); break;
                
                case bool y when y = (chkNumeroLocal.IsChecked == true):
                    SearchByNumeroLocal(); break;

                case bool y when y = (chkNumeroGroupe.IsChecked == true):
                    SearchByNoGroupe(); break;

                case bool y when y = (chkNoJour.IsChecked == true):
                    SearchByJour(); break;

                default:
                    MessageBox.Show("Vous n'avez rien choisi", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); break;
            }

            
        }




        private void CopyNomCours_Click(object sender, RoutedEventArgs e)
        {
            if (lst_Horaire_copy.Count > 0)
            {
                System.Windows.Clipboard.SetText(lst_Horaire_copy[lst_View.SelectedIndex].NomCours);
            }
            else
            {
                System.Windows.Clipboard.SetText(lst_Horaire[lst_View.SelectedIndex].NomCours);
            }
            
        }
        private void CopyNoCours_Click(object sender, RoutedEventArgs e)
        {
            if (lst_Horaire_copy.Count > 0)
            {
                System.Windows.Clipboard.SetText(lst_Horaire_copy[lst_View.SelectedIndex].NoCours);
            }
            else
            {
                System.Windows.Clipboard.SetText(lst_Horaire[lst_View.SelectedIndex].NoCours);
            }
        }

        private void CopyNumeroLocal_Click(object sender, RoutedEventArgs e)
        {
            if (lst_Horaire_copy.Count > 0)
            {
                System.Windows.Clipboard.SetText(lst_Horaire_copy[lst_View.SelectedIndex].NumeroLocal);
            }
            else
            {
                System.Windows.Clipboard.SetText(lst_Horaire[lst_View.SelectedIndex].NumeroLocal);
            }
        }

        private void CopyNoGroupe_Click(object sender, RoutedEventArgs e)
        {
            if (lst_Horaire_copy.Count > 0)
            {
                System.Windows.Clipboard.SetText(lst_Horaire_copy[lst_View.SelectedIndex].NoGroupe);
            }
            else
            {
                System.Windows.Clipboard.SetText(lst_Horaire[lst_View.SelectedIndex].NoGroupe);
            }
        }



        //<--------- Обработчики меню -------------->

        //Открыть файл
        private void Event_Ouvrir_Click(object sender, RoutedEventArgs e)
        {
            // éfacer des infos dans la collection
            lst_Horaire.Clear();
            lst_Horaire_copy.Clear();

            // utilisé pour l'adresse de fichier
            string adFichier = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; // seulement les fichier .txt

            // mettre OpenfileDialog initialDirectory pour ouvrir le bureau
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;


            if (openFileDialog.ShowDialog() == true)
            {

                adFichier = openFileDialog.FileName.ToString(); // pour montrer adresse du fichier dans le Statusbar

                string line;
                StreamReader lireFich = new StreamReader(adFichier);

                try
                {     //-----si l'utilisateur choisi un fichier non utilisable

                    // lire le fichier
                    while ((line = lireFich.ReadLine()) != null)
                    {

                        // pour séparer des valeurs dans chaque ligne avec le champ "$" et les mettre dans un Array de type string
                        string[] data;
                        data = line.Split('|');

                        //création des objets avec les paramétre défini dans le constructeur 
                        Horaire hor = new Horaire(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10]);
                        // ajouter cette séance dans la liste
                        lst_Horaire.Add(hor);


                    }
                    // si il n'y a aucun ligne valide dans le fchier
                    if (lst_Horaire.Count() == 0)
                    {
                        throw (new Exception());
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Le fichier choisi n'est pas valide\nSVP vérifiez le fichier", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //fermer flux de lirefich
                lireFich.Close();

                if (lst_Horaire.Count < 2)
                    MessageBox.Show("désolée, il n'y a pas  de deuxième élement!");

                lst_Horaire.RemoveAt(0);

                this.lst_View.ItemsSource = lst_Horaire;
            }
        }


        //Сбросить фильтр
        private void Event_Annuler_Click(object sender, RoutedEventArgs e)
        {
            
            this.lst_View.ItemsSource = lst_Horaire;
            this.lst_View.Items.Refresh();
            tbl_1.Focus();
            lst_Horaire_copy.Clear();

        }

        //Выход из программы
        private void Event_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        // <--------- Обработчики чекбоксов --------->

        //Метод сброса всех чекбокс и текстбоксов
        private void Clear_All()
        {
            chkNoCours.IsChecked = false;
            chkNomCours.IsChecked = false;
            chkDateDebut.IsChecked = false;
            chkHeureDebut.IsChecked = false;
            chkNumeroLocal.IsChecked = false;
            chkNumeroGroupe.IsChecked = false;
            chkNoJour.IsChecked = false;

            txtNoCours.Text = String.Empty;
            txtNomCours.Text = String.Empty;
            txt_NoGroupe.Text = String.Empty;
            txtNumLocal.Text = String.Empty;

            txtNoCours.IsEnabled = false;
            txtNomCours.IsEnabled = false;
            txt_NoGroupe.IsEnabled = false;
            txtNumLocal.IsEnabled = false;

            cmbAns.SelectedIndex = -1;
            cmbMois.SelectedIndex = -1;
            cmbJour.SelectedIndex = -1;
            cmbHeures.SelectedIndex = -1;
            cmbNo.SelectedIndex = -1;
            cmbAns.IsEnabled = false;
            cmbMois.IsEnabled = false;
            cmbJour.IsEnabled = false;
            cmbHeures.IsEnabled = false;
            cmbNo.IsEnabled = false;

        }

        //Обработчик чекбокс NoCours
        private void Event_IsChecked(object sender, RoutedEventArgs e)
        {

            if (chkNoCours.IsChecked == true)
            {
                Clear_All();
                txtNoCours.IsEnabled = true;
                chkNoCours.IsChecked = true;
            }
            else
            {
                Clear_All();
            }
        }

        //Обработчик чекбокс NomCours
        private void Event_NomCours_Checked(object sender, RoutedEventArgs e)
        {
            if (chkNomCours.IsChecked == true)
            {
                Clear_All();
                chkNomCours.IsChecked = true;
                txtNomCours.IsEnabled = true;

            }
            else
            {
                Clear_All();
            }
        }


        //Обработчик чекбокс DateDebut
        private void chkDateDebut_Checked(object sender, RoutedEventArgs e)
        {
            if (chkDateDebut.IsChecked == true)
            {
                Clear_All();
                chkDateDebut.IsChecked = true;
                chkHeureDebut.IsChecked = true;

                cmbAns.SelectedIndex = 0;
                cmbMois.SelectedIndex = 0;
                cmbJour.SelectedIndex = 0;
                cmbAns.IsEnabled = true;
                cmbMois.IsEnabled = true;
                cmbJour.IsEnabled = true;

                cmbHeures.IsEnabled = true;
                cmbHeures.SelectedIndex = 0;
            }
            else
            {
                Clear_All();
            }
        }

        //Обработчик чекбокс HeureDebut
        private void Event_Check_Click_Button(object sender, RoutedEventArgs e)
        {
            if (chkHeureDebut.IsChecked == true)
            {
                Clear_All();
                cmbHeures.IsEnabled = true;
            }
            else
            {
                Clear_All();
            }
        }

        //Обработчик чекбокс NumeroLocal
        private void chkNumeroLocal_Click(object sender, RoutedEventArgs e)
        {
            if (chkNumeroLocal.IsChecked == true)
            {
                Clear_All();
                chkNumeroLocal.IsChecked = true;
                txtNumLocal.IsEnabled = true;
            }
            else
            {
                Clear_All();
            }
        }

        //Обработчик чекбокс NumeroGroupe
        private void chkNumeroGroupe_Click(object sender, RoutedEventArgs e)
        {
            if (chkNumeroGroupe.IsChecked == true)
            {
                Clear_All();
                chkNumeroGroupe.IsChecked = true;
                txt_NoGroupe.IsEnabled = true;

            }
            else
            {
                Clear_All();
            }
        }

        //Обработчик чекбокс NoJour
        private void chkNoJour_Click(object sender, RoutedEventArgs e)
        {
            if (chkNoJour.IsChecked == true)
            {
                Clear_All();
                chkNoJour.IsChecked = true;
                cmbNo.IsEnabled = true;
                cmbNo.SelectedIndex = 0;
            }
            else
            {
                Clear_All();
            }
        }

        //<----------------------------------------------------->

        //<--------- Методы фильтра (поиска) --------->

        //Поиск по NoCours
        private void SearchByNoCours()
        {
            lst_Horaire_copy.Clear();
            
            if (txtNoCours.Text != String.Empty)
            {
                foreach (var item in lst_Horaire)
                {
                    if (item.NoCours == txtNoCours.Text)
                    {
                        lst_Horaire_copy.Add(item);
                    }
                }
                this.lst_View.ItemsSource = lst_Horaire_copy;
                this.lst_View.Items.Refresh();
                tbl_1.Focus();
            }
            else
            {
                MessageBox.Show("NoCours est vide!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Поиск по NomCours
        internal void SearchByNom()
        {
            lst_Horaire_copy.Clear();

            if (txtNomCours.Text == string.Empty)
            {
                MessageBox.Show("Nom du cours est vide", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                foreach (var item in lst_Horaire)
                {
                        if (item.NomCours == txtNomCours.Text)
                        {
                            lst_Horaire_copy.Add(item);
                        }
                }

                this.lst_View.ItemsSource = lst_Horaire_copy;
                this.lst_View.Items.Refresh();
                tbl_1.Focus();

            }
        }

        //Поиск по NumeroLocal
        private void SearchByNumeroLocal()
        {
            lst_Horaire_copy.Clear();
            
            if (txtNumLocal.Text != String.Empty)
            {
                foreach (var item in lst_Horaire)
                {
                    if (item.NumeroLocal == txtNumLocal.Text)
                    {
                        lst_Horaire_copy.Add(item);
                    }
                }

                this.lst_View.ItemsSource = lst_Horaire_copy;
                this.lst_View.Items.Refresh();
                tbl_1.Focus();
            }
            else
            {
                MessageBox.Show("NumeroLocal est vide!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Поиск по NoGroupe
        private void SearchByNoGroupe()
        {
            lst_Horaire_copy.Clear();
            

            if (txt_NoGroupe.Text != String.Empty)
            {
                foreach (var item in lst_Horaire)
                {
                    if (item.NoGroupe == txt_NoGroupe.Text)
                    {
                        lst_Horaire_copy.Add(item);
                    }
                }
                this.lst_View.ItemsSource = lst_Horaire_copy;
                this.lst_View.Items.Refresh();
                tbl_1.Focus();
            }
            else
            {
                MessageBox.Show("NoGroupe est vide!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Поиск по DateDebut
        private void SearchByDate()
        {
            lst_Horaire_copy.Clear();
            
            foreach (var item in lst_Horaire)
            {
                if(item.NoCours == txtNoCours.Text)
                {
                    lst_Horaire_copy.Add(item);
                }
                else if (dataDate == item.DateDebut && dataHeures == item.HeureDebut)
                {
                    lst_Horaire_copy.Add(item);
                }
            }
            this.lst_View.ItemsSource = lst_Horaire_copy;
            this.lst_View.Items.Refresh();
            tbl_1.Focus();
        }



        //Поиск по NoJour
        private void SearchByJour()
        {
            lst_Horaire_copy.Clear();
            
            if (cmbNo.SelectedIndex >= 0)
            {
                string st = (cmbNo.SelectedItem as ComboBoxItem).Content.ToString();

                foreach (var item in lst_Horaire)
                {
                    if (item.NoJour == st)
                    {
                        lst_Horaire_copy.Add(item);
                        
                    }
                }
                this.lst_View.ItemsSource = lst_Horaire_copy;
                this.lst_View.Items.Refresh();
                tbl_1.Focus();
                
            }
            else
                MessageBox.Show("Nojour est vide!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        //<----------------------------------------------------->

    }
}
