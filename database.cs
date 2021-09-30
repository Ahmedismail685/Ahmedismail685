using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace SuperMarket
{
    public static class database
    {
        public static void setup()
        {
            try
            {
                con = new MySqlConnection("server=localhost;user id=root;password=Maxahmed19;database=supermarket;Convert Zero Datetime=True;");
                cmd = new MySqlCommand("", con);
                opencon();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public static MySqlConnection con;
        public static MySqlCommand cmd;


        public static void opencon()
        {
            try { if (con.State == ConnectionState.Closed) con.Open(); } catch (Exception ex) { MessageBox.Show(ex.Message + "Open con"); }
        }
        public static void add_withvalue(string Name, object value)
        {
            try
            {
                cmd.Parameters.AddWithValue(Name, value);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public static void cmd_execute()
        {
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public static void set_to_add(string oparation)
        {
            try
            {
                if (oparation == "sale")
                {
                    cmd.CommandText = "insert into sales (`name`,`quantity`,`amount`,`the_net_balance`,`date_time`)values(@name,@quantity,@amount,@the_net_balance,@date_time)";
                }

                else if (oparation == "order")
                {
                    cmd.CommandText = "insert into orders (`name`,`quantity`,`amount`,`the_net_balance`,`date_time`)values(@name,@quantity,@amount,@the_net_balance,@date_time)";
                }
                else if (oparation == "store")
                {
                    cmd.CommandText = "insert into store (`name`,`price`,`quantity`,`amount`)values(@name,@price,@quantity,@amount)";
                }
                else if (oparation == "account")
                {
                    cmd.CommandText = "insert into account (`date`,`net_per_day`,`daily_net_sales`,`net_daily_import`)values(@date,@net_per_day,@daily_net_sales,@net_daily_import)";
                }
                else if (oparation == "informations")
                {
                    cmd.CommandText = "insert into informations (`the_net_balance`,`date`)values(@the_net_balance,@date)";
                }
            }
            catch (Exception ex) { cmd.Parameters.Clear(); MessageBox.Show(ex.Message); }
        }

        public static MySqlDataReader load_sales()
        {
            try { cmd.CommandText = "select * from sales"; return cmd.ExecuteReader(); } catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
        }
        public static string get_item(string tabel, string condetion, string column_name)
        {
            try { cmd.CommandText = "select * from " + tabel + condetion + " ;"; DataTable dt = new DataTable(); dt.Load(cmd.ExecuteReader()); if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "") return dt.Rows[0][column_name].ToString(); else return "0"; } catch (Exception ex) { MessageBox.Show(ex.Message + "    get_item"); return "0"; }
        }
        public static string get_max(string tabel, string column)
        {
            try { cmd.CommandText = "SELECT max(" + column + ") FROM " + tabel + ";"; DataTable dt = new DataTable(); dt.Load(cmd.ExecuteReader()); if (dt.Rows[0][0].ToString() != "") return dt.Rows[0][0].ToString(); else return "1000-10-10"; } catch (Exception ex) { MessageBox.Show(ex.Message + "    get_max"); return "1000-10-10"; }
        }
        static DataTable dt = new DataTable();
        public static string the_net_balance()
        {
            try { database.cmd.CommandText = "select the_net_balance from informations where date = (select max(date) from informations)"; dt.Load(database.cmd.ExecuteReader()); return dt.Rows[0][0].ToString(); } catch (Exception ex) { MessageBox.Show(ex.Message); return "0"; }
        }
        public static void up_date_item(string tabel, string columnchaing, string columnching_to, string column, string condetion)
        {
            try { cmd.CommandText = "update " + tabel + " set " + columnchaing + " = " + columnching_to + " where " + column + " = '" + condetion + "'"; cmd_execute(); } catch (Exception ex) { MessageBox.Show(ex.Message + "    up_date_item"); }
        }
        public static void delete_item(string tabel, string column, string condetion)
        {
            try { cmd.CommandText = "delete from " + tabel + " where " + column + " = '" + condetion + "'"; cmd_execute(); } catch (Exception ex) { MessageBox.Show(ex.Message + "    delete_item"); }
        }
        public static MySqlDataReader load_orders() { try { cmd.CommandText = "select * from orders"; return cmd.ExecuteReader(); } catch (Exception ex) { MessageBox.Show(ex.Message); return null; } }

        public static MySqlDataReader load_store() { try { cmd.CommandText = "select * from store"; return cmd.ExecuteReader(); } catch (Exception ex) { MessageBox.Show(ex.Message); return null; } }

        public static MySqlDataReader load_account() { try { cmd.CommandText = "select * from account"; return cmd.ExecuteReader(); } catch (Exception ex) { MessageBox.Show(ex.Message + "Load_account"); return null; } }
    }
}