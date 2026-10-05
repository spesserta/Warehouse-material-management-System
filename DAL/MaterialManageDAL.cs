using Models1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DAL;
using Util;

namespace DAL1
{
    public class MaterialManageDAL
    {
        //新增物料
        public bool AddMaterial(Material material)
        {
            try
            {
                string sqlStr = @"insert into MaterialDB(MaterialCode , MaterialName , Specification , Unit , CurrentStock , MinStock , MaxStock  , MaterialDescription , CategoryId) 
                            values (@MaterialCode,@MaterialName,@Specification,@Unit,@CurrentStock,@MinStock,@MaxStock,@MaterialDescription , @CategoryId)";

                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@MaterialCode", material.MaterialCode),
                    new SqlParameter("@MaterialName", material.MaterialName),
                    new SqlParameter("@Specification", material.Specification),
                    new SqlParameter("@Unit", material.Unit),
                    new SqlParameter("@CurrentStock", material.CurrentStock),
                    new SqlParameter("@MinStock", material.MinStock),
                    new SqlParameter("@MaxStock", material.MaxStock),
                    new SqlParameter("@MaterialDescription",material.MaterialDescription ),
                    new SqlParameter("@CategoryId",material.CategoryId )
                };
                return DBHelper.ExcuteCommand(sqlStr, param);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 新增物料失败");
                return false;
            }
        }



        //修改物料
        public bool UpdateMaterial(Material material)
        {
            try
            {
                string sqlStr = @"update MaterialDB 
                              set MaterialCode = @MaterialCode , MaterialName = @MaterialName , Specification = @Specification , Unit = @Unit ,CurrentStock = @CurrentStock ,
                                  MinStock = @MinStock , MaxStock = @MaxStock , MaterialDescription = @MaterialDescription , CategoryId=@CategoryId
                              where MaterialCode = @MaterialCode";

                SqlParameter[] param = new SqlParameter[] 
                {
                    new SqlParameter("@MaterialCode",material.MaterialCode),
                    new SqlParameter("@MaterialName", material.MaterialName),
                    new SqlParameter("@Specification", material.Specification),
                    new SqlParameter("@Unit", material.Unit),
                    new SqlParameter("@CurrentStock", material.CurrentStock),
                    new SqlParameter("@MinStock", material.MinStock),
                    new SqlParameter("@MaxStock", material.MaxStock),
                    new SqlParameter("@MaterialDescription",material.MaterialDescription ),
                    new SqlParameter("@CategoryId",material.CategoryId )
                };
                return DBHelper.ExcuteCommand(sqlStr,param);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 修改物料失败");
                return false;
            }
        }



        //删除物料
        public bool DeleteMaterial(Material material)
        {
            try
            {
                string str = "delete from MaterialDB where  MaterialCode = @MaterialCode";

                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@MaterialCode",material.MaterialCode)
                };
                return DBHelper.ExcuteCommand(str, param);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 删除物料失败");
                return false;
            }
        }



        //根据物料名称模糊查找
        public DataTable GetMaterialByName(string name)
        {
            try
            {
                string strsql = $"select * from MaterialDB where MaterialName like N'%{name}%'";
                SqlDataAdapter da = new SqlDataAdapter(strsql, DBHelper.connString);
                DataSet dt = new DataSet();
                da.Fill(dt);
                return dt.Tables[0];
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 按物料名称查询失败");
                return null;
            }
        }


        //根据物料分类模糊查找
        public DataTable GetMaterialByCategory(string category)
        {
            try
            {
                string strsql = $"select * from MaterialDB where MaterialName like N'%{category}%'";
                SqlDataAdapter da = new SqlDataAdapter(strsql, DBHelper.connString);
                DataSet dt = new DataSet();
                da.Fill(dt);
                return dt.Tables[0];
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 按物料分类查询失败");
                return null;
            }
        }

        //根据物料编号精确查找
        public DataTable GetMaterialByCode(string code)
        {
            try
            {
                string strsql = $"select * from MaterialDB where MaterialName like N'%{code}%'";
                SqlDataAdapter da = new SqlDataAdapter(strsql, DBHelper.connString);
                DataSet dt = new DataSet();
                da.Fill(dt);
                return dt.Tables[0];
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 按物料编号查询失败");
                return null;
            }
        }

        //查询所有物料
        public DataTable GetAllMaterial()
        {
            try
            {
                string strsql = "select * from MaterialDB";
                SqlDataAdapter da = new SqlDataAdapter(strsql ,DBHelper.connString);
                DataSet dt = new DataSet();
                da.Fill(dt);
                return dt.Tables[0];
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 查询所有物料失败");
                return null;
            }
        }


        //出库/入库 修改库存
        public bool MaterialStockInOut(Material material)
        {
            try
            {
                string sqlStr = "update MaterialDB set CurrentStock = @CurrentStock where MaterialCode = @MaterialCode";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@CurrentStock",material.CurrentStock),
                    new SqlParameter("@MaterialCode",material.MaterialCode)
                };
                return DBHelper.ExcuteCommand(sqlStr, param);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 更新库存失败");
                return false;
            }
        }


    }
}
