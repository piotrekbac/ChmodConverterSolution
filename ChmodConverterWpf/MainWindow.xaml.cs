using ChmodConverterLib;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//Piotr Bacior 15 722 - WSEI Kraków

namespace ChmodConverterWpf
{
    ///Główne okno naszej aplikacji, uprawniające do konwersji trybu symbolicznego na numeryczny oraz odwrotnie 

    //Definiujemy klasę MainWindow, która dziedziczy po klasie Window
    public partial class MainWindow : Window
    {
        //Teraz definiujemy zmienną, która odpowiadać będzie za blokadę aktualizacji pól tekstowych, aby uniknąć cyklicznej aktualizacji
        private bool _isUpdating = false;

        ///Definiujemy konstruktor klasy MainWindow, który odpowiada za inicjalizację okna
        public MainWindow()
        {
            //Inicjalizujemy komponenty okna
            InitializeComponent();
        }

        // Reaguje na zmianę pola z symbolicznym zapisem(np.rwxr-xr--) - automatycznie przelicza i aktualizauje pole z zapisem numerycznym
        private void Symbolic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating)
                return;

            //Blokujemy aktualizację
            _isUpdating = true; 

            try
            {
                //Definiujemy zmienną symbolic, która odpowiadać będzie za zapis symboliczny
                string symbolic = SymbolicInput.Text;

                //Sprawdzamy czy string symbolic jest pusty - jeżeli tak, to rzucamy błąd 
                if (string.IsNullOrEmpty(symbolic))
                {
                    NumericOutput.Text = "";
                    ErrorText.Text = "Błędny zapis symboliczny!";
                    return;
                }
                else
                {
                    //Sprawdzamy czy wejście składa się z 9 znaków oraz czy są to znaki r, w, x, - odpowiadające za zapis symboliczny
                    NumericOutput.Text = ChmodConverter.SymbolicToNumeric(symbolic);

                    //Czyścimy komunikat o błędzie
                    ErrorText.Text = "";
                }
            }
            catch
            {
                NumericOutput.Text = "";
                ErrorText.Text = "Podano niepoprawny zapis symboliczny!";
            }
            finally
            {
                //Odblokowujemy aktualizację
                _isUpdating = false; 
            }

        }

        //Reaguje na zmianę pola z zapisem numerycznym (np. 754)
        //Automatycznie przelicza i aktualizauje pole z zapisem symbolicznym
        private void Numeric_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating)
                return;

            //Blokujemy aktualizację
            _isUpdating = true;

            try
            {
                string numeric = NumericInput.Text;

                if (string.IsNullOrEmpty(numeric))
                {
                    SymbolicOutput.Text = "";
                    ErrorText.Text = "";
                }
                else
                {
                    //Sprawdzamy czy wejście składa się z 3 cyfr oraz czy są to cyfry (0-7) odpowiadające za zapis numeryczny
                    SymbolicOutput.Text = ChmodConverter.NumericToSymbolic(numeric);

                    //Czyścimy komunikat o błędzie
                    ErrorText.Text = "";
                }
            }
            catch
            {
                SymbolicOutput.Text = "";
                ErrorText.Text = "Podano nieprawidłowy zapis numeryczny!";
            }
            finally
            {
                //Odblokowujemy aktualizację
                _isUpdating = false;
            }
        }
    }
}