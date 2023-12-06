using System.Net;
using IFinder.Application.Contracts.Documents.Dtos.Card;
using IFinder.Application.Contracts.Documents.Requests.Card;
using IFinder.Application.Contracts.Documents.Responses;
using IFinder.Application.Contracts.Services;
using IFinder.Application.Contracts.Services.Security;
using IFinder.Domain.Contracts.Page;
using IFinder.Domain.Contracts.Repositories;
using IFinder.Domain.Models;

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

    public async Task<Response<Page<GetSimpleCardDto>>> ListFromUserAsync(string idUser, Pageable pageable)
    {
        var cards = await _cardRepository.GetAllByUserIdAsync(idUser, pageable);
        
        return new Response<Page<GetSimpleCardDto>>(
            cards.Map(x => new GetSimpleCardDto()
            {
                TextCard = x.Text,
            })
        );
    }

    public async Task<Response<Page<GetCardDto>>> ListHome(Pageable pageable)
    {
        var userLoggedId = _currentUserService.GetCurrentUserId();
        var cards = await _cardRepository.GetHomeAsync(userLoggedId, pageable);

        var cardsDto = new List<GetCardDto>();

        foreach (var card in cards.Data)
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
        
        return new Response<Page<GetCardDto>>(
            new Page<GetCardDto>(cardsDto, cards.TotalPages, cards.TotalRegisters, cards.LastPage));    
    }
}