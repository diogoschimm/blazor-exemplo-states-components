using BlazorWebApp01.Services.Contracts;
using BlazorWebApp01.Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWebApp01.Services
{
    public class ClienteService : IClienteService
    {
        public Task<List<Cliente>> GetAll()
        {
            System.Threading.Thread.Sleep(3 * 1000);
            var lista = new List<Cliente>();
            for (int i = 1; i <= 10; i++)
            {
                lista.Add(new Cliente
                {
                    ClienteId = i,
                    NomeCliente = $"Cliente {i}"
                });
            }
            return Task.FromResult(lista);
        }
    }
}
