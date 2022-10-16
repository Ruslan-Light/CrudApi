using Application.EntityDbContext;
using Application.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.BuyerCases.EditBuyer
{
    public class EditBuyerCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        private class Handler : IRequestHandler<EditBuyerCommand, Unit>
        {
            private readonly IApiContext _context;

            public Handler(IApiContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EditBuyerCommand request, CancellationToken cancellationToken)
            {
                var buyer = _context.Buyers.FirstOrDefault(b => b.Id == request.Id)
                    ?? throw new NotFoundException("Покупатель не найден в системе");

                if (!string.IsNullOrEmpty(request.Name))
                {
                    buyer.Name = request.Name;
                }

                await _context.SaveChangesAsync();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
