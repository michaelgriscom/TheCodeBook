#region

using Microsoft.VisualStudio.TestTools.UnitTesting;
using VigenereBreaker;

#endregion

namespace VigenereTests
{
    [TestClass]
    public class EncryptionTests
    {
        // samples taken from The Code Book
        [TestMethod]
        public void EncryptSample1()
        {
            string key = "KING";
            string plainText = "thesunandthemaninthemoon";
            string expectedCipherText = "DPRYEVNTNBUKWIAOXBUKWWBT";

            VigenereEncrypter encrypter = new VigenereEncrypter();
            string cipherText = encrypter.Encrypt(key, plainText);
            Assert.AreEqual(expectedCipherText.ToUpper(), cipherText.ToUpper());
        }

        [TestMethod]
        public void EncryptSample2()
        {
            string key = "WHITE";
            string plainText = "diverttroopstoeastridge";
            string expectedCipherText = "ZPDXVPAZHSLZBHIWZBKMZNM";

            VigenereEncrypter encrypter = new VigenereEncrypter();
            string cipherText = encrypter.Encrypt(key, plainText);
            Assert.AreEqual(expectedCipherText.ToUpper(), cipherText.ToUpper());
        }

        [TestMethod]
        public void DecryptSample1()
        {
            string key = "KING";
            string expectedPlainText = "thesunandthemaninthemoon";
            string cipherText = "DPRYEVNTNBUKWIAOXBUKWWBT";

            VigenereEncrypter encrypter = new VigenereEncrypter();
            string plainText = encrypter.Decrypt(key, cipherText);
            Assert.AreEqual(expectedPlainText.ToUpper(), plainText.ToUpper());
        }

        [TestMethod]
        public void DecryptSample2()
        {
            string key = "WHITE";
            string expectedPlainText = "diverttroopstoeastridge";
            string cipherText = "ZPDXVPAZHSLZBHIWZBKMZNM";

            VigenereEncrypter encrypter = new VigenereEncrypter();
            string plainText = encrypter.Decrypt(key, cipherText);
            Assert.AreEqual(expectedPlainText.ToUpper(), plainText.ToUpper());
        }

        [TestMethod]
        public void EncryptChar()
        {
            char plainLetter = 't';
            char cipherLetter = 'd';
            char keyLetter = 'k';

            VigenereEncrypter encrypter = new VigenereEncrypter();
            char actualCipherLetter = encrypter.EncryptChar(keyLetter, plainLetter);
            Assert.AreEqual(actualCipherLetter, cipherLetter);
        }

        [TestMethod]
        public void DecryptChar()
        {
            char plainLetter = 't';
            char cipherLetter = 'd';
            char keyLetter = 'k';

            VigenereEncrypter encrypter = new VigenereEncrypter();
            char actualPlainLetter = encrypter.DecryptChar(keyLetter, cipherLetter);
            Assert.AreEqual(actualPlainLetter, plainLetter);
        }

        [TestMethod]
        public void CalculateKey()
        {
            char plainLetter = 't';
            char cipherLetter = 'd';
            char keyLetter = 'k';

            VigenereEncrypter encrypter = new VigenereEncrypter();
            char actualKeyLetter = encrypter.CalculateKey(plainLetter, cipherLetter);
            Assert.AreEqual(actualKeyLetter, keyLetter);
        }
    }
}