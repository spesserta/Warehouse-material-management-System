using DAL;
using DAL1;
using Models;
using Models1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Util;

namespace BLL
{
    
    public class MaterialManageBLL
    {
        MaterialManageDAL materialManageDAL = new MaterialManageDAL();
        //新增物料
        public bool AddMaterial(Material material)
        {
            try
            {
                return materialManageDAL.AddMaterial(material);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 新增物料失败");
                return false;
            }
        }

        //修改物料
        public bool UpdateMaterial(Material material)
        {
            try
            {
                return materialManageDAL.UpdateMaterial(material);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 修改物料失败");
                return false;
            }
        }

        //删除物料
        public bool DeleteMaterial(Material material)
        {
            try
            {
                return materialManageDAL.DeleteMaterial(material);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 删除物料失败");
                return false;
            }
        }

        //根据物料名称模糊查找
        public DataTable GetMaterialByName(string name)
        {
            try
            {
                return materialManageDAL.GetMaterialByName(name);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 按物料名称查询失败");
                return null;
            }
        }

        //根据物料分类模糊查找
        public DataTable GetMaterialByCategory(string category)
        {
            try
            {
                return materialManageDAL.GetMaterialByCategory(category);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 按物料分类查询失败");
                return null;
            }
        }

        //根据物料编号精确查找
        public DataTable GetMaterialByCode(string code)
        {
            try
            {
                return materialManageDAL.GetMaterialByCode(code);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 按物料编号查询失败");
                return null;
            }
        }

        //查询所有物料
        public DataTable GetAllMaterial()
        {
            try
            {
                return materialManageDAL.GetAllMaterial();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 查询所有物料失败");
                return null;
            }
        }

        //出库入库
        public bool MaterialStockInOut(Material material)
        {
            try
            {
                return materialManageDAL.MaterialStockInOut(material);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 更新库存失败");
                return false;
            }
        }
    }
}
