using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Proyecto2Recu
{

    public class BaseDatos
    {
        readonly SQLiteAsyncConnection db;
        public BaseDatos(String dbpath)
        {
            db = new SQLiteAsyncConnection(dbpath);
            db.CreateTableAsync<Picture>().Wait();
        }
    }
}
