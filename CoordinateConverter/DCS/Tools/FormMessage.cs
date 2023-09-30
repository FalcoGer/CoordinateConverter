using System;
using System.Drawing;
using System.Windows.Forms;

namespace CoordinateConverter.DCS.Tools
{
    /// <summary>
    /// A form to display a simple message to the user
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormMessage : Form
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FormAskBinaryQuestion"/> class.
        /// </summary>
        /// <param name="parent">The parent form</param>
        /// <param name="message">The question to be asked, will be the title of the form.</param>
        /// <param name="acceptButtonText">The yes button text.</param>
        /// <param name="messageBodyText">A more detailed question text to be displayed in the form itself. If <c>null</c>, then <paramref name="message"/> will be used.</param>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="message"/> or <paramref name="acceptButtonText"/> is null
        /// </exception>
        public FormMessage(Form parent, string message, string acceptButtonText = "OK", string messageBodyText = null)
        {
            // Check arguments
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message) + " must not be null or empty");
            }
            if (string.IsNullOrEmpty(acceptButtonText))
            {
                throw new ArgumentNullException(nameof(acceptButtonText) + " must not be null or empty");
            }

            // Build the form
            InitializeComponent();

            // Set texts
            Text = message;
            lbl_Message.Text = messageBodyText ?? message;
            btn_Accept.Text = acceptButtonText;

            // Get border sizes
            Rectangle screenRectangle = RectangleToScreen(ClientRectangle);
            int titleHeight = screenRectangle.Top - Top;
            int borderWidth = screenRectangle.Left - Left;

            // Spacing between controls and also between controls and window border
            const int MARGIN = 13;

            lbl_Message.Location = new Point(MARGIN, MARGIN);

            int width = Math.Max(btn_Accept.Width, lbl_Message.Width) + (MARGIN * 2) + (borderWidth * 2);
            int height = borderWidth + titleHeight + lbl_Message.Top + lbl_Message.Height + MARGIN + btn_Accept.Height + MARGIN;

            Size = new Size(width, height);

            // button (right)
            btn_Accept.Location = new Point(this.Width - (borderWidth * 2) - MARGIN - btn_Accept.Width, lbl_Message.Location.Y + lbl_Message.Height + MARGIN);

            ShowDialog(parent);
        }

        private void Btn_Accept_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
