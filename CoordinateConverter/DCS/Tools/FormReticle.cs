using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CoordinateConverter.DCS.Tools
{
    /// <summary>
    /// A Form that has magenta as the transparency key and is placed at the center of a screen. An image of a reticle is loaded.
    /// </summary>
    /// <seealso cref="Form" />
    public partial class FormReticle : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormReticle"/> class.
        /// </summary>
        public FormReticle()
        {
            InitializeComponent();
            FileInfo fi = new FileInfo("Crosshair.png");
            if (fi.Exists )
            {
                using (FileStream fileStream = fi.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BackgroundImage = Image.FromStream(fileStream);
                }
            }
            Size = BackgroundImage.Size;
        }
    }
}
