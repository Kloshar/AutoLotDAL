//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Drawing;

namespace AutoLotConnectedLayer
{
    public class InventoryDAl //DAL id Data Access Layer
    {
        private SqlConnection sqlCn = null;
        public void OpenConnection(string connectionString)
        {
            sqlCn = new SqlConnection(connectionString);
            sqlCn.Open();
        }
        public void CloseConnection()
        {
            sqlCn.Close();
        }
        public void InsertAuto(int id, string make, string color, string petName)
        {
            string sql = string.Format("insert into inventory" + "(CarID, Make, Color, PetName)" + $"'{id}', '{make}', '{color}', '{petName}'");
            using (SqlCommand cmd = new SqlCommand(sql, sqlCn)) cmd.ExecuteNonQuery();
        }
        public void InsertAuto(NewCar car)
        {
            string sql = string.Format("insert into inventory" + "(CarID, Make, Color, PetName)" + $"'{car.CarID}', '{car.Make}', '{car.Color}', '{car.PetName}'");
            using (SqlCommand cmd = new SqlCommand(sql, sqlCn)) cmd.ExecuteNonQuery();
        }
        public void DeleteCar(int id)
        {


        }
    }
    public class NewCar
    {
        public int CarID {  get; set; }
        public string Make { get; set; }
        public string Color { get; set; }
        public string PetName { get; set; }
    }
}
