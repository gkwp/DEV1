using System;
using System.Data.OleDb;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AddrBook
{
    public partial class FrmListView : Form
    {
        public FrmListView()
        {
            InitializeComponent();
        }

        private OleDbConnection LocalConn;


        //----------------------------------------------
        // ListView에 데이터 전체를 로드하는 함수
        //----------------------------------------------
        private void DataLoad(OleDbDataReader myDataReader)
        {
            listView1.Items.Clear();

            try
            {
                while (myDataReader.Read())
                {
                    ListViewItem myitem1;
                    string gubun;


                    if (myDataReader["Sex"].ToString() == "M")
                    {
                        gubun = "남자";
                    }
                    else
                    {
                        gubun = "여자";
                    }

                    myitem1 = new ListViewItem(myDataReader["Name"].ToString());
                    myitem1.SubItems.Add(gubun);
                    //myitem1.SubItems.Add(myDataReader["Addr"].ToString());

                    //주소가 null 값을 리턴하면...

                    if (myDataReader.IsDBNull(2))
                    {
                        myitem1.SubItems.Add("");
                    }
                    else
                    {
                        myitem1.SubItems.Add(myDataReader["Addr"].ToString());
                    }

                    myitem1.SubItems.Add(myDataReader["Tel"].ToString());

                    listView1.Items.Add(myitem1);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                Log.WriteLine("FrmListView", e1.ToString());
            }
            finally
            {
                if (!myDataReader.IsClosed)
                {
                    myDataReader.Close();
                    myDataReader = null;
                }
            }

        }

        //-----------------------------------------------------------
        // 신규 버튼
        //-----------------------------------------------------------
        private void btnNew_Click(object sender, System.EventArgs e)
        {
            this.txtName.Clear();
            this.txtAddr.Clear();
            this.txtTel.Clear();
            this.comSex.Text = "남자";

            this.txtName.Focus();
        }

        //-------------------------------------------------------------
        // 입력 버튼
        //-------------------------------------------------------------
        private void btnInput_Click(object sender, System.EventArgs e)
        {
            if (txtName.Text == "" || txtTel.Text == "")
            {
                MessageBox.Show("성명, 전화번호는 필수 입력사항 입니다.");
                txtName.Focus();
                return;
            }

            string gubun;
            if (comSex.Text == "남자") { gubun = "M"; }
            else { gubun = "F"; }

            try
            {

                LocalConn.Open();

                string sql = "Insert Into AddrBook (Name, Sex, Addr, Tel) values(";
                sql += "'" + txtName.Text + "'" + ",";
                sql += "'" + gubun + "'" + ",";
                sql += "'" + txtAddr.Text + "'" + ",";
                sql += "'" + txtTel.Text + "'" + ")";

                if (Common_DB.DataManupulation(sql, LocalConn))
                {
                    OleDbDataReader myReader = Common_DB.DataSelect("select * from AddrBook", LocalConn);
                    DataLoad(myReader);
                    Log.WriteLine("FrmListView", "데이터 한건 입력(" + txtName + "," + txtTel + ")");
                    MessageBox.Show("정상적으로 입력 되었습니다...");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                Log.WriteLine("FrmListView", e1.ToString());
            }
            finally
            {
                LocalConn.Close();
            }
        }

        //-------------------------------------------------------------
        // 수정 버튼 클릭시 DB에 Update 하는 부분
        //-------------------------------------------------------------
        private void btnUpdate_Click(object sender, System.EventArgs e)
        {
            if (txtName.Text == "" || txtTel.Text == "")
            {
                MessageBox.Show("성명, 전화번호는 필수 입력사항 입니다.");
                txtName.Focus();
                return;
            }

            string gubun;
            if (comSex.Text == "남자")
            {
                gubun = "M";
            }
            else
            {
                gubun = "F";
            }

            try
            {

                LocalConn.Open();
                string myExecuteQuery = "Update AddrBook set Name='" + txtName.Text + "'" + ",";
                myExecuteQuery += " Addr = '" + txtAddr.Text + "'" + ",";
                myExecuteQuery += " Sex = '" + gubun + "'" + ",";
                myExecuteQuery += " Tel = '" + txtTel.Text + "'";
                myExecuteQuery += " where Name = " + "'" + txtName.Text + "'";

                if (Common_DB.DataManupulation(myExecuteQuery, LocalConn))
                {
                    OleDbDataReader myReader = Common_DB.DataSelect("select * from AddrBook", LocalConn);
                    DataLoad(myReader);
                    MessageBox.Show(" 정상적으로 수정 되었습니다...");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                Log.WriteLine("FrmListView", e1.ToString());
            }
            finally
            {
                LocalConn.Close();
            }
        }

        //-------------------------------------------------------------
        // 삭제 버튼 클릭시 DB에 Update 하는 부분
        //-------------------------------------------------------------
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            OleDbDataReader myReader = null;
            if (txtName.Text == "" || txtTel.Text == "")
            {
                MessageBox.Show("성명, 전화번호는 필수 입력사항 입니다.");
                txtName.Focus();
                return;
            }

            //-------------------------------- 삭제 확인
            if (MessageBox.Show("정말 삭제 하시겠습니까?", "삭제확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }

            try
            {
                LocalConn.Open();
                string myExecuteQuery = "Delete AddrBook ";
                myExecuteQuery += " where Name = " + "'" + txtName.Text + "'";

                if (Common_DB.DataManupulation(myExecuteQuery, LocalConn))
                {
                    myReader = Common_DB.DataSelect("select * from AddrBook", LocalConn);
                    DataLoad(myReader);
                    MessageBox.Show(" 정상적으로 삭제 되었습니다...");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                Log.WriteLine("FrmListView", e1.ToString());
            }
            finally
            {
                LocalConn.Close();
            }
        }


        //------------------------------------------------------
        // Refresh 버튼 
        //------------------------------------------------------
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                LocalConn.Open();

                OleDbDataReader myReader = Common_DB.DataSelect("select * from AddrBook", LocalConn);
                DataLoad(myReader);

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                Log.WriteLine("FrmListView", e1.ToString());
            }
            finally
            {
                LocalConn.Close();
            }
        }

        //------------------------------------------------------------
        // 검색 버튼
        //------------------------------------------------------------
        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            try
            {
                LocalConn.Open();

                string sql = "select * from AddrBook ";
                sql += " where name like '%" + txtSearchName.Text + "%'";

                OleDbDataReader myReader = Common_DB.DataSelect(sql, LocalConn);

                if (myReader != null)
                {
                    DataLoad(myReader);
                    myReader.Close();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                Log.WriteLine("FrmListView", e1.ToString());
            }
            finally
            {
                LocalConn.Close();
            }
        }


        //--------------------------------------------------------
        // 메인화면 로딩시 처리되는 부분
        //--------------------------------------------------------     

        private void FrmListView_Load(object sender, EventArgs e)
        {
            OleDbDataReader myReader = null;
            try
            {
                //--------------------------------------------
                LocalConn = Common_DB.DBConnection();
                //--------------------------------------------
                LocalConn.Open();
                myReader = Common_DB.DataSelect("select * from AddrBook ", LocalConn);
                DataLoad(myReader);

                //Combo Box 채우기
                comSex.Items.Add("남자");
                comSex.Items.Add("여자");
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                Log.WriteLine("FrmListView", e1.ToString());
            }
            finally
            {
                if (!myReader.IsClosed)
                {
                    myReader.Close();
                    myReader = null;
                }
                LocalConn.Close();
            }

        }

        //-----------------------------------------------------------
        // ListView에서 더블클릭시 데이를 보여줌
        //-----------------------------------------------------------
        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            txtName.Text = listView1.SelectedItems[0].SubItems[0].Text;
            comSex.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtAddr.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txtTel.Text = listView1.SelectedItems[0].SubItems[3].Text;
        }
    }
}
