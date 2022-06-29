using CRUDColegio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CRUDColegio.Controllers
{
    public class GradoController : Controller
    {
        
        //Dependency injection
        private readonly IConfiguration _configuration;
        public GradoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var grados = new List<Grado>();
            string query = @"
                                SELECT IdGrado, Nombre, ProfesorId
                                FROM Grado
                            ";
            //adding source for the data
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            //setting a reader object
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        var grado = new Grado();
                        grado.Id = myReader.GetInt32(0);
                        grado.Nombre = myReader.GetString(1);
                        grado.ProfesorId = myReader.GetInt32(2);

                        grados.Add(grado);
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }

            return View(grados);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Grado grado)
        {
            string error = "";
            try
            {
                string query = @"
                                INSERT INTO Grado
                                VALUES (@Nombre, @ProfesorId)
                            ";

                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        //adding values into table
                        myCommand.Parameters.AddWithValue("@Nombre", grado.Nombre);
                        myCommand.Parameters.AddWithValue("@ProfesorId", grado.ProfesorId);

                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();
                    }
                }

                TempData["mensaje"] = "El grado se ha creado correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                error = e.Message;
                return View(error);
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            var grado = new Grado();
            string query = @"
                                SELECT IdGrado, Nombre, ProfesorId
                                FROM Grado
                                WHERE IdGrado = @IdGrado
                            ";

            //adding source for the data
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            //setting a reader object
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdGrado", id);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        grado.Id = myReader.GetInt32(0);
                        grado.Nombre = myReader.GetString(1);
                        grado.ProfesorId = myReader.GetInt32(2);
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }

            return View(grado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Grado grado)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string error = "";
            try
            {
                string query = @"
                                UPDATE Grado
                                SET Nombre = @Nombre, 
                                ProfesorId = @ProfesorId
                                WHERE IdGrado = @IdGrado
                            ";

                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        //adding values into table
                        myCommand.Parameters.AddWithValue("@IdGrado", grado.Id);
                        myCommand.Parameters.AddWithValue("@Nombre", grado.Nombre);
                        myCommand.Parameters.AddWithValue("@ProfesorId", grado.ProfesorId);

                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();
                    }
                }

                TempData["mensaje"] = "El grado se ha editado correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                error = e.Message;
                return View(error);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {

            var grado = new Grado();
            string query = @"
                                SELECT IdGrado, Nombre, ProfesorId
                                FROM Grado
                                WHERE IdGrado = @IdGrado
                            ";

            //adding source for the data
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            //setting a reader object
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdGrado", id);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        grado.Id = myReader.GetInt32(0);
                        grado.Nombre = myReader.GetString(1);
                        grado.ProfesorId = myReader.GetInt32(2);
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }

            return View(grado);
        }

        [HttpPost]
        public IActionResult Delete(Grado grado)
        {
            string error;
            try
            {
                string query = @"
                                DELETE FROM Grado
                                WHERE IdGrado = @IdGrado
                            ";
                //adding source for the data
                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@IdGrado", grado.Id);

                        myCommand.ExecuteReader();

                        myCon.Close();
                    }
                }

                TempData["mensaje"] = "El grado se ha eliminado correctamente";
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                error = e.Message;
                return View(error);
            }
        }


    }
}
