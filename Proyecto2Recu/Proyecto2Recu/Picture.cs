using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Proyecto2Recu
{
    public class Picture
    {
        [AutoIncrement, PrimaryKey]
        public int id { set; get; }

        public string Name { get; set; } 

        public string Desc { get; set; }

        public byte[] Imagen { get; set; }
    }
  
}
