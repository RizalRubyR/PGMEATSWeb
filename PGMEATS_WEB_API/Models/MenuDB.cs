using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PGMEATS_WEB_API.Models
{
    public class Menu
    {
        public string MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuDescription { get; set; }
        public string MenuGroup { get; set; }
        public int GroupIndex { get; set; }
        public int MenuIndex { get; set; }
        public int AllowAccess { get; set; }
    }

    public class MenuDB
    {
        public IEnumerable<Menu> MenuList(string UserID)
        {
            List<Menu> Menus = new List<Menu>();

            string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("usp_UserMenu_Sel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserID", UserID);
                con.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Menu Menu = new Menu();
                    Menu.MenuID = rd["MenuID"].ToString();
                    Menu.MenuName = rd["MenuName"].ToString();
                    Menu.MenuDescription = rd["MenuDescription"].ToString();
                    Menu.MenuGroup = rd["MenuGroup"].ToString();
                    Menu.GroupIndex = Convert.ToInt16(rd["GroupIndex"]);
                    Menu.MenuIndex = Convert.ToInt16(rd["MenuIndex"]);
                    Menu.AllowAccess = Convert.ToInt16(rd["AllowAccess"]);
                    Menus.Add(Menu);
                }
                return Menus;
            }
        }
    }
}