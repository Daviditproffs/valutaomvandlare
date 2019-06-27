using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ValutaVäxling
{

    static class Globals
    {
        public static double kommision;
        public static string currencyToGet;
        public static string currencyToConvert;
        public static string kommisionChanged;

        public static int getSameCurrency;

        public static string result;
        public static string currencytest;
        public static dynamic json;
        public static dynamic currencyParseJson;

        public static float foreignCurrencyValue;
        public static double total;
        public static string totalString;
        public static string[] totalStringParse;




    }
    public partial class Form1 : Form
    {



        public Form1()
        {
            InitializeComponent();

            comboBox2.SelectedIndex = 0;

            comboBox1.SelectedIndex = 0;

            button2.Enabled = false;

        }



        private void button1_Click(object sender, EventArgs e)
        {

            var fromCurrency = comboBox2.SelectedItem.ToString();

            var toCurrency = comboBox1.SelectedItem.ToString();

            var sumToChange = 0;

            try
            {
                sumToChange = Int32.Parse(textBox3.Text);

                if (Globals.kommisionChanged == null)
                {
                    Globals.kommision = sumToChange * 0.01;

                }
                else
                {
                    var kommisiontoInt = Int32.Parse(Globals.kommisionChanged);

                    var totalKommision = (double)kommisiontoInt / 100;

                    Globals.kommision = sumToChange * totalKommision;
                }


                var afterKommission = sumToChange - Globals.kommision;

                string[] splitStringfrom = fromCurrency.Split('-');

                string[] splitStringto = toCurrency.Split('-');

                Globals.currencyToGet = splitStringfrom[0];

                Globals.currencyToConvert = splitStringto[0];

                label3.Text = "Belopp (" + Globals.currencyToGet + ")";

                label4.Text = "Att utbetala (" + Globals.currencyToConvert + ")";

                var i = 1;

                if (Globals.getSameCurrency < 1)
                {
                    Globals.getSameCurrency = +i;
                    var getCurrencyApi = " https://api.exchangeratesapi.io/latest?base=" + Globals.currencyToGet;

                    var webrequest = (HttpWebRequest)WebRequest.Create(getCurrencyApi);

                    using (var response = webrequest.GetResponse())
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        Globals.result = reader.ReadToEnd();

                        Globals.currencytest = Globals.currencyToConvert;

                        Globals.json = JsonConvert.DeserializeObject(Globals.result);

                        Globals.currencyParseJson = Globals.json.rates[Globals.currencyToConvert].ToString();

                        if (!float.TryParse(Globals.currencyParseJson, out Globals.foreignCurrencyValue))
                        {

                        }

                        Globals.total = afterKommission * Globals.foreignCurrencyValue;

                        Globals.totalString = Globals.total.ToString();

                        Globals.totalStringParse = Globals.totalString.Split(',');

                        textBox4.Text = Globals.totalStringParse[0];

                        button2.Enabled = true;
                    }
                }

                else
                {


                    Globals.currencytest = Globals.currencyToConvert;

                    Globals.json = JsonConvert.DeserializeObject(Globals.result);

                    Globals.currencyParseJson = Globals.json.rates[Globals.currencyToConvert].ToString();

                    if (!float.TryParse(Globals.currencyParseJson, out Globals.foreignCurrencyValue))
                    {

                    }

                    Globals.total = afterKommission * Globals.foreignCurrencyValue;

                    Globals.totalString = Globals.total.ToString();

                    Globals.totalStringParse = Globals.totalString.Split(',');

                    textBox4.Text = Globals.totalStringParse[0];

                    button2.Enabled = true;


                }
            }
            catch (Exception)
            {

                MessageBox.Show("Skriv in ett heltal!", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ComboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            Globals.getSameCurrency = 0;
            button2.Enabled = false;
        }

        private void ComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Kvitto kvittoForm = new Kvitto();

            kvittoForm.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            Inställningar settingsForm = new Inställningar();

            settingsForm.ShowDialog();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

            button2.Enabled = false;

        }


    }




}
