using CoordinateConverter.DCS.Aircraft;
using CoordinateConverter.DCS.Communication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace CoordinateConverter
{
    /// <summary>
    /// Main application
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
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
        private readonly System.Drawing.Color ERROR_COLOR = Color.Pink;
        private readonly System.Drawing.Color DCS_ERROR_COLOR = Color.Yellow;
        private readonly System.Drawing.Color DCS_OK_COLOR = Color.Green;

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

        ReticleForm reticleForm = new ReticleForm();

        private DCSAircraft selectedAircraft = null;

        /// <summary>
        /// The lock object for the progress bar, so that tmr250 doesn't set value before SendToDCS has set the maximum, causing exceptions during race conditions
        /// </summary>
        private object lockObjProgressBar = new object();

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
                    input = new CoordinateDataEntry(dataEntries.Count, coordinate, GetAltitudeInM(), cb_altitudeIsAGL.Checked, tb_Label.Text);
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
                    input = new CoordinateDataEntry(dataEntries.Count, coordinate, GetAltitudeInM(), cb_altitudeIsAGL.Checked, tb_Label.Text);
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
                    input = new CoordinateDataEntry(dataEntries.Count, coordinate, GetAltitudeInM(), cb_altitudeIsAGL.Checked, tb_Label.Text);
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
                    input = new CoordinateDataEntry(dataEntries.Count, coordinate, GetAltitudeInM(), cb_altitudeIsAGL.Checked, tb_Label.Text);
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
                    input = new CoordinateDataEntry(dataEntries.Count, coordinate, GetAltitudeInM(), cb_altitudeIsAGL.Checked, tb_Label.Text);
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

        /// <summary>
        /// Gets the altitude in m from the input text box.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException">Altitude unit not implemented.</exception>
        private double GetAltitudeInM()
        {
            lbl_Error.Visible = false;

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
                if (cb_AltitudeUnit.Text == "ft")
                {
                    tb_Altitude.Text = ((int)Math.Round(input.AltitudeInFt)).ToString();
                }
                else if (cb_AltitudeUnit.Text == "m")
                {
                    tb_Altitude.Text = ((int)Math.Round(input.AltitudeInM)).ToString();
                }
                else
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

                if (!updateInputfields)
                {
                    return;
                }

                // altitude
                tb_Altitude.TextChanged -= tb_Altitude_TextChanged;
                cb_altitudeIsAGL.CheckedChanged -= cb_altitutudeIsAGL_CheckedChanged;

                cb_altitudeIsAGL.Checked = input.AltitudeIsAGL;
                tb_Altitude.Text = Math.Round(cb_AltitudeUnit.Text == "ft" ? input.AltitudeInFt : input.AltitudeInM).ToString();

                tb_Label.Text = input.Name;

                tb_Altitude.TextChanged += tb_Altitude_TextChanged;
                cb_altitudeIsAGL.CheckedChanged += cb_altitutudeIsAGL_CheckedChanged;

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
                else if (selectedAircraft.GetType() == typeof(F18C))
                {
                    cb_pointType.SelectedIndex = 0;
                    F18CSpecificData extraData = input.AircraftSpecificData[selectedAircraft.GetType()] as F18CSpecificData;
                    if (extraData == null || extraData.WeaponType == null)
                    {
                        cb_pointType_SelectedIndexChanged(cb_pointType, null);
                        cb_pointOption.SelectedIndex = 0;
                        cb_PointOption_SelectedIndexChanged(cb_pointOption, null);
                    }
                    else if (extraData.WeaponType != null)
                    {
                        // not a waypoint, waypoint handled by default above (extraData == null)
                        if (!extraData.PreplanPointIdx.HasValue)
                        {
                            // SLAM-ER STP
                            cb_pointType.SelectedIndex = cb_pointType.FindStringExact(F18C.SLAMER_STP_STR);
                            cb_pointType_SelectedIndexChanged(cb_pointType, null);
                            cb_pointOption.SelectedIndex = cb_pointOption.FindStringExact(string.Format("PP {0} - {1}", extraData.PreplanPointIdx.Value, extraData.StationSetting.ToString()));
                            cb_PointOption_SelectedIndexChanged(cb_pointOption, null);
                        }
                        else
                        {
                            // PP
                            cb_pointType.SelectedIndex = cb_pointType.FindStringExact(F18C.GetPointTypePPStrForWeaponType(extraData.WeaponType.Value));
                            cb_pointType_SelectedIndexChanged(cb_pointType, null);
                            cb_pointOption.SelectedIndex = cb_pointOption.FindStringExact(string.Format("PP {0} - {1}", extraData.PreplanPointIdx.Value, extraData.StationSetting.ToString()));
                            cb_PointOption_SelectedIndexChanged(cb_pointOption, null);
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

                List<Control> controls = new List<Control>() { RB_LL_N, RB_LL_S, RB_LL_E, RB_LL_W };
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
                TB_MGRS_LatZone.Text = mgrsText.Substring(2, 1); // one space after this
                TB_MGRS_Digraph.Text = mgrsText.Substring(4, 2); // one space after this
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
                    Altitude = entry.getAltitudeString(cb_AltitudeUnit.Text == "ft"),
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
            dataEntries.Sort((a, b) => a.Id.CompareTo(b.Id));
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
                        string data = sr.ReadToEnd();
                        dataEntries = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CoordinateDataEntry>>(data, jsonSerializerSettings);
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

        private void cb_altitutudeIsAGL_CheckedChanged(object objSender, EventArgs e)
        {
            CheckBox sender = objSender as CheckBox;

            if (input == null)
            {
                return;
            }

            input.AltitudeIsAGL = sender.Checked;
        }

        private List<ToolStripMenuItem> AircraftSelectionMenuStripItems {
            get => new List<ToolStripMenuItem>()
            {
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
        }

        private void autoAircraftToolStripMenuItem_Click(object objSender, EventArgs e)
        {
            lbl_Error.Visible = false;

            ToolStripMenuItem sender = objSender as ToolStripMenuItem;

            sender.Checked = !sender.Checked;

            if (sender.Checked)
            {
                foreach (ToolStripMenuItem menuItem in AircraftSelectionMenuStripItems)
                {
                    menuItem.Enabled = false;
                    menuItem.Checked = false;
                }
                selectedAircraft = null;
            }
            else
            {
                // auto was deactivated
                foreach (ToolStripMenuItem menuItem in AircraftSelectionMenuStripItems)
                {
                    menuItem.Enabled = true;
                }
            }
        }

        private void aircraftSelectionToolStripMenuItem_Click(object objSender, EventArgs e)
        {
            lbl_Error.Visible = false;

            // Select the clicked option
            ToolStripMenuItem sender = objSender as ToolStripMenuItem;

            foreach (ToolStripMenuItem mi in AircraftSelectionMenuStripItems)
            {
                mi.Checked = mi.Name == sender.Name;
            }

            // Remind user here: "Transfer uses MGRS instead of L/L if mgrs selected, cockpit must match"
            if (sender.Name == aH64PLTToolStripMenuItem.Name)
            {
                selectedAircraft = new AH64(true);
            }
            else if (sender.Name == aH64CPGToolStripMenuItem.Name)
            {
                selectedAircraft = new AH64(false);
            }
            else if (sender.Name == f18ToolStripMenuItem.Name)
            {
                selectedAircraft = new F18C();
                MessageBox.Show("Make sure PRECISE mode is selected in HSI->Data.\n" +
                    "Make sure waypoint sequence is not selected before putting in waypoints.\n" +
                    "The next waypoint number and up from the currently selected one will be overwritten\n" +
                    "Make sure aircraft is in L/L Decimal mode (default). Check in HSI -> Data -> Aircraft -> Bottom right\n" +
                    "Make sure no weapon is selected prior to entering weapon data\n" +
                    "Maximum number SLAM-ER of steer points is 5.", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
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

            // Reset the combo boxes
            cb_pointType.Items.Clear();
            cb_pointType.Items.AddRange(selectedAircraft.GetPointTypes().Select(x => (object)x).ToArray());
            cb_pointType.Enabled = cb_pointType.Items.Count > 1;
            cb_pointType.SelectedIndex = 0; // this will populate cb_pointOption
            cb_pointOption.SelectedIndex = 0;
            cb_pointOption.Enabled = cb_pointOption.Items.Count > 1;

            // Either set cb_pointOption to the correct index if data exists
            // Or create a new data instance with default values
            if (selectedAircraft.GetType() == typeof(AH64))
            {
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
                    input.AircraftSpecificData.Add(selectedAircraft.GetType(), extraData);
                }
            }
            else if (selectedAircraft.GetType() == typeof(F18C))
            {
                // if the point has F18C data, we load it.
                if (input != null && input.AircraftSpecificData.ContainsKey(selectedAircraft.GetType()))
                {
                    F18CSpecificData extraData = input.AircraftSpecificData[selectedAircraft.GetType()] as F18CSpecificData;
                    if (extraData.WeaponType.HasValue) // if no value, is a standard waypoint
                    {
                        F18C.EWeaponType pwt = extraData.WeaponType.Value;
                        if (extraData.PreplanPointIdx.HasValue)
                        {
                            // GPS PP Target
                            cb_pointType.SelectedIndex = cb_pointType.FindStringExact(F18C.GetPointTypePPStrForWeaponType(pwt));
                            cb_pointOption.SelectedIndex = extraData.PreplanPointIdx.Value;
                        }
                        else
                        {
                            // SLAM-ER STP
                            cb_pointType.SelectedIndex = cb_pointType.Items.Count - 1;
                        }
                    }
                    // else, is a standard waypoint, but that's the default.
                }
                else if (input != null) // otherwise we add it.
                {
                    F18CSpecificData extraData = new F18CSpecificData();
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

            RefreshDataGrid();
        }

        private void cb_pointType_SelectedIndexChanged(object objSender, EventArgs e)
        {
            ComboBox sender = objSender as ComboBox;
            cb_pointOption.Items.Clear();

            if (selectedAircraft == null)
            {
                cb_pointOption.Enabled = false;
                return;
            }

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
            else if (selectedAircraft.GetType() == typeof(F18C))
            {
                string pointTypeStr = cb_pointType.Items[cb_pointType.SelectedIndex] as string;
                cb_pointOption.Items.AddRange((selectedAircraft as F18C).GetPointOptionsForType(pointTypeStr).Select(x => (object)x).ToArray());
                cb_pointOption.SelectedIndex = 0;
            }
            else
            {
                cb_pointOption.Items.Add("Waypoint");
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
            else if (selectedAircraft.GetType() == typeof(F18C))
            {
                if (!input.AircraftSpecificData.ContainsKey(typeof(F18C)))
                {
                    input.AircraftSpecificData.Add(selectedAircraft.GetType(), null);
                }
                
                string pointType = cb_pointType.Items[cb_pointType.SelectedIndex] as string;
                if (pointType == F18C.WAYPOINT_STR)
                {
                    input.AircraftSpecificData[selectedAircraft.GetType()] = new F18CSpecificData();
                }
                else if (pointType == F18C.SLAMER_STP_STR)
                {
                    // SLAM-ER Steerpoint
                    string pointOption = cb_pointOption.Items[cb_pointOption.SelectedIndex] as string;
                    F18CSpecificData.EStationSetting stationSetting = (F18CSpecificData.EStationSetting)Enum.Parse(typeof(F18CSpecificData.EStationSetting), pointOption.Substring("Auto Increment - ".Length));
                    input.AircraftSpecificData[selectedAircraft.GetType()] = new F18CSpecificData(true, stationSetting);
                }
                else
                {
                    // PrePlanned Target
                    F18C.EWeaponType pwt = (F18C.EWeaponType)Enum.Parse(typeof(F18C.EWeaponType), pointType.Split(' ').First());
                    string pointOption = cb_pointOption.Items[cb_pointOption.SelectedIndex] as string;
                    int ppIdx = int.Parse(pointOption.Substring("PP ".Length, 1));
                    F18CSpecificData.EStationSetting stationSetting = (F18CSpecificData.EStationSetting)Enum.Parse(typeof(F18CSpecificData.EStationSetting), pointOption.Substring("PP # - ".Length));
                    input.AircraftSpecificData[selectedAircraft.GetType()] = new F18CSpecificData(pwt, ppIdx, stationSetting);
                }
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
            try
            {
                lock (lockObjProgressBar)
                {
                    int totalCommands = selectedAircraft.SendToDCS(dataEntries);
                    pb_Transfer.Maximum = totalCommands;
                }
            }
            catch (InvalidOperationException ex)
            {
                lbl_DCS_Status.BackColor = DCS_ERROR_COLOR;
                lbl_DCS_Status.Text = ex.Message;
            }
        }

        private void fetchF10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            input = dcsCoordinate;
            RefreshCoordinates(true);
        }

        private CoordinateDataEntry dcsCoordinate = null;
        private bool wasConnected = false;
        private DateTime lastDCSErrorTime = DateTime.MinValue;
        private void tmr250ms_Tick(object sender, EventArgs e)
        {
            tmr250ms.Stop(); // only run one timer at a time
            try
            {
                DCSMessage message = new DCSMessage()
                {
                    FetchCameraPosition = true,
                    FetchAircraftType = autoToolStripMenuItem.Checked,
                    FetchWeaponStations = selectedAircraft != null && selectedAircraft.GetType() == typeof(F18C)
                };
                message = DCSConnection.sendRequest(message);

                if (message == null)
                {
                    lbl_DCS_Status.Text = "Not connected";
                    lbl_DCS_Status.BackColor = DCS_ERROR_COLOR;
                    wasConnected = false;
                    if (eReticleSetting == EReticleSetting.WhenF10)
                    {
                        reticleForm.Hide();
                    }
                    return;
                }

                if (!wasConnected)
                {
                    // update AGL values
                    wasConnected = true;
                    RefreshDataGrid();
                }

                if (message.ServerErrors != null && message.ServerErrors.Count > 0)
                {
                    lbl_DCS_Status.Text = message.ServerErrors.First();
                    lbl_DCS_Status.BackColor = DCS_ERROR_COLOR;
                    return;
                }

                if (autoToolStripMenuItem.Checked)
                {
                    AutoSelectAircraft(message.AircraftType);
                }

                if (message.CameraPosition == null)
                {
                    lbl_DCS_Status.Text = "Connected, but no coordinates";
                    lbl_DCS_Status.BackColor = DCS_ERROR_COLOR;
                    return;
                }

                if (message.CurrentCommandIndex.HasValue)
                {
                    lock (lockObjProgressBar)
                    {
                        pb_Transfer.Value = message.CurrentCommandIndex.Value;
                        pb_Transfer.Visible = true;
                    }
                }
                else
                {
                    pb_Transfer.Visible = false;
                }

                if (eReticleSetting == EReticleSetting.WhenF10)
                {
                    if (message.IsF10View ?? false)
                    {
                        reticleForm.Show();
                    }
                    else
                    {
                        reticleForm.Hide();
                    }
                }

                if (selectedAircraft != null && selectedAircraft.GetType() == typeof(F18C) && message.WeaponStations != null)
                {
                    (selectedAircraft as F18C).UpdateWeaponStations(message.WeaponStations);
                }

                // Update display
                if ((DateTime.Now - lastDCSErrorTime) < TimeSpan.FromSeconds(10))
                {
                    return;
                }

                dcsCoordinate = new CoordinateDataEntry()
                {
                    Lat = message.CameraPosition.Lat,
                    Longi = message.CameraPosition.Lon,
                    AltitudeIsAGL = cameraPosMode == ECameraPosMode.TerrainElevation,
                    AltitudeInM = cameraPosMode == ECameraPosMode.TerrainElevation ? 0 : message.CameraPosition.Alt ?? 0,
                    GroundElevationInM = message.CameraPosition.Elev,
                    XFer = true,
                    Name = String.Empty
                };

                string coordinateText = GetEntryCoordinateStr(dcsCoordinate) + " | " + dcsCoordinate.getAltitudeString(cb_AltitudeUnit.Text == "ft");
                

                lbl_DCS_Status.Text = coordinateText;
                lbl_DCS_Status.BackColor = DCS_OK_COLOR;
            }
            finally
            {
                // restart timer for next time
                tmr250ms.Start();
            }
        }

        private void lbl_DCS_Status_BackColorChanged(object objSender, EventArgs e)
        {
            ToolStripStatusLabel sender = objSender as ToolStripStatusLabel;
            if (sender.BackColor == DCS_ERROR_COLOR)
            {
                lastDCSErrorTime = DateTime.Now;
            }
            else if (sender.BackColor == DCS_OK_COLOR)
            {
                lastDCSErrorTime = DateTime.MinValue;
            }
            else
            {
                throw new ArgumentException("Color should be DCS_ERROR or DCS_OK");
            }
        }

        private void AutoSelectAircraft(string model)
        {
            foreach (ToolStripMenuItem mi in AircraftSelectionMenuStripItems)
            {
                mi.Enabled = false;
            }
            if (string.IsNullOrEmpty(model) || model == "null")
            {
                selectedAircraft = null;
            }
            else
            {
                // Switch aircraft. Ask user here which version of the cockit they are in. (AH64, F15E)
                switch (model)
                {
                    case "AH-64D_BLK_II":
                        aH64PLTToolStripMenuItem.Enabled = true;
                        aH64CPGToolStripMenuItem.Enabled = true;
                        if (selectedAircraft != null && selectedAircraft.GetType() == typeof(AH64))
                        {
                            break;
                        }
                        bool isPlt = DialogResult.Yes == MessageBox.Show("Are you pilot?", "PLT/CPG?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        aircraftSelectionToolStripMenuItem_Click(isPlt ? aH64PLTToolStripMenuItem : aH64CPGToolStripMenuItem, null);
                        break;
                    case "FA-18C_hornet":
                        f18ToolStripMenuItem.Enabled = true;
                        f18ToolStripMenuItem.Checked = true;
                        if (selectedAircraft != null && selectedAircraft.GetType() == typeof(F18C))
                        {
                            break;
                        }
                        aircraftSelectionToolStripMenuItem_Click(f18ToolStripMenuItem, null);
                        break;
                    default:
                        lbl_DCS_Status.Text = "Unknown aircraft: \"" + model + "\"";
                        lbl_DCS_Status.BackColor = DCS_ERROR_COLOR;
                        break;
                }
            }
        }

        #endregion

        #region "Settings"

        private enum ECameraPosMode
        {
            CameraAltitude,
            TerrainElevation
        }

        private ECameraPosMode cameraPosMode;
        private void terrainElevationUnderCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cameraPosMode = ECameraPosMode.TerrainElevation;
            terrainElevationUnderCameraToolStripMenuItem.Checked = true;
            cameraAltitudeToolStripMenuItem.Checked = false;
        }

        private void cameraAltitudeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            cameraPosMode = ECameraPosMode.CameraAltitude;
            terrainElevationUnderCameraToolStripMenuItem.Checked = false;
            cameraAltitudeToolStripMenuItem.Checked = true;
        }

        enum EReticleSetting
        {
            Never,
            Always,
            WhenF10
        }

        private EReticleSetting eReticleSetting = EReticleSetting.WhenF10;

        private void ScreenToolStripMenuItemClick(object objSender, EventArgs e)
        {
            // Gets the screen associated with the menu item and sets the reticle to the center of that screen
            ToolStripMenuItem sender = objSender as ToolStripMenuItem;
            int idx = int.Parse(sender.Name.Split('_').Last());
            Rectangle screen = Screen.AllScreens[idx].Bounds;
            Point screenCenter = new Point(screen.X + screen.Width / 2, screen.Y + screen.Height / 2);

            reticleForm.Location = new Point(screenCenter.X - reticleForm.Width / 2, screenCenter.Y - reticleForm.Height / 2);

            // Unsets all checkboxes except the one clicked
            foreach (ToolStripMenuItem mi in dCSMainScreenToolStripMenuItem.DropDownItems)
            {
                mi.Checked = mi.Name == sender.Name;
            }
        }
        private void whenInF10MapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eReticleSetting = EReticleSetting.WhenF10;
            SetReticleSettingsCheckmarks();
        }

        private void alwaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eReticleSetting = EReticleSetting.Always;
            SetReticleSettingsCheckmarks();
        }

        private void neverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eReticleSetting = EReticleSetting.Never;
            SetReticleSettingsCheckmarks();
        }

        private void SetReticleSettingsCheckmarks()
        {
            whenInF10MapToolStripMenuItem.Checked = eReticleSetting == EReticleSetting.WhenF10;
            alwaysToolStripMenuItem.Checked = eReticleSetting == EReticleSetting.Always;
            neverToolStripMenuItem.Checked = eReticleSetting == EReticleSetting.Never;

            if (eReticleSetting == EReticleSetting.Always)
            {
                reticleForm.Show();
            }
            else
            {
                reticleForm.Hide();
            }
        }
        #endregion

        /// <summary>
        /// CTOR
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            SetReticleSettingsCheckmarks();
            tmr250ms.Start();

            // Create screen selection menu
            int idx = 0;
            foreach(Screen screen in Screen.AllScreens)
            {
                Rectangle bounds = screen.Bounds;
                ToolStripMenuItem screenMenuItem = new ToolStripMenuItem()
                {
                    Text = idx.ToString() + ": " + screen.DeviceName + " [" + bounds.Width + "x" + bounds.Height + "]",
                    Checked = screen.Primary,
                    Name = string.Format("ScreenToolStripMenuItem_{0}", idx),
                };
                screenMenuItem.Click += ScreenToolStripMenuItemClick;
                dCSMainScreenToolStripMenuItem.DropDownItems.Add(screenMenuItem);
                if (screenMenuItem.Checked)
                {
                    ScreenToolStripMenuItemClick(screenMenuItem, null);
                }
                idx++;
            }

            cameraPosMode = terrainElevationUnderCameraToolStripMenuItem.Checked ? ECameraPosMode.TerrainElevation : ECameraPosMode.CameraAltitude;
        }
    }
}
