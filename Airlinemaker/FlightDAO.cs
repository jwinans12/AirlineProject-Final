using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlinemaker.Data
{
    public class FlightDAO : IFlightDAO
    {
        private string connString = "Data Source=DESKTOP-BTOOPNK;Initial Catalog=Airliner;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /*public void EditFlight(Flight flight)
        {
            int id = 0;
            Flight temp = flight;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd0 = new SqlCommand(@"Delete from dbo.Flights where Id = @Id", conn);
                cmd0.Parameters.AddWithValue("@Id", temp.Id);
                SqlCommand cmd1 = new SqlCommand(@"Insert into dbo.Flights (DepartLoc, ArriveLoc, DepartDate, ArriveDate, DepartTime, ArriveTime, PassengerLimit) output INSERTED.Id values (@DepartLoc, @ArriveLoc, @DepartDate, @ArriveDate, @DepartTime, @ArriveTime, @PassengerLimit)", conn);
                cmd1.Parameters.AddWithValue("@DepartLoc", flight.DepartLoc);
                cmd1.Parameters.AddWithValue("@ArriveLoc", flight.ArriveLoc);
                cmd1.Parameters.AddWithValue("@DepartDate", flight.DepartDate);
                cmd1.Parameters.AddWithValue("@ArriveDate", flight.ArriveDate);
                cmd1.Parameters.AddWithValue("@DepartTime", flight.DepartTime);
                cmd1.Parameters.AddWithValue("@ArriveTime", flight.ArriveTime);
                cmd1.Parameters.AddWithValue("@PassengerLimit", flight.PassengerLimit);

                try
                {
                    conn.Open();
                    cmd0.ExecuteNonQuery();
                    id = (int)cmd1.ExecuteScalar();
                    flight.Id = id;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not update flight\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }*/

        public void EditFlight(Flight flight)
        {
            
            using (SqlConnection conn = new SqlConnection(connString))
            {
                
                SqlCommand cmd1 = new SqlCommand(@"UPDATE dbo.Flights set DepartLoc=@DepartLoc, ArriveLoc=@ArriveLoc, DepartDate=@DepartDate, ArriveDate=@ArriveDate, DepartTime=@DepartTime, ArriveTime=@ArriveTime, PassengerLimit=@PassengerLimit where Id=@Id)", conn);
                cmd1.Parameters.AddWithValue("@DepartLoc", flight.DepartLoc);
                cmd1.Parameters.AddWithValue("@ArriveLoc", flight.ArriveLoc);
                cmd1.Parameters.AddWithValue("@DepartDate", flight.DepartDate);
                cmd1.Parameters.AddWithValue("@ArriveDate", flight.ArriveDate);
                cmd1.Parameters.AddWithValue("@DepartTime", flight.DepartTime);
                cmd1.Parameters.AddWithValue("@ArriveTime", flight.ArriveTime);
                cmd1.Parameters.AddWithValue("@PassengerLimit", flight.PassengerLimit);
                cmd1.Parameters.AddWithValue("@Id", flight.Id);

                try
                {
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not update flight\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeleteFlight(Flight flight)
        {
            

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(@"Delete from dbo.Flights where Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", flight.Id);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not delete Passenger!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void AddFlight(Flight flight)
        {
            int id = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //SqlCommand cmd = new SqlCommand("Insert into dbo.Flights (DepartLoc, ArriveLoc, DepartDate, ArriveDate, DepartTime, ArriveTime, PassengerLimit) values (@DepartLoc, @ArriveLoc, @DepartDate, @ArriveDate, @DepartTime, @ArriveTime, @PassengerLimit)", conn);
                SqlCommand cmd = new SqlCommand(@"Insert into dbo.Flights (DepartLoc, ArriveLoc, DepartDate, ArriveDate, DepartTime, ArriveTime, PassengerLimit) output INSERTED.Id values (@DepartLoc, @ArriveLoc, @DepartDate, @ArriveDate, @DepartTime, @ArriveTime, @PassengerLimit)", conn);
                cmd.Parameters.AddWithValue("@DepartLoc", flight.DepartLoc);
                cmd.Parameters.AddWithValue("@ArriveLoc", flight.ArriveLoc);
                cmd.Parameters.AddWithValue("@DepartDate", flight.DepartDate);
                cmd.Parameters.AddWithValue("@ArriveDate", flight.ArriveDate);
                cmd.Parameters.AddWithValue("@DepartTime", flight.DepartTime);
                cmd.Parameters.AddWithValue("@ArriveTime", flight.ArriveTime);
                cmd.Parameters.AddWithValue("@PassengerLimit", flight.PassengerLimit);

                try
                {
                    conn.Open();
                    //cmd.ExecuteNonQuery();
                    id = (int)cmd.ExecuteScalar();
                    flight.Id = id;
                } catch (SqlException ex)
                {
                    Console.WriteLine("Could not add Flight!\n{0}", ex.Message);
                }finally
                {
                    conn.Close();
                }
            }

           //return id;
        }

        public Flight GetFlight(int id)
        {
            Flight flight = new Flight();

            string query = "Select * from dbo.Flights where Id = @Id";

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
                        Flight temp = new Flight(reader["DepartLoc"].ToString(), reader["ArriveLoc"].ToString(), reader["DepartDate"].ToString(), reader["ArriveDate"].ToString(), reader["DepartTime"].ToString(), reader["ArriveTime"].ToString(), int.Parse(reader["PassengerLimit"].ToString()));
                        temp.Id = Convert.ToInt32(reader["Id"]);
                        //temp.PassengerLimit = Convert.ToInt32(reader["PassengerLimit"]);

                        flight = temp;
                    }
                }
                catch (SqlException ex)
                {
                    Console.Write("This home does not exist\n{0}", ex.Message);
                }
            }
            return flight;
        }

        public IEnumerable<Flight> GetFlights()
        {
            List<Flight> flightList = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(connString)) {
                SqlCommand cmd = new SqlCommand("SELECT * from dbo.Flights;", conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        
                        Flight temp = new Flight(reader["DepartLoc"].ToString(), reader["ArriveLoc"].ToString(), reader["DepartDate"].ToString(), reader["ArriveDate"].ToString(), reader["DepartTime"].ToString(), reader["ArriveTime"].ToString(), int.Parse(reader["PassengerLimit"].ToString()));
                        temp.Id = Convert.ToInt32(reader["Id"]);
                        flightList.Add(temp);
                    }
                }catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all flights!\n{0}", ex.Message);
                }finally
                {
                    conn.Close();
                }
            }
            return flightList;
        }

        
        public DataTable GetTable()
        {
            DataTable dt = new DataTable();

            string query = "Select * from dbo.Flights";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    conn.Open();
                    int ret = adapter.Fill(dt);

                } catch (SqlException ex)
                {
                    Console.WriteLine("Error could not fill the data table!\n{0}", ex.Message);
                }
            }
            return dt;
        }
    }
}
