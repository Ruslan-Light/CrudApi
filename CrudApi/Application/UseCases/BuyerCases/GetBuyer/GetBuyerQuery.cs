using Application.EntityDbContext;
using Application.Exceptions;
using Application.UseCases.BuyerCases.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.BuyerCases.GetBuyer
{
    public class GetBuyerQuery : Profile, IRequest<BuyerVm>
    {
        public Guid Id { get; set; }

        public GetBuyerQuery()
        {
            CreateMap<Buyer, BuyerVm>();
        }

        private class Handler : IRequestHandler<GetBuyerQuery, BuyerVm>
        {
            private readonly IApiContext _context;
            private readonly IMapper _mapper;

            public Handler(
                IApiContext context,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BuyerVm> Handle(GetBuyerQuery request, CancellationToken cancellationToken)
            {
                var user = _context.Buyers
                    .AsNoTracking()
                    .FirstOrDefault(b => b.Id == request.Id)
                        ?? throw new NotFoundException("Пользователь не найден в системе");

                var buyer = _mapper.Map<BuyerVm>(user);

                return buyer;
            }
        }
    }
}
