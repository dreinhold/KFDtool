﻿using KFDtool.P25.Constant;
using KFDtool.P25.Generator;
using KFDtool.P25.TransferConstructs;
using KFDtool.P25.Validator;
using KFDtool.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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

namespace KFDtool.Gui.Control
{
    /// <summary>
    /// Interaction logic for P25Keyload.xaml
    /// </summary>
    public partial class P25Keyload : UserControl
    {
        public P25Keyload()
        {
            InitializeComponent();

            cbActiveKeyset.IsChecked = true; // check here to trigger the cb/txt logic on load
            cboAlgo.SelectedIndex = 0; // set to the first item here to trigger the cbo/txt logic on load            
        }

        private void OnActiveKeysetChecked(object sender, RoutedEventArgs e)
        {
            txtKeysetIdDec.Text = string.Empty;
            txtKeysetIdHex.Text = string.Empty;
            txtKeysetIdDec.IsEnabled = false;
            txtKeysetIdHex.IsEnabled = false;
        }

        private void OnActiveKeysetUnchecked(object sender, RoutedEventArgs e)
        {
            txtKeysetIdDec.IsEnabled = true;
            txtKeysetIdHex.IsEnabled = true;
        }

        private void KeysetIdDec_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtKeysetIdDec.IsFocused)
            {
                int num;

                if (int.TryParse(txtKeysetIdDec.Text, out num))
                {
                    txtKeysetIdHex.Text = string.Format("{0:X}", num);
                }
                else
                {
                    txtKeysetIdHex.Text = string.Empty;
                }
            }
        }

        private void KeysetIdHex_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtKeysetIdHex.IsFocused)
            {
                int num;

                if (int.TryParse(txtKeysetIdHex.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
                {
                    txtKeysetIdDec.Text = num.ToString();
                }
                else
                {
                    txtKeysetIdDec.Text = string.Empty;
                }
            }
        }

        private void SlnDec_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSlnDec.IsFocused)
            {
                int num;

                if (int.TryParse(txtSlnDec.Text, out num))
                {
                    txtSlnHex.Text = string.Format("{0:X}", num);
                }
                else
                {
                    txtSlnHex.Text = string.Empty;
                }
            }
        }

        private void SlnHex_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSlnHex.IsFocused)
            {
                int num;

                if (int.TryParse(txtSlnHex.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
                {
                    txtSlnDec.Text = num.ToString();
                }
                else
                {
                    txtSlnDec.Text = string.Empty;
                }
            }
        }

        private void KeyIdDec_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtKeyIdDec.IsFocused)
            {
                int num;

                if (int.TryParse(txtKeyIdDec.Text, out num))
                {
                    txtKeyIdHex.Text = string.Format("{0:X}", num);
                }
                else
                {
                    txtKeyIdHex.Text = string.Empty;
                }
            }
        }

        private void KeyIdHex_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtKeyIdHex.IsFocused)
            {
                int num;

                if (int.TryParse(txtKeyIdHex.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
                {
                    txtKeyIdDec.Text = num.ToString();
                }
                else
                {
                    txtKeyIdDec.Text = string.Empty;
                }
            }
        }

        private void OnAlgoChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboAlgo.SelectedItem != null)
            {
                string name = ((ComboBoxItem)cboAlgo.SelectedItem).Name as string;

                if (name == "AES256")
                {
                    txtAlgo.Text = "84";
                    txtAlgo.IsEnabled = false;
                }
                else if (name == "DESOFB")
                {
                    txtAlgo.Text = "81";
                    txtAlgo.IsEnabled = false;
                }
                else if (name == "DESXL")
                {
                    txtAlgo.Text = "9F";
                    txtAlgo.IsEnabled = false;
                }
                else if (name == "ADP")
                {
                    txtAlgo.Text = "AA";
                    txtAlgo.IsEnabled = false;
                }
                else
                {
                    txtAlgo.Text = string.Empty;
                    txtAlgo.IsEnabled = true;
                }
            }
        }

        private void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            int algId = 0;

            try
            {
                algId = Convert.ToInt32(txtAlgo.Text, 16);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Parsing Algorithm ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!FieldValidator.IsValidAlgorithmId(algId))
            {
                MessageBox.Show("Algorithm ID invalid - valid range 0 to 255 (dec), 0x00 to 0xFF (hex)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<byte> key = new List<byte>();

            if (algId == (byte)AlgorithmId.AES256)
            {
                key = KeyGenerator.GenerateVarKey(32);
            }
            else if (algId == (byte)AlgorithmId.DESOFB || algId == (byte)AlgorithmId.DESXL)
            {
                key = KeyGenerator.GenerateSingleDesKey();
            }
            else if (algId == (byte)AlgorithmId.ADP)
            {
                key = KeyGenerator.GenerateVarKey(5);
            }
            else
            {
                MessageBox.Show(string.Format("No key generator exists for algorithm ID 0x{0:X2}", algId), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            txtKey.Text = BitConverter.ToString(key.ToArray()).Replace("-", string.Empty);
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            int keysetId = 0;
            int sln = 0;
            int keyId = 0;
            int algId = 0;
            List<byte> key = new List<byte>();

            bool useActiveKeyset = cbActiveKeyset.IsChecked == true;

            if (useActiveKeyset)
            {
                keysetId = 2; // to pass validation, will not get used
            }
            else
            {
                try
                {
                    keysetId = Convert.ToInt32(txtKeysetIdHex.Text, 16);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error Parsing Keyset ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            try
            {
                sln = Convert.ToInt32(txtSlnHex.Text, 16);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Parsing SLN", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                keyId = Convert.ToInt32(txtKeyIdHex.Text, 16);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Parsing Key ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                algId = Convert.ToInt32(txtAlgo.Text, 16);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Parsing Algorithm ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                key = Utility.ByteStringToByteList(txtKey.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Parsing Key", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Tuple<ValidateResult, string> validateResult = FieldValidator.KeyloadValidate(keysetId, sln, keyId, algId, key);

            if (validateResult.Item1 == ValidateResult.Warning)
            {
                if (MessageBox.Show(string.Format("{1}{0}{0}Continue?", Environment.NewLine, validateResult.Item2), "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
            }
            else if (validateResult.Item1 == ValidateResult.Error)
            {
                MessageBox.Show(validateResult.Item2, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Interact.Keyload(Settings.Port, useActiveKeyset, keysetId, sln, keyId, algId, key);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error -- {0}", ex.Message), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Key Loaded Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
