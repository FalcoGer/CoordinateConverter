using System.Data.SqlTypes;
using System.Windows.Forms;

namespace CoordinateConverter
{
    /// <summary>
    /// Item in a winforms ListBox
    /// </summary>
    public class ListBoxItem<TValue> where TValue : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListBoxItem{TValue}"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        public ListBoxItem(string text, TValue value)
        {
            this.Text = text;
            this.Value = value;
        }

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
        /// Gets the selected value of a list.
        /// </summary>
        /// <param name="ListBox">The ListBox.</param>
        /// <returns></returns>
        public static TValue GetSelectedValue(ListBox ListBox)
        {
            if (ListBox.SelectedIndex == -1)
            {
                return null;
            }
            return (ListBox.Items[ListBox.SelectedIndex] as ListBoxItem<TValue>).Value;
        }

        /// <summary>
        /// Gets the selected text of a ListBox.
        /// </summary>
        /// <param name="ListBox">The ListBox.</param>
        /// <returns>The text value of the selected item, null if no item is selected.</returns>
        public static string GetSelectedText(ListBox ListBox)
        {
            if (ListBox.SelectedIndex == -1)
            {
                return null;
            }
            return (ListBox.Items[ListBox.SelectedIndex] as ListBoxItem<TValue>).Text;
        }

        /// <summary>
        /// Finds the index of where the ListBox item has the specified value.
        /// </summary>
        /// <param name="ListBox">The ListBox.</param>
        /// <param name="value">The value to search for.</param>
        /// <returns>The index of the item in the ListBox, null if not found.</returns>
        public static int? FindValue(ListBox ListBox, TValue value)
        {
            int idx = 0;
            foreach (ListBoxItem<TValue> item in ListBox.Items)
            {
                if (item.Value.Equals(value))
                {
                    return idx;
                }
                idx++;
            }
            return null;
        }

        /// <summary>
        /// Finds the index of where the ListBox item has the specified text.
        /// </summary>
        /// <param name="ListBox">The ListBox.</param>
        /// <param name="text">The text to search for.</param>
        /// <returns>The index of the item in the ListBox, null if not found.</returns>
        public static int? FindText(ListBox ListBox, string text)
        {
            int idx = 0;
            foreach (ListBoxItem<TValue> item in ListBox.Items)
            {
                if (item.Value.Equals(text))
                {
                    return idx;
                }
                idx++;
            }
            return null;
        }
    }
}
