using Microsoft.AspNetCore.Builder;

namespace WebApi1
{
    public static class EndpointExtensions
    {
        public static void MapProductEndpoints(this IApplicationBuilder app)
        {
            app.Map("/products", productsApp =>
            {
                productsApp.UseRouting();
                productsApp.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/", async context =>
                    {
                        // Handle GET request for listing all products
                        await context.Response.WriteAsync("Listing all products");
                    });

                    endpoints.MapGet("/{id:int}", async context =>
                    {
                        // Handle GET request for retrieving a specific product by ID
                        var id = int.Parse(context.Request.RouteValues["id"].ToString());
                        await context.Response.WriteAsync($"Retrieving product with ID: {id}");
                    });

                    endpoints.MapPost("/", async context =>
                    {
                        // Handle POST request for creating a new product
                        await context.Response.WriteAsync("Creating a new product");
                    });

                    endpoints.MapPut("/{id:int}", async context =>
                    {
                        // Handle PUT request for updating a product by ID
                        var id = int.Parse(context.Request.RouteValues["id"].ToString());
                        await context.Response.WriteAsync($"Updating product with ID: {id}");
                    });

                    endpoints.MapDelete("/{id:int}", async context =>
                    {
                        // Handle DELETE request for deleting a product by ID
                        var id = int.Parse(context.Request.RouteValues["id"].ToString());
                        await context.Response.WriteAsync($"Deleting product with ID: {id}");
                    });
                });
            });
        }
        public static void MapWarehouseEndpoints(this IApplicationBuilder app)
        {
            app.Map("/warehouse", warehousesApp =>
            {
                warehousesApp.UseRouting();
                warehousesApp.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/", async context =>
                    {
                        // Handle GET request for listing all warehouses
                        await context.Response.WriteAsync("Listing all warehouses");
                    });

                    endpoints.MapGet("/{id:int}", async context =>
                    {
                        // Handle GET request for retrieving a specific warehouse by ID
                        var id = int.Parse(context.Request.RouteValues["id"].ToString());
                        await context.Response.WriteAsync($"Retrieving warehouse with ID: {id}");
                    });

                    endpoints.MapPost("/", async context =>
                    {
                        // Handle POST request for creating a new warehouse
                        await context.Response.WriteAsync("Creating a new warehouse");
                    });

                    endpoints.MapPut("/{id:int}", async context =>
                    {
                        // Handle PUT request for updating a warehouse by ID
                        var id = int.Parse(context.Request.RouteValues["id"].ToString());
                        await context.Response.WriteAsync($"Updating warehouse with ID: {id}");
                    });

                    endpoints.MapDelete("/{id:int}", async context =>
                    {
                        // Handle DELETE request for deleting a warehouse by ID
                        var id = int.Parse(context.Request.RouteValues["id"].ToString());
                        await context.Response.WriteAsync($"Deleting warehouse with ID: {id}");
                    });
                });
            });
        }
    }

}
