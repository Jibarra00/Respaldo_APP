﻿using System;
using Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;


namespace Logic
{
    public class ClienteLog
    {
        ClienteDat objCli = new ClienteDat();

        //Metodo para mostrar todas los clientes
        public DataSet showClient()
        {
            return objCli.showClients();     
        }

        //Metodo para mostrar unicamente el id 
        public DataSet showClientDDL()
        {
            return objCli.showClientsDDL();
        }

        //Metodo para guardar un nuevo cliente
        public bool saveClient(string _names, string _lastnames,
            string _mail, string _phone, string _addres)
        {
            return objCli.saveClient(_names, _lastnames,
            _mail, _phone, _addres);
        }

        //Metodo para actualizar un cliente
        public bool updateClient(int c_id, string c_names, string c_lastnames,
            string c_mail, string c_phone, string c_addres)
        {
            return objCli.updateClient(c_id, c_names, c_lastnames,
             c_mail,  c_phone, c_addres);
        }
        //Metodo para borrar un cliente
        public bool deleteClient(int _id)
        {
            return objCli.deleteClient(_id);
        }

    }
}