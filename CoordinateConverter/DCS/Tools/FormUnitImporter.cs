using CoordinateConverter.DCS.Aircraft;
using CoordinateConverter.DCS.Communication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CoordinateConverter.DCS.Tools
{
    /// <summary>
    /// Represents a form that is used to import the coordinates of units from DCS
    /// </summary>
    /// <seealso cref="Form" />
    public partial class FormUnitImporter : Form
    {
        private int nextPointID;
        private List<DCSUnit> allDCSUnits = null;
        /// <summary>
        /// The coordinates that have been imported
        /// </summary>
        /// <value>
        /// The coordinates.
        /// </value>
        public List<CoordinateDataEntry> Coordinates { get; private set; } = null;
        private readonly Dictionary<int, CoordinateDataEntry> referencePoints = null;
        private const int IMPORT_CHECKBOX_COLUMN_ID = 8;
        private readonly DataTable currentView = new DataTable();

        private ERangeUnit GetSelectedRangeUnit()
        {
            return ComboItem<ERangeUnit>.GetSelectedValue(cb_RadiusUnit);
        }

        private double GetRangeConversionFactor(ERangeUnit from, ERangeUnit to)
        {
            double toMeters = 1.0;
            switch (from)
            {
                case ERangeUnit.Feet:
                    toMeters = 0.3048;
                    break;
                case ERangeUnit.NauticalMile:
                    toMeters = 1852;
                    break;
                case ERangeUnit.Meter:
                    toMeters = 1.0;
                    break;
                case ERangeUnit.KiloMeter:
                    toMeters = 1000;
                    break;
            }

            switch (to)
            {
                case ERangeUnit.Feet:
                    return toMeters * 3.28084;
                case ERangeUnit.NauticalMile:
                    return toMeters / 1852;
                case ERangeUnit.Meter:
                    return toMeters;
                case ERangeUnit.KiloMeter:
                    return toMeters * 1000;
                default:
                    throw new ArgumentException("Bad enum value");
            }
        }

        private void UpdateAllUnitsFromDCS()
        {
            DCSMessage message = new DCSMessage()
            {
                FetchUnits = true
            };
            message = DCSConnection.SendRequest(message);
            if (message != null && message.Units != null)
            {
                // ignore deactivated and pilots and crap
                allDCSUnits = message.Units.Where(x => x != null && x.Flags.IsActivated == true && x.Type.Level3 != DCSUnitTypeInformation.ELevel3Type.Parts).ToList();
            }
            else
            {
                allDCSUnits = null;
            }
            Filter();
        }

        private void Btn_Import_Click(object sender, EventArgs e)
        {
            Coordinates = new List<CoordinateDataEntry>();
            foreach (DataGridViewRow row in dgv_Units.Rows)
            {
                if ((bool)(row.Cells[IMPORT_CHECKBOX_COLUMN_ID] as DataGridViewCheckBoxCell).Value)
                {
                    int unitId = (int)row.Cells[0].Value;
                    DCSUnit unit = allDCSUnits.First(x => x.ObjectId == unitId);
                    DCSCoordinate dcsCoord = unit.Coordinate;
                    CoordinateSharp.Coordinate coordinate = new CoordinateSharp.Coordinate(dcsCoord.Lat, dcsCoord.Lon);
                    string freeText = unit.TypeName;
                    if (freeText.StartsWith("SA-"))
                    {
                        freeText = "S" + freeText.Substring(3);
                    }
                    freeText = freeText.Replace("-", string.Empty);

                    CoordinateDataEntry entry = new CoordinateDataEntry(nextPointID++, coordinate, dcsCoord.Alt.Value, false, freeText, true)
                    {
                        GroundElevationInM = dcsCoord.Elevation
                    };

                    // Add aircraft specific data data
                    entry.AircraftSpecificData.Add(typeof(AH64), new AH64SpecificData(unit));
                    entry.AircraftSpecificData.Add(typeof(KA50), new KA50SpecificData(KA50.EPointType.TargetPoint));
                    entry.AircraftSpecificData.Add(typeof(JF17), new JF17SpecificData(JF17.EPointType.Waypoint));
                    entry.AircraftSpecificData.Add(typeof(F18C), new F18CSpecificData());
                    Coordinates.Add(entry);
                }
            }
            Close();
        }

        private Bullseye GetRefPointBullseye()
        {
            int refPointId = ComboItem<int>.GetSelectedValue(cb_RadiusCenter);
            CoordinateDataEntry refPoint;
            if (refPointId == -1)
            {
                DCSMessage message = new DCSMessage() { FetchCameraPosition = true };
                message = DCSConnection.SendRequest(message);
                if (message == null)
                {
                    return null;
                }
                refPoint = new CoordinateDataEntry(-1, message.CameraPosition.GetCoordinate());
            }
            else
            {
                refPoint = referencePoints[refPointId];
            }
            return new Bullseye(refPoint.Coordinate);
        }

        private void Filter()
        {
            PopulateCurrentView();

            if (allDCSUnits == null)
            {
                return;
            }
            (dgv_Units.DataSource as DataView).RowFilter = "1 = 1";

            // Range
            // Filter units by range
            if (cb_WithRadiusFilter.Checked)
            {
                double rawRangeValue = (double)nud_RadiusValue.Value;
                (dgv_Units.DataSource as DataView).RowFilter += $" And {EDGVColumnHeaders.RNG} < {rawRangeValue.ToString(CultureInfo.InvariantCulture)}";
            }

            // Coalition
            ECoalition selectedCoalition = ComboItem<ECoalition>.GetSelectedValue(cb_CoalitionFilter);
            switch (selectedCoalition)
            {
                case ECoalition.Any:
                    break;
                case ECoalition.Red:
                case ECoalition.Blue:
                case ECoalition.Neutral:
                    (dgv_Units.DataSource as DataView).RowFilter += $" And {EDGVColumnHeaders.Coalition} = '{selectedCoalition}'";
                    break;
            }

            // Check type
            EUnitCategory unitCategory = ComboItem<EUnitCategory>.GetSelectedValue(cb_TypeFilter);
            switch (unitCategory)
            {
                case EUnitCategory.Any:
                    break;
                case EUnitCategory.Ground:
                    (dgv_Units.DataSource as DataView).RowFilter +=
                        " And" +
                        $" ({EDGVColumnHeaders.TypeL1} = {(int)DCSUnitTypeInformation.ELevel1Type.Ground}" +
                        $" Or {EDGVColumnHeaders.TypeL1} = {(int)DCSUnitTypeInformation.ELevel1Type.Static})";
                    break;
                case EUnitCategory.Naval:
                    (dgv_Units.DataSource as DataView).RowFilter += $" And {EDGVColumnHeaders.TypeL1} = {(int)DCSUnitTypeInformation.ELevel1Type.Navy}";
                    break;
                case EUnitCategory.Air:
                    (dgv_Units.DataSource as DataView).RowFilter += $" And {EDGVColumnHeaders.TypeL1} = {(int)DCSUnitTypeInformation.ELevel1Type.Air}";
                    break;
            }
        }

        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            UpdateAllUnitsFromDCS();
        }
        private void Btn_Cancel_Click(object sender, EventArgs e)
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

        private enum EDGVColumnHeaders
        {
            ID,
            Coalition,
            TypeName,
            TypeL1,
            TypeL2,
            TypeL3,
            TypeL4,
            Class,
            Name,
            Position,
            BRG,
            RNG,
            Import
        }

        private void InitCurrentView()
        {
            currentView.Clear();
            currentView.Columns.Clear();

            currentView.Columns.Add(EDGVColumnHeaders.ID.ToString(), typeof(int));
            currentView.Columns.Add(EDGVColumnHeaders.Coalition.ToString(), typeof(string));
            currentView.Columns.Add(EDGVColumnHeaders.TypeName.ToString(), typeof(string));
            currentView.Columns.Add(EDGVColumnHeaders.Class.ToString(), typeof(string));
            currentView.Columns.Add(EDGVColumnHeaders.TypeL1.ToString(), typeof(int));
            currentView.Columns.Add(EDGVColumnHeaders.TypeL2.ToString(), typeof(int));
            currentView.Columns.Add(EDGVColumnHeaders.TypeL3.ToString(), typeof(int));
            currentView.Columns.Add(EDGVColumnHeaders.TypeL4.ToString(), typeof(int));
            currentView.Columns.Add(EDGVColumnHeaders.Name.ToString(), typeof(string));
            currentView.Columns.Add(EDGVColumnHeaders.Position.ToString(), typeof(string));
            currentView.Columns.Add(EDGVColumnHeaders.BRG.ToString(), typeof(double));
            currentView.Columns.Add(EDGVColumnHeaders.RNG.ToString(), typeof(double));
            currentView.Columns.Add(EDGVColumnHeaders.Import.ToString(), typeof(bool));
        }

        private void PopulateCurrentView()
        {
            CoordinateSharp.CoordinateFormatOptions options = new CoordinateSharp.CoordinateFormatOptions()
            {
                Display_Symbols = true,
                Display_Leading_Zeros = true,
                Display_Trailing_Zeros = true,
                Display_Hyphens = false,
                Round = 2,
                Format = CoordinateSharp.CoordinateFormatType.Degree_Minutes_Seconds
            };

            currentView.Rows.Clear(); // Delete all data

            Bullseye be = GetRefPointBullseye();
            if (allDCSUnits == null)
            {
                return;
            }
            foreach (DCSUnit unit in allDCSUnits)
            {
                CoordinateSharp.Coordinate coordinate = new CoordinateSharp.Coordinate(unit.Coordinate.Lat, unit.Coordinate.Lon);
                double conversionFactor = GetRangeConversionFactor(ERangeUnit.NauticalMile, GetSelectedRangeUnit());

                currentView.Rows.Add(
                    unit.ObjectId,
                    unit.Coalition.ToString(),
                    unit.TypeName,
                    unit.Type.Level2 + " / " + unit.Type.Level3,
                    (int)unit.Type.Level1,
                    (int)unit.Type.Level2,
                    (int)unit.Type.Level3,
                    unit.Type.Level4,
                    unit.UnitName + " / " + unit.GroupName,
                    coordinate.ToString(options),
                    (be == null) ? (double?)null : be.GetBRA(coordinate).Bearing,
                    (be == null) ? (double?)null : be.GetBRA(coordinate).Range * conversionFactor,
                    true
                );
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormUnitImporter"/> class.
        /// </summary>
        /// <param name="referencePoints">The reference points.</param>
        public FormUnitImporter(List<CoordinateDataEntry> referencePoints)
        {
            InitializeComponent();

            // Don't auto generate columns
            // This prevents the generation of columns when assigning the data source
            // Instead we set each column's DataPropertyName individually
            dgv_Units.AutoGenerateColumns = false;

            InitCurrentView();

            // This binds the existing columns to the data properties in the data view
            dgv_Units.Columns["dgvColId"].DataPropertyName = EDGVColumnHeaders.ID.ToString();
            dgv_Units.Columns["dgvColCoalition"].DataPropertyName = EDGVColumnHeaders.Coalition.ToString();
            dgv_Units.Columns["dgvColTypeName"].DataPropertyName = EDGVColumnHeaders.TypeName.ToString();
            dgv_Units.Columns["dgvColClass"].DataPropertyName = EDGVColumnHeaders.Class.ToString();
            dgv_Units.Columns["dgvColUnitName"].DataPropertyName = EDGVColumnHeaders.Name.ToString();
            dgv_Units.Columns["dgvColPosition"].DataPropertyName = EDGVColumnHeaders.Position.ToString();
            dgv_Units.Columns["dgvColBearing"].DataPropertyName = EDGVColumnHeaders.BRG.ToString();
            dgv_Units.Columns["dgvColRange"].DataPropertyName = EDGVColumnHeaders.RNG.ToString();
            dgv_Units.Columns["dgvColImport"].DataPropertyName = EDGVColumnHeaders.Import.ToString();

            dgv_Units.DataSource = currentView.AsDataView();

            // Setup coalition filter
            cb_CoalitionFilter.DisplayMember = "Text";
            cb_CoalitionFilter.ValueMember = "Value";
            foreach (ECoalition coalition in Enum.GetValues(typeof(ECoalition)))
            {
                cb_CoalitionFilter.Items.Add(new ComboItem<ECoalition>(coalition.ToString(), coalition));
            }
            cb_CoalitionFilter.SelectedIndex = (int)ECoalition.Red;

            // Setup type filter
            cb_TypeFilter.DisplayMember = "Text";
            cb_TypeFilter.ValueMember = "Value";
            foreach (EUnitCategory category in Enum.GetValues(typeof(EUnitCategory)))
            {
                cb_TypeFilter.Items.Add(new ComboItem<EUnitCategory>(category.ToString(), category));
            }
            cb_TypeFilter.SelectedIndex = (int)EUnitCategory.Ground;

            // Setup unit distance filter
            cb_RadiusUnit.DisplayMember = "Text";
            cb_RadiusUnit.ValueMember = "Value";
            cb_RadiusUnit.Items.Add(new ComboItem<ERangeUnit>("nmi", ERangeUnit.NauticalMile));
            cb_RadiusUnit.Items.Add(new ComboItem<ERangeUnit>("ft", ERangeUnit.Feet));
            cb_RadiusUnit.Items.Add(new ComboItem<ERangeUnit>("km", ERangeUnit.KiloMeter));
            cb_RadiusUnit.Items.Add(new ComboItem<ERangeUnit>("m", ERangeUnit.Meter));
            cb_RadiusUnit.SelectedIndex = 0;

            Cb_WithRadiusFilter_CheckedChanged(cb_WithRadiusFilter, null);

            // Setup reference points
            cb_RadiusCenter.DisplayMember = "Text";
            cb_RadiusCenter.ValueMember = "Value";
            this.referencePoints = new Dictionary<int, CoordinateDataEntry>();
            cb_RadiusCenter.Items.Add(new ComboItem<int>("Camera position", -1));
            foreach (CoordinateDataEntry refPoint in referencePoints)
            {
                cb_RadiusCenter.Items.Add(new ComboItem<int>(refPoint.ToString(), refPoint.Id));
                this.referencePoints.Add(refPoint.Id, refPoint);
            }
            cb_RadiusCenter.SelectedIndex = 0;

            nextPointID = referencePoints.Count;

            UpdateAllUnitsFromDCS();
        }

        private void Cb_WithRadiusFilter_CheckedChanged(object sender, EventArgs e)
        {
            nud_RadiusValue.Enabled = cb_WithRadiusFilter.Checked;
            cb_RadiusUnit.Enabled = cb_WithRadiusFilter.Checked;
        }

        private void Btn_ApplyFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void Dgv_Units_CellContentClick(object objSender, DataGridViewCellEventArgs e)
        {
            DataGridView sender = objSender as DataGridView;
            switch (sender.Columns[e.ColumnIndex].Name)
            {
                case "dgvColImport":
                    // a checkbox was clicked
                    if (e.RowIndex >= 0)
                    {
                        DataGridViewCheckBoxCell cell = sender.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
                        cell.Value = !(cell.Value as bool? ?? false); // invert current selection
                        break;
                    }
                    // a checkbox header was clicked
                    else
                    {
                        bool allAreOn = true;
                        foreach (DataGridViewRow row in sender.Rows)
                        {
                            DataGridViewCheckBoxCell cell = (row.Cells[IMPORT_CHECKBOX_COLUMN_ID] as DataGridViewCheckBoxCell);
                            if (!(cell.Value as bool? ?? false))
                            {
                                allAreOn = false;
                                break;
                            }
                        }

                        foreach (DataGridViewRow row in sender.Rows)
                        {
                            (row.Cells[IMPORT_CHECKBOX_COLUMN_ID] as DataGridViewCheckBoxCell).Value = !allAreOn;
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void Dgv_Units_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (dgv_Units.Columns[e.ColumnIndex].Name)
            {
                case "dgvColClass":
                    if (dgv_Units.SortedColumn != null && dgv_Units.SortedColumn.Index != e.ColumnIndex)
                    {
                        // Set the old sorted column glyph to empty
                        dgv_Units.SortedColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                    }

                    System.ComponentModel.ListSortDirection direction;
                    if (dgv_Units.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                    {
                        (dgv_Units.DataSource as DataView).Sort =
                            $"{EDGVColumnHeaders.TypeL1} DESC," +
                            $"{EDGVColumnHeaders.TypeL2} DESC," +
                            $"{EDGVColumnHeaders.TypeL3} DESC," +
                            $"{EDGVColumnHeaders.TypeL4} DESC," +
                            $"{EDGVColumnHeaders.TypeName} DESC," +
                            $"{EDGVColumnHeaders.RNG} ASC";
                        dgv_Units.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    else
                    {
                        (dgv_Units.DataSource as DataView).Sort =
                            $"{EDGVColumnHeaders.TypeL1} ASC," +
                            $"{EDGVColumnHeaders.TypeL2} ASC," +
                            $"{EDGVColumnHeaders.TypeL3} ASC," +
                            $"{EDGVColumnHeaders.TypeL4} ASC," +
                            $"{EDGVColumnHeaders.TypeName} ASC," +
                            $"{EDGVColumnHeaders.RNG} ASC";
                        dgv_Units.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    break;
                default:
                    break;
            }
        }

        private void Dgv_Units_KeyPress(object objSender, KeyPressEventArgs e)
        {
            DataGridView sender = objSender as DataGridView;
            if (e.KeyChar == ' ')
            {
                List<int> selectedRowIds = new List<int>();
                foreach (DataGridViewCell cell in sender.SelectedCells)
                {
                    if (!selectedRowIds.Contains(cell.RowIndex))
                    {
                        selectedRowIds.Add(cell.RowIndex);
                    }
                }
                foreach (DataGridViewRow row in sender.SelectedRows)
                {
                    if (!selectedRowIds.Contains(row.Index))
                    {
                        selectedRowIds.Add(row.Index);
                    }
                }

                bool allAreOn = true;
                foreach (int rowIdx in selectedRowIds)
                {
                    DataGridViewRow row = sender.Rows[rowIdx];
                    DataGridViewCheckBoxCell cell = row.Cells[IMPORT_CHECKBOX_COLUMN_ID] as DataGridViewCheckBoxCell;
                    if (!(cell.Value as bool? ?? false))
                    {
                        allAreOn = false;
                        break;
                    }
                }

                foreach (int rowIdx in selectedRowIds)
                {
                    DataGridViewRow row = sender.Rows[rowIdx];
                    DataGridViewCheckBoxCell cell = row.Cells[IMPORT_CHECKBOX_COLUMN_ID] as DataGridViewCheckBoxCell;
                    cell.Value = !allAreOn;
                }
            }
        }

        private void FormUnitImporter_FormClosed(object sender, FormClosedEventArgs e)
        {
            dgv_Units.DataSource = null;
            dgv_Units.Rows.Clear();
            currentView.Dispose();
        }

        private void Dgv_Units_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (dgv_Units.Columns[e.ColumnIndex].Name)
            {
                case "dgvColBearing":
                    e.Value = Math.Round((double)e.Value, 1).ToString("N1", CultureInfo.InvariantCulture) + " °";
                    break;
                case "dgvColRange":
                    e.Value = Math.Round((double)e.Value, 1).ToString("N1", CultureInfo.InvariantCulture) + " " + ComboItem<ERangeUnit>.GetSelectedText(cb_RadiusUnit);
                    break;
                case "dgvColPosition":
                    e.Value = (e.Value as string).Replace("º", "°").Replace(",", ".");
                    break;
                default:
                    e.FormattingApplied = false;
                    break;
            }
        }
    }
}
