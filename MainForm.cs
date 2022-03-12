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
                        Name = $"i{i:D2}j{j}",
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
            CheckBox c = ((CheckBox)sender);
            int i = int.Parse(c.Name.Substring(1, 2));
            int j = int.Parse(c.Name[4].ToString());

            Label l = pnlGrid.Controls[$"row{j}"] as Label;
            int rowValue = int.Parse(l.Text);

            if (c.Checked == true)
            {
                rowValue += (int)Math.Pow(2, 14 - i);

                richTextBox1.Text = $"cbox: {c.Name} \n i = {i} \n j = {j} \n\n lbl: {l.Name} \n {rowValue}";
            }
            else
            {
                rowValue -= (int)Math.Pow(2, 14 - i);
                
                richTextBox1.Text = "";
            }
            l.Text = rowValue.ToString();
        }
    }
}
