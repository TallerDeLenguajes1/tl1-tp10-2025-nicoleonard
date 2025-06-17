using System.Text.Json;

List<Tarea> ListaDeTareas = await GetTareasAsync();

foreach (var tarea in ListaDeTareas)
{
    if(!tarea.completed)Console.WriteLine($"Tarea: {tarea.title} - Estado: {(tarea.completed? "Completada":"Pendiente")}");
}
foreach (var tarea in ListaDeTareas)
{
    if (tarea.completed) Console.WriteLine($"Tarea: {tarea.title} - Estado: {(tarea.completed ? "Completada" : "Pendiente")}");
}
Console.WriteLine();

string jsonTareas = JsonSerializer.Serialize(ListaDeTareas);
Console.WriteLine(jsonTareas);
File.WriteAllText("tareas.json",jsonTareas);

static async Task<List<Tarea>> GetTareasAsync()
{
    var url = "https://jsonplaceholder.typicode.com/todos/";

    try
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        List<Tarea> listaTareas = JsonSerializer.Deserialize<List<Tarea>>(responseBody);
        return listaTareas;
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("Problemas de acceso a la API");
        Console.WriteLine("Message :{0} ", e.Message);
        return null;
    }
}
