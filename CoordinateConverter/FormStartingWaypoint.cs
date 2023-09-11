using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoordinateConverter
{
    /// <summary>
    /// A form to select a starting waypoint for the F16
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormF16StartingWaypoint : Form
    {
        /// <summary>
        /// Gets the starting waypoint.
        /// </summary>
        /// <value>
        /// The starting waypoint.
        /// </value>
        public int StartingWaypoint { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="FormF16StartingWaypoint"/> class.
        /// </summary>
        public FormF16StartingWaypoint()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void nud_PointNumber_ValueChanged(object sender, EventArgs e)
        {
            StartingWaypoint = (int)((sender as NumericUpDown).Value);
        }
    }
}
