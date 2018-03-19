using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using System.Data.SqlClient;
using System.Collections;

namespace TestInterop
{
    public class DatabaseFunc
    {
        private static SqlConnection con;
        private const string ConnectionString = @"database=oracle;" +
                                            @"Server=192.168.101.3,1433;User ID=sa;Password=adminsa";

        //测试项目数据库列名
        //规格表  
        private const string Product_Name="Product_Name";
        private const string Product_Mode="Product_Mode";
        private const string Tx_Wavelength = "Tx_Wavelength";
        private const string Rx_Wavelength = "Rx_Wavelength";
        private const string Frequence = "Frequence";
        private const string PRBS = "PRBS";
        private const string Mask_Type = "Mask_Type";
        private const string Record_Spec_Man = "Record_Spec_Man";
        private const string Software_Version = "Software_Version=";
        private const string Hardware_Version = "Hardware_Version";
        private const string Buyer_name = "Buyer_name";
        private const string Firmware_Vision = "Firmware_Vision";
        private const string R1_Min = "R1_Min";
        private const string R1_Max = "R1_Max";
        private const string R1_Ini = "R1_Ini";
        private const string R2_Min = "R2_Min";
        private const string R2_Max = "R2_Max";
        private const string R2_Ini = "R2_Ini";
        private const string Ap_min_first = "Ap_min_first";
        private const string Ap_max_first = "Ap_max_first";
        private const string Ap_select_first = "Ap_select_first";
        private const string Er_min_first = "Er_min_first";
        private const string Er_max_first = "Er_max_first";
        private const string Er_select_first = "Er_select_first";
        private const string Mask_Count_First = "Mask_Count_First";
        private const string Mask_Margin_First = " Mask_Margin_First";
        private const string Mask_select_first = "Mask_select_first";
        private const string Crossing_min_First = "Crossing_min_First";
        private const string Crossing_max_First = "Crossing_max_First";
        private const string Crossing_select_first = "Crossing_select_first";
        private const string Saturation_Min_First = "Saturation_Min_First";
        private const string Saturation_select_first = "Saturation_select_first";
        private const string Sen_Min_First = "Sen_Min_First";
        private const string Sen_Max_First = "Sen_Max_First";
        private const string Sen_select_first = "Sen_select_first";
        private const string Thl_Min_First = "Thl_Min_First";
        private const string Thl_Max_First = "Thl_Max_First";
        private const string Thl_select_first = "Thl_select_first";
        private const string Thh_Min_First = " Thh_Min_First";
        private const string Thh_Max_First = "Thh_Max_First";
        private const string Thh_select_first = " Thh_select_first";
        private const string Thh_Thl_Min_First = "Thh_Thl_Min_First";
        private const string Thh_Thl_Max_First = "Thh_Thl_Max_First";
        private const string Thh_Thl_select_first = "Thh_Thl_select_first";
        private const string AP_Min_Last = "AP_Min_Last";
        private const string AP_Max_Last = "AP_Max_Last";
        private const string Ap_select_last = "Ap_select_last";
        private const string Er_Min_Last = "Er_Min_Last";
        private const string Er_Max_Last = "Er_Max_Last";
        private const string Er_select_last = "Er_select_last";
        private const string Mask_Count_Last = "Mask_Count_Last";
        private const string Mask_Margin_Last = "Mask_Margin_Last";
        private const string Mask_select_last = "Mask_select_last";
        private const string Crossing_min_Last = "Crossing_min_Last";
        private const string Crossing_max_Last = "Crossing_max_Last";
        private const string Crossing_select_last = "Crossing_select_last";
        private const string Rx_Mon_First = "Rx_Mon_First";
        private const string Rx_Mon_Second = "Rx_Mon_Second";
        private const string Rx_Mon_Third = "Rx_Mon_Third";
        private const string Rx_Mon_four = "Rx_Mon_four";
        private const string Rx_Mon_five = "Rx_Mon_five";
        private const string RX_CheckPower1_max = "RX_CheckPower1_max";
        private const string RX_CheckPower2_max = "RX_CheckPower2_max";
        private const string RX_CheckPower3_max = "RX_CheckPower3_max";
        private const string RX_CheckPower4_max = "RX_CheckPower4_max";
        private const string RX_CheckPower5_max = "RX_CheckPower5_max";
        private const string Tx_DDM_Check = "Tx_DDM_Check";
        private const string VoIP_Select_Test = "VoIP_Select_Test";
        private const string Data_Select_Test = "Data_Select_Test";
        private const string Data_Down_dBm = "Data_Down_dBm";
        private const string Data_Time_Length = "Data_Time_Length";

