using System.Net;
using IFinder.Application.Contracts.Documents.Dtos.Card;
using IFinder.Application.Contracts.Documents.Requests.Card;
using IFinder.Application.Contracts.Documents.Responses;
using IFinder.Application.Contracts.Services;
using IFinder.Domain.Contracts.Repositories;
using IFinder.Domain.Models;
using IFootball.Application.Contracts.Services.Core;

namespace IFinder.Application.Implementations.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;

    public CardService(ICardRepository cardRepository, ICurrentUserService currentUserService, IUserRepository userRepository)
    {
        _cardRepository = cardRepository;
        _currentUserService = currentUserService;
        _userRepository = userRepository;
    }

    public async Task<Response<CreateCardDto>> CreateAsync(CreateCardRequest request)
    {
        var userLoggedId = _currentUserService.GetCurrentUserId();

        var card = new Card()
        {
            IdUser = userLoggedId,
            Text = request.Text,
        };

        await _cardRepository.InsertAsync(card);
        return new Response<CreateCardDto>(new CreateCardDto()
        {
            Id = card.Id,
            Text = card.Text
        });
    }

    public async Task<Response> DeleteAsync(string idCard)
    {
        var userLoggedId = _currentUserService.GetCurrentUserId();
        var card = await _cardRepository.GetByIdAsync(idCard);
        
        if (card is null)
            return new Response(HttpStatusCode.Forbidden, "A carta não existe!");
        
        if (card.IdUser != userLoggedId)
            return new Response(HttpStatusCode.Forbidden, "A carta não é de sua autoria!");
        
        await _cardRepository.DeleteByIdAsync(idCard);
        return new Response();
    }

    public async Task<Response<IEnumerable<GetSimpleCardDto>>> ListFromUserAsync(string idUser)
    {
        var cards = await _cardRepository.GetAllByUserIdAsync(idUser);
        return new Response<IEnumerable<GetSimpleCardDto>>(
            cards.Select(x => new GetSimpleCardDto()
            {
                TextCard = x.Text,
            })
        );
    }

    public async Task<Response<IEnumerable<GetCardDto>>> ListHome()
    {
        var userLoggedId = _currentUserService.GetCurrentUserId();
        var cards = await _cardRepository.GetHomeAsync(userLoggedId);

        var cardsDto = new List<GetCardDto>();

        foreach (var card in cards)
        {
            var authorCard = await _userRepository.GetByIdAsync(card.IdUser);
            
            var cardDto = new GetCardDto()
            {
                IdCard = card.Id,
                IdAuthor = card.IdUser,
                TextCard = card.Text,
                SexAuthor = authorCard.Sex,
                hoobiesAuthor = authorCard.Hobbies,
                descriptionAuthor = authorCard.Description,
                ageAuthor = authorCard.Age,
                nameAuthor = authorCard.Name,
            };
            cardsDto.Add(cardDto);
        }
        
        return new Response<IEnumerable<GetCardDto>>(cardsDto);    
    }
}