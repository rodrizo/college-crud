using CRUDColegio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CRUDColegio.Controllers
{
    public class AlumnoGradoController : Controller
    {
        //Dependency injection
        private readonly IConfiguration _configuration;
        public AlumnoGradoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var alumnoGrados = new List<AlumnoGrado>();
            string query = @"
                                SELECT IdAlumnoGrado, AlumnoId, GradoId, Seccion
                                FROM AlumnoGrado
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
                        var alumGrado = new AlumnoGrado();
                        alumGrado.Id = myReader.GetInt32(0);
                        alumGrado.AlumnoId = myReader.GetInt32(1);
                        alumGrado.GradoId = myReader.GetInt32(2);
                        alumGrado.Seccion = myReader.GetString(3);

                        alumnoGrados.Add(alumGrado);
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }

            return View(alumnoGrados);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AlumnoGrado alumno)
        {
            string error = "";
            try
            {
                string query = @"
                                INSERT INTO AlumnoGrado
                                VALUES (@AlumnoId, @GradoId, @Seccion)
                            ";

                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        //adding values into table
                        myCommand.Parameters.AddWithValue("@AlumnoId", alumno.AlumnoId);
                        myCommand.Parameters.AddWithValue("@GradoId", alumno.GradoId);
                        myCommand.Parameters.AddWithValue("@Seccion", alumno.Seccion);

                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();
                    }
                }

                TempData["mensaje"] = "Alumno y grado se han creado correctamente";
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

            var alumnoGrado = new AlumnoGrado();
            string query = @"
                                SELECT IdAlumnoGrado, AlumnoId, GradoId, Seccion
                                FROM AlumnoGrado
                                WHERE IdAlumnoGrado = @IdAlumnoGrado
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
                    myCommand.Parameters.AddWithValue("@IdAlumnoGrado", id);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        alumnoGrado.Id = myReader.GetInt32(0);
                        alumnoGrado.AlumnoId = myReader.GetInt32(1);
                        alumnoGrado.GradoId = myReader.GetInt32(2);
                        alumnoGrado.Seccion = myReader.GetString(3);
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }

            return View(alumnoGrado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AlumnoGrado alumno)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string error = "";
            try
            {
                string query = @"
                                UPDATE AlumnoGrado
                                SET AlumnoId = @AlumnoId, 
                                GradoId = @GradoId,
                                Seccion = @Seccion
                                WHERE IdAlumnoGrado = @IdAlumnoGrado
                            ";

                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        //adding values into table
                        myCommand.Parameters.AddWithValue("@IdAlumnoGrado", alumno.Id);
                        myCommand.Parameters.AddWithValue("@AlumnoId", alumno.AlumnoId);
                        myCommand.Parameters.AddWithValue("@GradoId", alumno.GradoId);
                        myCommand.Parameters.AddWithValue("@Seccion", alumno.Seccion);

                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();
                    }
                }

                TempData["mensaje"] = "Alumno y grado se han editado correctamente";
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

            var alumnoGrado = new AlumnoGrado();
            string query = @"
                                SELECT IdAlumnoGrado, AlumnoId, GradoId, Seccion
                                FROM AlumnoGrado
                                WHERE IdAlumnoGrado = @IdAlumnoGrado
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
                    myCommand.Parameters.AddWithValue("@IdAlumnoGrado", id);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        alumnoGrado.Id = myReader.GetInt32(0);
                        alumnoGrado.AlumnoId = myReader.GetInt32(1);
                        alumnoGrado.GradoId = myReader.GetInt32(2);
                        alumnoGrado.Seccion = myReader.GetString(3);
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }

            return View(alumnoGrado);
        }

        [HttpPost]
        public IActionResult Delete(AlumnoGrado alumno)
        {
            string error;
            try
            {
                string query = @"
                                DELETE FROM AlumnoGrado
                                WHERE IdAlumnoGrado = @IdAlumnoGrado
                            ";
                //adding source for the data
                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@IdAlumnoGrado", alumno.Id);

                        myCommand.ExecuteReader();

                        myCon.Close();
                    }
                }

                TempData["mensaje"] = "Alumno y grado se han eliminado correctamente";
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
