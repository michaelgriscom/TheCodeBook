using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace VigenereBreaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MakeColumns();
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string key = KeyTextBox.Text;
            string plainText = PlainTextBox.Text;
            var vignereEncrypter = new VigenereEncrypter();
            string cipherText = vignereEncrypter.Encrypt(key, plainText);
            CipherTextBox.Text = cipherText.ToUpper();
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {

        }

        // taken from http://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void Analyze_Click(object sender, RoutedEventArgs e)
        {
            var textAnalyzer = new TextAnalyzer();
            string cipherText = CrackingCipherTextBox.Text;
            int keyLength = int.Parse(KeyLengthTextBox.Text);
            var frequencies = textAnalyzer.DetermineFrequencies(cipherText, keyLength);
            foreach (var freq in frequencies)
            {
                DataGridItem dgi = new DataGridItem() {String = freq.Key, Frequency = freq.Value};
                FrequencyTable.Items.Add(dgi);
            }
        }

        // taken from http://stackoverflow.com/questions/704724/programatically-add-column-rows-to-wpf-datagrid
        private void MakeColumns()
        {
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "String";
            c1.Binding = new Binding("String");
            c1.Width = 110;
            FrequencyTable.Columns.Add(c1);
            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Frequency";
            c2.Width = 110;
            c2.Binding = new Binding("Frequency");
            FrequencyTable.Columns.Add(c2);
        }

        private void CalculatePlainLetter_Click(object sender, RoutedEventArgs e)
        {
            var vignereEncrypter = new VigenereEncrypter();
            char cipherLetter = CipherLetterTextBox.Text[0];
            char keyLetter = KeyLetterTextBox.Text[0];
            char plainLetter = vignereEncrypter.DecryptChar(keyLetter, cipherLetter);
            PlainLetterTextBox.Text = plainLetter.ToString();
        }

        private void CalculateKeyLetter_Click(object sender, RoutedEventArgs e)
        {
            var vignereEncrypter = new VigenereEncrypter();
            char cipherLetter = CipherLetterTextBox.Text[0];
            char plainLetter = PlainLetterTextBox.Text[0];
            char keyLetter = vignereEncrypter.CalculateKey(plainLetter, cipherLetter);
            KeyLetterTextBox.Text = keyLetter.ToString();
        }

        private void CalculateCipherLetter_Click(object sender, RoutedEventArgs e)
        {
            var vignereEncrypter = new VigenereEncrypter();
            char plainLetter = PlainLetterTextBox.Text[0];
            char keyLetter = KeyLetterTextBox.Text[0];
            char cipherLetter = vignereEncrypter.EncryptChar(keyLetter, plainLetter);
            CipherLetterTextBox.Text = cipherLetter.ToString();
        }
    }

    public class DataGridItem
    {
        public string String { get; set; }
        public int Frequency { get; set; }
    }
}
