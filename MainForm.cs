using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SegmentToDecimal
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    CheckBox c = new CheckBox() 
                    {
                        BackColor = Color.DimGray,
                        Size = new Size(14, 15),
                        Name = $"i{i}j{j}",
                        Location = new Point(i * 18 + 15, 18 * j + 15)
                    };
                    c.CheckedChanged += CheckedChanged;
                    pnlGrid.Controls.Add(c);

                    if (!c.Name.Contains("i14")) continue;
                    Label l = new Label()
                    {
                        Size = new Size(50, 15),
                        Text = "0",
                        Name = $"row{j}",
                        Location = new Point(i * 20 + 15, 18 * j + 15)
                    };
                    pnlGrid.Controls.Add(l);
                }
            }
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = ((CheckBox)sender).Name + "    " + ((CheckBox)sender).Location.X;
        }
    }
}
