using CoordinateConverter.DCS.Aircraft;
using CoordinateConverter.DCS.Communication;
using System;
using System.Windows.Forms;

namespace CoordinateConverter.DCS.Tools
{
    /// <summary>
    /// A form for executing custom lua code.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormExecute : Form
    {
        private MainForm parent;
        /// <summary>
        /// Initializes a new instance of the <see cref="FormExecute"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public FormExecute(MainForm parent)
        {
            this.parent = parent;
            InitializeComponent();

            ShowDialog(this.parent);
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            DCSMessage message = new DCSMessage()
            { 
                Execute = tb_code.Text
            };
            message = DCSConnection.SendRequest(message);

            if (message == null)
            {
                return;
            }

            var newtonsoftSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                Formatting = Newtonsoft.Json.Formatting.Indented,
                Culture = System.Globalization.CultureInfo.InvariantCulture,
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects,
                StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.Default,
            };

            string sep = new string('=', 40);
            string nl = Environment.NewLine;

            tb_output.Text = message.TimeStamp.ToString("G") + nl + sep + nl + message.Execute + nl + sep + nl + Newtonsoft.Json.JsonConvert.SerializeObject(message.ServerErrors, newtonsoftSettings);
        }
    }
}
