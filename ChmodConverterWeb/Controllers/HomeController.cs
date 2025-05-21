using ChmodConverterLib;
using ChmodConverterWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

//Piotr Bacior 15 722 - WSEI Kraków

namespace ChmodConverterWeb.Controllers
{
    //Kontroler odpowiedzialny za obs³ugê konwersji uprawnieñ plików (chmod) pomiêdzy zapisem symbolicznym i numerycznym.
    public class ChmodController : Controller
    {
        //Stosujemy [HttpGet] -> Wyœwietla u¿ytkownikowi formularz konwersji (metoda GET).
        [HttpGet]
        public IActionResult Index()
        {
            //Zwracamy widok formularza konwersji (Index.cshtml)
            return View();
        }

        //Obs³uguje przetwarzanie danych przes³anych z formularza (sotsujemy do tego metody POST).
        //Pozwala zarówno na konwersjê z zapisu symbolicznego do numerycznego, jak i odwrotnie.
        [HttpPost]
        public IActionResult Index(string input)
        {
            // Przekazujemy dane wejœciowe do widoku, aby nie one zniknê³y z formularza
            ViewBag.Input = input;

            try
            {
                //Definiujemy zmienn¹ result, która bêdzie przechowywaæ wynik konwersji
                string result;

                //Dokonujemy konwersji z trybu symbolicznego (np. "rwxr-xr--") na numeryczny ("755")
                if (!string.IsNullOrEmpty(input) && input.Length == 9 && input.All(c => c == 'r' || c == 'w' || c == 'x' || c == '-'))
                {
                    //Wywo³ujemy metodê konwertuj¹c¹ z zapisu symbolicznego na numeryczny
                    result = ChmodConverter.SymbolicToNumeric(input);

                    //Ustawiamy typ wyniku na "Zapis numeryczny"
                    ViewBag.ResultType = "Zapis numeryczny";
                }

                //Dokonujemy konwersji z trybu numerycznego (np. "754") na symboliczny ("rwxr-xr--")
                else if (!string.IsNullOrEmpty(input) && input.Length == 3 && input.All(char.IsDigit))
                {
                    //Wywo³ujemy metodê konwertuj¹c¹ z zapisu numerycznego na symboliczny
                    result = ChmodConverter.NumericToSymbolic(input);

                    //Ustawiamy typ wyniku na "Zapis symboliczny"
                    ViewBag.ResultType = "Zapis symboliczny";
                }

                //W przypadku podania niepoprawnych danych wejœciowych ustawiamy odpowiedni komunikat
                else
                {
                    //Ustawiamny komunikat o b³êdzie
                    ViewBag.Error = "WprowadŸ poprawny zapis uprawnieñ (np. rwxr-xr-- lub 755).";

                    //Zwracamy widok z b³êdem
                    return View();
                }

                // Przekazujemy wynik konwersji do widoku
                ViewBag.Result = result;
            }
            catch (Exception ex)
            {
                // Obs³uga nietypowych b³êdów pojawiaj¹cych siê przy konwersji
                ViewBag.Error = $"B³¹d podczas przetwarzania: {ex.Message}";
            }

            // Zwracamy widok, w którym wyœwietlamy wynik lub komunikat o b³êdzie
            return View();
        }
    }
}