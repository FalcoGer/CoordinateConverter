using System;
using System.Windows.Forms;

namespace CoordinateConverter.DCS.Tools
{
    /// <summary>
    /// A form to select a starting waypoint for the F16
    /// </summary>
    /// <seealso cref="Form" />
    public partial class FormStartingWaypoint : Form
    {
        /// <summary>
        /// Gets the starting waypoint.
        /// </summary>
        /// <value>
        /// The starting waypoint.
        /// </value>
        public int StartingWaypoint { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="FormStartingWaypoint"/> class.
        /// </summary>
        public FormStartingWaypoint(int minSteerPoint, int maxSteerPoint, int defaultValue)
        {
            InitializeComponent();
            if (minSteerPoint > defaultValue || minSteerPoint > maxSteerPoint || maxSteerPoint < defaultValue)
            {
                throw new ArgumentException(string.Format("Arguments make no sense. Must be {0} ({3}) <= {1} ({4}) <= {2} ({5})", nameof(minSteerPoint), nameof(defaultValue), nameof(maxSteerPoint), minSteerPoint, defaultValue, maxSteerPoint));
            }
            lbl_FirstSPTPToUse.Text = string.Format("First STPT to use [{0} .. {1}]:", minSteerPoint, maxSteerPoint);
            nud_PointNumber.Minimum = minSteerPoint;
            nud_PointNumber.Maximum = maxSteerPoint;
            nud_PointNumber.Value = defaultValue;
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Nud_PointNumber_ValueChanged(object sender, EventArgs e)
        {
            StartingWaypoint = (int)((sender as NumericUpDown).Value);
        }
    }
}
