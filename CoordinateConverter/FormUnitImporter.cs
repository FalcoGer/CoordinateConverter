using CoordinateConverter.DCS.Aircraft;
using CoordinateConverter.DCS.Communication;
using Newtonsoft.Json;
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
    public partial class FormUnitImporter : Form
    {
        private int nextPointID;
        private List<DCSUnit> allDCSUnits = null;
        public List<CoordinateDataEntry> Coordinates { get; set; } = null;
        private Dictionary<int, CoordinateDataEntry> referencePoints = null;

        private void UpdateAllUnitsFromDCS()
        {
            DCSMessage message = new DCSMessage()
            {
                FetchUnits = true
            };
            message = DCSConnection.sendRequest(message);
            if (message != null && message.Units != null)
            {
                // ignore deactivated and pilots and crap
                allDCSUnits = message.Units.Where(x => x.Flags.IsActivated == true && x.Type.Level3 != DCSUnitTypeInformation.ELevel3Type.Parts).ToList();
            }
            else
            {
                allDCSUnits = null;
            }
            Filter();
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            Coordinates = new List<CoordinateDataEntry>();
            foreach (DataGridViewRow row in dgv_Units.Rows)
            {
                if ((bool)(row.Cells[5] as DataGridViewCheckBoxCell).Value)
                {
                    int unitId = int.Parse(row.Cells[0].Value as string);
                    DCSUnit unit = allDCSUnits.First(x => x.ObjectId == unitId);
                    DCSCoordinate dcsCoord = unit.Coordinate;
                    CoordinateSharp.Coordinate coordinate = new CoordinateSharp.Coordinate(dcsCoord.Lat, dcsCoord.Lon);
                    string freetext = unit.TypeName;
                    if (freetext.StartsWith("SA-"))
                    {
                        freetext = "S" + freetext.Substring(3);
                    }
                    freetext = freetext.Replace("-", string.Empty);

                    CoordinateDataEntry entry = new CoordinateDataEntry(nextPointID++, coordinate, dcsCoord.Alt.Value, false, freetext, true);

                    // Add the AH64 data
                    entry.AircraftSpecificData.Add(typeof(AH64), new AH64SpecificData(unit));
                    Coordinates.Add(entry);
                }
            }
            Close();
        }

        private void Filter()
        {
            dgv_Units.Rows.Clear();

            if (allDCSUnits == null)
            {
                return;
            }

            List<DCSUnit> units = allDCSUnits.Select(x => new DCSUnit(x)).ToList();
            // Filter units by user selection
            // Range
            int refPointId = ComboItem<int>.GetSelectedValue(cb_RadiusCenter);
            CoordinateDataEntry refPoint = null;
            if (refPointId == -1)
            {
                DCSMessage message = new DCSMessage() { FetchCameraPosition = true };
                message = DCSConnection.sendRequest(message);

                refPoint = new CoordinateDataEntry(-1, message.CameraPosition.getCoordinate());
            }
            else
            {
                refPoint = referencePoints[refPointId];
            }
            Bullseye be = new Bullseye(refPoint.Coordinate);

            if (cb_WithRadiusFilter.Checked)
            {
                ERangeUnit rangeUnit = ComboItem<ERangeUnit>.GetSelectedValue(cb_RadiusUnit);
                double rawRangeValue = (double)nud_RadiusValue.Value;
                double rangeInM = 0;
                switch (rangeUnit)
                {
                    case ERangeUnit.Feet:
                        rangeInM = rawRangeValue / 0.3048;
                        break;
                    case ERangeUnit.NauticalMile:
                        rangeInM = rawRangeValue * 1852;
                        break;
                    case ERangeUnit.Meter:
                        rangeInM = rawRangeValue;
                        break;
                    case ERangeUnit.KiloMeter:
                        rangeInM = rawRangeValue * 1000;
                        break;
                }
                units = units.Where(x =>
                {
                    CoordinateSharp.Coordinate coord = new CoordinateSharp.Coordinate(x.Coordinate.Lat, x.Coordinate.Lon);
                    BRA bra = be.GetBRA(coord);
                    return bra.Range < (rangeInM / 1852); // bra range is in nmi
                }).ToList();
            }

            // Coalition
            ECoalition selectedCoalition = ComboItem<ECoalition>.GetSelectedValue(cb_CoalitionFilter);
            switch (selectedCoalition)
            {
                case ECoalition.Any:
                    break;
                case ECoalition.Red:
                    units = units.Where(x => x.Coalition == DCSUnit.ECoalition.Red).ToList();
                    break;
                case ECoalition.Blue:
                    units = units.Where(x => x.Coalition == DCSUnit.ECoalition.Blue).ToList();
                    break;
                case ECoalition.Neutral:
                    units = units.Where(x => x.Coalition == DCSUnit.ECoalition.Neutral).ToList();
                    break;
            }

            // Check type
            EUnitCategory unitCategory = ComboItem<EUnitCategory>.GetSelectedValue(cb_TypeFilter);
            switch (unitCategory)
            {
                case EUnitCategory.Any:
                    break;
                case EUnitCategory.Ground:
                    units = units.Where(x => x.Type.Level1 == DCSUnitTypeInformation.ELevel1Type.Ground || x.Type.Level1 == DCSUnitTypeInformation.ELevel1Type.Static).ToList();
                    break;
                case EUnitCategory.Naval:
                    units = units.Where(x => x.Type.Level1 == DCSUnitTypeInformation.ELevel1Type.Navy).ToList();
                    break;
                case EUnitCategory.Air:
                    units = units.Where(x => x.Type.Level1 == DCSUnitTypeInformation.ELevel1Type.Air).ToList();
                    break;
            }

            // Add to the data grid
            foreach (DCSUnit unit in units)
            {
                CoordinateSharp.Coordinate coordinate = new CoordinateSharp.Coordinate(unit.Coordinate.Lat, unit.Coordinate.Lon);
                CoordinateSharp.CoordinateFormatOptions options = new CoordinateSharp.CoordinateFormatOptions()
                {
                    Display_Symbols = true,
                    Display_Leading_Zeros = true,
                    Display_Trailing_Zeros = true,
                    Display_Hyphens = false,
                    Round = 2,
                    Format = CoordinateSharp.CoordinateFormatType.Degree_Minutes_Seconds
                };

                string positionStr = coordinate.ToString(options);
                if (cb_WithRadiusFilter.Checked)
                {

                    positionStr += " | From Ref: [" + be.GetBRA(coordinate).ToString() + "]";
                }

                int rowIdx = dgv_Units.Rows.Add(
                    (unit.ObjectId).ToString(),
                    unit.Coalition.ToString(),
                    unit.TypeName,
                    (unit.UnitName ?? string.Empty) + " / " + (unit.GroupName ?? string.Empty),
                    positionStr,
                    true
                );

                dgv_Units.Rows[rowIdx].Cells[0].ToolTipText = JsonConvert.SerializeObject(unit, Formatting.Indented);
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            UpdateAllUnitsFromDCS();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Coordinates = null;
            Close();
        }

        private enum ECoalition
        {
            Any,
            Red,
            Blue,
            Neutral
        }

        private enum EUnitCategory
        {
            Any,
            Ground,
            Naval,
            Air
        }

        private enum ERangeUnit
        {
            Feet,
            NauticalMile,
            Meter,
            KiloMeter
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormUnitImporter"/> class.
        /// </summary>
        public FormUnitImporter(List<CoordinateDataEntry> referencePoints)
        {
            InitializeComponent();

            // Setup coalition filter
            cb_CoalitionFilter.DisplayMember = "Text";
            cb_CoalitionFilter.ValueMember = "Value";
            foreach (ECoalition coalition in Enum.GetValues(typeof(ECoalition)))
            {
                cb_CoalitionFilter.Items.Add(new ComboItem<ECoalition>() { Text = coalition.ToString(), Value = coalition });
            }
            cb_CoalitionFilter.SelectedIndex = (int)ECoalition.Red;

            // Setup type filter
            cb_TypeFilter.DisplayMember = "Text";
            cb_TypeFilter.ValueMember = "Value";
            foreach (EUnitCategory category in Enum.GetValues(typeof(EUnitCategory)))
            {
                cb_TypeFilter.Items.Add(new ComboItem<EUnitCategory>() { Text = category.ToString(), Value = category });
            }
            cb_TypeFilter.SelectedIndex = (int)EUnitCategory.Ground;

            // Setup unit distance filter
            cb_RadiusUnit.DisplayMember = "Text";
            cb_RadiusUnit.ValueMember = "Value";
            cb_RadiusUnit.Items.Add(new ComboItem<ERangeUnit>() { Text = "nmi", Value = ERangeUnit.NauticalMile });
            cb_RadiusUnit.Items.Add(new ComboItem<ERangeUnit>() { Text = "ft", Value = ERangeUnit.Feet });
            cb_RadiusUnit.Items.Add(new ComboItem<ERangeUnit>() { Text = "km", Value = ERangeUnit.KiloMeter });
            cb_RadiusUnit.Items.Add(new ComboItem<ERangeUnit>() { Text = "m", Value = ERangeUnit.Meter });
            cb_RadiusUnit.SelectedIndex = 0;

            cb_WithRadiusFilter_CheckedChanged(cb_WithRadiusFilter, null);

            // Setup reference points
            cb_RadiusCenter.DisplayMember = "Text";
            cb_RadiusCenter.ValueMember = "Value";
            this.referencePoints = new Dictionary<int, CoordinateDataEntry>();
            cb_RadiusCenter.Items.Add(new ComboItem<int>() { Text = "Camera position", Value = -1 });
            foreach (CoordinateDataEntry refPoint in referencePoints)
            {
                cb_RadiusCenter.Items.Add(new ComboItem<int>() { Text = refPoint.ToString(), Value = refPoint.Id });
                this.referencePoints.Add(refPoint.Id, refPoint);
            }
            cb_RadiusCenter.SelectedIndex = 0;

            nextPointID = referencePoints.Count;

            UpdateAllUnitsFromDCS();
        }

        private void cb_WithRadiusFilter_CheckedChanged(object sender, EventArgs e)
        {
            nud_RadiusValue.Enabled = cb_WithRadiusFilter.Checked;
            cb_RadiusUnit.Enabled = cb_WithRadiusFilter.Checked;
            cb_RadiusCenter.Enabled = cb_WithRadiusFilter.Checked;
        }

        private void btn_ApplyFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }
    }
}
