using ApiEstructurasDemo.Models;
using ApiEstructurasDemo.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// GET: devuelve todos los nodos
app.MapGet("/api/nodos", () =>
{
    return Results.Ok(NodoRepositorio.ObtenerTodos());
});

// POST: inserta un nuevo nodo
app.MapPost("/api/nodos", (NodoElemento nuevoNodo) =>
{
    if (nuevoNodo.Id <= 0 || string.IsNullOrEmpty(nuevoNodo.Valor))
        return Results.BadRequest("Datos del nodo inválidos.");

    bool insertado = NodoRepositorio.Insertar(nuevoNodo);
    if (!insertado)
        return Results.BadRequest("No hay espacio en el arreglo.");

    return Results.Created($"/api/nodos/{nuevoNodo.Id}", nuevoNodo);
});

app.Run();