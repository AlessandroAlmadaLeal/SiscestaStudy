using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Siscesta.Model
{
    public class Marca : ClasseBase
    {
        public string Descricao { get; set; }
        public List<Modelo> Modelo { get; set; }

        MySqlCommand cmd = new MySqlCommand();
        Conexao conn = new Conexao();

        public bool Add()
        {
            cmd.CommandText = $"INSERT INTO `marca` (`Descricao`) " +
                $"VALUES ('{this.Descricao}')";

            try
            {
                cmd.Connection = conn.connect();
                cmd.ExecuteNonQuery();
                conn.disconnect();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu o seguinte erro: {ex.Message}");
                return false;
            }
        }
    }


}
