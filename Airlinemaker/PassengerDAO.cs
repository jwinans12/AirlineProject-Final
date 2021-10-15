using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlinemaker.Data
{
    public class PassengerDAO : IPassengerDAO
    {

        private string connString = "Data Source=DESKTOP-BTOOPNK;Initial Catalog=Airliner;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void DeletePassenger(Passenger passenger)
        {
            //Passenger passenger = new Passenger();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(@"Delete from dbo.Passengers where Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", passenger.Id);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not delete Passenger!\n{0}", ex.Message);
                }finally
                {
                    conn.Close();
                }
            }
        }

        /*public void EditPassenger(Passenger passenger)
        {
            int id = 0;
            Passenger temp = passenger;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd0 = new SqlCommand(@"Delete from dbo.Passengers where Id = @Id", conn);
                cmd0.Parameters.AddWithValue("@Id", temp.Id);
                SqlCommand cmd1 = new SqlCommand(@"Insert into dbo.Passengers (Name, Job, Email, Age, FlightNumber) output INSERTED.Id values (@Name, @Job, @Email, @Age, @FlightNumber)", conn);
                cmd1.Parameters.AddWithValue("@Name", passenger.Name);
                cmd1.Parameters.AddWithValue("@Job", passenger.Job);
                cmd1.Parameters.AddWithValue("@Email", passenger.Email);
                cmd1.Parameters.AddWithValue("@Age", passenger.Age);
                cmd1.Parameters.AddWithValue("@FlightNumber", passenger.FlightNumber);

                try
                {
                    conn.Open();
                    cmd0.ExecuteNonQuery();
                    id = (int)cmd1.ExecuteScalar();
                    passenger.Id = id;
                }catch (SqlException ex)
                {
                    Console.WriteLine("Could not update passenger\n{0}", ex.Message);
                }finally
                {
                    conn.Close();
                }
            }
        }*/

        public void EditPassenger(Passenger passenger)
        {
            

            using (SqlConnection conn = new SqlConnection(connString))
            {
                
                SqlCommand cmd1 = new SqlCommand(@"UPDATE dbo.Passengers set Name=@Name, Job=@Job, Email=@Email, Age=@Age, FlightNumber=@FlightNumber where Id=@Id", conn);
                cmd1.Parameters.AddWithValue("@Name", passenger.Name);
                cmd1.Parameters.AddWithValue("@Job", passenger.Job);
                cmd1.Parameters.AddWithValue("@Email", passenger.Email);
                cmd1.Parameters.AddWithValue("@Age", passenger.Age);
                cmd1.Parameters.AddWithValue("@FlightNumber", passenger.FlightNumber);
                cmd1.Parameters.AddWithValue("@Id", passenger.Id);

                try
                {
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not update passenger\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void AddPassenger(Passenger passenger)
        {
            int id = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(@"Insert into dbo.Passengers (Name, Job, Email, Age, FlightNumber) output INSERTED.Id values (@Name, @Job, @Email, @Age, @FlightNumber)", conn);
                cmd.Parameters.AddWithValue("@Name", passenger.Name);
                cmd.Parameters.AddWithValue("@Job", passenger.Job);
                cmd.Parameters.AddWithValue("@Email", passenger.Email);
                cmd.Parameters.AddWithValue("@Age", passenger.Age);
                cmd.Parameters.AddWithValue("@FlightNumber", passenger.FlightNumber);

                try
                {
                    conn.Open();
                    //cmd.ExecuteNonQuery();
                    id = (int)cmd.ExecuteScalar();
                    passenger.Id = id;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not add Passenger!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

        }


        public IEnumerable<Passenger> GetPassengers()
        {
            List<Passenger> passengerList = new List<Passenger>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * from dbo.Passengers;", conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Passenger temp = new Passenger(reader["Name"].ToString(), reader["Job"].ToString(), reader["Email"].ToString(), int.Parse(reader["Age"].ToString()), int.Parse(reader["FlightNumber"].ToString()));
                        temp.Id = Convert.ToInt32(reader["Id"]);
                        //temp.ConfirmNumber = Convert.ToInt32(reader["ConfirmNumber"]);
                        passengerList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all passengers!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return passengerList;
        }

        public IEnumerable<Passenger> GetPassengersByFlight(int flightNumber)
        {
            List<Passenger> passengerList = new List<Passenger>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * from dbo.Passengers where FlightNumber=@FlightNumber;", conn);
                cmd.Parameters.AddWithValue("@FlightNumber", flightNumber);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Passenger temp = new Passenger(reader["Name"].ToString(), reader["Job"].ToString(), reader["Email"].ToString(), int.Parse(reader["Age"].ToString()), int.Parse(reader["FlightNumber"].ToString()));
                        temp.Id = Convert.ToInt32(reader["Id"]);
                        //temp.ConfirmNumber = Convert.ToInt32(reader["ConfirmNumber"]);
                        passengerList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all passengers!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return passengerList;
        }

        public Passenger GetPassenger(int id)
        {
            Passenger passenger = new Passenger();

            string query = "Select * from dbo.Passengers where Id = @Id";

            using (SqlConnection conn = new SqlConnection(connString))
            {

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Passenger temp = new Passenger(reader["Name"].ToString(), reader["Job"].ToString(), reader["Email"].ToString(), int.Parse(reader["Age"].ToString()), int.Parse(reader["FlightNumber"].ToString()));
                        temp.Id = Convert.ToInt32(reader["Id"]);


                        passenger = temp;
                    }
                }
                catch (SqlException ex)
                {
                    Console.Write("This passenger does not exist\n{0}", ex.Message);
                }
            }
            return passenger;
        }

        public DataTable GetTable()
        {
            DataTable dt = new DataTable();

            string query = "Select * from dbo.Passengers";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    conn.Open();
                    int ret = adapter.Fill(dt);

                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error could not fill the data table!\n{0}", ex.Message);
                }
            }
            return dt;
        }
    }
}
