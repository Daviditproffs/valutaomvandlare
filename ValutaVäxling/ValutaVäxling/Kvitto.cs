using Newtonsoft.Json;
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
    public partial class Kvitto : Form
    {
       

        public Kvitto()
        {

            InitializeComponentKvitto();

            float oneForeignToLocal;
            var getCurrencyApi = " https://api.exchangeratesapi.io/latest?base=" + Globals.currencyToConvert;

            var webrequest = (HttpWebRequest)WebRequest.Create(getCurrencyApi);

            using (var response = webrequest.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
             
                dynamic json = JsonConvert.DeserializeObject(result);

                var currencyParseJson = json.rates[Globals.currencyToGet].ToString();


                if (!float.TryParse(currencyParseJson, out oneForeignToLocal))
                {

                }

            }
        
            Form f = Application.OpenForms["Form1"];
            label6.Text = ((Form1)f).textBox3.Text +" "+ Globals.currencyToGet;
            label7.Text = Globals.kommision +" " + Globals.currencyToGet;
            label8.Text = ((Form1)f).textBox4.Text +" "+ Globals.currencyToConvert;
            label9.Text = "1 " + Globals.currencyToConvert + " = " + oneForeignToLocal + " "+ Globals.currencyToGet;

            ((Form1)f).button2.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Kvitto
            // 
            this.ClientSize = new System.Drawing.Size(686, 570);
            this.Name = "Kvitto";
            this.ResumeLayout(false);

        }
    }
}
