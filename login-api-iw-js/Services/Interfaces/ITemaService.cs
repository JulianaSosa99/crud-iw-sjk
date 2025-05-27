using login_api_iw_js.DTOs;

namespace login_api_iw_js.Services.Interfaces
{
    public interface ITemaService
    {
        Task<IEnumerable<Tema>> GetAllAsync();
        Task<Tema> GetByIdAsync(int id);
        Task CreateAsync(Tema tema);
        Task UpdateAsync(Tema tema);
        Task DeleteAsync(int id);

    }
}
