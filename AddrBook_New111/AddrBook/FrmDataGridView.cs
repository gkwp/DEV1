using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Timers;

namespace AddrBook
{
    public partial class FrmDataGridView : Form
    {
        public FrmDataGridView()
        {
            InitializeComponent();
        }

        private OleDbConnection LocalConn;        
        private DataSet ds;
        private OleDbDataAdapter adapter;
        private OleDbTransaction tran;
        OleDbCommandBuilder cb;

        //-----------------------------
        // 화면이 로드될 때 처리
        //-----------------------------
        private void FrmDataGridView_Load(object sender, EventArgs e)
        {
            try
            {
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 60 * 1000; // 60초 마다 DataGridView 갱신
                timer.Elapsed += new ElapsedEventHandler(SetGrid);
                timer.Start();

                LoadData();

                dataGrid1.Columns[dataGrid1.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGrid1.Columns[0].HeaderText = "이 름";
                dataGrid1.Columns[1].HeaderText = "성 별";
                dataGrid1.Columns[2].HeaderText = "주 소";
                dataGrid1.Columns[2].Width = 180;
                dataGrid1.Columns[3].HeaderText = "전화번호";
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                Log.WriteLine("FrmDataGridView", e1.ToString());
            }
            finally
            {
                LocalConn.Close();                
            }
        }

        //-----------------------------
        // 저장버튼 클릭
        //-----------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                LocalConn.Open();

                //트랜잭션 시작
                tran = LocalConn.BeginTransaction();

                adapter.InsertCommand.Connection = tran.Connection;
                adapter.UpdateCommand.Connection = tran.Connection;
                adapter.DeleteCommand.Connection = tran.Connection;

                adapter.DeleteCommand.Transaction = tran;
                adapter.InsertCommand.Transaction = tran;
                adapter.UpdateCommand.Transaction = tran;

                dataGrid1.EndEdit();
                adapter.Update(ds.Tables["ADDRBOOK"]);

                tran.Commit();
                Log.WriteLine("FrmDataGridView", "저장OK");
                MessageBox.Show("저장 OK~");
            }
            catch (Exception e1)
            {
                if (tran != null) tran.Rollback();
                MessageBox.Show("주소록 저장 오류~" + e1.ToString());
                Log.WriteLine("FrmDataGridView", e1.ToString());
            }
            finally
            {
                LocalConn.Close();
            }
        }

        //-----------------------------
        //DataGridView에 데이터 채우기
        //-----------------------------
        private void LoadData()
        {
            try
            {
                string sql = "select * from addrbook";

                LocalConn = Common_DB.DBConnection();
                LocalConn.Open();
                adapter = new OleDbDataAdapter(sql, LocalConn);
                cb = new OleDbCommandBuilder(adapter);

                adapter.DeleteCommand = cb.GetDeleteCommand();
                adapter.InsertCommand = cb.GetInsertCommand();
                adapter.UpdateCommand = cb.GetUpdateCommand(); 

                ds = new DataSet();
                adapter.Fill(ds, "ADDRBOOK");

                dataGrid1.DataSource = ds.Tables["ADDRBOOK"];
            }
            catch (Exception e1)
            {
                MessageBox.Show("주소록 저장 오류~" + e1.ToString());
                Log.WriteLine("FrmDataGridView", e1.ToString());
            }
            finally
            {
                LocalConn.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        delegate void SetGridDeligate(object sender, EventArgs e);

        public void SetGrid(object sender, EventArgs e)
        {            

            if (this.dataGrid1.InvokeRequired)
            {
                SetGridDeligate d = new SetGridDeligate(btnRefresh_Click); //델리게이트 선언
                this.Invoke(d, new object[] { sender, e }); //델리게이트로 글을쓴다.
            }
            else
            {
                btnRefresh_Click(sender, e);
            }
        }

    }
}
