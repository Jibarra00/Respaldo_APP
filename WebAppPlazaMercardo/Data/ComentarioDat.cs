﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class ComentarioDat
    {
        Persistence objPer = new Persistence();

        //Metodo para mostrar todas los comentarios
        public DataSet showComment()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectComent";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        //Metodo para mostrar unicamente el id
        public DataSet showCommentDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCommentDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        //Metodo para guardar un nuevo comentario
        public bool saveComment(string p_com_text, DateTime p_com_fecha, int p_com_clasificacion, int _fkclient, int _fkproduct)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertComment"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_com_text", MySqlDbType.VarString).Value = p_com_text;
            objSelectCmd.Parameters.Add("p_com_fecha", MySqlDbType.DateTime).Value = p_com_fecha;
            objSelectCmd.Parameters.Add("p_com_clasificacion", MySqlDbType.Int32).Value = p_com_clasificacion;
            objSelectCmd.Parameters.Add("p_tbl_cliente_cli_id", MySqlDbType.Int32).Value = _fkclient;
            objSelectCmd.Parameters.Add("p_tbl_producto_pro_id", MySqlDbType.Int32).Value = _fkproduct;


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

        //Metodo para actualizar una Comentario
        public bool updateComment(int p_com_id, int p_com_clasificacion, string p_com_text, DateTime p_com_fecha, int _fkclient, int _fkproduct)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateComent"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("c_id", MySqlDbType.Int32).Value = p_com_id;
            objSelectCmd.Parameters.Add("c_clasificacion", MySqlDbType.Int32).Value = p_com_clasificacion;
            objSelectCmd.Parameters.Add("c_text", MySqlDbType.VarString).Value = p_com_text;
            objSelectCmd.Parameters.Add("c_fecha", MySqlDbType.DateTime).Value = p_com_fecha;
            objSelectCmd.Parameters.Add("_fkclient", MySqlDbType.Double).Value = _fkclient;
            objSelectCmd.Parameters.Add("_fkproduct", MySqlDbType.Int32).Value = _fkproduct;
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

        //Metodo para borrar una Comentario
        public bool deleteComment(int p_com_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteComent"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("c_id", MySqlDbType.Int32).Value = p_com_id;

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