//////////////////
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.Windows.Forms;
using System.Collections;

namespace AddrBook
{
   
    public class Common_DB
    {
        //-----------------------------------------------------------------------------
        // DataBase Connection
        //-----------------------------------------------------------------------------
        public static OleDbConnection DBConnection()
        {
            OleDbConnection Conn;
            //아래는 오라클용 접속 문자열, data source 애는 tnsnames.ora 파일에 있는 Alias명을 넣으면 됩니다.
            //32비트 
            //string ConStr = "Provider=MSDAORA;data source=onj;User ID=scott;Password=tiger";

            //64비트
            string ConStr = @"Provider=SQLOLEDB;Data Source=192.239.13.6;Initial Catalog=test;user id=itmaster;Password=!t@dmin100$krc96(^";

            Conn = new OleDbConnection(ConStr);
            return Conn;
        }


        //-----------------------------------------------------------------------------
        // DataSelect 
        //-----------------------------------------------------------------------------
        public static OleDbDataReader DataSelect(string sql, OleDbConnection Conn)
        {
            try
            {
                OleDbCommand myCommand = new OleDbCommand(sql, Conn);
                return myCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                //Log File에 출력 

                MessageBox.Show(sql + "\n" + ex.Message, "DataSelect");
                return null;
            }
            finally
            {
            }
        }


        //-----------------------------------------------------------------------------
        // DataDelete, DataInsert 
        //-----------------------------------------------------------------------------
        public static bool DataManupulation(string sql, OleDbConnection Conn)
        {
            try
            {
                OleDbCommand myCommand = new OleDbCommand(sql, Conn);
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //Log File에 출력 

                MessageBox.Show(sql + "\n" + ex.Message, "DataManupulation");
                return false;
            }
            finally
            {

            }
        }
    }
 


}
