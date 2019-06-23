using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CoordinateConverter
{
    /*
     * TODOs
     * Checking Input BULLS
     * Input Bullseye coordinates
     * Calculating Coordinate from bulls + offset
     * Calculating bullsoffset from coordinate
     * Insert units °, ', " when defocussing L/L and L/L Decimal Textboxes
     */
    public partial class MainForm : Form
    {
        #region "RegexConstants"
        #endregion
        /// <summary>
        /// Regex for Latitude. Allows valid numbers 0-90, 0-59, 0-59 and coordinate units, optional decimal part for seconds
        /// </summary>
        private const string REGEX_LL_LAT = @"^(?:[0-8]\d|90)\s*°?\s*[0-5]\d\s*'?\s*[0-5]\d(?:\.\d+)?\s*(?:\""|'')?$";
        /// <summary>
        /// Regex for Longitude. Allows valid numbers 0-180, 0-59, 0-59 and coordinate units, optional decimal part for seconds
        /// </summary>
        private const string REGEX_LL_LON = @"^(?:0\d\d|1[0-7]\d|180)\s*°?\s*[0-5]\d\s*'?\s*[0-5]\d(?:\.\d+)?\s*(?:\""|'')?$";
        /// <summary>
        /// Regex for Latitude. Allows valid numbers 0-90, 0-59, 0-59 and coordinate units, optional decimal part for seconds
        /// </summary>
        private const string REGEX_LL_DECIMAL_LAT = @"^(?:[0-8]\d|90)\s*°?\s*[0-5]\d\s*(?:\.\d+)?'?$";
        /// <summary>
        /// Regex for Longitude. Allows valid numbers 0-180, 0-59, 0-59 and coordinate units, optional decimal part for seconds
        /// </summary>
        private const string REGEX_LL_DECIMAL_LON = @"^(?:0\d\d|1[0-7]\d|180)\s*°?\s*[0-5]\d\s*(?:\.\d+)?'?$";

        private readonly System.Drawing.Color ERROR_COLOR = System.Drawing.Color.Pink;

        private CoordinateSharp.Coordinate input = new CoordinateSharp.Coordinate(0.0, 0.0);
        private Bullseye bulls = null;

        #region "Input"

        #region "LL"

        private bool CheckAndMarkLL()
        {
            bool ok = true;
            // TB Lat
            if (Regex.IsMatch(TB_LL_Lat.Text, REGEX_LL_LAT))
            {
                TB_LL_Lat.BackColor = default;
            }
            else
            {
                ok = false;
                TB_LL_Lat.BackColor = ERROR_COLOR;
            }

            // TB Lon
            if (Regex.IsMatch(TB_LL_Lon.Text, REGEX_LL_LON))
            {
                TB_LL_Lon.BackColor = default;
            }
            else
            {
                ok = false;
                TB_LL_Lon.BackColor = ERROR_COLOR;                
            }

            return ok;
        }

        private void CalcLL()
        {
            try
            {
                LbL_Error.Visible = false;

                if (CheckAndMarkLL())
                {
                    double lat = 0.0;
                    double lon = 0.0;
                    // get Lat
                    {
                        string strLat = TB_LL_Lat.Text; // Lat = N/S
                        strLat = strLat.Replace("°", string.Empty).Replace("'", string.Empty).Replace("\"", string.Empty).Replace(" ", string.Empty).Trim();
                        double deg = int.Parse(strLat.Substring(0, 2), System.Globalization.CultureInfo.InvariantCulture);
                        double min = int.Parse(strLat.Substring(2, 2), System.Globalization.CultureInfo.InvariantCulture);
                        double sec = double.Parse(strLat.Substring(4), System.Globalization.CultureInfo.InvariantCulture);

                        lat = (RB_LL_N.Checked ? 1 : -1) * (deg + min / 60 + sec / 3600);
                    }
                    // get Lon
                    {
                        string strLon = TB_LL_Lon.Text; // Lon = E/W
                        strLon = strLon.Replace("°", string.Empty).Replace("'", string.Empty).Replace("\"", string.Empty).Replace(" ", string.Empty).Trim();
                        double deg = int.Parse(strLon.Substring(0, 3), System.Globalization.CultureInfo.InvariantCulture);
                        double min = int.Parse(strLon.Substring(3, 2), System.Globalization.CultureInfo.InvariantCulture);
                        double sec = double.Parse(strLon.Substring(5), System.Globalization.CultureInfo.InvariantCulture);

                        lon = (RB_LL_E.Checked ? 1 : -1) * (deg + min / 60 + sec / 3600);
                    }

                    input = new CoordinateSharp.Coordinate(lat, lon);
                    DisplayCoordinates();
                }
            }
            catch (Exception e)
            {
                LbL_Error.Visible = true;
                LbL_Error.Text = e.Message;
            }
        }

        private void TB_LL_Lat_TextChanged(object sender, EventArgs e)
        {
            CalcLL();
        }

        private void TB_LL_Lon_TextChanged(object sender, EventArgs e)
        {
            CalcLL();
        }
        
        private void RB_LL_CheckedChanged(object sender, EventArgs e)
        {
            CalcLL();
        }

        private void TB_LL_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] validSingleCharacters = { '°', '.', '"', '\'' };
            // allow numbers or any of the validSingleCharacters as long as they aren't in the text already
            if (
                (e.KeyChar >= '0' && e.KeyChar <= '9') ||
                (e.KeyChar == ' ') ||
                (validSingleCharacters.Contains(e.KeyChar) && !((TextBox)sender).Text.Contains(e.KeyChar)) ||
                (e.KeyChar < 32)
            )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        #endregion // LL

        #region "LLDecimal"
        private bool CheckAndMarkLLDecimal()
        {
            bool ok = true;
            // TB Lat
            if (Regex.IsMatch(TB_LLDec_Lat.Text, REGEX_LL_DECIMAL_LAT))
            {
                TB_LLDec_Lat.BackColor = default;
            }
            else
            {
                ok = false;
                TB_LLDec_Lat.BackColor = ERROR_COLOR;
            }
            // TB Lon
            if (Regex.IsMatch(TB_LLDec_Lon.Text, REGEX_LL_DECIMAL_LON))
            {
                TB_LLDec_Lon.BackColor = default;
            }
            else
            {
                ok = false;
                TB_LLDec_Lon.BackColor = ERROR_COLOR;
            }

            return ok;
        }

        private void CalcLLDecimal()
        {
            try
            {
                LbL_Error.Visible = false;

                if (CheckAndMarkLLDecimal())
                {
                    double lat = 0.0;
                    double lon = 0.0;

                    {
                        string strLat = TB_LLDec_Lat.Text; // Lat = N/S
                        strLat = strLat.Replace("°", string.Empty).Replace("'", string.Empty).Replace("\"", string.Empty).Replace(" ", string.Empty).Trim();
                        double deg = int.Parse(strLat.Substring(0, 2), System.Globalization.CultureInfo.InvariantCulture);
                        double min = double.Parse(strLat.Substring(2), System.Globalization.CultureInfo.InvariantCulture);

                        lat = (RB_LLDec_N.Checked ? 1 : -1) * (deg + min / 60);
                    }

                    {
                        string strLon = TB_LLDec_Lon.Text; // Lon = E/W
                        strLon = strLon.Replace("°", string.Empty).Replace("'", string.Empty).Replace("\"", string.Empty).Replace(" ", string.Empty).Trim();
                        double deg = int.Parse(strLon.Substring(0, 3), System.Globalization.CultureInfo.InvariantCulture);
                        double min = double.Parse(strLon.Substring(3), System.Globalization.CultureInfo.InvariantCulture);

                        lon = (RB_LLDec_E.Checked ? 1 : -1) * (deg + min / 60);
                    }

                    input = new CoordinateSharp.Coordinate(lat, lon);
                    DisplayCoordinates();
                }
            }
            catch (Exception e)
            {
                LbL_Error.Visible = true;
                LbL_Error.Text = e.Message;
            }
        }

        private void TB_LLDecimal_Lat_TextChanged(object sender, EventArgs e)
        {
            CalcLLDecimal();
        }

        private void TB_LLDecimal_Lon_TextChanged(object sender, EventArgs e)
        {
            CalcLLDecimal();
        }

        private void RB_LLDecimal_CheckedChanged(object sender, EventArgs e)
        {
            CalcLLDecimal();
        }

        private void TB_LL_Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] validSingleCharacters = { '°', '.', '\'' };
            // allow numbers or any of the validSingleCharacters as long as they aren't in the text already
            e.Handled = !(
                (e.KeyChar >= '0' && e.KeyChar <= '9') ||
                (e.KeyChar == ' ') ||
                (validSingleCharacters.Contains(e.KeyChar) && !(((TextBox)sender).Text.Contains(e.KeyChar))) ||
                (e.KeyChar < 32)
            );
        }
        #endregion // LLDecimal

        #region "UTM/MGRS GRID"
        private void TB_UTM_MGRS_EastGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar < 32));
        }

        private void TB_UTM_MGRS_NorthGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpperInvariant(e.KeyChar);
            e.Handled = !((e.KeyChar >= 'A' && e.KeyChar <= 'Z' && e.KeyChar != 'O' && e.KeyChar != 'I') || (e.KeyChar < 32));
        }
        #endregion

        #region "MGRS"
        private bool CheckAndMarkMGRS()
        {
            bool ok = true;
            // Check Latitude GridCoordinate
            string latz = TB_MGRS_NorthGrid.Text.ToUpperInvariant();
            if (!(latz.Length != 1 || (latz[0] == 'I' || latz[0] == 'O'))) // Must be single character which is not I or O
            {
                TB_MGRS_NorthGrid.BackColor = default;
            }
            else
            {
                ok = false;
                TB_MGRS_NorthGrid.BackColor = ERROR_COLOR; 
            }

            // Check Longitude GridCoordinate
            int lonz = 0;
            if (int.TryParse(TB_MGRS_EastGrid.Text, out lonz))
            {
                if (lonz >= 1 && lonz <= 60)
                {
                    TB_MGRS_EastGrid.BackColor = default;
                }
                else
                {
                    ok = false;
                    TB_MGRS_EastGrid.BackColor = ERROR_COLOR;
                }
            }
            else
            {
                ok = false;
                TB_MGRS_EastGrid.BackColor = ERROR_COLOR;
            }

            // Check Digraph
            string digraph = TB_MGRS_SubgridIdent.Text;
            if (digraph.Length == 2 && !(digraph.Select(x => { return char.ToUpperInvariant(x) >= 'A' && char.ToUpperInvariant(x) <= 'Z'; }).Contains(false)))
            {
                TB_MGRS_SubgridIdent.BackColor = default;
            }
            else
            {
                ok = false;
                TB_MGRS_SubgridIdent.BackColor = ERROR_COLOR;
            }
            // Check Fraction
            string strFraction = TB_MGRS_Fraction.Text.Replace(" ", "");
            if (strFraction.Length % 2 != 0)
            {
                ok = false;
                TB_MGRS_Fraction.BackColor = ERROR_COLOR;
            }
            else
            {
                if (strFraction.Length == 0)
                {
                    TB_MGRS_Fraction.BackColor = default;
                }
                else
                {
                    string strEasting = strFraction.Substring(0, strFraction.Length / 2).PadRight(5, '0');
                    string strNorthing = strFraction.Substring(strFraction.Length / 2).PadRight(5, '0');
                    // Check Easting
                    double check = 0.0;
                    if (double.TryParse(strEasting, out check))
                    {
                        TB_MGRS_Fraction.BackColor = default;
                    }
                    else
                    {
                        ok = false;
                        TB_MGRS_Fraction.BackColor = ERROR_COLOR;
                    }
                    // Check Northing
                    if (double.TryParse(strNorthing, out check))
                    {
                        TB_MGRS_Fraction.BackColor = default;
                    }
                    else
                    {
                        ok = false;
                        TB_MGRS_Fraction.BackColor = ERROR_COLOR;
                    }
                }
            }

            return ok;
        }

        private void CalcMGRS()
        {
            try
            {
                LbL_Error.Visible = false;

                if (CheckAndMarkMGRS())
                {
                    string latz = TB_MGRS_NorthGrid.Text.ToUpperInvariant();
                    int lonz = int.Parse(TB_MGRS_EastGrid.Text);
                    string digraph = TB_MGRS_SubgridIdent.Text.ToUpperInvariant();
                    string fractionText = TB_MGRS_Fraction.Text.Replace(" ", "");
                    double easting = double.Parse(fractionText.Substring(0, fractionText.Length / 2).PadRight(5, '0'));
                    double northing = double.Parse(fractionText.Substring(fractionText.Length / 2).PadRight(5, '0'));

                    CoordinateSharp.MilitaryGridReferenceSystem mgrs = new CoordinateSharp.MilitaryGridReferenceSystem(latz: latz, longz: lonz, d: digraph, e: easting, n: northing);
                    input = CoordinateSharp.MilitaryGridReferenceSystem.MGRStoLatLong(mgrs);
                    DisplayCoordinates();
                }
            }
            catch (Exception e)
            {
                LbL_Error.Visible = true;
                LbL_Error.Text = e.Message;
            }
        }
        
        private void InputMGRSChanged(object sender, EventArgs e)
        {
            CalcMGRS();
        }

        private void TB_MGRS_Fraction_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar < 32));
        }

        private void TB_MGRS_SubgridIdent_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpperInvariant(e.KeyChar);
            e.Handled = !((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar < 32));
        }

        private void TB_MGRS_Fraction_Enter(object sender, EventArgs e)
        {
            // remove space in the middle
            TB_MGRS_Fraction.Text = TB_MGRS_Fraction.Text.Replace(" ", "");
            TB_MGRS_Fraction.MaxLength = 10;
        }

        private void TB_MGRS_Fraction_Leave(object sender, EventArgs e)
        {
            // add space in the middle
            TB_MGRS_Fraction.MaxLength = 11;
            if (TB_MGRS_Fraction.Text.Length > 0 && TB_MGRS_Fraction.Text.Length % 2 == 0)
            {
                TB_MGRS_Fraction.Text = TB_MGRS_Fraction.Text.Insert(TB_MGRS_Fraction.Text.Length / 2, " ");
            }
            TB_MGRS_Fraction.Text = TB_MGRS_Fraction.Text;
        }

        #endregion // MGRS

        #region "UTM"
        private bool CheckAndMarkUTM()
        {
            bool ok = true;

            // Check Latitude GridCoordinate
            string latz = TB_UTM_NorthGrid.Text.ToUpperInvariant();
            if (!(latz.Length != 1 || (latz[0] == 'I' || latz[0] == 'O'))) // Must be single character which is not I or O
            {
                TB_UTM_NorthGrid.BackColor = default;
            }
            else
            {
                ok = false;
                TB_UTM_NorthGrid.BackColor = ERROR_COLOR;
            }

            // Check Longitude GridCoordinate
            int lonz = 0;
            if (int.TryParse(TB_UTM_EastGrid.Text, out lonz))
            {
                if (lonz >= 1 && lonz <= 60)
                {
                    TB_UTM_EastGrid.BackColor = default;
                }
                else
                {
                    ok = false;
                    TB_UTM_EastGrid.BackColor = ERROR_COLOR;
                }
            }
            else
            {
                ok = false;
                TB_UTM_EastGrid.BackColor = ERROR_COLOR;
            }
            // Check Easting
            double chk = 0.0;
            if (double.TryParse(TB_UTM_Easting.Text, out chk))
            {
                TB_UTM_Easting.BackColor = default;
            }
            else
            {
                ok = false;
                TB_UTM_Easting.BackColor = ERROR_COLOR;
            }
            // Check Northing
            if (double.TryParse(TB_UTM_Northing.Text, out chk))
            {
                TB_UTM_Northing.BackColor = default;
            }
            else
            {
                ok = false;
                TB_UTM_Northing.BackColor = ERROR_COLOR;
            }

            return ok;
        }

        private void CalcUTM()
        {
            try
            {
                LbL_Error.Visible = false;

                if (CheckAndMarkUTM())
                {
                    string latz = TB_UTM_NorthGrid.Text.ToUpperInvariant();
                    int lonz = int.Parse(TB_UTM_EastGrid.Text);
                    double easting = double.Parse(TB_UTM_Easting.Text);
                    double northing = double.Parse(TB_UTM_Northing.Text);

                    var utm = new CoordinateSharp.UniversalTransverseMercator(latz: latz, longz: lonz, est: easting, nrt: northing);
                    input = CoordinateSharp.UniversalTransverseMercator.ConvertUTMtoLatLong(utm);
                    DisplayCoordinates();
                }
            }
            catch (Exception e)
            {
                LbL_Error.Visible = true;
                LbL_Error.Text = e.Message;
            }
        }

        private void RB_UTM_Northing_Easting_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool tbHasDecimalPoint = ((TextBox)sender).Text.Contains(".");
            e.Handled = !(
                (e.KeyChar >= '0' && e.KeyChar <= '9') ||
                (e.KeyChar == '.' && !tbHasDecimalPoint) ||
                (e.KeyChar < 32)
            );
        }

        private void InputUTMChanged(object sender, EventArgs e)
        {
            CalcUTM();
        }

        #endregion

        #region "BULLS"
        private bool CheckAndMarkBulls()
        {
            if (bulls == null)
            {
                throw new Exception("Bullseye not set.");
            }
            else
            {
                bool ok = true;
                double chk = 0.0;
                // check bearing
                if (double.TryParse(TB_Bulls_Bearing.Text, out chk))
                {
                    TB_Bulls_Bearing.BackColor = default;
                }
                else
                {
                    ok = false;
                    TB_Bulls_Bearing.BackColor = ERROR_COLOR;
                }
                // check range
                if (double.TryParse(TB_Bulls_Range.Text, out chk))
                {
                    TB_Bulls_Range.BackColor = default;
                }
                else
                {
                    ok = false;
                    TB_Bulls_Range.BackColor = ERROR_COLOR;
                }

                return ok;
            }
        }

        private void CalcBulls()
        {
            try
            {
                LbL_Error.Visible = false;

                if (CheckAndMarkBulls())
                {
                    double bearing = double.Parse(TB_Bulls_Bearing.Text);
                    double range = double.Parse(TB_Bulls_Range.Text);

                    input = bulls.GetOffsetPosition(new BRA(bearing: bearing, range: range));
                    DisplayCoordinates();
                }
            }
            catch (Exception e)
            {
                LbL_Error.Visible = true;
                LbL_Error.Text = e.Message;
            }
        }

        private void TB_Bulls_Bearing_TextChanged(object sender, EventArgs e)
        {
            CalcBulls();
        }

        private void TB_Bulls_Bearing_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allow any number
            e.Handled = !(
                (e.KeyChar >= '0' && e.KeyChar <= '9') ||
                (e.KeyChar < 32)
            );
        }

        private void TB_Bulls_Range_TextChanged(object sender, EventArgs e)
        {
            CalcBulls();
        }

        private void TB_Bulls_Range_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allow any number
            e.Handled = !(
                (e.KeyChar >= '0' && e.KeyChar <= '9') ||
                (e.KeyChar < 32)
            );
        }

        #endregion

        #region "SETBULLS"
        private bool CheckAndMarkSetBulls()
        {
            bool ok = true;

            // TB Lat
            if (Regex.IsMatch(TB_BullsLat.Text, REGEX_LL_LAT))
            {
                TB_BullsLat.BackColor = default;
            }
            else
            {
                ok = false;
                TB_BullsLat.BackColor = ERROR_COLOR;
            }

            // TB Lon
            if (Regex.IsMatch(TB_BullsLon.Text, REGEX_LL_LON))
            {
                TB_BullsLon.BackColor = default;
            }
            else
            {
                ok = false;
                TB_BullsLon.BackColor = ERROR_COLOR;
            }

            return ok;
        }

        // KeyPress events reused from L/L input
        private void SetBulls()
        {
            try
            {
                LbL_Error.Visible = false;

                if (CheckAndMarkSetBulls())
                {
                    double lat = 0.0;
                    double lon = 0.0;
                    // get Lat
                    {
                        string strLat = TB_BullsLat.Text; // Lat = N/S
                        strLat = strLat.Replace("°", string.Empty).Replace("'", string.Empty).Replace("\"", string.Empty).Replace(" ", string.Empty).Trim();
                        double deg = int.Parse(strLat.Substring(0, 2), System.Globalization.CultureInfo.InvariantCulture);
                        double min = int.Parse(strLat.Substring(2, 2), System.Globalization.CultureInfo.InvariantCulture);
                        double sec = double.Parse(strLat.Substring(4), System.Globalization.CultureInfo.InvariantCulture);

                        lat = (RB_BullsN.Checked ? 1 : -1) * (deg + min / 60 + sec / 3600);
                    }
                    // get Lon
                    {
                        string strLon = TB_BullsLon.Text; // Lon = E/W
                        strLon = strLon.Replace("°", string.Empty).Replace("'", string.Empty).Replace("\"", string.Empty).Replace(" ", string.Empty).Trim();
                        double deg = int.Parse(strLon.Substring(0, 3), System.Globalization.CultureInfo.InvariantCulture);
                        double min = int.Parse(strLon.Substring(3, 2), System.Globalization.CultureInfo.InvariantCulture);
                        double sec = double.Parse(strLon.Substring(5), System.Globalization.CultureInfo.InvariantCulture);

                        lon = (RB_BullsE.Checked ? 1 : -1) * (deg + min / 60 + sec / 3600);
                    }

                    bulls = new Bullseye(new CoordinateSharp.Coordinate(lat, lon));
                    DisplayCoordinates();
                }
            }
            catch (Exception e)
            {
                LbL_Error.Visible = true;
                LbL_Error.Text = e.Message;
            }
        }

        private void TB_BullsLat_TextChanged(object sender, EventArgs e)
        {
            SetBulls();
        }

        private void TB_BullsLon_TextChanged(object sender, EventArgs e)
        {
            SetBulls();
        }

        private void RB_Bulls_CheckedChanged(object sender, EventArgs e)
        {
            SetBulls();
        }

        #endregion

        #endregion // Input

        /// <summary>
        /// CTOR
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }
        
        private void DisplayCoordinates()
        {
            if (input != null)
            {
                // General FormatOptions
                CoordinateSharp.CoordinateFormatOptions formatOptions = new CoordinateSharp.CoordinateFormatOptions();
                formatOptions.Display_Degree_Symbol = true;
                formatOptions.Display_Minute_Symbol = true;
                formatOptions.Display_Seconds_Symbol = true;
                formatOptions.Display_Leading_Zeros = true;
                formatOptions.Display_Trailing_Zeros = true;

                // Out LL
                formatOptions.Format = CoordinateSharp.CoordinateFormatType.Degree_Minutes_Seconds;
                formatOptions.Round = 2;
                input.FormatOptions = formatOptions;
                TB_Out_LL.Text = input.Display.Replace("º", "°").Replace(",", ".");

                // Out LL Decimal
                formatOptions.Format = CoordinateSharp.CoordinateFormatType.Degree_Decimal_Minutes;
                formatOptions.Round = 4;
                input.FormatOptions = formatOptions;
                TB_Out_LLDec.Text = input.Display.Replace("º", "°").Replace(",", ".");

                // Out MGRS
                CoordinateSharp.MilitaryGridReferenceSystem mgrs = input.MGRS;
                TB_Out_MGRS.Text = mgrs.ToString();

                // Out UTM
                CoordinateSharp.UniversalTransverseMercator utm = input.UTM;
                TB_Out_UTM.Text = utm.ToString();

                // Out Bulls
                if (bulls == null)
                {
                    TB_Out_Bulls.Text = "Bullseye not set";
                }
                else
                {
                    TB_Out_Bulls.Text = bulls.GetBRA(input).ToString();
                }
            }
        }
    }
}
