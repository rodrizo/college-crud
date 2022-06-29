using CRUDColegio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRUDColegio.Controllers
{
    public class AlumnoController : Controller
    {
        //Dependency injection
        private readonly IConfiguration _configuration;
        public AlumnoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var alumnos = new List<Alumno>();
            string query = @"
                                SELECT IdAlumno, Nombre, Apellidos, Genero, FechaNacimiento
                                FROM Alumno
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
                        var alumno = new Alumno();
                        alumno.Id = myReader.GetInt32(0);
                        alumno.Nombre = myReader.GetString(1);
                        alumno.Apellidos = myReader.GetString(2);
                        alumno.Genero = myReader.GetString(3);
                        alumno.FechaNacimiento = myReader.GetDateTime(4);

                        alumnos.Add(alumno);
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }

            return View(alumnos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Alumno alumno)
        {
            string error = "";
            try
            {
                string query = @"
                                INSERT INTO Alumno
                                VALUES (@Nombre, @Apellidos, @Genero, @FechaNacimiento)
                            ";

                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        //adding values into table
                        myCommand.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                        myCommand.Parameters.AddWithValue("@Apellidos", alumno.Apellidos);
                        myCommand.Parameters.AddWithValue("@Genero", alumno.Genero);
                        myCommand.Parameters.AddWithValue("@FechaNacimiento", alumno.FechaNacimiento);

                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();
                    }
                }
                TempData["mensaje"] = "El alumno se ha registrado correctamente";
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

            var alumno = new Alumno();
            string query = @"
                                SELECT IdAlumno, Nombre, Apellidos, Genero, FechaNacimiento
                                FROM Alumno
                                WHERE IdAlumno = @IdAlumno
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
                    myCommand.Parameters.AddWithValue("@IdAlumno", id);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        alumno.Id = myReader.GetInt32(0);
                        alumno.Nombre = myReader.GetString(1);
                        alumno.Apellidos = myReader.GetString(2);
                        alumno.Genero = myReader.GetString(3);
                        alumno.FechaNacimiento = myReader.GetDateTime(4);
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }

            return View(alumno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string error = "";
            try
            {
                string query = @"
                                UPDATE Alumno
                                SET Nombre = @Nombre, 
                                Apellidos = @Apellidos, 
                                Genero = @Genero, 
                                FechaNacimiento = @FechaNacimiento
                                WHERE IdAlumno = @IdAlumno
                            ";

                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        //adding values into table
                        myCommand.Parameters.AddWithValue("@IdAlumno", alumno.Id);
                        myCommand.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                        myCommand.Parameters.AddWithValue("@Apellidos", alumno.Apellidos);
                        myCommand.Parameters.AddWithValue("@Genero", alumno.Genero);
                        myCommand.Parameters.AddWithValue("@FechaNacimiento", alumno.FechaNacimiento);

                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();
                    }
                }
                TempData["mensaje"] = "El alumno se ha editado correctamente";
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

            var alumno = new Alumno();
            string query = @"
                                SELECT IdAlumno, Nombre, Apellidos, Genero, FechaNacimiento
                                FROM Alumno
                                WHERE IdAlumno = @IdAlumno
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
                    myCommand.Parameters.AddWithValue("@IdAlumno", id);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        alumno.Id = myReader.GetInt32(0);
                        alumno.Nombre = myReader.GetString(1);
                        alumno.Apellidos = myReader.GetString(2);
                        alumno.Genero = myReader.GetString(3);
                        alumno.FechaNacimiento = myReader.GetDateTime(4);
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }

            return View(alumno);
        }

        [HttpPost]
        public IActionResult Delete(Alumno alumno)
        {
            string error;
            try
            {
                string query = @"
                                DELETE FROM Alumno
                                WHERE IdAlumno = @IdAlumno
                            ";
                //adding source for the data
                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@IdAlumno", alumno.Id);

                        myCommand.ExecuteReader();

                        myCon.Close();
                    }
                }

                TempData["mensaje"] = "El alumno se ha eliminado correctamente";
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
