using ChmodConverterLib;
using ChmodConverterWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

//Piotr Bacior 15 722 - WSEI Krak�w

namespace ChmodConverterWeb.Controllers
{
    //Kontroler odpowiedzialny za obs�ug� konwersji uprawnie� plik�w (chmod) pomi�dzy zapisem symbolicznym i numerycznym.
    public class ChmodController : Controller
    {
        //Stosujemy [HttpGet] -> Wy�wietla u�ytkownikowi formularz konwersji (metoda GET).
        [HttpGet]
        public IActionResult Index()
        {
            //Zwracamy widok formularza konwersji (Index.cshtml)
            return View();
        }

        //Obs�uguje przetwarzanie danych przes�anych z formularza (sotsujemy do tego metody POST).
        //Pozwala zar�wno na konwersj� z zapisu symbolicznego do numerycznego, jak i odwrotnie.
        [HttpPost]
        public IActionResult Index(string input)
        {
            // Przekazujemy dane wej�ciowe do widoku, aby nie one znikn�y z formularza
            ViewBag.Input = input;

            try
            {
                //Definiujemy zmienn� result, kt�ra b�dzie przechowywa� wynik konwersji
                string result;

                //Dokonujemy konwersji z trybu symbolicznego (np. "rwxr-xr--") na numeryczny ("755")
                if (!string.IsNullOrEmpty(input) && input.Length == 9 && input.All(c => c == 'r' || c == 'w' || c == 'x' || c == '-'))
                {
                    //Wywo�ujemy metod� konwertuj�c� z zapisu symbolicznego na numeryczny
                    result = ChmodConverter.SymbolicToNumeric(input);

                    //Ustawiamy typ wyniku na "Zapis numeryczny"
                    ViewBag.ResultType = "Zapis numeryczny";
                }

                //Dokonujemy konwersji z trybu numerycznego (np. "754") na symboliczny ("rwxr-xr--")
                else if (!string.IsNullOrEmpty(input) && input.Length == 3 && input.All(char.IsDigit))
                {
                    //Wywo�ujemy metod� konwertuj�c� z zapisu numerycznego na symboliczny
                    result = ChmodConverter.NumericToSymbolic(input);

                    //Ustawiamy typ wyniku na "Zapis symboliczny"
                    ViewBag.ResultType = "Zapis symboliczny";
                }

                //W przypadku podania niepoprawnych danych wej�ciowych ustawiamy odpowiedni komunikat
                else
                {
                    //Ustawiamny komunikat o b��dzie
                    ViewBag.Error = "Wprowad� poprawny zapis uprawnie� (np. rwxr-xr-- lub 755).";

                    //Zwracamy widok z b��dem
                    return View();
                }

                // Przekazujemy wynik konwersji do widoku
                ViewBag.Result = result;
            }
            catch (Exception ex)
            {
                // Obs�uga nietypowych b��d�w pojawiaj�cych si� przy konwersji
                ViewBag.Error = $"B��d podczas przetwarzania: {ex.Message}";
            }

            // Zwracamy widok, w kt�rym wy�wietlamy wynik lub komunikat o b��dzie
            return View();
        }
    }
}