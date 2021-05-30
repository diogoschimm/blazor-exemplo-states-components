using BlazorWebApp01.Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWebApp01.Services.Contracts
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAll();
    }
}
