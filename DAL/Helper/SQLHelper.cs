using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper
{
    public class SQLHelper
    {
        private static string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();

        #region 执行格式化SQL语句

        public static int Update(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //将错误信息写入日志
                WriteLog("执行 public static int Update(string sql)方法时发生异常"+ex.Message);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static object GetSingleResult(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //将错误信息写入日志
                WriteLog("执行 public static object GetSingleResult(string sql) 方法时发生异常" + ex.Message);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        
        public static object GetReader(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                //将错误信息写入日志
                WriteLog("执行 public static object GetReader(string sql) 方法时发生异常" + ex.Message);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region 执行带参数的SQL语句和存储过程

        /// <summary>
        /// 执行带参数的SQL语句或存储过程
        /// </summary>
        /// <param name="sqlOrProcedureName">SQL语句或存储过程名称</param>
        /// <param name="param">参数数组</param>
        /// <param name="isProcedure">是存储过程</param>
        /// <returns></returns>
        public static int Update(string sqlOrProcedureName, SqlParameter[] param,bool isProcedure)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sqlOrProcedureName, conn);
            if (isProcedure)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            try
            {
                conn.Open();
                cmd.Parameters.AddRange(param);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //将错误信息写入日志
                WriteLog("执行 public static int Update(string sqlOrProcedureName, SqlParameter[] param,bool isProcedure) 方法时发生异常" + ex.Message);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static object GetSingleResult(string sqlOrProcedureName, SqlParameter[] param, bool isProcedure)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sqlOrProcedureName, conn);
            if (isProcedure)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            try
            {
                conn.Open();
                cmd.Parameters.AddRange(param);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //将错误信息写入日志
                WriteLog("执行  public static object GetSingleResult(string sqlOrProcedureName, SqlParameter[] param, bool isProcedure) 方法时发生异常" + ex.Message);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        
        public static object GetReader(string sqlOrProcedureName, SqlParameter[] param, bool isProcedure)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sqlOrProcedureName, conn);

            if (isProcedure)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                //将错误信息写入日志
                WriteLog("执行 public static object GetReader(string sqlOrProcedureName, SqlParameter[] param, bool isProcedure) 方法时发生异常" + ex.Message);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region 写入日志

        private static void WriteLog(string msg)
        {
            FileStream fs = new FileStream("projectlog.log", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString() + " " + msg);
            sw.Close();
            fs.Close();
        }

        #endregion
    }
}
