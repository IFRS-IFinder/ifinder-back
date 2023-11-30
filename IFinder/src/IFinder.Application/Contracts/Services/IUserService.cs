using IFinder.Application.Contracts.Documents.Dtos;
using IFinder.Application.Contracts.Documents.Requests.User;
using IFinder.Application.Contracts.Documents.Responses;
using IFinder.Domain.Models;

namespace IFinder.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<Response<EditUserDto>> EditAsync(EditUserRequest user);
        Task<Response<GetCompleteUserDto>> GetUserComplete(string id);
    }
}