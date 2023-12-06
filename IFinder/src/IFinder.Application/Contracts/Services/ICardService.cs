using IFinder.Application.Contracts.Documents.Dtos.Card;
using IFinder.Application.Contracts.Documents.Requests.Card;
using IFinder.Application.Contracts.Documents.Responses;
using IFinder.Domain.Contracts.Page;

namespace IFinder.Application.Contracts.Services;

public interface ICardService
{
    Task<Response<CreateCardDto>> CreateAsync(CreateCardRequest request);
    Task<Response> DeleteAsync(string idCard);
    Task<Response<Page<GetSimpleCardDto>>> ListFromUserAsync(string idUser, Pageable pageable);
    Task<Response<Page<GetCardDto>>> ListHome(Pageable pageable);
}