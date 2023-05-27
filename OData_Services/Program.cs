using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EnumType<Gender>();
modelBuilder.EntitySet<Customer>("Customers");

builder.Services.AddControllers().AddOData(
    options => options.EnableQueryFeatures().AddRouteComponents(
        routePrefix: "odata",
        model: modelBuilder.GetEdmModel()));

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hung Phu OData", Version = "v1" });
});

AddFormatters(builder.Services);

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OData V1");
});

// format Swagger UI
void AddFormatters(IServiceCollection services)
{
    services.AddMvcCore(opt =>
    {
        foreach (var outputFormatter in opt.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
        {
            outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatastxx-odata"));
        }

        foreach (var inputFormatter in opt.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
        {
            inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatastxx-odata"));
        }
    });
}

app.Run();
