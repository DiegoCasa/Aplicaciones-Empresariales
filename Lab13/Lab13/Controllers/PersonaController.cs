using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lab13.Models;
using System.Web.Mvc;

namespace Lab13.Controllers
{
    public class PersonaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar(string Buscar)
        {
            List<Persona> personas = new List<Persona>();
            personas.Add(new Persona
            {
                PersonaID = 1,
                Nombre = "Juan",
                Apellido = "Perez",
                Direccion = "Av. Evergreen 123",
                FechaNac = Convert.ToDateTime("1997-11-07"),
                Email = "juan@mail.com"
            });
            personas.Add(new Persona
            {
                PersonaID = 2,
                Nombre = "Maria",
                Apellido = "Salas",
                Direccion = "Av. Progreso 325",
                FechaNac = Convert.ToDateTime("1995-10-28"),
                Email = "maria@mail.com"
            });
            personas.Add(new Persona
            {
                PersonaID = 3,
                Nombre = "Carlos",
                Apellido = "Martinez",
                Direccion = "Av. Evemazanos 101",
                FechaNac = Convert.ToDateTime("1982-02-14"),
                Email = "carlos@mail.com"
            });

            var persona = from p in personas select p;

            if (!String.IsNullOrEmpty(Buscar))
            {
                persona = personas.Where(p => p.Apellido.Contains(Buscar) || p.Nombre.Contains(Buscar));
            }

            return View(persona.ToList());
        }

        
    }
    }