        //记录表
        private const string SN="SN";
        //private const string Product_Name="Product_Name";
        private const string MAC_ADD = "MAC_ADD";
        private const string BOSA_ID = "BOSA_ID";
        private const string Manufacture_ID = "Manufacture_ID";
        private const string Material_ID = "Material_ID";
        private const string PCB_ID = "PCB_ID";
        private const string Initialization_Operator = "Initialization_Operator";
        private const string Initialization_Date = "Initialization_Date";
        private const string Initialization_Line = "Initialization_Line";
        private const string Initialization_Flag = "Initialization_Flag";
        //发射初测
        private const string Test_APC_Date = "Test_APC_Date";
        private const string Test_APC_Operator = "Test_APC_Operator";
        private const string AP="AP";
        private const string ER = "ER";
        private const string AP_DAC = "AP_DAC";
        private const string ER_DAC = "ER_DAC";
        private const string OSC_ID = "OSC_ID";
        private const string Crossing = "Crossing";
        private const string Mask = "Mask";
        private const string Test_APC_Flag = "Test_APC_Flag";
        private const string CWL = "CWL";
        private const string Bandwidth = "Bandwidth";
        private const string DDM_Tx_DA_1 = "DDM_Tx_DA_1";
        private const string DDM_Tx_Cal_Date = "DDM_Tx_Cal_Date";
        private const string DDM_Tx_Cal_Flag = "DDM_Tx_Cal_Flag";
        private const string DDM_Tx_Cal_Line = "DDM_Tx_Cal_Line";
        private const string DDM_Tx_Cal_Operator = "DDM_Tx_Cal_Operator";      
        private const string DDM_I_Cal_Date = "DDM_I_Cal_Date";
        private const string DDM_I_Cal_Flag = "DDM_I_Cal_Flag";
        private const string DDM_I_Cal_Operator = "DDM_I_Cal_Operator";
        private const string DDM_I_Cal_Result_Flag = "DDM_I_Cal_Result_Flag";
        private const string BIAS_VALUE = "BIAS_VALUE";
        private const string BIAS_AD_FIRST_TEST = "BIAS_AD_FIRST_TEST";
        

        //接收初测
        private const string DDM_Rx_DA_1 = "DDM_Rx_DA_1";
        private const string DDM_Rx_DA_2 = "DDM_Rx_DA_2";
        private const string DDM_Rx_DA_3 = "DDM_Rx_DA_3";
        private const string DDM_Rx_DA_4 = "DDM_Rx_DA_4";
        private const string DDM_Rx_DA_5 = "DDM_Rx_DA_5";
        private const string DDM_Rx_Cal_Date = "DDM_Rx_Cal_Date";
        private const string DDM_Rx_Cal_Flag = "DDM_Rx_Cal_Flag";
        private const string DDM_Rx_Cal_Line = "DDM_Rx_Cal_Line";
        private const string DDM_Rx_Cal_Operator = "DDM_Rx_Cal_Operator";
        private const string RX_First_Test_Operator = "RX_First_Test_Operator";
        private const string Rx_First_Test_Date = "Rx_First_Test_Date";
        private const string Rx_First_Test_Line = "Rx_First_Test_Line";
        private const string Sensitivity = "Sensitivity";
        private const string Rx_First_Test_Flag = "Rx_First_Test_Flag";
        private const string LOSLEVEL = "LOSLEVEL";
        
