using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PGMEATS_WEB.Models
{
    public class clsMenuDB
    {
        public IEnumerable<clsMenu> MenuList()
        {
            List<clsMenu> Menus = new List<clsMenu>();

            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_UserMenu_Sel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsMenu Menu = new clsMenu();
                    Menu.MenuID = rd["MenuID"].ToString();
                    Menu.MenuName = rd["MenuName"].ToString();
                    Menu.MenuDescription = rd["MenuDesc"].ToString();
                    Menu.MenuGroup = rd["GroupID"].ToString();
                    Menu.GroupIndex = Convert.ToInt16(rd["GroupIndex"]);
                    Menu.MenuIndex = Convert.ToInt16(rd["MenuIndex"]);
                    Menu.AllowAccess = Convert.ToInt16(rd["ActiveStatus"]);
                    Menus.Add(Menu);
                }
                return Menus;
            }
        }
    }
}