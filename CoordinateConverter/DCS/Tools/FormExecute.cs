using CoordinateConverter.DCS.Aircraft;
using CoordinateConverter.DCS.Communication;
using System;
using System.Windows.Forms;

namespace CoordinateConverter.DCS.Tools
{
    public partial class FormExecute : Form
    {
        public FormExecute(Form parent)
        {
            InitializeComponent();

            ShowDialog(parent);
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            DCSMessage message = new DCSMessage()
            { 
                Execute = tb_code.Text
            };
            message = DCSConnection.SendRequest(message);

            var newtonsoftSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                Formatting = Newtonsoft.Json.Formatting.Indented,
                Culture = System.Globalization.CultureInfo.InvariantCulture,
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All,
                StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.Default,
            };

            string sep = new string('=', 40);
            string nl = Environment.NewLine;

            tb_output.Text = message.TimeStamp.ToString("G") + nl + sep + nl + message.Execute + nl + sep + nl + Newtonsoft.Json.JsonConvert.SerializeObject(message.ServerErrors, newtonsoftSettings);
        }
    }
}