        //接收终测
        private const string Rx_Last_Test_Operator = "Rx_Last_Test_Operator";
        private const string Rx_Last_Test_Date = "Rx_Last_Test_Date";
        private const string Rx_Last_Test_Line = "Rx_Last_Test_Line";
        private const string Rx_Sensitivity = "Rx_Sensitivity";
        private const string Rx_Deassert = "Rx_Deassert";
        private const string Rx_Assert = "Rx_Assert";
        private const string Rx_A_D = "Rx_A_D";
        private const string Rx_Saturation = "Rx_Saturation";
        private const string Rx_Last_Test_Flag = "Rx_Last_Test_Flag";
        private const string DDM_Rx_Power1 = "DDM_Rx_Power1";
        private const string DDM_Rx_Power2 = "DDM_Rx_Power2";
        private const string DDM_Rx_Power3 = "DDM_Rx_Power3";
        private const string DDM_Rx_Power4 = "DDM_Rx_Power4";
        private const string DDM_Rx_Power5 = "DDM_Rx_Power5";
        private const string Test_Rx_Power1 = "Test_Rx_Power1";
        private const string Test_Rx_Power2 = "Test_Rx_Power2";
        private const string Test_Rx_Power3 = "Test_Rx_Power3";
        private const string Test_Rx_Power4 = "Test_Rx_Power4";
        private const string Test_Rx_Power5 = "Test_Rx_Power5";
        private const string DDM_Rx_Flag = "DDM_Rx_Flag";

        //发射终测
        private const string DDM_Tx_Power1 = "DDM_Tx_Power1";
        private const string Test_Tx_Power1 = "Test_Tx_Power1";
        private const string DDM_Tx_Flag = "DDM_Tx_Flag";
        private const string Tx_Test_Operator = "Tx_Test_Operator";
        private const string Tx_Test_Date = "Tx_Test_Date";
        private const string Tx_Test_Line = "Tx_Test_Line";
        private const string Tx_AP = "Tx_AP";
        private const string Tx_ER = "Tx_ER";
        private const string Tx_Crossing = "Tx_Crossing";
        private const string Tx_Mask = "Tx_Mask";
        private const string Tx_Test_Flag = "Tx_Test_Flag";
        private const string DDM_BIAS = "DDM_BIAS";
        private const string BIAS_AD_LAST_TEST = "BIAS_AD_LAST_TEST";

        private const string RESET = "RESET";

        private const string TEST_COUNT = "TEST_COUNT";

        public SqlConnection SQLCONN
        {
            get
            {
                return con;
            }
        }

        public static int Check_Station(string taskCode,string onuSn,string unitid,string state)
        {
            string commandString = @"dbo.CANRUN";

            SqlCommand command = new SqlCommand(commandString, con);

            command.CommandText = @"dbo.CANRUN";

            command.Parameters.Add("@taskCode", SqlDbType.VarChar, 50);
            command.Parameters["@taskCode"].Value = taskCode;

            command.Parameters.Add("@onuSn", SqlDbType.VarChar, 50);
            command.Parameters["@onuSn"].Value = onuSn;

            //command.Parameters.Add("@username", SqlDbType.VarChar);
            //command.Parameters["@username"].Value = username;

            command.Parameters.Add("@unitid", SqlDbType.VarChar, 50);
            command.Parameters["@unitid"].Value = unitid;

            command.Parameters.Add("@state", SqlDbType.VarChar, 50);
            command.Parameters["@state"].Value = state;
            command.Parameters["@state"].Direction = ParameterDirection.Output;

            command.CommandType = CommandType.StoredProcedure;
            command.Connection = con;

            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("调用存储过程失败!");
            }
            state = command.Parameters["@state"].Value.ToString();
            return Convert.ToInt32(state);
        }

        public static bool Set_Next_Station(string taskCode, string onuSn, string username, string unitid, string result)
        {
            Int32 rowsAffected;

            string commandString = @"dbo.FINTASKS";

            SqlCommand command = new SqlCommand(commandString, con);

            command.CommandText = @"dbo.FINTASKS";

            command.Parameters.Add("@taskCode", SqlDbType.VarChar);
            command.Parameters["@taskCode"].Value = taskCode;

            command.Parameters.Add("@onuSn", SqlDbType.VarChar);
            command.Parameters["@onuSn"].Value = onuSn;

            command.Parameters.Add("@username", SqlDbType.VarChar);
            command.Parameters["@username"].Value = username;

            command.Parameters.Add("@unitid", SqlDbType.VarChar);
            command.Parameters["@unitid"].Value = unitid;

            command.Parameters.Add("@type", SqlDbType.VarChar);
            command.Parameters["@type"].Value = result;

            command.CommandType = CommandType.StoredProcedure;
            command.Connection = con;

            try
            {
                rowsAffected = command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                throw new Exception("调用存储过程失败!");
            }
           
        }

