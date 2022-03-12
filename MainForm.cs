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
                        Appearance = Appearance.Button,
                        FlatStyle = FlatStyle.Flat,
                        Size = new Size(23, 23),
                        Name = $"i{i:D2}j{j}",
                        Location = new Point(i * 25 + 15, 25 * j + 15)
                    };
                    c.FlatAppearance.CheckedBackColor = Color.Red;
                    c.CheckedChanged += CheckedChanged;
                    pnlGrid.Controls.Add(c);

                    if (!c.Name.Contains("i14")) continue;
                    Label l = new Label()
                    {
                        Font = new Font("Arial", 11),
                        ForeColor = Color.DarkSlateGray,
                        Size = new Size(50, 25),
                        Text = "0",
                        Name = $"row{j}",
                        Location = new Point(i * 28 + 10, 25 * j + 18)
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
            ushort rowValue = ushort.Parse(l.Text);

            if (c.Checked == true)
                rowValue += (ushort)Math.Pow(2, 14 - i);
            else
                rowValue -= (ushort)Math.Pow(2, 14 - i);

            l.Text = rowValue.ToString();
            //rtxtInfo.Text = $"cbox: {c.Name} \n i = {i} \n j = {j} \n\n lbl: {l.Name} \n {rowValue}";
        }

        private void btnShiftLeft_Click(object sender, EventArgs e)
        {
            bool[,] grid = GridToArray3D(pnlGrid);
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i != 14)
                        grid[i, j] = grid[i + 1, j];
                    else
                        grid[i, j] = false;
                }
            }
            Array3DToGrid(grid);
        }

        private void btnShiftRight_Click(object sender, EventArgs e)
        {
            bool[,] grid = GridToArray3D(pnlGrid);
            for (int i = 14; i >= 0; i--)
            {
                for (int j = 6; j >= 0; j--)
                {
                    if (i != 0)
                        grid[i, j] = grid[i - 1, j];
                    else
                        grid[i, j] = false;
                }
            }
            Array3DToGrid(grid);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Label l;
            string data = "DATA ";
            for (int i = 0; i < 7; i++)
            {
                l = pnlGrid.Controls[$"row{i}"] as Label;
                data += l.Text;
                if (i != 6) data += ", ";
            }
            rtxtInfo.Text = data;
        }

        private void btnOcisti_Click(object sender, EventArgs e)
        {
            foreach (Control c in pnlGrid.Controls)
            {
                if (c is CheckBox box)
                    box.Checked = false;
            }
            rtxtInfo.Text = "";
        }

        private bool[,] GridToArray3D(Panel panel)
        {
            bool[,] grid = new bool[15, 7];
            foreach (Control c in pnlGrid.Controls)
            {
                if (c is CheckBox box)
                {
                    int i = int.Parse(c.Name.Substring(1, 2));
                    int j = int.Parse(c.Name[4].ToString());
                    grid[i, j] = box.Checked;
                }
            }
            return grid;
        }

        private void Array3DToGrid(bool[,] grid)
        {
            CheckBox c;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    c = pnlGrid.Controls[$"i{i:D2}j{j}"] as CheckBox;
                    c.Checked = grid[i,j];
                }
            }
        }
    }
}
