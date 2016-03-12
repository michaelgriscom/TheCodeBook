#region

using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

#endregion

namespace VigenereBreaker
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
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
            string key = KeyTextBox.Text;
            string cipherText = CipherTextBox.Text;
            var vignereEncrypter = new VigenereEncrypter();
            string plainText = vignereEncrypter.Decrypt(key, cipherText);
            PlainTextBox.Text = plainText.ToUpper();
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
            FrequencyTable.Items.Clear();
            var textAnalyzer = new TextAnalyzer();
            string cipherText = CrackingCipherTextBox.Text;
            int keyLength = int.Parse(KeyLengthTextBox.Text);
            var frequencies = textAnalyzer.DetermineFrequencies(cipherText, keyLength);
            foreach (var freq in frequencies)
            {
                FrequencyTable.Items.Add(freq);
            }
        }

        // taken from http://stackoverflow.com/questions/704724/programatically-add-column-rows-to-wpf-datagrid
        private void MakeColumns()
        {
            FrequencyTable.Columns.Add(
                new DataGridTextColumn
                {
                    Header = "KeyIndex",
                    Binding = new Binding("KeyIndex"),
                    Width = 110
                });
            FrequencyTable.Columns.Add(
                new DataGridTextColumn
                {
                    Header = "String",
                    Binding = new Binding("String"),
                    Width = 110
                });
            FrequencyTable.Columns.Add(
                new DataGridTextColumn
                {
                    Header = "Frequency",
                    Binding = new Binding("Frequency"),
                    Width = 110
                });

            FrequencyTable.Columns.Add(
                new DataGridTextColumn
                {
                    Header = "ExpectedFrequency",
                    Binding = new Binding("ExpectedFrequency"),
                    Width = 110
                });
            FrequencyTable.Columns.Add(
                new DataGridTextColumn
                {
                    Header = "Abnormality",
                    Binding = new Binding("Abnormality"),
                    Width = 110
                });
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
}