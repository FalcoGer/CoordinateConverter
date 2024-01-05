using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoordinateConverter.DCS.Tools
{
    /// <summary>
    /// Form to enter DTC Data for AH64
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormAH64DTC : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormAH64DTC"/> class.
        /// </summary>
        public FormAH64DTC()
        {
            InitializeComponent();
        }

        private void cbPresetVHFAllowRecvOnlyRange_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox self = sender as CheckBox;
            nudVHFFreq.Minimum = self.Checked ? 108 : 116;
        }
    }
}
