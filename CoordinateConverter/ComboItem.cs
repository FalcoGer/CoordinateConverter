using System.Windows.Forms;

namespace CoordinateConverter
{
    /// <summary>
    /// Item in a winforms ComboBox
    /// </summary>
    public class ComboItem<TValue>
    {
        /// <summary>
        /// Gets or sets the display text.
        /// </summary>
        /// <value>
        /// The display text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public TValue Value { get; set; }

        /// <summary>
        /// Gets the selected value of a combo box.
        /// </summary>
        /// <param name="comboBox">The combo box.</param>
        /// <returns></returns>
        public static TValue GetSelectedValue(ComboBox comboBox)
        {
            return (comboBox.Items[comboBox.SelectedIndex] as ComboItem<TValue>).Value;
        }

        /// <summary>
        /// Gets the selected text of a combo box.
        /// </summary>
        /// <param name="comboBox">The combo box.</param>
        /// <returns></returns>
        public static string GetSelectedText(ComboBox comboBox)
        {
            return (comboBox.Items[comboBox.SelectedIndex] as ComboItem<TValue>).Text;
        }
    }
}
