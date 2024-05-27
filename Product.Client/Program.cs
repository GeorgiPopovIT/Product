using Grpc.Net.Client;
using ProductShop;

using var channel = GrpcChannel.ForAddress("https://localhost:7033");
var productClient = new Product.ProductClient(channel);
var reply =  await productClient.SaveProductAsync(
                  new ProductRequest { Name = "Bread", Price = 2.00, CategoryId = 1 });
Console.WriteLine("Is Product added successfully: " + reply.IsSuccess);


Console.WriteLine("Press any key to exit...");
Console.ReadKey();
