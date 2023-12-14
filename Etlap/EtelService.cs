using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etlap
{
    internal static class EtelService
    {
        static MySqlConnection connection = new MySqlConnection("server=localhost;user=root;database=etlapdb;port=3306;password=root");

        public static List<Etel> GetEtlap()
        {
            List<Etel> etlap = new List<Etel>();

            OpenConnection();

            MySqlCommand command = new MySqlCommand("SELECT * FROM etlap", connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Etel etel = new Etel();

                etel.Id = reader.GetInt32("id");
                etel.Nev = reader.GetString("nev");
                etel.Leiras = reader.GetString("leiras");
                etel.Kategoria = reader.GetString("kategoria");
                etel.Ar = reader.GetInt32("ar");

                etlap.Add(etel);
            }

            CloseConnection();

            return etlap;
        }

        public static bool CreateEtel(Etel etel)
        {
            OpenConnection();

            MySqlCommand command = new MySqlCommand("INSERT INTO etlap (nev, leiras, ar, kategoria) VALUES (@nev, @leiras, @ar, @kategoria)", connection);

            command.Parameters.AddWithValue("@nev", etel.Nev);
            command.Parameters.AddWithValue("@leiras", etel.Leiras);
            command.Parameters.AddWithValue("@ar", etel.Ar);
            command.Parameters.AddWithValue("@kategoria", etel.Kategoria);

            bool success = command.ExecuteNonQuery() == 1;

            CloseConnection();

            return success;
        }

        public static bool DeleteEtel(int id)
        {
            OpenConnection();

            MySqlCommand command = new MySqlCommand("DELETE FROM etlap WHERE id = @id", connection);

            command.Parameters.AddWithValue("@id", id);

            bool success = command.ExecuteNonQuery() == 1;

            CloseConnection();

            return success;
        }

        public static bool SzazalekosEmeles(int szazalek)
        {
            OpenConnection();

            MySqlCommand command = new MySqlCommand("UPDATE etlap SET ar = ar + ar * @szazalek / 100", connection);

            command.Parameters.AddWithValue("@szazalek", szazalek);

            bool success = command.ExecuteNonQuery() > 0;

            CloseConnection();

            return success;
        }

        public static bool SzazalekosEmeles(int id, int szazalek)
        {
            OpenConnection();

            MySqlCommand command = new MySqlCommand("UPDATE etlap SET ar = ar + ar * @szazalek / 100 WHERE id = @id ", connection);

            command.Parameters.AddWithValue("@szazalek", szazalek);
            command.Parameters.AddWithValue("@id", id);

            bool success = command.ExecuteNonQuery() == 1;

            CloseConnection();

            return success;
        }

        public static bool FixEmeles(int osszeg)
        {
            OpenConnection();

            MySqlCommand command = new MySqlCommand("UPDATE etlap SET ar = ar + @osszeg", connection);

            command.Parameters.AddWithValue("@osszeg", osszeg);

            bool success = command.ExecuteNonQuery() > 0;

            CloseConnection();

            return success;
        }

        public static bool FixEmeles(int id, int osszeg)
        {
            OpenConnection();

            MySqlCommand command = new MySqlCommand("UPDATE etlap SET ar = ar + @osszeg WHERE id = @id", connection);

            command.Parameters.AddWithValue("@osszeg", osszeg);
            command.Parameters.AddWithValue("@id", id);

            bool success = command.ExecuteNonQuery() == 1;

            CloseConnection();

            return success;
        }

        // Open and close connection
        static private void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }
        static private void CloseConnection()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}
