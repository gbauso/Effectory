using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Effectory.Application.Query;
using Effectory.Application.Dto;
using System.Collections.Generic;
using Effectory.Core.Commands;

namespace Effectory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : Controller
    {
        private readonly IMediator _Mediator;

        public QuestionnaireController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SimpleQuestionnaireDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuestionnaire()
        {
            var query = new QuestionnairesQuery();
            query.Validate();
            var result = await _Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AnswerQuestion([FromBody] AnswerQuestionCommand command)
        {
            var result = await _Mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestionnaireDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string[]))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuestionnaire([FromRoute] int id)
        {
            var query = new QuestionnaireQuery { QuestionnaireId = id };
            query.Validate();
            var result = await _Mediator.Send(query);

            return Ok(result);
        }
    }
}
