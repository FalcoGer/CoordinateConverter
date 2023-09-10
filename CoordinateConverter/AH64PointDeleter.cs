﻿using CoordinateConverter.DCS.Aircraft;
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
    /// A tool to delete pointy from the AH64
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class AH64PointDeleter : Form
    {
        public int NumberOfCommands { get; private set; } = 0;
        private AH64 selectedAircraft;
        /// <summary>
        /// Initializes a new instance of the <see cref="AH64PointDeleter"/> class.
        /// </summary>
        public AH64PointDeleter(AH64 selectedAircraft)
        {
            InitializeComponent();
            cb_PointType.Items.Clear();
            cb_PointType.DisplayMember = "Text";
            cb_PointType.ValueMember = "Value";
            cb_PointType.Items.Add(new ComboItem<AH64.EPointType>("WP/HZ", AH64.EPointType.Waypoint));
            cb_PointType.Items.Add(new ComboItem<AH64.EPointType>("CM", AH64.EPointType.ControlMeasure));
            cb_PointType.Items.Add(new ComboItem<AH64.EPointType>("TG", AH64.EPointType.Target));
            cb_PointType.SelectedIndex = 0;

            this.selectedAircraft = selectedAircraft;
        }

        private void cb_PointType_SelectedIndexChanged(object objSender, EventArgs e)
        {
            ComboBox sender = objSender as ComboBox;
            AH64.EPointType pointType = ComboItem<AH64.EPointType>.GetSelectedValue(sender);
            // Update Minima
            if (pointType == AH64.EPointType.ControlMeasure)
            {
                nud_firstPointIdx.Minimum = 51;
                nud_lastPointIdx.Minimum = 51;
            }
            else
            {
                nud_firstPointIdx.Minimum = 1;
                nud_lastPointIdx.Minimum = 1;
            }
            // Update Maxima
            nud_firstPointIdx.Maximum = nud_firstPointIdx.Minimum + 49;
            nud_lastPointIdx.Maximum = nud_lastPointIdx.Minimum + 49;
            
            // Set Values
            nud_firstPointIdx.Value = nud_firstPointIdx.Minimum;
            nud_lastPointIdx.Value = nud_lastPointIdx.Maximum;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            NumberOfCommands = 0;
            Close();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            AH64.EPointType pointType = ComboItem<AH64.EPointType>.GetSelectedValue(cb_PointType);
            NumberOfCommands = selectedAircraft.ClearPoints(pointType, (int)nud_firstPointIdx.Value, (int)nud_lastPointIdx.Value);
            Close();
        }
    }
}
