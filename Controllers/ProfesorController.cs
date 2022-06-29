using CRUDColegio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CRUDColegio.Controllers
{
    public class ProfesorController : Controller
    {
        //Dependency injection
        private readonly IConfiguration _configuration;
        public ProfesorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var profesores = new List<Profesor>();

            string query = @"
                                SELECT IdProfesor, Nombre, Apellidos, Genero
                                FROM Profesor
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
                        var profesor = new Profesor();
                        profesor.Id = myReader.GetInt32(0);
                        profesor.Nombre = myReader.GetString(1);
                        profesor.Apellidos = myReader.GetString(2);
                        profesor.Genero = myReader.GetString(3);

                        profesores.Add(profesor);
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }

            List<Profesor> tempProfList = profesores;
            return View(profesores);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Profesor profesor)
        {
            string error = "";
            try
            {
                string query = @"
                                INSERT INTO Profesor
                                VALUES (@Nombre, @Apellidos, @Genero)
                            ";

                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        //adding values into table
                        myCommand.Parameters.AddWithValue("@Nombre", profesor.Nombre);
                        myCommand.Parameters.AddWithValue("@Apellidos", profesor.Apellidos);
                        myCommand.Parameters.AddWithValue("@Genero", profesor.Genero);

                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();
                    }
                }

                TempData["mensaje"] = "El profesor se ha creado correctamente";
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

            var profesor = new Profesor();
            string query = @"
                                SELECT IdProfesor, Nombre, Apellidos, Genero
                                FROM Profesor
                                WHERE IdProfesor = @IdProfesor
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
                    myCommand.Parameters.AddWithValue("@IdProfesor", id);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        profesor.Id = myReader.GetInt32(0);
                        profesor.Nombre = myReader.GetString(1);
                        profesor.Apellidos = myReader.GetString(2);
                        profesor.Genero = myReader.GetString(3);
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }

            return View(profesor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Profesor profesor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string error = "";
            try
            {
                string query = @"
                                UPDATE Profesor
                                SET Nombre = @Nombre, 
                                Apellidos = @Apellidos, 
                                Genero = @Genero
                                WHERE IdProfesor = @IdProfesor
                            ";

                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        //adding values into table
                        myCommand.Parameters.AddWithValue("@IdProfesor", profesor.Id);
                        myCommand.Parameters.AddWithValue("@Nombre", profesor.Nombre);
                        myCommand.Parameters.AddWithValue("@Apellidos", profesor.Apellidos);
                        myCommand.Parameters.AddWithValue("@Genero", profesor.Genero);

                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();
                    }
                }

                TempData["mensaje"] = "El profesor se ha editado correctamente";
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

            var profesor = new Profesor();
            string query = @"
                                SELECT IdProfesor, Nombre, Apellidos, Genero
                                FROM Profesor
                                WHERE IdProfesor = @IdProfesor
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
                    myCommand.Parameters.AddWithValue("@IdProfesor", id);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        profesor.Id = myReader.GetInt32(0);
                        profesor.Nombre = myReader.GetString(1);
                        profesor.Apellidos = myReader.GetString(2);
                        profesor.Genero = myReader.GetString(3);
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }

            return View(profesor);
        }

        [HttpPost]
        public IActionResult Delete(Profesor profesor)
        {
            string error;
            try
            {
                string query = @"
                                DELETE FROM Profesor
                                WHERE IdProfesor = @IdProfesor
                            ";
                //adding source for the data
                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@IdProfesor", profesor.Id);

                        myCommand.ExecuteReader();

                        myCon.Close();
                    }
                }

                TempData["mensaje"] = "El profesor se ha eliminado correctamente";
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
