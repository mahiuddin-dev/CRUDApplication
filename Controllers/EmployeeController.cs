using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace WebApplication.Controllers
{
    public class EmployeeController : Controller
    {
        string Str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\C#\WebApplication\App_Data\MYDB.mdf;Integrated Security=True";
        // GET: Employee
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();
                string q = "Select * from Employee";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                da.Fill(dt);
            }
            return View(dt);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View(new Employee());
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                // TODO: Add insert logic here

                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    string q = "insert into Employee values('"+employee.name+"','"+employee.city+"','"+employee.phone+"')";
                    SqlCommand cmd = new SqlCommand(q,con);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = new Employee();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();
                string q = "Select * from Employee where id="+id;
                SqlDataAdapter da = new SqlDataAdapter(q,con);
                da.Fill(dataTable);
            }
            if (dataTable.Rows.Count == 1)
            {
                employee.id = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                employee.name = dataTable.Rows[0][1].ToString();
                employee.city = dataTable.Rows[0][2].ToString();
                employee.phone = dataTable.Rows[0][3].ToString();
                return View(employee);
            }
            return RedirectToAction("Index");
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                // TODO: Add update logic here
                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    string q = "Update Employee SET name=@name,city=@city,phone=@phone where id=@id";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.Parameters.AddWithValue("@id",employee.id);
                    cmd.Parameters.AddWithValue("@name",employee.name);
                    cmd.Parameters.AddWithValue("@city",employee.city);
                    cmd.Parameters.AddWithValue("@phone",employee.phone);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();
                string q = "Delete from Employee where id =@id";
                SqlCommand cmd = new SqlCommand(q,con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // POST: Employee/Delete/5
    }
}
