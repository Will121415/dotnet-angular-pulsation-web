using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;

namespace BLL
{
    public class PersonService
    {
        private readonly ConnectionManager conexion;
        private readonly PersonRepository repositorio;
        public PersonService(string connectionString)
        {
            conexion = new ConnectionManager(connectionString);
            repositorio = new PersonRepository(conexion);
        }
        public SavePersonResponse Save(Person person)
        {
            try
            {
                person.CalculatePulsation();
                conexion.Open();
                repositorio.Save(person);
                conexion.Close();
                return new SavePersonResponse(person);
            }
            catch (Exception e)
            {
                return new SavePersonResponse($"Error de la Aplicacion: {e.Message}");
            }
            finally { conexion.Close(); }
        }
        public ConsultPersonResponse GetList()
        {
            try
            {
                conexion.Open();
                IList<Person> persons = repositorio.GetList();
                conexion.Close();
                
                return new ConsultPersonResponse(persons);
            }
            catch (Exception e)
            {
                return new ConsultPersonResponse($"Error de la Aplicacion: {e.Message}");
            }
            finally { conexion.Close(); }

        }
        public string Delete(string Identification)
        {
            try
            {
                conexion.Open();
                var Person = repositorio.SearchById(Identification);
                if (Person != null)
                {
                    repositorio.Delete(Person);
                    conexion.Close();
                    return ($"El registro {Person.Name} se ha eliminado satisfactoriamente.");
                }
                else
                {
                    return ($"Lo sentimos, {Identification} no se encuentra registrada.");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicación: {e.Message}";
            }
            finally { conexion.Close(); }

        }
        public string Modidy(Person newPerson)
        {
            try
            {
                newPerson.CalculatePulsation();
                conexion.Open();
                var PersonVieja = repositorio.SearchById(newPerson.Identification);
                if (PersonVieja != null)
                {
                    repositorio.Modify(newPerson);
                    conexion.Close();
                    return ($"El registro {newPerson.Name} se ha modificado satisfactoriamente.");
                }
                else
                {
                    return ($"Lo sentimos, {newPerson.Identification} no se encuentra registrada.");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicación: {e.Message}";
            }
            finally { conexion.Close(); }

        }
        public SearchPersonResponse SearchById(string Identification)
        {
            try
            {

                conexion.Open();
                Person person = repositorio.SearchById(Identification);
                conexion.Close();
                return new SearchPersonResponse(person);
            }
            catch (Exception e)
            {
                
                return new SearchPersonResponse($"Error de la Aplicacion: {e.Message}");
            }
            finally { conexion.Close(); }
        }
        public ConteoPersonRespuesta Totalizar()
        {
            ConteoPersonRespuesta respuesta = new ConteoPersonRespuesta();
            try
            {

                conexion.Open();
                respuesta.Cuenta = repositorio.Totalizar(); ;
                conexion.Close();
                respuesta.Error = false;
                respuesta.Mensaje = "Se consultan los Datos";
                
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = $"Error de la Aplicacion: {e.Message}";
                respuesta.Error = true;
                return respuesta;
            }
            finally { conexion.Close(); }
           
        }
        public ConteoPersonRespuesta TotalizarTipo(string tipo)
        {
            ConteoPersonRespuesta respuesta = new ConteoPersonRespuesta();
            try
            {

                conexion.Open();
                respuesta.Cuenta = repositorio.TotalizarTipo(tipo);
                conexion.Close();
                respuesta.Error = false;
                respuesta.Mensaje = "Se consultan los Datos";
               
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = $"Error de la Aplicacion: {e.Message}";
                respuesta.Error = true;
                return respuesta;
            }
            finally { conexion.Close(); }
           
        }
       
    }

    public class SavePersonResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public Person Person { get; set; }
        public SavePersonResponse(Person person)
        {
            Error = false;
            Person = person;
        }

        public SavePersonResponse(string message)
        {
            Error = true;
            Message = message;
        }
    }

    public class ConsultPersonResponse
    {
        public ConsultPersonResponse(IList<Person> persons)
        {
            Error =  false;
            Persons = persons;
        }

        public ConsultPersonResponse(string message)
        {
            Error = true;
            Message = message;
            
        }
        public bool Error { get; set; }
        public string Message { get; set; }
        public IList<Person> Persons { get; set; }
    }


    public class SearchPersonResponse
    {
        public SearchPersonResponse(Person person)
        {
            Error = false;
            Person =  person;
        }

        public SearchPersonResponse(string message)
        {
            Error = true;
            Message = message;
        }
        public bool Error { get; set; }
        public string Message { get; set; }
        public Person Person { get; set; }
    }



    public class ConteoPersonRespuesta
    {
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public int Cuenta { get; set; }
    }
}