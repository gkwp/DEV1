using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AddrBook
{
    public partial class FrmMDIMain : Form
    {

        FrmListView addrbook = new FrmListView();
        FrmDataGridView frmgrid = new FrmDataGridView();
        FrmCalc frmcalc = new FrmCalc();
        FrmLogViewer frmlogvieer = new FrmLogViewer();

        public FrmMDIMain()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("종료 하시겠습니까?", "종료 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        

        private void listView이용ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            addrbook.MdiParent = this;
            addrbook.WindowState = FormWindowState.Maximized;
            addrbook.Show();

            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void dataGrid이용ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            frmgrid.MdiParent = this;
            frmgrid.WindowState = FormWindowState.Maximized;
            frmgrid.Show();

            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void MDIMain_Load(object sender, EventArgs e)
        {
            TreeNode node1 = new TreeNode("화일");
            node1.Tag = 1;
            node1.ImageIndex = 0;
            node1.SelectedImageIndex = 0;
            node1.Expand();

            TreeNode node11 = new TreeNode("Calculator");
            node11.ImageIndex = 1;
            node11.SelectedImageIndex = 1;
            node11.Tag = 11;

            TreeNode node12 = new TreeNode("View Log");
            node12.ImageIndex = 2;
            node12.SelectedImageIndex = 2;
            node12.Tag = 12;

            TreeNode node13 = new TreeNode("Exit");
            node13.ImageIndex = 3;
            node13.SelectedImageIndex = 3;
            node13.Tag = 13;

            node1.Nodes.Add(node11);
            node1.Nodes.Add(node12);
            node1.Nodes.Add(node13);
            //-------------------------------------
            TreeNode node2 = new TreeNode("주소록");
            node2.ImageIndex = 0;
            node2.SelectedImageIndex = 0;
            node2.Tag = 2;
            node2.Expand();

            TreeNode node21 = new TreeNode("ListView이용");
            node21.ImageIndex = 1;
            node21.SelectedImageIndex = 1;
            node21.Tag = 21;

            TreeNode node22 = new TreeNode("DataGrid이용");
            node22.ImageIndex = 1;
            node22.SelectedImageIndex = 1;
            node22.Tag = 22;

            node2.Nodes.Add(node21);
            node2.Nodes.Add(node22);

            treeView1.Nodes.Add(node1);
            treeView1.Nodes.Add(node2);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode trnode = new TreeNode();
            trnode = e.Node;

            //계산기
            if (trnode.Tag.ToString() == "11")
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                frmcalc.MdiParent = this;
                frmcalc.WindowState = FormWindowState.Maximized;
                frmcalc.Show();

                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

            //View Log
            if (trnode.Tag.ToString() == "12")
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                frmlogvieer.MdiParent = this;
                frmlogvieer.WindowState = FormWindowState.Maximized;
                frmlogvieer.Show();

                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

			//Exit
			if (trnode.Tag.ToString() == "13")
			{
                if (MessageBox.Show("종료 하시겠습니까?", "종료 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
			}

            //ListView를 이용
            if (trnode.Tag.ToString() == "21")
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                addrbook.MdiParent = this;
                addrbook.WindowState = FormWindowState.Maximized;
                addrbook.Show();

                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            //DataGrid 이용
            else if (trnode.Tag.ToString() == "22")
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                frmgrid.MdiParent = this;
                frmgrid.WindowState = FormWindowState.Maximized;
                frmgrid.Show();

                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

		private void MDIMain_FormClosing(object sender, FormClosingEventArgs e)
		{            
            Application.Exit();
		}

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            frmcalc.MdiParent = this;
            frmcalc.WindowState = FormWindowState.Maximized;
            frmcalc.Show();

            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void viewLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            frmlogvieer.MdiParent = this;
            frmlogvieer.WindowState = FormWindowState.Maximized;
            frmlogvieer.Show();

            this.Cursor = System.Windows.Forms.Cursors.Default;
        }


    }
}
