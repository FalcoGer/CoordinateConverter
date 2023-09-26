using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoordinateConverter.DCS.Tools
{
    /// <summary>
    /// A form to ask the user a question with two choices
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormAskBinaryQuestion : Form
    {
        /// <summary>
        /// Gets a value indicating the result of the question.
        /// </summary>
        /// <value>
        ///   <c>true</c> if answered positive; otherwise, <c>false</c>.
        /// </value>
        public bool Result { get; private set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormAskBinaryQuestion"/> class.
        /// </summary>
        /// <param name="parent">The parent form</param>
        /// <param name="question">The question to be asked, will be the title of the form.</param>
        /// <param name="yesButtonText">The yes button text.</param>
        /// <param name="noButtonText">The no button text.</param>
        /// <param name="questionText">A more detailed question text to be displayed in the form itself. If <c>null</c>, then <paramref name="question"/> will be used.</param>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="question"/>, <paramref name="yesButtonText"/> or <paramref name="noButtonText"/> is null
        /// </exception>
        public FormAskBinaryQuestion(Form parent, string question, string yesButtonText = "Yes", string noButtonText = "No", string questionText = null)
        {
            // Check arguments
            if (string.IsNullOrEmpty(question))
            {
                throw new ArgumentNullException(nameof(question) + " must not be null or empty");
            }
            if (string.IsNullOrEmpty(yesButtonText))
            {
                throw new ArgumentNullException(nameof(yesButtonText) + " must not be null or empty");
            }
            if (string.IsNullOrEmpty(noButtonText))
            {
                throw new ArgumentNullException(nameof(noButtonText) + " must not be null or empty");
            }

            // Build the form
            InitializeComponent();

            // Set texts
            Text = question;
            lbl_QuestionText.Text = questionText ?? question;
            btn_Affirm.Text = yesButtonText;
            btn_Deny.Text = noButtonText;

            // Get border sizes
            Rectangle screenRectangle = RectangleToScreen(ClientRectangle);
            int titleHeight = screenRectangle.Top - Top;
            int borderWidth = screenRectangle.Left - Left;

            // Spacing between controls and also between controls and window border
            const int MARGIN = 13;

            lbl_QuestionText.Location = new Point(MARGIN, MARGIN);

            int widthRequiredForButtons = btn_Deny.Width + MARGIN + btn_Affirm.Width;
            int width = Math.Max(widthRequiredForButtons, lbl_QuestionText.Width) + (MARGIN * 2) + (borderWidth * 2);
            int height = borderWidth + titleHeight + lbl_QuestionText.Top + lbl_QuestionText.Height + MARGIN + Math.Max(btn_Affirm.Height, btn_Deny.Height) + MARGIN;

            Size = new Size(width, height);

            // Left button
            btn_Deny.Location = new Point(MARGIN, lbl_QuestionText.Location.Y + lbl_QuestionText.Height + MARGIN);
            // Right button
            btn_Affirm.Location = new Point(btn_Deny.Location.X + btn_Deny.Width + MARGIN, btn_Deny.Location.Y);

            ShowDialog(parent);
        }

        private void Btn_Deny_Click(object sender, EventArgs e)
        {
            Result = false;
            Close();
        }

        private void Btn_Affirm_Click(object sender, EventArgs e)
        {
            Result = true;
            Close();
        }
    }
}
