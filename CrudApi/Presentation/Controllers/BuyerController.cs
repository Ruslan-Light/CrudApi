using Application.UseCases.BuyerCases.AddBuyer;
using Application.UseCases.BuyerCases.EditBuyer;
using Application.UseCases.BuyerCases.GetBuyer;
using Application.UseCases.BuyerCases.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class BuyerController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BuyerVm>> GetBuyerById([FromQuery] GetBuyerQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> EditBuyer([FromBody] EditBuyerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddBuyer([FromBody] AddBuyerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteBuyer(Guid id)
        {
            return NoContent();
        }
    }
}
