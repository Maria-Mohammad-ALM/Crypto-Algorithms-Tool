using System;
using System.Linq;
using System.Windows;
using EncryptionSuite.Ciphers;

namespace EncryptionSuite
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CipherSelector.Items.Add("Caesar Cipher");
            CipherSelector.Items.Add("Monoalphabetic Cipher");
            CipherSelector.Items.Add("Vigenere Cipher");
            CipherSelector.Items.Add("Playfair Cipher");
            CipherSelector.Items.Add("Row Transposition Cipher");
            CipherSelector.Items.Add("Rail Fence Cipher");
            CipherSelector.Items.Add("Fistel Cipher");


        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string text = InputText.Text;
            string key = KeyBox.Text;
            string cipher = CipherSelector.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(cipher))
            {
                MessageBox.Show("Please select a cipher method.");
                return;
            }

            try
            {
                switch (cipher)
                {
                    case "Caesar Cipher":
                        if (!int.TryParse(key, out int caesarKey))
                            throw new ArgumentException("Key must be a number for Caesar Cipher.");
                        OutputText.Text = Caesar.Encrypt(text, caesarKey);
                        break;

                    case "Monoalphabetic Cipher":
                        OutputText.Text = Monoalphabetic.Encrypt(text, key);
                        break;

                    case "Playfair Cipher":
                        OutputText.Text = Playfair.Encrypt(text, key);
                        break;

                    case "Fistel Cipher":
                        OutputText.Text = Feistel.Encrypt(text, key);
                        break;

                    case "Vigenere Cipher":
                        OutputText.Text = Vigenere.Encrypt(text, key);
                        break;

                    case "Row Transposition Cipher":
                        OutputText.Text = RowTransposition.Encrypt(text, key);
                        break;

                    case "Rail Fence Cipher":
                        if (!int.TryParse(key, out int railKey))
                            throw new ArgumentException("Key must be a number for Rail Fence Cipher.");
                        OutputText.Text = RailFence.Encrypt(text, railKey);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            string text = InputText.Text;
            string key = KeyBox.Text;
            string cipher = CipherSelector.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(cipher))
            {
                MessageBox.Show("Please select a cipher method.");
                return;
            }

            try
            {
                switch (cipher)
                {
                    case "Caesar Cipher":
                        if (!int.TryParse(key, out int caesarKey))
                            throw new ArgumentException("Key must be a number for Caesar Cipher.");
                        OutputText.Text = Caesar.Decrypt(text, caesarKey);
                        break;

                    case "Monoalphabetic Cipher":
                        OutputText.Text = Monoalphabetic.Decrypt(text, key);
                        break;


                    case "Fistel Cipher":
                        OutputText.Text = Feistel.Decrypt(text, key);
                        break;

                    case "Playfair Cipher":
                        OutputText.Text = Playfair.Decrypt(text, key);
                        break;

                    case "Vigenere Cipher":
                        OutputText.Text = Vigenere.Decrypt(text, key);
                        break;

                    case "Row Transposition Cipher":
                        OutputText.Text = RowTransposition.Decrypt(text, key);
                        break;

                    case "Rail Fence Cipher":
                        if (!int.TryParse(key, out int railKey))
                            throw new ArgumentException("Key must be a number for Rail Fence Cipher.");
                        OutputText.Text = RailFence.Decrypt(text, railKey);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            InputText.Clear();
            KeyBox.Clear();
            OutputText.Clear();
            CipherSelector.SelectedIndex = -1;
        }

        private void InputText_GotFocus(object sender, RoutedEventArgs e)
        {
            if (InputText.Text == "Enter text here...")
                InputText.Clear();
        }

        private void KeyBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (KeyBox.Text == "Enter key (if needed)")
                KeyBox.Clear();
        }
    }

    internal class fistel
    {
        internal static string EncryptFeistel(string text, string key)
        {
            throw new NotImplementedException();
        }
    }
}
