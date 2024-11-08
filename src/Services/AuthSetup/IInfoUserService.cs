using Web_2.Models;

namespace Web_2.AuthSetup;

public interface IInfoUserService
{
    Task<InformationUser> AddInformationAsync(InformationUserChange informationuc);
    Task<InformationUser> FindIformationUserAsync(int id_user);
    Task<InformationUser> UpdateInformationAsync(InformationUserChange informationuc);

}