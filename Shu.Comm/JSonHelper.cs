using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using Shu.Comm;
using System.Web.Script.Serialization;

    public class JSonHelper
    {

        public static string CreateJson(DataTable table)
        {
            string jsname = "total";
            StringBuilder json = new StringBuilder("{\""+jsname+"\":[");
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    json.Append("{");
                    foreach (DataColumn column in table.Columns)
                    {
                        json.Append("\""+column.ColumnName+"\":\""+row[column.ColumnName].ToString()+"\",");
                    }
                    json.Remove(json.Length - 1, 1);
                    json.Append("},");
                }
                json.Remove(json.Length - 1, 1);
            }
            json.Append("]}");
            return json.ToString();
        }

        public static string CreateJsonParameters(DataTable dt, bool displayCount, int totalcount)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null)
            {
                JsonString.Append("{ ");
                if (displayCount)
                {
                    JsonString.Append("\"total\":");
                    JsonString.Append(totalcount);
                    JsonString.Append(",");
                }
                JsonString.Append("\"rows\":[ ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            //if (dt.Rows[i][j] == DBNull.Value) continue;
                            if (dt.Columns[j].DataType == typeof(bool))
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" +
                                                  dt.Rows[i][j].ToString().ToLower() + ",");
                            }
                            else if (dt.Columns[j].DataType == typeof(string))
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + "\"" +
                                                  dt.Rows[i][j].ToString().Replace("\"", "\\\"") + "\",");
                            }
                            else
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + "\"" + dt.Rows[i][j] + "\",");
                            }
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            //if (dt.Rows[i][j] == DBNull.Value) continue;
                            if (dt.Columns[j].DataType == typeof(bool))
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" +
                                                  dt.Rows[i][j].ToString().ToLower());
                            }
                            else if (dt.Columns[j].DataType == typeof(string))
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + "\"" +
                                                  dt.Rows[i][j].ToString().Replace("\"", "\\\"") + "\"");
                            }
                            else
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + "\"" + dt.Rows[i][j] + "\"");
                            }
                        }
                    }
                    /*end Of String*/
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }
                JsonString.Append("]");
                JsonString.Append("}");
                return JsonString.ToString().Replace("\n", "");
            }
            else
            {
                return null;
            }
        }
        public static string DataTableToJson(DataTable table, string name)
        {
            StringBuilder Json = new StringBuilder("{\""+name+"\":[");
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Json.Append("{");
                    foreach (DataColumn cloumn in table.Columns)
                    {
                        Json.Append("\""+cloumn.ColumnName+"\":\""+row[cloumn.ColumnName].ToString()+"\",");
                    }
                    Json.Remove(Json.Length - 1, 1);
                    Json.Append("},");
                }
                Json.Remove(Json.Length - 1, 1);
            }
            Json.Append("]}");
            return Json.ToString();
        }

        /// <summary>
        /// 带foot的Datagrid数据
        /// </summary>
        /// <param name="dtGrid">Grid数据dt</param>
        /// <param name="dtFoot">Foot数据dt</param>
        /// <param name="total">总数</param>
        /// <returns></returns>
        public static StringBuilder DataGridShowFoot(DataTable dtGrid, DataTable dtFoot, int total = 0)
        {
            StringBuilder strJson = new StringBuilder();
            strJson.Append("{\"total\":");
            if (total == 0)
            {
                total = dtGrid.Rows.Count;
            }
            strJson.Append(total);
            strJson.Append(",");
            strJson.Append("\"rows\":");
            strJson.Append(DataTableToJsonList(dtGrid));
            strJson.Append(",");
            strJson.Append("\"footer\":");
            strJson.Append(DataTableToJsonList(dtFoot));
            strJson.Append("}");
            return strJson;
        }

        public static StringBuilder TableToDataGrid(DataTable dtGrid)
        {
            StringBuilder strJson = new StringBuilder();
            strJson.Append("{\"total\":");
            int total = dtGrid.Rows.Count;
            strJson.Append(total);
            strJson.Append(",");
            strJson.Append("\"rows\":");
            strJson.Append(DataTableToJsonList(dtGrid));
            strJson.Append("}");
            return strJson;
        }

        public static StringBuilder TableToDataGrid(DataTable dtGrid, int total)
        {
            StringBuilder strJson = new StringBuilder();
            strJson.Append("{\"total\":");
            strJson.Append(total);
            strJson.Append(",");
            strJson.Append("\"rows\":");
            strJson.Append(DataTableToJsonList(dtGrid));
            strJson.Append("}");
            return strJson;
        }

        /// <summary> 
        /// DataTable转为json 
        /// </summary> 
        /// <param name="dt">DataTable</param> 
        /// <returns>json数据</returns> 
        public static StringBuilder DataTableToJsonList(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }
            StringBuilder str = new StringBuilder();
            str.Append(SerializeToJson(list));
            return str;
        }

        /// <summary>
        /// 序列化对象为Json字符串
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <param name="recursionLimit">序列化对象的深度，默认为100</param>
        /// <returns>Json字符串</returns>
        public static string SerializeToJson(object obj, int recursionLimit = 100)
        {
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            serialize.RecursionLimit = recursionLimit;
            return serialize.Serialize(obj);
        }
    }