        public static void Set_Repair_Station(string taskCode, string onuSn, string username, string unitid, string type)
        {
            Int32 rowsAffected;

            string commandString = @"dbo.SETREPAIR";

            SqlCommand command = new SqlCommand(commandString, con);

            command.CommandText = @"dbo.SETREPAIR";

            command.Parameters.Add("@taskCode", SqlDbType.VarChar);
            command.Parameters["@taskCode"].Value = taskCode;

            command.Parameters.Add("@onuSn", SqlDbType.VarChar);
            command.Parameters["@onuSn"].Value = onuSn;

            command.Parameters.Add("@username", SqlDbType.VarChar);
            command.Parameters["@username"].Value = username;

            command.Parameters.Add("@unitid", SqlDbType.VarChar);
            command.Parameters["@unitid"].Value = unitid;

            command.Parameters.Add("@type", SqlDbType.VarChar);
            command.Parameters["@type"].Value = type;

            command.CommandType = CommandType.StoredProcedure;
            command.Connection = con;

            try
            {
                rowsAffected = command.ExecuteNonQuery();
                return;
            }
            catch
            {
                throw new Exception("调用存储过程失败!");
            }
        }

        public static SqlConnection Connect_DataBase(string ConnectionString)
        {
            try
            {
                con = new SqlConnection(ConnectionString);
                con.Open();
                return con;
            }
            catch
            {
                return null;
            }
        }

        public bool DB_CONNECTED
        {
            get
            {
                return con.State == System.Data.ConnectionState.Open;
            }
 
        }

