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
    }
}
