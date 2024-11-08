using Microsoft.AspNetCore.Mvc;
using Web_2.Models.Thanhtoan;

namespace Web_2.Services.Donmua;

public interface IDonmuaService
{
    Task<Models.Thanhtoan.Donmua> CreateDonmuaAsync(DonmuaCreateDto donmuaDto);
    Task<Models.Thanhtoan.Donmua> GetDonmuaByIdAsync(int id);
    Task<List<Models.Thanhtoan.Donmua>> GetDonmuaByUserIdAsync(int userId);
    Task<bool> UpdateThanhtoanState(int Id);
    Task<List<Models.Thanhtoan.Donmua>> GetDonmuaBynguoimuaAsync(int nguoimuaId);
}