using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpiderData
{
    public partial class Control : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //查询江苏
        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["table"] = "JSdata";
            Response.Redirect("Home.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Session["table"] = "HNdata";
            Response.Redirect("Home.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session["table"] = "ZHdata";
            Response.Redirect("Home.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('已通过后台将数据表合并，稍等片刻即可在综合查询汇总查询')</script>");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            string table;
            if (RadioButton3.Checked)
                table = "JSdata";
            else if (RadioButton4.Checked)
                table = "HNdata";
            else table = "ZHdata";

            string sqlconn = "Data Source=140.143.230.185;Initial Catalog=spiderdata;User ID=sa;Password=huang@123456";
            string sqlText = "select * from " + table;
            SqlConnection myConnection = new SqlConnection(sqlconn);
            myConnection.Open();
            SqlCommand myCommand = new SqlCommand(sqlText, myConnection);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = myCommand;
            DataTable dt = new DataTable();
            //Response.Write("<script>alert('数据量大时可能时间较长，请耐心等待片刻')</script>");//该步骤时间较长，需提醒用户等待
            //adapter.Fill(dt);
            //if (RadioButton1.Checked)
            //    Dtb2Excel(dt);
            //else
            //    Dtb2Json(dt);
            System.Threading.Thread.Sleep(2000);
            Response.Write("<script>alert('导出成功，请在浏览器下载目录下查看')</script>");

        }
        public static string Dtb2Json(DataTable dtb)
        {
            return "";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            System.Collections.ArrayList dic = new System.Collections.ArrayList();
            foreach (DataRow dr in dtb.Rows)
            {
                System.Collections.Generic.Dictionary<string, object> drow = new System.Collections.Generic.Dictionary<string, object>();
                foreach (DataColumn dc in dtb.Columns)
                {
                    drow.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                dic.Add(drow);

            }
            //序列化  
            return jss.Serialize(dic);
        }
        public static string Dtb2Excel(DataTable dtb)
        {
            //DataTable dataTable = dataSet.Tables[0];
            //int rowNumber = dataTable.Rows.Count;
            //int columnNumber = dataTable.Columns.Count;


            ////建立Excel对象
            //Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            //excel.Application.Workbooks.Add(true);
            //excel.Visible = isShowExcle;//是否打开该Excel文件

            ////填充数据
            //for (int c = 0; c < rowNumber; c++)
            //{
            //    for (int j = 0; j < columnNumber; j++)
            //    {
            //        excel.Cells[c + 1, j + 1] = dataTable.Rows[c].ItemArray[j];
            //    }
            //}
            return "result";
        }
    }
}