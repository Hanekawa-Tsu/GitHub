using Microsoft.AspNetCore.Mvc;
using ActividadJulio17.Models;

namespace ActividadJulio17.Controllers;

public class EstudianteController : Controller
{
    private static Estudiante[] estudiantes = new Estudiante[20];
    private static int contador = 2;

    static EstudianteController()
    {
        estudiantes[0] = new Estudiante { Carnet = 20245012, Nombre = "Pablo Charuc", Promedio = 91.5 };
        estudiantes[1] = new Estudiante { Carnet = 20239115, Nombre = "Melissa Mercedes", Promedio = 84.0 };
    }

    public IActionResult Listar()
    {
        return View(estudiantes);
    }

    public IActionResult Registrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registrar(Estudiante nuevoEstudiante)
    {
        if (nuevoEstudiante.Carnet <= 0 || nuevoEstudiante.Nombre == null || nuevoEstudiante.Nombre == "")
        {
            ViewBag.Error = "Datos del estudiante invalidos.";
            return View(nuevoEstudiante);
        }

        estudiantes[contador] = nuevoEstudiante;
        contador++;

        return RedirectToAction("Listar");
    }

    public IActionResult Historial(int id)
    {
        Estudiante encontrado = null;

        for (int i = 0; i < contador; i++)
        {
            if (estudiantes[i].Carnet == id)
            {
                encontrado = estudiantes[i];
            }
        }

        if (encontrado == null)
        {
            return NotFound();
        }

        return View(encontrado);
    }
}