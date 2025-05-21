using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChmodConverterLib;

namespace ChmodConverterTests
{
    [TestClass]
    public class ChmodConverterTests
    {
        //Testujemy poprawność podstawowych przypadków konwersji z trybu numerycznego na symboliczny
        [TestMethod]
        public void SymbolicToNumeric_PodstawowyPrzypadekZamiany()
        {
            Assert.AreEqual("777", ChmodConverter.SymbolicToNumeric("rwxrwxrwx"));
            Assert.AreEqual("770", ChmodConverter.SymbolicToNumeric("rwxrwx---"));
            Assert.AreEqual("700", ChmodConverter.SymbolicToNumeric("rwx------"));
            Assert.AreEqual("444", ChmodConverter.SymbolicToNumeric("r--r--r--"));
            Assert.AreEqual("440", ChmodConverter.SymbolicToNumeric("r--r-----"));
            Assert.AreEqual("400", ChmodConverter.SymbolicToNumeric("r--------"));
            Assert.AreEqual("000", ChmodConverter.SymbolicToNumeric("---------"));
            Assert.AreEqual("751", ChmodConverter.SymbolicToNumeric("rwxr-x--x"));
        }

        //Testujemy poprawność podstawowych przypadków konwersji z trybu symbolicznego na numeryczny
        [TestMethod]
        public void NumericToSymbolic_PodstawowyPrzypadekZamiany()
        {
            Assert.AreEqual("rwxrwxrwx", ChmodConverter.NumericToSymbolic("777"));
            Assert.AreEqual("rwxrwx---", ChmodConverter.NumericToSymbolic("770"));
            Assert.AreEqual("rwx------", ChmodConverter.NumericToSymbolic("700"));
            Assert.AreEqual("r--------", ChmodConverter.NumericToSymbolic("400"));
            Assert.AreEqual("r--r--r--", ChmodConverter.NumericToSymbolic("444"));
            Assert.AreEqual("---------", ChmodConverter.NumericToSymbolic("000"));
            Assert.AreEqual("rwx-wx--x", ChmodConverter.NumericToSymbolic("731"));
        }

        //Teraz testujemy poprawnośc konwersji z trybu numerycznego na symboliczny dla pojedynczej cyfry
        [TestMethod]
        public void NumericToSymbolic_PojedynczaCyfra()
        {
            string[] expected = { "---", "--x", "-w-", "-wx", "r--", "r-x", "rw-", "rwx" };
            for (int i = 0; i <= 7; i++)
            {
                string num = i.ToString();
                //Testujemy jako user
                Assert.AreEqual(expected[i], ChmodConverter.NumericToSymbolic(num + "00").Substring(0, 3));
                //Terstujemy jako group
                Assert.AreEqual(expected[i], ChmodConverter.NumericToSymbolic("0" + num + "0").Substring(3, 3));
                //Testujemy jako other
                Assert.AreEqual(expected[i], ChmodConverter.NumericToSymbolic("00" + num).Substring(6, 3));
            }
        }

        //Testujemy poprawność podstawowych przypadków błędów w konwersji z trybu symbolicznego na numeryczny
        [TestMethod]
        public void SymbolicToNumeric_WszystkieSymbole()
        {
            Assert.AreEqual("000", ChmodConverter.SymbolicToNumeric("---------"));
            Assert.AreEqual("111", ChmodConverter.SymbolicToNumeric("--x--x--x"));
            Assert.AreEqual("222", ChmodConverter.SymbolicToNumeric("-w--w--w-"));
            Assert.AreEqual("333", ChmodConverter.SymbolicToNumeric("-wx-wx-wx"));
            Assert.AreEqual("444", ChmodConverter.SymbolicToNumeric("r--r--r--"));
            Assert.AreEqual("555", ChmodConverter.SymbolicToNumeric("r-xr-xr-x"));
            Assert.AreEqual("666", ChmodConverter.SymbolicToNumeric("rw-rw-rw-"));
            Assert.AreEqual("777", ChmodConverter.SymbolicToNumeric("rwxrwxrwx"));
        }

        //Testujemy wywołanie konwersji symbolicznej o zbyt krótkim ciągu symboli
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SymbolicToNumeric_ZbytKrotki()
        {
            ChmodConverter.SymbolicToNumeric("rwxrw");
        }

        //Testujemy wywołanie konwersji symbolicznej o zbyt długim ciągu symboli
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SymbolicToNumeric_ZbytDlugi()
        {
            ChmodConverter.SymbolicToNumeric("rwxrwxrwxr");
        }

        //Testujemy wywołanie konwersji symbolicznej z użyciem niepoprawnych znaków
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SymbolicToNumeric_NiewlasciweZnaki()
        {
            ChmodConverter.SymbolicToNumeric("rwxrwTr--");
        }

        //Testujemy wywołanie konwersji numerycznej na symbole o zbyt krótkim ciągu cyfr 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolic_ZbytKrotki()
        {
            ChmodConverter.NumericToSymbolic("44");
        }

        //Testujemy wywołanie konwersji numerycznej na symbole o zbyt długim ciągu cyfr
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolic_ZbytDlugi()
        {

            ChmodConverter.NumericToSymbolic("4444");
        }

        //Testujemy wywołanie konwersji numerycznej na symbole z użyciem niepoprawnych znaków
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolic_NiedozwoloneZnaki()
        {
            ChmodConverter.NumericToSymbolic("4a4");
        }

        //Testujemy wywołanie konwersji numerycznej na symbole z użyciem cyfr poza zakresem 0-7
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolic_CyfryPozaZakresem()
        {
            ChmodConverter.NumericToSymbolic("999");
        }


        //Testujemy wywołanie konwersji numerycznej na pustym ciągu znaków
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolic_PustyStringNaWejsciu()
        {
            ChmodConverter.NumericToSymbolic("");
        }

        //Testujemy wywołanie konwersji numerycznej na null
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolic_NullNaWejsciu()
        {
            ChmodConverter.NumericToSymbolic(null);
        }

        //Testujemy wywołanie konwersji symbolicznej na pustym ciągu znaków
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SymbolicToNumeric_PustyStringNaWejsciu()
        {
            ChmodConverter.NumericToSymbolic("");
        }

        //Testujemy wywołanie konwersji symbolicznej na null
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SymbolicToNumeric_NullNaWejsciu()
        {
            ChmodConverter.SymbolicToNumeric(null);
        }

        // Testuje przypadek, gdy w numeric występuje cyfra większa niż 7 (np. '8')
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolic_CyfraPozaZakresem_8()
        {
            ChmodConverter.NumericToSymbolic("782"); // '8' jest poza zakresem
        }

        //Testuje przypadek, gdy w numeric występuje spacja między cyframi (np. '7 4')
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolic_ZawieraSpacje()
        {
            ChmodConverter.NumericToSymbolic("7 4"); // spacja w środku
        }
    }

}
