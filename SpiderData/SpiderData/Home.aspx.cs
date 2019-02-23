using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpiderData
{
    public partial class Home : System.Web.UI.Page
    {
        DataClasses1DataContext DataClasses1DataContext = new DataClasses1DataContext();
        private string connectionString = "Data Source=140.143.230.185;Initial Catalog=SpiderData;Persist Security Info=True;User ID=sa;Password=huang@123456";
        protected void Page_Load(object sender, EventArgs e)
        {

            if(Session["table"].ToString()=="JSdata")
            {
                LinqDataSource1.TableName = "JSdata";
            }
            else if(Session["table"].ToString() == "HNdata")
            {
                LinqDataSource1.TableName = "HNdata";
            }
            else
            {
                LinqDataSource1.TableName = "ZHdata";
            }
            GridView1.DataBind();
            DetailsView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Session["User"].ToString() != "1")
            {
                Response.Write("<script>alert('你没有权限访问此功能')</script>");
                return;
            }
            GridView1.Visible = false;
            int index = int.Parse(e.CommandArgument.ToString());
            string id = GridView1.Rows[index].Cells[1].Text;
            DetailsView1.Visible = true;
            if (e.CommandName == "info")
            {

                LinqDataSource1.Where = "id=" + id;
                DetailsView1.DataBind();
                DetailsView1.Visible = true;
                Button1.Visible = true;
                //Button1.Visible = true;

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string name = TextBox1.Text.ToString();
            string firm = TextBox2.Text.ToString();
            string zid = TextBox3.Text.ToString();
            
            if(name!="")
                LinqDataSource1.Where = "产品名称=\"" + name + "\"";
            else if(firm!="")
                LinqDataSource1.Where = "生产厂家=\"" + firm + "\"";
            else
                LinqDataSource1.Where= "注册证号=\"" + zid + "\"";
            //SqlConnection myConnection = new SqlConnection(connectionString);
            //myConnection.Open();
            //string selectcommand;
            //if(Session["table"].ToString()=="JSdata")
            //    selectcommand= "select * from JSdata where 产品名称 like '%" + name + "%' and 注册证号 like '%" + zid + "%'";
            //else if(Session["table"].ToString() == "HNdata")
            //    selectcommand = "select * from HNdata where 产品中文名称 like '%" + name + "%' and 产品注册号 like '%" + zid +
            //        "%' and 生产厂家 like '%" + firm + "%'";
            //else
            //    selectcommand = "select * from ZHdata where 产品名称 like '%" + name + "%' and 注册证号 like '%" + zid + 
            //        "%' and 单位名称 like '%"+firm+"%'";
            //SqlCommand myCommand = new SqlCommand(selectcommand, myConnection);
            GridView1.DataBind();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["User"] = "";
            Session["table"] = "";
            Server.Transfer("Login.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GridView1.Visible = true;
            DetailsView1.Visible = false;
        }

        protected void DetailsView1_ItemDeleted(object sender, DetailsViewDeletedEventArgs e)
        {
            Response.Write("<script>alert('删除成功')</script>");
        }
    }
}