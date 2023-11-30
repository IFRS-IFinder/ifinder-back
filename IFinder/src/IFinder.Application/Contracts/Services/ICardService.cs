using IFinder.Application.Contracts.Documents.Dtos.Card;
using IFinder.Application.Contracts.Documents.Requests.Card;
using IFinder.Application.Contracts.Documents.Responses;

namespace IFinder.Application.Contracts.Services;

public interface ICardService
{
    Task<Response<CreateCardDto>> CreateAsync(CreateCardRequest request);
    Task<Response> DeleteAsync(string idCard);
    Task<Response<IEnumerable<GetCardDto>>> ListFromUserAsync(string idUser);
    Task<Response<IEnumerable<GetCardDto>>> ListHome();
}