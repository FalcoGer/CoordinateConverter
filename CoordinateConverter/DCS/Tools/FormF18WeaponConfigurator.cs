using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoordinateConverter.DCS.Tools
{
    public partial class FormF18WeaponConfigurator : Form
    {
        private readonly MainForm parent;

        public FormF18WeaponConfigurator(MainForm parent)
        {
            InitializeComponent();

            this.parent = parent ?? throw new ArgumentNullException(nameof(parent));

        }


    }
}
