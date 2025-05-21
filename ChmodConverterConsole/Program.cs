using ChmodConverterLib;
using System; 

//Piotr Bacior 15 722 - WSEI Kraków

class Program
{
    static void Main(string[] args)
    {
        //Sprawdzamy czy użytkownik podał odpowiednie argumenty do programu
        if (args.Length  != 1)
        {
            Console.WriteLine("Używamy trybu symbolicznego/numerycznego programu chmodconverter");
            return;

            //Prawym na ChmodConverterConsole i wybieram "Właściwości" -> "Debug" -> w zakłdace Debugowanie wpisałem: "rwxrwxrwx" czyli w wyniku powinienem otrzymać 777
        }

        //Pobieramy teraz argument podany przez użytkownika
        string input = args[0];

        try
        {
            //Sprawdzamy czy wejście składa się z 9 znaków oraz czy są to znaki r, w, x, - odpowiadające za zapis symboliczny
            if (input.Length == 9 && input.All(c => "rwx-".Contains(c)))
            {
                string numeric = ChmodConverter.SymbolicToNumeric(input);
                Console.WriteLine(numeric);
            }
            //Sprawdzamy czy wejście składa się z 3 cyfr oraz czy są to cyfry (0-7) odpowiadające za zapis numeryczny
            else if (input.Length == 3 && input.All(char.IsDigit))
            {
                string symbolic = ChmodConverter.NumericToSymbolic(input);
                Console.WriteLine(symbolic);
            }
            else
            {
                Console.WriteLine("Niepoprawny format wejścia. Użyj trybu symbolicznego lub numerycznego.");
            }
        }
        catch (Exception ex)
        {
            //W przypadku wystąpienia błędu, wyświetlamy komunikat o błędzie
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
    }
}