using IFinder.Application.Contracts.Documents.Dtos;
using IFinder.Application.Contracts.Documents.Dtos.Card;
using IFinder.Application.Contracts.Documents.Requests.Card;
using IFinder.Application.Contracts.Documents.Requests.User;
using IFinder.Application.Contracts.Documents.Responses;
using IFinder.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace IFinder.WebApi.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        
        [HttpPost]
        public async Task<ActionResult<Response<CreateCardDto>>> Create([FromBody] CreateCardRequest request)
        {
            // TODO: validacao dos dados da request 

            var response = await _cardService.CreateAsync(request);

            if (response.IsErrorStatusCode())
                return StatusCode(response.GetErrorCode(), response.GetErrorMessage());

            return Ok(response);
        }
        
        [HttpGet]
        [Route("{idUser}")]
        public async Task<ActionResult<IEnumerable<GetCardDto>>> ListFromUser([FromRoute] string idUser)
        {
            // TODO: validacao dos dados da request 

            var response = await _cardService.ListFromUserAsync(idUser);

            if (response.IsErrorStatusCode())
                return StatusCode(response.GetErrorCode(), response.GetErrorMessage());

            return Ok(response);
        }
        
        [HttpDelete]
        [Route("{idCard}")]
        public async Task<ActionResult<Response>> Delete([FromRoute] string idCard)
        {
            // TODO: validacao dos dados da request 

            var response = await _cardService.DeleteAsync(idCard);

            if (response.IsErrorStatusCode())
                return StatusCode(response.GetErrorCode(), response.GetErrorMessage());

            return Ok(response);
        }
    }
}
