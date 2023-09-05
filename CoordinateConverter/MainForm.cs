using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CoordinateConverter
{
    /*
     * TODOs
     * Test apache data transfer
     * Automatically determine aircraft type from UDP socket from TheWay
     *      - deselect aircraft when no ping for a while
     *      - ask for seat for AH64 and F15E when seat is not already selected
     *      - ask user if they want to use MGRS or L/L for A10
     *      - remind user to set PRECISE in the hornet
     * Import point from DCS using the UDP socket from TheWay
     *      - create a form that is always on top and transparent with no decorations
     *      - make the form unmovable (except for from configuration)
     * Import point list to other aircraft (https://github.com/aronCiucu/DCSTheWay/tree/main/src/moduleCommands)
     *      - allow F18 weapons programming in addition to waypoints
     *      - ask users for weapon stations and types. programmable types:
     *          - jdam
     *          - jsow
     *          - harm (?)
     *          - SLAM
     *          - SLAM-ER
     *          - harpoon (with HPTP)
     * Allow users to do tedious setups, perhaps save a sequence of commands to be played back later
     *      - find out how to determine device and keycodes
     * Make pressing N/S/E/W switch the RB (on text change to allow pasting)
     * next waypoint bearing/range column
     */
    public partial class MainForm : Form
    {
        #region "RegexConstants"
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
        #endregion
        private readonly System.Drawing.Color ERROR_COLOR = System.Drawing.Color.Pink;
        private readonly System.Drawing.Color DCS_ERROR_COLOR = System.Drawing.Color.Yellow;

        private CoordinateDataEntry input = null;
        private Bullseye bulls = null;
        private List<CoordinateDataEntry> dataEntries = new List<CoordinateDataEntry>();
        private static readonly System.Globalization.CultureInfo CI = System.Globalization.CultureInfo.InvariantCulture;
        private static readonly Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
        {
            Culture = CI,
            Formatting = Newtonsoft.Json.Formatting.Indented,
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects
        };

        private string oldAltitudeUnit;

        private DCSAircraft selectedAircraft = null;

        #region "Input"

        #region "LL"        
        /// <summary>
        /// Checks the LL textboxes and marks them as error if format invalid.
        /// </summary>
        /// <returns>true if valid, otherwise false</returns>
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
        /// <summary>
        /// Calculates the Coordinates from the LL textboxes.
        /// </summary>
        private void CalcLL()
        {
            try
            {
                lbl_Error.Visible = false;

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

                        lat = (RB_LL_N.Checked ? 1 : -1) * (deg + (min / 60) + (sec / 3600));
                    }
                    // get Lon
                    {
                        string strLon = TB_LL_Lon.Text; // Lon = E/W
                        strLon = strLon.Replace("°", string.Empty).Replace("'", string.Empty).Replace("\"", string.Empty).Replace(" ", string.Empty).Trim();
                        double deg = int.Parse(strLon.Substring(0, 3), System.Globalization.CultureInfo.InvariantCulture);
                        double min = int.Parse(strLon.Substring(3, 2), System.Globalization.CultureInfo.InvariantCulture);
                        double sec = double.Parse(strLon.Substring(5), System.Globalization.CultureInfo.InvariantCulture);

                        lon = (RB_LL_E.Checked ? 1 : -1) * (deg + (min / 60) + (sec / 3600));
                    }

                    CoordinateSharp.Coordinate coordinate = new CoordinateSharp.Coordinate(lat, lon);
                    input = new CoordinateDataEntry(dataEntries.Count, coordinate, GetAltitudeInM(), tb_Label.Text);
                    RefreshCoordinates(false);
                }
                else
                {
                    input = null;
                }
            }
            catch (Exception e)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = e.Message;
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
            RefreshCoordinates(true);
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
        /// <summary>
        /// Checks the LL Decimal textboxes and marks them as error if format invalid.
        /// </summary>
        /// <returns>true if valid, otherwise false</returns>
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

        /// <summary>
        /// Calculates the coordinates from the ll decimal textboxes.
        /// </summary>
        private void CalcLLDecimal()
        {
            try
            {
                lbl_Error.Visible = false;

                if (CheckAndMarkLLDecimal())
                {
                    double lat = 0.0;
                    double lon = 0.0;

                    {
                        string strLat = TB_LLDec_Lat.Text; // Lat = N/S
                        strLat = strLat.Replace("°", string.Empty).Replace("'", string.Empty).Replace("\"", string.Empty).Replace(" ", string.Empty).Trim();
                        double deg = int.Parse(strLat.Substring(0, 2), System.Globalization.CultureInfo.InvariantCulture);
                        double min = double.Parse(strLat.Substring(2), System.Globalization.CultureInfo.InvariantCulture);

                        lat = (RB_LLDec_N.Checked ? 1 : -1) * (deg + (min / 60));
                    }

                    {
                        string strLon = TB_LLDec_Lon.Text; // Lon = E/W
                        strLon = strLon.Replace("°", string.Empty).Replace("'", string.Empty).Replace("\"", string.Empty).Replace(" ", string.Empty).Trim();
                        double deg = int.Parse(strLon.Substring(0, 3), System.Globalization.CultureInfo.InvariantCulture);
                        double min = double.Parse(strLon.Substring(3), System.Globalization.CultureInfo.InvariantCulture);

                        lon = (RB_LLDec_E.Checked ? 1 : -1) * (deg + (min / 60));
                    }

                    CoordinateSharp.Coordinate coordinate = new CoordinateSharp.Coordinate(lat, lon);
                    input = new CoordinateDataEntry(dataEntries.Count, coordinate, GetAltitudeInM(), tb_Label.Text);
                    RefreshCoordinates(false);
                }
                else
                {
                    input = null;
                }
            }
            catch (Exception e)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = e.Message;
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
            RefreshCoordinates(true);
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
        private void TB_UTM_MGRS_LongZone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only allow numbers and control characters (backspace + delete)
            e.Handled = !((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar < 0x20) || e.KeyChar == 0x7F);
        }

        private void TB_UTM_MGRS_LatZone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpperInvariant(e.KeyChar);
            e.Handled = !((e.KeyChar >= 'A' && e.KeyChar <= 'Z' && e.KeyChar != 'O' && e.KeyChar != 'I') || (e.KeyChar < 32));
        }
        #endregion

        #region "MGRS"        
        /// <summary>
        /// Checks the MGRS textboxes and marks them as error if format invalid.
        /// </summary>
        /// <returns>true if valid, otherwise false</returns>
        private bool CheckAndMarkMGRS()
        {
            bool ok = true;
            // Check Latitude GridCoordinate
            string latz = TB_MGRS_LatZone.Text.ToUpperInvariant();
            if (!(latz.Length != 1 || latz[0] == 'I' || latz[0] == 'O')) // Must be single character which is not I or O
            {
                TB_MGRS_LatZone.BackColor = default;
            }
            else
            {
                ok = false;
                TB_MGRS_LatZone.BackColor = ERROR_COLOR;
            }

            // Check Longitude GridCoordinate
            if (int.TryParse(TB_MGRS_LongZone.Text, out int lonz))
            {
                if (lonz >= 1 && lonz <= 60)
                {
                    TB_MGRS_LongZone.BackColor = default;
                }
                else
                {
                    ok = false;
                    TB_MGRS_LongZone.BackColor = ERROR_COLOR;
                }
            }
            else
            {
                ok = false;
                TB_MGRS_LongZone.BackColor = ERROR_COLOR;
            }

            // Check Digraph
            string digraph = TB_MGRS_Digraph.Text;
            if (digraph.Length == 2 &&
                digraph[0] >= 'A' && digraph[0] <= 'Z' && digraph[0] != 'I' && digraph[0] != 'O' &&
                digraph[1] >= 'A' && digraph[1] <= 'V' && digraph[1] != 'I' && digraph[1] != 'O')
            {
                TB_MGRS_Digraph.BackColor = default;
            }
            else
            {
                ok = false;
                TB_MGRS_Digraph.BackColor = ERROR_COLOR;
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
                    if (double.TryParse(strEasting, out double check))
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

        /// <summary>
        /// Calculates the coordnates from the MGRS textboxes.
        /// </summary>
        private void CalcMGRS()
        {
            try
            {
                lbl_Error.Visible = false;

                if (CheckAndMarkMGRS())
                {
                    string latz = TB_MGRS_LatZone.Text.ToUpperInvariant();
                    int lonz = int.Parse(TB_MGRS_LongZone.Text);
                    string digraph = TB_MGRS_Digraph.Text.ToUpperInvariant();
                    string fractionText = TB_MGRS_Fraction.Text.Replace(" ", "");
                    double easting = double.Parse(fractionText.Substring(0, fractionText.Length / 2).PadRight(5, '0'));
                    double northing = double.Parse(fractionText.Substring(fractionText.Length / 2).PadRight(5, '0'));

                    CoordinateSharp.MilitaryGridReferenceSystem mgrs = new CoordinateSharp.MilitaryGridReferenceSystem(latz: latz, longz: lonz, d: digraph, e: easting, n: northing);
                    CoordinateSharp.Coordinate coordinate = CoordinateSharp.MilitaryGridReferenceSystem.MGRStoLatLong(mgrs);
                    input = new CoordinateDataEntry(dataEntries.Count, coordinate, GetAltitudeInM(), tb_Label.Text);
                    RefreshCoordinates(false);
                }
                else
                {
                    input = null;
                }
            }
            catch (Exception e)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = e.Message;
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

        private void TB_MGRS_Digraph_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpperInvariant(e.KeyChar);
            e.Handled = !((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar < 0x20));
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
            RefreshCoordinates(true);

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
            string latz = TB_UTM_LatZone.Text.ToUpperInvariant();
            if (!(latz.Length != 1 || latz[0] == 'I' || latz[0] == 'O')) // Must be single character which is not I or O
            {
                TB_UTM_LatZone.BackColor = default;
            }
            else
            {
                ok = false;
                TB_UTM_LatZone.BackColor = ERROR_COLOR;
            }

            // Check Longitude GridCoordinate
            if (int.TryParse(TB_UTM_LongZone.Text, out int lonz))
            {
                if (lonz >= 1 && lonz <= 60)
                {
                    TB_UTM_LongZone.BackColor = default;
                }
                else
                {
                    ok = false;
                    TB_UTM_LongZone.BackColor = ERROR_COLOR;
                }
            }
            else
            {
                ok = false;
                TB_UTM_LongZone.BackColor = ERROR_COLOR;
            }
            // Check Easting
            if (double.TryParse(TB_UTM_Easting.Text, System.Globalization.NumberStyles.AllowDecimalPoint, CI, out _))
            {
                TB_UTM_Easting.BackColor = default;
            }
            else
            {
                ok = false;
                TB_UTM_Easting.BackColor = ERROR_COLOR;
            }

            // Check Northing
            if (double.TryParse(TB_UTM_Northing.Text, System.Globalization.NumberStyles.AllowDecimalPoint, CI, out _))
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
                lbl_Error.Visible = false;

                if (CheckAndMarkUTM())
                {
                    string latz = TB_UTM_LatZone.Text.ToUpperInvariant();
                    int lonz = int.Parse(TB_UTM_LongZone.Text, CI);
                    double easting = double.Parse(TB_UTM_Easting.Text, System.Globalization.NumberStyles.AllowDecimalPoint, CI);
                    double northing = double.Parse(TB_UTM_Northing.Text, System.Globalization.NumberStyles.AllowDecimalPoint, CI);

                    CoordinateSharp.UniversalTransverseMercator utm = new CoordinateSharp.UniversalTransverseMercator(latz: latz, longz: lonz, est: easting, nrt: northing);
                    CoordinateSharp.Coordinate coordinate = CoordinateSharp.UniversalTransverseMercator.ConvertUTMtoLatLong(utm);
                    input = new CoordinateDataEntry(dataEntries.Count, coordinate, GetAltitudeInM(), tb_Label.Text);
                    RefreshCoordinates(false);
                }
                else
                {
                    input = null;
                }
            }
            catch (Exception e)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = e.Message;
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
                // check bearing
                if (double.TryParse(TB_Bulls_Bearing.Text, System.Globalization.NumberStyles.AllowDecimalPoint, CI, out _))
                {
                    TB_Bulls_Bearing.BackColor = default;
                }
                else
                {
                    ok = false;
                    TB_Bulls_Bearing.BackColor = ERROR_COLOR;
                }
                // check range
                if (double.TryParse(TB_Bulls_Range.Text, System.Globalization.NumberStyles.AllowDecimalPoint, CI, out _))
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
                lbl_Error.Visible = false;

                if (CheckAndMarkBulls())
                {
                    double bearing = double.Parse(TB_Bulls_Bearing.Text, System.Globalization.NumberStyles.AllowDecimalPoint, CI);
                    double range = double.Parse(TB_Bulls_Range.Text, System.Globalization.NumberStyles.AllowDecimalPoint, CI);

                    CoordinateSharp.Coordinate coordinate = bulls.GetOffsetPosition(new BRA(bearing: bearing, range: range));
                    input = new CoordinateDataEntry(dataEntries.Count, coordinate, GetAltitudeInM(), tb_Label.Text);
                    RefreshCoordinates(false);
                }
                else
                {
                    input = null;
                }
            }
            catch (Exception e)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = e.Message;
            }
        }

        private void TB_Bulls_Bearing_TextChanged(object sender, EventArgs e)
        {
            CalcBulls();
        }

        private void TB_Bulls_Bearing_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool tbHasDecimalPoint = ((TextBox)sender).Text.Contains(".");
            // allow any number and a single decimal point
            e.Handled = !(
                (e.KeyChar >= '0' && e.KeyChar <= '9') ||
                (!tbHasDecimalPoint && e.KeyChar == '.') || 
                (e.KeyChar < 0x20) ||
                (e.KeyChar == 0x7F)
            );
        }

        private void TB_Bulls_Range_TextChanged(object sender, EventArgs e)
        {
            CalcBulls();
        }

        private void TB_Bulls_Range_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool tbHasDecimalPoint = ((TextBox)sender).Text.Contains(".");
            // allow any number and a single decimal point
            e.Handled = !(
                (e.KeyChar >= '0' && e.KeyChar <= '9') ||
                (!tbHasDecimalPoint && e.KeyChar == '.') ||
                (e.KeyChar < 0x20) ||
                (e.KeyChar == 0x7F)
            );
        }

        private void btn_SetBE_Click(object sender, EventArgs e)
        {
            lbl_Error.Visible = false;
            if (input == null)
            {
                lbl_Error.Text = "Input invalid";
                lbl_Error.Visible = true;
                return;
            }
            bulls = new Bullseye(input.Coordinate);

            CoordinateSharp.CoordinatePart lat = input.Coordinate.Latitude;
            CoordinateSharp.CoordinatePart lon = input.Coordinate.Longitude;

            // LL
            lbl_BENorthing.Text = lat.Position.ToString().PadRight(3, ' ') + lat.Degrees.ToString(CI).PadLeft(2, '0') + "°" + lat.Minutes.ToString(CI).PadLeft(2, '0') + "'" + Math.Round(lat.Seconds, 2).ToString(CI).PadLeft(2, '0') + "\"";
            lbl_BEEasting.Text = lon.Position.ToString().PadRight(2, ' ') + lon.Degrees.ToString(CI).PadLeft(3, '0') + "°" + lon.Minutes.ToString(CI).PadLeft(2, '0') + "'" + Math.Round(lon.Seconds, 2).ToString(CI).PadLeft(2, '0') + "\"";

            RefreshCoordinates(true);
            RefreshDataGrid();
        }

        #endregion

        #region MiscInput

        private void tb_Altitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only allow numbers and control characters (backspace + delete)
            e.Handled = !((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar < 0x20 || e.KeyChar == 0x7F);
        }

        private void tb_Label_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only allow numbers, letters, space and control characters (backspace + delete)
            e.Handled = !(
                (e.KeyChar >= '0' && e.KeyChar <= '9') ||
                (e.KeyChar >= 'a' && e.KeyChar <= 'z') ||
                (e.KeyChar >= 'A' && e.KeyChar <= 'Z') ||
                e.KeyChar <= 0x20 ||
                e.KeyChar == 0x7F
            );
        }
        private void tb_Altitude_TextChanged(object sender, EventArgs e)
        {
            int altitude = 0;
            if (tb_Altitude.Text == string.Empty || !int.TryParse(tb_Altitude.Text, out altitude))
            {
                tb_Altitude.Text = "0";
                altitude = 0;
            }

            if (input == null)
            {
                return;
            }
            
            if (cb_AltitudeUnit.Text == "ft")
            {
                input.AltitudeInFt = altitude;
            }
            else if (cb_AltitudeUnit.Text == "m")
            {
                input.AltitudeInM = altitude;
            }
            else
            {
                throw new NotImplementedException("Altitude unit not implemented");
            }
        }

        private double? GetAltitudeInM()
        {
            lbl_Error.Visible = false;

            if (cb_defaultAltitude.Checked)
            {
                return null;
            }

            int altitude;
            if (!int.TryParse(tb_Altitude.Text, out altitude))
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "Could not parse altitude as a whole number.";
                return 0.0;
            }

            if (cb_AltitudeUnit.Text == "m")
            {
                return altitude;
            }
            if (cb_AltitudeUnit.Text == "ft")
            {
                return altitude / CoordinateDataEntry.FT_PER_M;
            }
            throw new NotImplementedException("Altitude unit not implemented.");
        }

        private void cb_AltitudeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_Error.Visible = false;

            if (input != null)
            {
                if (cb_AltitudeUnit.Text == "ft" && input.AltitudeInFt.HasValue)
                {
                    tb_Altitude.Text = ((int)Math.Round(input.AltitudeInFt.Value)).ToString();
                }
                else if (cb_AltitudeUnit.Text == "m" && input.AltitudeInM.HasValue)
                {
                    tb_Altitude.Text = ((int)Math.Round(input.AltitudeInM.Value)).ToString();
                }
                else if (input.AltitudeInM.HasValue || input.AltitudeInFt.HasValue)
                {
                    throw new NotImplementedException("Altitude unit not implemented");
                }
            }
            else
            {
                int oldAltitudeValue;
                if (!int.TryParse(tb_Altitude.Text, out oldAltitudeValue))
                {
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "Could not parse altitude as a whole number.";
                    return;
                }

                if (cb_AltitudeUnit.Text == "ft" && oldAltitudeUnit == "m")
                {
                    tb_Altitude.Text = ((int)Math.Round(oldAltitudeValue * CoordinateDataEntry.FT_PER_M)).ToString();
                }
                else if (cb_AltitudeUnit.Text == "m")
                {
                    tb_Altitude.Text = ((int)Math.Round(oldAltitudeValue / CoordinateDataEntry.FT_PER_M)).ToString();
                }
                else
                {
                    throw new NotImplementedException("Altitude unit not implemented");
                }
            }

            oldAltitudeUnit = cb_AltitudeUnit.Text;
            RefreshDataGrid();
        }

        private void tb_Label_TextChanged(object sender, EventArgs e)
        {
            if (input != null)
            {
                input.Name = tb_Label.Text;
            }
        }

        private void TB_Input_Leave(object sender, EventArgs e)
        {
            RefreshCoordinates(true);
        }

        #endregion

        #endregion

        #region Output

        private void RefreshCoordinates(bool updateInputfields)
        {
            if (input != null)
            {
                TB_Out_LL.Text = input.getCoordinateStrLL();
                TB_Out_LLDec.Text = input.getCoordinateStrLLDec();
                TB_Out_MGRS.Text = input.getCoordinateStrMGRS((int)nud_MGRS_Precision.Value);
                TB_Out_UTM.Text = input.getCoordinateStrUTM();
                TB_Out_Bulls.Text = input.getCoordinateStrBullseye(bulls);

                if (updateInputfields)
                {
                    // altitude
                    tb_Altitude.TextChanged -= tb_Altitude_TextChanged;
                    cb_defaultAltitude.CheckedChanged -= cb_defaultAltitude_CheckedChanged;
                    if (input.AltitudeInM.HasValue)
                    {
                        tb_Altitude.Enabled = true;
                        tb_Altitude.Text = Math.Round(cb_AltitudeUnit.Text == "ft" ? input.AltitudeInFt.Value : input.AltitudeInM.Value).ToString();
                        cb_defaultAltitude.Checked = false;
                    }
                    else
                    {
                        tb_Altitude.Text = "0";
                        tb_Altitude.Enabled = false;
                        cb_defaultAltitude.Checked = true;
                    }
                    tb_Label.Text = input.Name;

                    tb_Altitude.TextChanged += tb_Altitude_TextChanged;
                    cb_defaultAltitude.CheckedChanged += cb_defaultAltitude_CheckedChanged;

                    // point type & point option
                    cb_pointType.SelectedIndexChanged -= cb_pointType_SelectedIndexChanged;
                    cb_pointOption.SelectedIndexChanged -= cb_PointOption_SelectedIndexChanged;

                    if (selectedAircraft == null || !input.AircraftSpecificData.ContainsKey(selectedAircraft.GetType()))
                    {
                        cb_pointType.SelectedIndex = 0;
                        cb_pointType_SelectedIndexChanged(cb_pointType, null);
                        if (cb_pointOption.Items.Count > 0)
                        {
                            cb_pointOption.SelectedIndex = 0;
                        }
                    }
                    else if (selectedAircraft.GetType() == typeof(AH64))
                    {
                        AH64SpecificData extraData = input.AircraftSpecificData[selectedAircraft.GetType()] as AH64SpecificData;
                        cb_pointType.SelectedIndex = 0;
                        for (int pointTypeIDX = 0; pointTypeIDX < cb_pointType.Items.Count; pointTypeIDX++)
                        {
                            if (cb_pointType.Items[pointTypeIDX].ToString() == extraData.PointType)
                            {
                                cb_pointType.SelectedIndex = pointTypeIDX;
                                break;
                            }
                        }
                        // select the correct point option
                        cb_pointType_SelectedIndexChanged(cb_pointType, null); // needed to repopulate the point option combo box
                        if (cb_pointOption.Items.Count > 0)
                        {
                            cb_pointOption.SelectedIndex = 0; // sane default, in case the value isn't found
                            // search for the value
                            for (int pointOptionIDX = 0; pointOptionIDX < cb_pointOption.Items.Count; pointOptionIDX++)
                            {
                                ComboItem ci = cb_pointOption.Items[pointOptionIDX] as ComboItem;
                                if (ci.Value == extraData.Ident)
                                {
                                    cb_pointOption.SelectedIndex = pointOptionIDX;
                                    break;
                                }
                            }
                        }
                    }

                    cb_pointType.SelectedIndexChanged += cb_pointType_SelectedIndexChanged;
                    cb_pointOption.SelectedIndexChanged += cb_PointOption_SelectedIndexChanged;
                    // coordinates

                    CoordinateSharp.CoordinatePart lat = input.Coordinate.Latitude;
                    CoordinateSharp.CoordinatePart lon = input.Coordinate.Longitude;
                    CoordinateSharp.UniversalTransverseMercator utm = input.Coordinate.UTM;

                    // LL
                    TB_LL_Lat.TextChanged -= TB_LL_Lat_TextChanged;
                    TB_LL_Lat.Text = lat.Degrees.ToString(CI).PadLeft(2, '0') + "°" + lat.Minutes.ToString(CI).PadLeft(2, '0') + "'" + Math.Round(lat.Seconds, 2).ToString(CI).PadLeft(2, '0') + "\"";
                    TB_LL_Lat.TextChanged += TB_LL_Lat_TextChanged;
                    
                    TB_LL_Lon.TextChanged -= TB_LL_Lon_TextChanged;
                    TB_LL_Lon.Text = lon.Degrees.ToString(CI).PadLeft(3, '0') + "°" + lon.Minutes.ToString(CI).PadLeft(2, '0') + "'" + Math.Round(lon.Seconds, 2).ToString(CI).PadLeft(2, '0') + "\"";
                    TB_LL_Lon.TextChanged += TB_LL_Lon_TextChanged;

                    List<Control> controls =  new List<Control>() { RB_LL_N, RB_LL_S, RB_LL_E, RB_LL_W };
                    foreach (RadioButton rb in controls)
                    {
                        rb.CheckedChanged -= RB_LL_CheckedChanged;
                    }
                    RB_LL_N.Checked = lat.Position == CoordinateSharp.CoordinatesPosition.N;
                    RB_LL_S.Checked = lat.Position == CoordinateSharp.CoordinatesPosition.S;
                    RB_LL_E.Checked = lon.Position == CoordinateSharp.CoordinatesPosition.E;
                    RB_LL_W.Checked = lon.Position == CoordinateSharp.CoordinatesPosition.W;
                    foreach (RadioButton rb in controls)
                    {
                        rb.CheckedChanged += RB_LL_CheckedChanged;
                    }

                    // LL Dec
                    TB_LLDec_Lat.TextChanged -= TB_LLDecimal_Lat_TextChanged;
                    TB_LLDec_Lat.Text = lat.Degrees.ToString(CI).PadLeft(2, '0') + "°" + Math.Round(lat.DecimalMinute, 4).ToString(CI).PadLeft(2, '0');
                    TB_LLDec_Lat.TextChanged += TB_LLDecimal_Lat_TextChanged;
                    
                    TB_LLDec_Lon.TextChanged -= TB_LLDecimal_Lon_TextChanged;
                    TB_LLDec_Lon.Text = lon.Degrees.ToString(CI).PadLeft(3, '0') + "°" + Math.Round(lon.DecimalMinute, 4).ToString(CI).PadLeft(2, '0');
                    TB_LLDec_Lon.TextChanged += TB_LLDecimal_Lon_TextChanged;

                    controls = new List<Control>() { RB_LLDec_N, RB_LLDec_S, RB_LLDec_E, RB_LLDec_W };
                    foreach (RadioButton rb in controls)
                    {
                        rb.CheckedChanged -= RB_LLDecimal_CheckedChanged;
                    }
                    RB_LLDec_N.Checked = lat.Position == CoordinateSharp.CoordinatesPosition.N;
                    RB_LLDec_S.Checked = lat.Position == CoordinateSharp.CoordinatesPosition.S;
                    RB_LLDec_E.Checked = lon.Position == CoordinateSharp.CoordinatesPosition.E;
                    RB_LLDec_W.Checked = lon.Position == CoordinateSharp.CoordinatesPosition.W;
                    foreach (RadioButton rb in controls)
                    {
                        rb.CheckedChanged += RB_LLDecimal_CheckedChanged;
                    }

                    // MGRS
                    controls = new List<Control>() { TB_MGRS_LongZone, TB_MGRS_LatZone, TB_MGRS_Digraph, TB_MGRS_Fraction };
                    foreach (TextBox tb in controls)
                    {
                        tb.TextChanged -= InputMGRSChanged;
                    }
                    string mgrsText = input.getCoordinateStrMGRS();
                    TB_MGRS_LongZone.Text = mgrsText.Substring(0, 2);
                    TB_MGRS_LatZone.Text = mgrsText.Substring(2,1); // one space after this
                    TB_MGRS_Digraph.Text = mgrsText.Substring(4,2); // one space after this
                    TB_MGRS_Fraction.Text = mgrsText.Substring(7).Remove(5, 1); // remove center space
                    foreach (TextBox tb in controls)
                    {
                        tb.TextChanged += InputMGRSChanged;
                    }

                    // UTM
                    controls = new List<Control>() { TB_UTM_LongZone, TB_UTM_LatZone, TB_UTM_Easting, TB_UTM_Northing };
                    foreach (TextBox tb in controls)
                    {
                        tb.TextChanged -= InputUTMChanged;
                    }
                    TB_UTM_LongZone.Text = utm.LongZone.ToString().PadLeft(2, '0');
                    TB_UTM_LatZone.Text = utm.LatZone;
                    TB_UTM_Easting.Text = Math.Round(utm.Easting, 3).ToString(CI);
                    TB_UTM_Northing.Text = Math.Round(utm.Northing, 3).ToString(CI);
                    foreach (TextBox tb in controls)
                    {
                        tb.TextChanged += InputUTMChanged;
                    }

                    if (bulls != null)
                    {
                        BRA bra = bulls.GetBRA(input.Coordinate);

                        TB_Bulls_Bearing.TextChanged -= TB_Bulls_Bearing_TextChanged;
                        TB_Bulls_Bearing.Text = Math.Round(bra.Bearing, 1).ToString(CI);
                        TB_Bulls_Bearing.TextChanged += TB_Bulls_Bearing_TextChanged;

                        TB_Bulls_Range.TextChanged -= TB_Bulls_Range_TextChanged;
                        TB_Bulls_Range.Text = Math.Round(bra.Range, 2).ToString(CI);
                        TB_Bulls_Range.TextChanged += TB_Bulls_Range_TextChanged;
                    }
                }
            }
        }

        private void nud_MGRS_Precision_ValueChanged(object sender, EventArgs e)
        {
            RefreshCoordinates(false);
            RefreshDataGrid();
        }

        #region GridView

        private void RefreshDataGrid()
        {
            var result = dataEntries.Select(
                entry => new {
                    ID = entry.Id,
                    Name = entry.Name +
                        (
                            (selectedAircraft == null || !entry.AircraftSpecificData.ContainsKey(selectedAircraft.GetType())) ? String.Empty
                            : " [" + entry.AircraftSpecificData[selectedAircraft.GetType()].ToString() + "]"
                        ),
                    CoordinateStr = GetEntryCoordinateStr(entry),
                    Altitude = entry.AltitudeInM.HasValue ? (Math.Round(cb_AltitudeUnit.Text == "m" ? entry.AltitudeInM.Value : entry.AltitudeInFt.Value)).ToString(CI) : "Default",
                    XFER = entry.XFer
                }
            ).OrderBy(x => x.ID).ToList();

            dgv_CoordinateList.DataSource = result;

            // Deselect all cells and all rows
            foreach (DataGridViewRow row in dgv_CoordinateList.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Selected = false;
                }
                row.Selected = false;
            }
        }

        private void dgv_CoordinateList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                input = dataEntries.ElementAt(e.RowIndex).Clone(dataEntries.Count);
                RefreshCoordinates(true);
            }
        }

        private void dgv_CoordinateList_CellContentClick(object objSender, DataGridViewCellEventArgs e)
        {
            const int DELETE_BUTTON_COLID = 0;
            const int XFER_CB_COLID = 5;

            DataGridView sender = objSender as DataGridView;

            if (sender.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                // A button cell was clicked
                if (e.ColumnIndex == DELETE_BUTTON_COLID) // delete
                {
                    dataEntries.RemoveAt(e.RowIndex);
                    ResetIDs();
                    RefreshDataGrid();
                    return;
                }
            }
            if (sender.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex < 0)
            {
                // a button colum was clicked
                if (e.ColumnIndex == DELETE_BUTTON_COLID)
                {
                    DialogResult answer = MessageBox.Show("Delete all points?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (answer != DialogResult.Yes)
                    {
                        return;
                    }
                    dataEntries.Clear();
                    RefreshDataGrid();
                    return;
                }
            }
            if (sender.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                // a checkbox was clicked
                if (e.ColumnIndex == XFER_CB_COLID)
                {
                    DataGridViewCheckBoxCell cell = sender.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
                    dataEntries[e.RowIndex].XFer = !(cell.Value as bool? ?? false); // invert current selection
                    RefreshDataGrid();
                    return;
                }
            }
            if (sender.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex < 0)
            {
                // a checkbox header was clicked
                if (e.ColumnIndex == XFER_CB_COLID)
                {
                    bool allAreOn = dataEntries.Find(elem => !(elem.XFer)) == null;

                    foreach (CoordinateDataEntry entry in dataEntries)
                    {
                        entry.XFer = !allAreOn;
                    }

                    RefreshDataGrid();
                    return;
                }
            }
            return;
        }

        private void ResetIDs()
        {
            int currentCount = dataEntries.Count;
            for (int idx = 0; idx < currentCount; idx++)
            {
                CoordinateDataEntry entry = dataEntries.ElementAt(idx).Clone(idx);
                dataEntries.Add(entry);
            }
            dataEntries.RemoveRange(0, currentCount);
        }

        private string GetEntryCoordinateStr(CoordinateDataEntry entry)
        {
            string coordStr = null;
            if (rb_Format_LL.Checked)
            {
                coordStr = entry.getCoordinateStrLL();
            }
            else if (rb_Format_LLDec.Checked)
            {
                coordStr = entry.getCoordinateStrLLDec();
            }
            else if (rb_Format_MGRS.Checked)
            {
                coordStr = entry.getCoordinateStrMGRS((int)nud_MGRS_Precision.Value);
            }
            else if (rb_Format_UTM.Checked)
            {
                coordStr = entry.getCoordinateStrUTM();
            }
            else if (rb_Format_BE.Checked)
            {
                coordStr = entry.getCoordinateStrBullseye(bulls);
            }
            if (string.IsNullOrEmpty(coordStr)) { throw new NotImplementedException("Couldn't format coordinate to string."); }

            return coordStr;
        }

        /// <summary>
        /// Handles the Click event of the btn_Add control.
        /// Adds the current output into the <see cref="dgv_CoordinateList"/>
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            lbl_Error.Visible = false;
            if (input == null)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "Input invalid";
                return;
            }
            dataEntries.Add(input.Clone(dataEntries.Count));
            RefreshDataGrid();
        }

        private void btn_Replace_Click(object sender, EventArgs e)
        {
            lbl_Error.Visible = false;

            int? rowIdx = GetSelectedRowIndex();
            if (!rowIdx.HasValue)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "Need to select exactly one row or one cell.";
                return;
            }

            dataEntries[rowIdx.Value] = input.Clone(rowIdx.Value);
            RefreshDataGrid();
            dgv_CoordinateList.Rows[rowIdx.Value].Selected = true;
        }

        private int? GetSelectedRowIndex()
        {
            int? idx = null;
            if (dgv_CoordinateList.SelectedCells.Count == 1)
            {
                idx = dgv_CoordinateList.SelectedCells[0].RowIndex;
            }
            else if (dgv_CoordinateList.SelectedRows.Count == 1)
            {
                idx = dgv_CoordinateList.SelectedRows[0].Index;
            }
            return idx;
        }

        private void btn_MoveUp_Click(object sender, EventArgs e)
        {
            lbl_Error.Visible = false;

            int? idx = GetSelectedRowIndex();

            if (idx == null)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "Need to select exactly one row or one cell.";
                return;
            }

            int tgt = idx.Value - 1;
            if (swapRows(idx.Value, tgt))
            {
                dgv_CoordinateList.Rows[tgt].Selected = true;
            }
        }

        private void btn_MoveDown_Click(object sender, EventArgs e)
        {
            lbl_Error.Visible = false;

            int? idx = GetSelectedRowIndex();

            if (idx is null)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "Need to select exactly one row or one cell.";
                return;
            }

            int tgt = idx.Value + 1;
            if (swapRows(idx.Value, tgt))
            {
                dgv_CoordinateList.Rows[tgt].Selected = true;
            }
        }

        /// <summary>
        /// Swaps the two rows in the data grid
        /// </summary>
        /// <param name="idx">The index of the first row.</param>
        /// <param name="targetIdx">Index of row where it supposed to go.</param>
        /// <returns>true if a swap occurred, false if the targetidx is invalid</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">idx is not a valid index in the datagrid</exception>
        private bool swapRows(int idx, int targetIdx)
        {
            lbl_Error.Visible = false;
            if (idx < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(idx) + " < 0");
            }
            if (idx >= dgv_CoordinateList.RowCount)
            {
                throw new ArgumentOutOfRangeException(nameof(idx) + " > Rowcount");
            }
            if (targetIdx < 0 || targetIdx >= dgv_CoordinateList.RowCount)
            {
                return false; // just ignore button presses when already at top/bottom
            }
            CoordinateDataEntry entry = dataEntries[idx];
            CoordinateDataEntry other = dataEntries[targetIdx];
            entry.SwapIds(other);
            dataEntries.Sort((a,b) => a.Id.CompareTo(b.Id));
            RefreshDataGrid();
            return true;
        }

        private void rb_Format_LL_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                RefreshDataGrid();
            }
        }

        private void rb_Format_LLDec_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                RefreshDataGrid();
            }
        }

        private void rb_Format_MGRS_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                RefreshDataGrid();
            }
        }

        private void rb_Format_UTM_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                RefreshDataGrid();
            }
        }

        private void rb_Format_BE_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                RefreshDataGrid();
            }
        }
        #endregion

        #endregion

        #region "File management"

        private OpenFileDialog ofd = new OpenFileDialog()
        {
            Title = "Open Coordinate List",
            AddExtension = true,
            DefaultExt = "json",
            Filter = "JSON files (*.json)|*.json|Text files (*.txt)|*.txt|All files (*.*)|*.*",
            FileName = "coordinates.json",
            Multiselect = false,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            ShowReadOnly = false
        };

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbl_Error.Visible = false;
            ofd.CheckFileExists = false;

            DialogResult result = ofd.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            string filePath = ofd.FileName;
            FileInfo fi = new FileInfo(filePath);
            if (fi.Exists)
            {
                result = MessageBox.Show("You are about to overwrite this file.", "Overwrite file?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result != DialogResult.OK)
                {
                    return;
                }
            }

            try
            {
                using (FileStream fileHandle = fi.Open(FileMode.Create, FileAccess.Write))
                {
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dataEntries, jsonSerializerSettings);
                    byte[] data = new System.Text.UTF8Encoding(true).GetBytes(jsonData);
                    fileHandle.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = ex.Message;
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbl_Error.Visible = false;

            ofd.CheckFileExists = true;
            DialogResult result = ofd.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            string filePath = ofd.FileName;
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Exists)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "File does not exist";
                return;
            }

            try
            {
                using (FileStream fileHandle = fi.Open(FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fileHandle, System.Text.Encoding.UTF8))
                    {
                        dataEntries = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CoordinateDataEntry>>(sr.ReadToEnd(), jsonSerializerSettings);
                        ResetIDs();
                        RefreshDataGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = ex.Message;
            }
        }


        #endregion

        #region "DCS"

        private void cb_defaultAltitude_CheckedChanged(object objSender, EventArgs e)
        {
            CheckBox sender = objSender as CheckBox;
            tb_Altitude.Enabled = !sender.Checked;

            if (input == null)
            {
                return;
            }

            if (sender.Checked)
            {
                input.AltitudeInM = null;
            }
            else
            {
                tb_Altitude_TextChanged(sender, e);
            }
        }

        private void aircraftSelectionToolStripMenuItem_Click(object objSender, EventArgs e)
        {
            lbl_Error.Visible = false;

            // Select the clicked option
            ToolStripMenuItem sender = objSender as ToolStripMenuItem;

            List<ToolStripMenuItem> controlsList = new List<ToolStripMenuItem>()
            {
                autoToolStripMenuItem,
                a10ToolStripMenuItem,
                aH64CPGToolStripMenuItem,
                aH64PLTToolStripMenuItem,
                aV8BToolStripMenuItem,
                f15EPLTToolStripMenuItem,
                f15EWSOToolStripMenuItem,
                f16ToolStripMenuItem,
                f18ToolStripMenuItem,
                kA50ToolStripMenuItem,
                m2000ToolStripMenuItem
            };

            foreach (ToolStripMenuItem mi in controlsList)
            {
                mi.Checked = mi.Name == sender.Name;
            }

            const string AH64PLT = "aH64PLTToolStripMenuItem";
            const string AH64CPG = "aH64CPGToolStripMenuItem";

            switch (sender.Name)
            {
                case AH64PLT:
                    selectedAircraft = new AH64(true);
                    break;
                case AH64CPG:
                    selectedAircraft = new AH64(false);
                    break;
                default:
                    selectedAircraft = null;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "Currently this aircraft is not implemented";

                    cb_pointType.Items.Clear();
                    cb_pointType.Items.Add("Waypoint");
                    cb_pointType.SelectedIndex = 0;
                    cb_pointType.Enabled = false;
                    cb_pointOption.ValueMember = null;
                    cb_pointOption.DisplayMember = null;
                    cb_pointOption.DataSource = null;
                    cb_pointOption.Items.Clear();
                    cb_pointOption.Enabled = false;
                    RefreshDataGrid();
                    return;
            }

            if (selectedAircraft.GetType() == typeof(AH64))
            {
                cb_pointType.Items.Clear();
                cb_pointType.Items.AddRange(Enum.GetNames(typeof(AH64.EPointType)).ToArray());
                cb_pointType.Enabled = true;
                cb_pointType.SelectedIndex = 0;
                cb_pointOption.Enabled = true;

                // if the point has AH64 data, we load it.
                if (input != null && input.AircraftSpecificData.ContainsKey(selectedAircraft.GetType()))
                {
                    AH64SpecificData extraData = input.AircraftSpecificData[selectedAircraft.GetType()] as AH64SpecificData;
                    cb_pointType.SelectedIndex = cb_pointType.FindStringExact(extraData.PointType);
                    cb_pointOption.SelectedValue = extraData.Ident;
                }
                else if (input != null) // otherwise we add it.
                {
                    AH64SpecificData extraData = new AH64SpecificData();
                    extraData.PointType = nameof(AH64.EPointType.Waypoint);
                    extraData.Ident = nameof(AH64.EPointIdent.WP_WP);
                    input.AircraftSpecificData.Add(selectedAircraft.GetType(), extraData);
                }
            }
            else
            {
                cb_pointType.Items.Clear();
                cb_pointType.Items.Add("Waypoint");
                cb_pointType.SelectedIndex = 0;
                cb_pointType.Enabled = false;

                cb_pointOption.Items.Clear();
                cb_pointOption.Enabled = false;
            }

            tb_Altitude.Enabled = !cb_defaultAltitude.Checked;
            RefreshDataGrid();
        }

        private void cb_pointType_SelectedIndexChanged(object objSender, EventArgs e)
        {
            if (selectedAircraft == null)
            {
                cb_pointOption.Enabled = false;
                return;
            }

            ComboBox sender = objSender as ComboBox;
            cb_pointOption.Items.Clear();

            if (selectedAircraft.GetType() == typeof(AH64))
            {
                // add all the options for the AH64
                AH64.EPointType ePointType = (AH64.EPointType)Enum.Parse(typeof(AH64.EPointType), cb_pointType.Text, true);

                object[] items;
                cb_pointOption.DisplayMember = "Text";
                cb_pointOption.ValueMember = "Value";
                switch (ePointType)
                {
                    case AH64.EPointType.Waypoint:
                        items = AH64.EWPOptionDescriptions.Select(x => (object)(new ComboItem() { Value = x.Key.ToString(), Text = x.Value })).ToArray();
                        cb_pointOption.Items.AddRange(items);
                        break;
                    case AH64.EPointType.Hazard:
                        items = AH64.EHZOptionDescriptions.Select(x => (object)(new ComboItem() { Value = x.Key.ToString(), Text = x.Value })).ToArray();
                        cb_pointOption.Items.AddRange(items);
                        break;
                    case AH64.EPointType.ControlMeasure:
                        items = AH64.ECMOptionDescriptions.Select(x => (object)(new ComboItem() { Value = x.Key.ToString(), Text = x.Value })).ToArray();
                        cb_pointOption.Items.AddRange(items);
                        break;
                    case AH64.EPointType.Target:
                        items = AH64.ETGOptionDescriptions.Select(x => (object)(new ComboItem() { Value = x.Key.ToString(), Text = x.Value })).ToArray();
                        cb_pointOption.Items.AddRange(items);
                        break;
                    default:
                        throw new Exception("Bad point type.");
                }


                if (input == null)
                {
                    if (cb_pointOption.Items.Count > 0)
                    {
                        cb_pointOption.SelectedIndex = 0;
                    }
                }
                else
                {
                    // Set input to the relevant value
                    if (!input.AircraftSpecificData.ContainsKey(typeof(AH64)))
                    {
                        input.AircraftSpecificData.Add(selectedAircraft.GetType(), new AH64SpecificData());
                    }
                    AH64SpecificData extraData = input.AircraftSpecificData[selectedAircraft.GetType()] as AH64SpecificData;
                    extraData.PointType = sender.Items[sender.SelectedIndex] as string;
                    cb_pointOption.SelectedIndex = 0;
                    input.AircraftSpecificData[selectedAircraft.GetType()] = extraData;
                }
            }
            cb_pointOption.Enabled = cb_pointOption.Items.Count > 1;
        }

        private void cb_PointOption_SelectedIndexChanged(object objSender, EventArgs e)
        {
            if (selectedAircraft == null)
            {
                return;
            }

            ComboBox sender = objSender as ComboBox;
            if (sender.SelectedIndex < 0)
            {
                // newly populated
                return;
            }

            if (input == null)
            {
                return;
            }

            if (selectedAircraft.GetType() == typeof(AH64))
            {
                if (!input.AircraftSpecificData.ContainsKey(typeof(AH64)))
                {
                    input.AircraftSpecificData.Add(selectedAircraft.GetType(), new AH64SpecificData());
                }
                AH64SpecificData extraData = input.AircraftSpecificData[selectedAircraft.GetType()] as AH64SpecificData;
                extraData.Ident = (sender.SelectedItem as ComboItem).Value;
                input.AircraftSpecificData[selectedAircraft.GetType()] = extraData;
            }
        }

        private void transferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbl_Error.Visible = false;
            if (selectedAircraft == null)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "Need to select aircraft type.";
                return;
            }

            selectedAircraft.SendToDCS(dataEntries);
        }

        private void fetchF10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbl_Error.Visible = true;
            lbl_Error.Text = "Importing coordinates is currently not supported";
        }

        #endregion

        /// <summary>
        /// CTOR
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            tb_Altitude.Enabled = !cb_defaultAltitude.Checked;
        }
    }
}
