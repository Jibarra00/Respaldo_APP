﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class EmpleadoDat
    {
        Persistence objPer = new Persistence();

        //Metodo para mostrar todos los Empleados
        public DataSet showEmployees()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectEmployees";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        //Metodo para mostrar unicamente el id y la descripcion de los Empleados
        public DataSet showEmployeesDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectEmployeesDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        //Metodo para guardar un nuevo Empleado
        public bool saveEmployee(string e_identification, string e_names,
     string e_lastnames, string e_phone, string e_addres)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertEmployee"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("e_identification", MySqlDbType.VarString).Value = e_identification;
            objSelectCmd.Parameters.Add("e_names", MySqlDbType.VarString).Value = e_names;
            objSelectCmd.Parameters.Add("e_lastnames", MySqlDbType.VarString).Value = e_lastnames;
            objSelectCmd.Parameters.Add("e_phone", MySqlDbType.VarString).Value = e_phone;
            objSelectCmd.Parameters.Add("e_addres", MySqlDbType.VarString).Value = e_addres;

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


        //Metodo para actualizar un Empleado
        // Metodo para actualizar un Empleado
        public bool updateEmployee(int e_id, string e_identification, string e_names,
            string e_lastnames, string e_phone, string e_addres)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateEmployee"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Asegúrate de que los nombres coincidan con los del procedimiento almacenado
            objSelectCmd.Parameters.Add("e_id", MySqlDbType.Int32).Value = e_id;
            objSelectCmd.Parameters.Add("e_identification", MySqlDbType.VarString).Value = e_identification;
            objSelectCmd.Parameters.Add("e_names", MySqlDbType.VarString).Value = e_names;
            objSelectCmd.Parameters.Add("e_lastnames", MySqlDbType.VarString).Value = e_lastnames;
            objSelectCmd.Parameters.Add("e_phone", MySqlDbType.VarString).Value = e_phone;
            objSelectCmd.Parameters.Add("e_addres", MySqlDbType.VarString).Value = e_addres;

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.ToString());
            }
            objPer.closeConnection();
            return executed;
        }


        //Metodo para borrar una Empleado
        public bool deleteEmployee(int e_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteEmployee"; // nombre correcto del procedimiento
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Asegúrate de que el nombre del parámetro coincida exactamente con el definido en el SP
            objSelectCmd.Parameters.Add("e_id", MySqlDbType.Int32).Value = e_id;

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.ToString());
            }
            objPer.closeConnection();
            return executed;
        }

    }
}