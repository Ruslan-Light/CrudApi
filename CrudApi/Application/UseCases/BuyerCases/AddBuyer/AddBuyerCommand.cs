using Application.EntityDbContext;
using AutoMapper;
using Domain.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.BuyerCases.AddBuyer
{
    public class AddBuyerCommand : Profile, IRequest<Unit>
    {
        public string Name { get; set; }

        public AddBuyerCommand()
        {
            CreateMap<AddBuyerCommand, Buyer>();
        }

        private class Handler : IRequestHandler<AddBuyerCommand, Unit>
        {
            private readonly IApiContext _context;
            private readonly IMapper _mapper;

            public Handler(IApiContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(AddBuyerCommand request, CancellationToken cancellationToken)
            {
                if (!string.IsNullOrEmpty(request.Name))
                {
                    var buyer = _mapper.Map<Buyer>(request);

                    _context.Buyers.Add(buyer);
                    await _context.SaveChangesAsync();
                }
                
                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
