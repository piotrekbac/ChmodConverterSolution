namespace ChmodConverterLib
{
    //Piotr Bacior - 15 722 - WSEI Kraków 
    public class ChmodConverter
    {
        //Definujemy metodę, która odpowiadać będzie za konwersję trybu symbolicznego na numeryczny 
        public static string SymbolicToNumeric(string symbolic)
        {
            //Sprawdzamy czy wejście jest poprawne, a dokładniej czy nie składa się z nulla - jeżeli tak, to rzucamy wyjątek ArgumentException
            if (symbolic == null)
                throw new ArgumentException("Tryb symboliczny nie może być null!");

            //Sprawdzamy czy wejście jest poprawne, czyli czy ma 9 znaków - jeżeli nie, to rzucamy wyjątek ArgumentException
            if (symbolic.Length != 9)
                throw new ArgumentException("Tryb symboliczny musi mieć 9 znaków!");

            //Sprawdzamy czy wejście jest poprawne, a dokładniej czy składa się ono z 9 znaków oraz czy są to znaki r, w, x, - - jeżeli nie, to rzucamy wyjątek ArgumentException
            if (!symbolic.All(c => c == 'r' || c == 'w' || c == 'x' || c == '-'))
                throw new ArgumentException("Tryb symboliczny może zawierać tylko znaki r, w, x, -!");

            //Definiujemy teraz pustą zmienną wynik typu string, w której będziemy przechowywać wynik konwersji 
            string wynik = "";

            //Iterujemy po każdym z 3 bloków po 3 znaki, które odpowiadają za uprawnienia dla właściciela, grupy i innych użytkowników (przykład. rwx, rw-, r--)
            for (int i = 0; i < 9; i += 3)
            {
                //Definujemy zmienną wartosc typu int, ustawiamy na 0, która będzie odpowiadać za wartość uprawnień w systemie numerycznym (przykład. r=4, w=2, x=1)
                int wartosc = 0;

                //Sprawdzamy każdy znak oraz dodajemy odpowiednią wartość do wcześniej zdefiniowanej zmiennej wartosc (r = 4, w = 2, x = 1)
                //Znak r dodaje 4, jeżeli jest obecny, odpowiada za pozycje 0, 3 oraz 6 w stringu symbolic 
                if (symbolic[i] == 'r') wartosc += 4;

                //Znak w dodaje 2, jeżeli jest obecny, odpowiada za pozycje 1, 4 oraz 7 w stringu symbolic
                if (symbolic[i + 1] == 'w') wartosc += 2;

                //Znak x dodaje 1, jeżeli jest obecny, odpowiada za pozycje 2, 5 oraz 8 w stringu symbolic
                if (symbolic[i + 2] == 'x') wartosc += 1;

                //Teraz dodajemy wartość zmiennej wartosc do zmiennej wynik, która odpowiadać będzie za wynik całej konwersji
                wynik += wartosc.ToString();
            }

            //Na końcu zwracamy wynik konwersji, który jest w formacie numerycznym (przykład. 777, 400, 731)
            return wynik;
        }

        //Definiujemy metodę, która odpowiadać będzie za konwersję trybu numerycznego na symboliczny
        public static string NumericToSymbolic(string numeric)
        {
            //Sprawdzamy czy wejście jest poprawne, a dokładniej czy składa się ono z 3 cyfr oraz czy są to cyfry (0-9) - jeżeli nie, to rzucamy wyjątek ArgumentException
            if (numeric == null || numeric.Length != 3 || !numeric.All(char.IsDigit))
                throw new ArgumentException("Tryb numeryczny musi mieć 3 znaki!");

            foreach (char c in numeric)
            {
                if (c < '0' || c > '7')
                    throw new ArgumentException("Cyfry muszą być w zakresie 0-7!");
            }

            string wynik = "";
            foreach (char znak in numeric)
            {
                int value = znak - '0';
                wynik += (value & 4) != 0 ? "r" : "-";
                wynik += (value & 2) != 0 ? "w" : "-";
                wynik += (value & 1) != 0 ? "x" : "-";
            }
            return wynik;
        }
    }
}