        public static DataTable GetTestRefer(string productname)
        {
            try
            {
                string cmd = @"select * from ODC_SPESHEET where Product_Name='" + productname + "'";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd, con);
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static int GetTestTimes(string sn)
        {
            try
            {
                string strCmd = @"select TEST_COUNT from ODC_TESTRECORD where sn='"+sn+"'";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                da.Fill(ds);
                return Convert.ToInt32(ds.Tables[0].Rows[0]["TEST_COUNT"].ToString());
            }
            catch
            {
                return -1;
            }
        }

        public static bool SaveResult(string strID, string strTaskCode, string strMac, string strHostLable, string strWeight)
        {
            string strCmd = string.Format(@"INSERT INTO ODC_WEIGHT (TASKSCODE,MAC,HOSTLABLE,WEIGHT) 
     VALUES ('{0}','{1}','{2}','{3}');", strTaskCode, strMac, strHostLable, strWeight);

            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand(strCmd, con);
                    int n = cmd.ExecuteNonQuery();
                    if (n != 1)
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool SaveResult_Weight(string strID, string strTaskCode, string strMac, string strHostLable, string strWeight, string remark)
        {
            string strCmd = string.Format(@"INSERT INTO ODC_WEIGHT (TASKSCODE,MAC,HOSTLABLE,WEIGHT,REMARK) 
     VALUES ('{0}','{1}','{2}','{3}','{4}');", strTaskCode, strMac, strHostLable, strWeight , remark);

            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand(strCmd, con);
                    int n = cmd.ExecuteNonQuery();
                    if (n != 1)
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool GetInfo(string strSN, out string strTaskCode, out string strMac)
        {
            strTaskCode = "";
            strMac = "";
            try
            {
                string strCmd = @"select TASKSCODE, MAC from ODC_MACINFO where BARCODE1 = '" + strSN + "'";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                da.Fill(ds);
                strTaskCode = ds.Tables[0].Rows[0]["TASKSCODE"].ToString();
                strMac = ds.Tables[0].Rows[0]["MAC"].ToString();
                strMac = strMac.Replace("-", "");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DataTable GetTasksID()
        {
            try
            {
                string strCmd = @"select TASKSCODE from ODC_TASKS";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetTasksInfo(string strTasksID)
        {
            try
            {
                string strCmd = @"select * from ODC_TASKS where taskscode='" + strTasksID + "'";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetAccountInfo()
        {
            try
            {
                string strCmd = @"select * from odc_fhpartitionpass where fdepart in ('生产') and fpermission in ('1','0') order by id";

                //string strCmd = @"select fusername from odc_fhpartitionpass where fdepart in ('生产','0') and fpermission in ('1','0') order by id";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetMACInfo(string strMAC)
        {
            try
            {
                string strCmd = @"select * from ODC_MACINFO where MAC='" + strMAC + "'";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static int CheckLogin(string Username,string Pwd,string UnitId)
        {
            int index;

            string unitid="";

            //验证员工密码和权限
            string strCmd = @"select * from USERS where USERNAME='" + Username + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            da = new SqlDataAdapter(strCmd, con);
            da.Fill(ds);

            //验证用户密码
            if (ds.Tables[0].Rows.Count == 0)
            {
                return -1;
            }
            else if (ds.Tables[0].Rows[0]["PWORD"].ToString() != Pwd)
            {
                return -2;
            }

            string AccreditID = ds.Tables[0].Rows[0]["ID"].ToString();

            ds.Clear();

            strCmd = @"select * from ACCREDIT where AUSERID='" + AccreditID + "'";
            da = new SqlDataAdapter(strCmd, con);
            da.Fill(ds);

            //验证工号权限
            for (index = 0; index < ds.Tables[0].Rows.Count; index++)
            {
                if (ds.Tables[0].Rows[index]["BUSERID"].ToString() == UnitId)
                {
                    unitid = ds.Tables[0].Rows[index]["BUSERID"].ToString();
                    break;
                }
            }

            if (unitid == "")
            {
                return -3;
            }
            else
            {
                return Convert.ToInt32(unitid);
            }
        }

        public static double GetVBR(string MAC)
        {
            try
            {
                string cmd = @"select * from BOSA where BOSA_SN=(select BOSASN from odc_alllable where hostlable='" + MAC + "')";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd, con);
                da.Fill(ds);
                return Convert.ToDouble(ds.Tables[0].Rows[0]["VBR"]);
            }
            catch
            {
                return 0;
            }
        }

        public static String GetBOSASN(string MAC)
        {
            try
            {
                string cmd = @"select BOSASN from odc_alllable where hostlable='" + MAC + "'";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd, con);
                da.Fill(ds);
                return Convert.ToString(ds.Tables[0].Rows[0]["BOSASN"]);

            }
            catch
            {
                return "";
            }
        }

        public static double GetBiasSet(string MAC)
        {
            try
            {
                string cmd = @"select * from odc_testrecord where sn='" + MAC + "'";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd, con);
                da.Fill(ds);
                return Convert.ToDouble(ds.Tables[0].Rows[0]["Bias_value"]);
            }
            catch
            {
                return 0;
            }
        }

        //保存老化数据
        public static int SaveLaohuaRecord(string tt_barcode, string tt_taskscode)
        {
            int i = 0;
            //DateTime ds = System.DateTime.Now;
            string commandString = @"INSERT INTO ODC_BARCODELH" +
                "("
                + "BARCODE" +    //单板号
                ", " + "TASKSCODE" +    //工单号
                ", " + "FDATE" +   //时间
                ") " +

                "VALUES (" +
                "'" + tt_barcode + "'," +   //单板号
                "'" + tt_taskscode + "'," +  //工单号
                " getdate() " +  //时间
                ")";

            SqlCommand command = new SqlCommand(commandString, con);
            try
            {
                i = command.ExecuteNonQuery();

            }
            catch
            {
                return -1;
            }
            return i;
        }

        public class Context
        {
            public static Hashtable ContextData = new Hashtable();
        }

        //取上下限计算参数
        public static bool GetWeight_coefficient(string strTasksID, out double fweight_c)
        {
            fweight_c = 0;
            try
            {
                string strCmd = @"select fweight_c from odc_dypowertype where ftype in 
                                 (select product_name from odc_tasks where taskscode = '" + strTasksID + "')";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
                da.Fill(ds);
                fweight_c = double.Parse(ds.Tables[0].Rows[0]["fweight_c"].ToString());
                if (fweight_c != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
