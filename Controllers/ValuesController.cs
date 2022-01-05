using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using webAPIprueba.Models;

namespace webAPIprueba.Controllers
{
    public class ValuesController : ApiController
    {
        MySqlCommand comando;
        MySqlDataReader lector;

        public IEnumerable<Student> Get()
        {
            IList<Student> LstEstudiantes = new List<Student>();
            Connection cnn = new Connection();
            cnn.crearConexion();
            string consulta = "SELECT * FROM student;";

            try
            {
                comando = new MySqlCommand(consulta, cnn.objConexion);

                lector = comando.ExecuteReader();

                Student objEstudiante = new Student();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        objEstudiante.StudentId = lector.GetInt32(0);
                        objEstudiante.StudentName = lector.GetString(1);
                        objEstudiante.StudentAge = lector.GetInt32(2);
                        LstEstudiantes.Add(objEstudiante);
                        objEstudiante = new Student();
                    }
                }

                return LstEstudiantes.ToList().AsEnumerable();
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine("error: " + error.Message);
                return LstEstudiantes.ToList().AsEnumerable();
            }
            finally
            {
                cnn.desConectar();
            }
        }

        public Student Get(int id)
        {
            Connection cnn = new Connection();
            cnn.crearConexion();
            string consulta = "SELECT * FROM student WHERE StudentId=" + id + ";";
            Student objEstudiante = new Student();

            try
            {
                comando = new MySqlCommand(consulta, cnn.objConexion);

                lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    lector.Read();
                    objEstudiante.StudentId = lector.GetInt32(0);
                    objEstudiante.StudentName = lector.GetString(1);
                    objEstudiante.StudentAge = lector.GetInt32(2);
                }

                return objEstudiante;
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine("error: " + error.Message);
                return objEstudiante;
            }
            finally
            {
                cnn.desConectar();
            }
        }

        public HttpResponseMessage Post(Student registrar)
        {
            Connection cnn = new Connection();
            cnn.crearConexion();
            if (registrar.StudentId != 0)
            {
                System.Diagnostics.Debug.WriteLine("entra: " + registrar.StudentId);
                Put(registrar);
                return null;
            } else
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("entra insert");
                    string cadenaSentencia = "INSERT INTO student (StudentName, StudentAge) VALUES ('{0}',{1});";
                    string sentencia = String.Format(cadenaSentencia, registrar.StudentName, registrar.StudentAge);
                    comando = new MySqlCommand(sentencia, cnn.objConexion);
                    comando.ExecuteNonQuery();
                    HttpResponseMessage respuesta = Request.CreateResponse(HttpStatusCode.Created, registrar.StudentName);
                    return respuesta;
                }
                catch (Exception error)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, error.ToString());
                }
                finally
                {
                    cnn.desConectar();
                }
            }
        }

        public HttpResponseMessage Put(Student modificar)
        {
            System.Diagnostics.Debug.WriteLine("entra");
            Connection cnn = new Connection();
            cnn.crearConexion();
            try
            {
                string cadenaSentencia = "UPDATE student SET StudentName='{1}' WHERE StudentId={0};";
                string sentencia = String.Format(cadenaSentencia, modificar.StudentId, modificar.StudentName);
                comando = new MySqlCommand(sentencia, cnn.objConexion);
                comando.ExecuteNonQuery();
                HttpResponseMessage respuesta = Request.CreateResponse(HttpStatusCode.Accepted, modificar.StudentName);

                return respuesta;
            }
            catch (Exception error)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, error.ToString());
            }
            finally
            {
                cnn.desConectar();
            }
        }

        public HttpResponseMessage Delete(int StudentId)
        {
            Connection cnn = new Connection();
            cnn.crearConexion();
            try
            {
                string cadenaSentencia = "DELETE FROM student WHERE StudentId={0};";
                string sentencia = String.Format(cadenaSentencia, StudentId);
                comando = new MySqlCommand(sentencia, cnn.objConexion);
                comando.ExecuteNonQuery();
                HttpResponseMessage respuesta = Request.CreateResponse(HttpStatusCode.Gone, StudentId);

                return respuesta;
            }
            catch (Exception error)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, error.ToString());
            }
            finally
            {
                cnn.desConectar();
            }
        }
    }
}
