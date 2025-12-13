//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
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
            string sql = string.Format($"delete from inventory where CarId = '{id}'");
            using (SqlCommand cmd = new SqlCommand(sql, sqlCn))
            {
                try { cmd.ExecuteNonQuery(); }
                catch (SqlException ex)
                {
                    Exception error = new Exception("Sorry! That car is on order!", ex);
                    throw error;
                }
            }
        }
        public void UpdateCarPetName(int id, string newPetName)
        {
            string sql = string.Format($"update inventory set PetName = '{newPetName}' where CarId = '{id}'");
            using (SqlCommand cmd = new SqlCommand(sql, sqlCn)) cmd.ExecuteNonQuery();
        }
        public List<NewCar> GetAllInventoryAsList()
        {
            List<NewCar> inv = new List<NewCar>();
            string sql = $"select * from inventory";
            using(SqlCommand cmd = new SqlCommand(sql, sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) inv.Add(new NewCar 
                { CarID = (int)dr["CarId"], Color = (string)dr["Color"], Make = (string)dr["Make"], PetName = (string)dr["PetName"] });
                dr.Close();
            }
            return inv;
        }
        public DataTable GetAllInventoryAsDataTable()
        {
            DataTable inv = new DataTable();
            string sql = $"select * fron inventory";
            using (SqlCommand cmd = new SqlCommand(sql, sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                inv.Load(dr);
                dr.Close();
            }

            return inv;
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
