using Application.EntityDbContext;
using Application.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.BuyerCases.DeleteBuyer
{
    public class DeleteBuyerCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        private class Handler : IRequestHandler<DeleteBuyerCommand, Unit>
        {
            private readonly IApiContext _context;

            public Handler(IApiContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteBuyerCommand request, CancellationToken cancellationToken)
            {
                if(request.Id != Guid.Empty)
                {
                    var buyer = _context.Buyers.FirstOrDefault(b => b.Id == request.Id) 
                        ?? throw new NotFoundException("Пользователь не найден в системе.");

                    _context.Buyers.Remove(buyer);
                    await _context.SaveChangesAsync();
                }

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
