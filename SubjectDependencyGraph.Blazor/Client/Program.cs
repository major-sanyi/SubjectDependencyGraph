using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Newtonsoft.Json;
using SubjectDependencyGraph.Blazor.Services;
using SubjectDependencyGraph.Shared.Models;
using SubjectDependencyGraph.Shared.Services;

namespace SubjectDependencyGraph.Blazor
{
    public class Program
    {
        /// <summary>
        /// The entry point for the client-side Blazor WebAssembly application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static async Task Main(string[] args)
        {

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            // This part is kinda not nice.
            // But we need to make sure resources loaded beforehand.
            // TODO: Check for a nicer solution
            using (var client = new HttpClient())
            {
                var syllabi = JsonConvert.DeserializeObject<List<Syllabus>>(await client.GetStringAsync("https://major-sanyi.github.io/SubjectDependencyGraph-Syllabi/syllabi.json"));
                var equalTableDto = JsonConvert.DeserializeObject<List<EqualTableDto>>(await client.GetStringAsync("https://major-sanyi.github.io/SubjectDependencyGraph-Syllabi/equivalence.json"));
                // Fallback to defaults
                //  syllabi ??= JsonConvert.DeserializeObject<List<Syllabus>>(SubjectDependencyGraph.Shared.Resources.Resource.OENIK_E) ?? [];
                //  equalTableDto ??= JsonConvert.DeserializeObject<List<EqualTableDto>>(SubjectDependencyGraph.Shared.Resources.Resource.OENIK_E_equals) ?? [];
                SyllabiService.InjectDefaultDependency(syllabi, equalTableDto);

            }
            // End of uglyness.

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            builder.Services.AddScoped(sp => new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<ISyllabiService, SyllabiService>();
            builder.Services.AddScoped<IBlazorPageMemoryService, BlazorPageMemoryService>();
            await builder.Build().RunAsync();
        }
    }
}