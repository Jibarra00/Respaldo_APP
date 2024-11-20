﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class GestionDat
    {
        Persistence objPer = new Persistence();

        //Metodo para mostrar todas las gestiones
        public DataSet showManagement()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectManagement";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        //Metodo para mostrar unicamente por ID
        public DataSet showManagementDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectManagementDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        //Metodo para guardar una nueva gestion
        public bool saveManagement(string p_ges_descripcion, DateTime p_ges_fecha, int _fkemployee, int _fkproduct)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertManagement"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("g_fecha", MySqlDbType.DateTime).Value = p_ges_fecha;
            objSelectCmd.Parameters.Add("g_descripcion", MySqlDbType.VarString).Value = p_ges_descripcion;
            objSelectCmd.Parameters.Add("tbl_empleado_emp_id", MySqlDbType.Int32).Value = _fkemployee;
            objSelectCmd.Parameters.Add("tbl_producto_pro_id", MySqlDbType.Int32).Value = _fkproduct;



            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;

        }

        //Metodo para actualizar una gestion
        public bool updateManagement(int p_ges_id, DateTime p_ges_fecha, string p_ges_descripcion, int _fkemployee, int _fkproduct)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateManagement"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Ajusta los nombres de los parámetros para que coincidan con el procedimiento almacenado
            objSelectCmd.Parameters.Add("tbl_empleado_emp_id", MySqlDbType.Int32).Value = _fkemployee;
            objSelectCmd.Parameters.Add("tbl_producto_pro_id", MySqlDbType.Int32).Value = _fkproduct;
            objSelectCmd.Parameters.Add("g_id", MySqlDbType.Int32).Value = p_ges_id;
            objSelectCmd.Parameters.Add("g_fecha", MySqlDbType.DateTime).Value = p_ges_fecha;
            objSelectCmd.Parameters.Add("g_descripcion", MySqlDbType.VarString).Value = p_ges_descripcion;

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }


        //Metodo para borrar una gestion
        public bool deleteManagement(int _idManagement)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteManagement"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("g_id", MySqlDbType.Int32).Value = _idManagement;

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;

        }
    }
}