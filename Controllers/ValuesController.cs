using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StoredProceduresWithWebAPI.Models;

namespace StoredProceduresWithWebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString);
        Employee emp = new Employee();

        // GET api/values
        public List<Employee> Get()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("usp_GetAllEmployees", _connection);
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<Employee> empList = new List<Employee>();
            if(dataTable.Rows.Count > 0)
            {
                for(int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Employee emp = new Employee();
                    emp.Name = dataTable.Rows[i]["Name"].ToString();
                    emp.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                    emp.Age = Convert.ToInt32(dataTable.Rows[i]["Age"]);
                    emp.Active = Convert.ToInt32(dataTable.Rows[i]["Active"]);
                    empList.Add(emp);
                }
            }
            if(empList.Count > 0)
            {
                return empList;
            }
            else
            {
                return null;
            }
        }

        // GET api/values/5
        public Employee Get(int id)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("usp_GetEmployeeById", _connection);
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Id", id);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            Employee emp = new Employee();
            //List<Employee> empList = new List<Employee>();
            if (dataTable.Rows.Count > 0)
            {
                //for (int i = 0; i < dataTable.Rows.Count; i++)
                //{
                    //Employee emp = new Employee();
                    emp.Name = dataTable.Rows[0]["Name"].ToString();
                    emp.Id = Convert.ToInt32(dataTable.Rows[0]["Id"]);
                    emp.Age = Convert.ToInt32(dataTable.Rows[0]["Age"]);
                    emp.Active = Convert.ToInt32(dataTable.Rows[0]["Active"]);
                    //empList.Add(emp);
                //}
            }
            if (emp != null)
            {
                return emp;
            }
            else
            {
                return null;
            }
        }

        // POST api/values
        public string Post(Employee employee)
        {
            string msg = "";
            if (employee != null)
            {
                SqlCommand cmd = new SqlCommand("usp_AddEmployee", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Active", employee.Active);

                _connection.Open();
                int i = cmd.ExecuteNonQuery();
                _connection.Close();

                if (i > 0)
                {
                    msg =  "Data has been inserted";
                }
                else
                {
                    msg = "Error";
                }
            }
            return msg;
        }

        // PUT api/values/5
        public string Put(int id, Employee employee)
        {
            string msg = "";
            if (employee != null)
            {
                SqlCommand cmd = new SqlCommand("usp_UpdateEmployee", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Active", employee.Active);

                _connection.Open();
                int i = cmd.ExecuteNonQuery();
                _connection.Close();

                if (i > 0)
                {
                    msg = "Data has been Updated";
                }
                else
                {
                    msg = "Error";
                }
            }
            return msg;
        }

        // DELETE api/values/5
        public string Delete(int id)
        {
            string msg = "";
            //if (employee != null)
            //{
                SqlCommand cmd = new SqlCommand("usp_DeleteEmployee", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                //cmd.Parameters.AddWithValue("@Name", employee.Name);
                //cmd.Parameters.AddWithValue("@Age", employee.Age);
                //cmd.Parameters.AddWithValue("@Active", employee.Active);

                _connection.Open();
                int i = cmd.ExecuteNonQuery();
                _connection.Close();

                if (i > 0)
                {
                    msg = "Data has been Deleted";
                }
                else
                {
                    msg = "Error";
                }
            //}
            return msg;
        }
    }
}
