using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Caching.API.Interfaces;
using Application.Infrastructure.Persistence.API.Interfaces;
using Application.Paging.API.Extensions;
using Application.Storage.API.Storage.Contacts.Models;
using AutoMapper;
using MediatR;

namespace Application.Storage.API.Storage.Contacts.Queries.Details
{
    public class DetailsQuery : IRequest<ContactDto>
    {
        public int ContactId { get; set; }

        private class Handler : IRequestHandler<DetailsQuery, ContactDto>
        {
            private readonly IWlodzimierzCachingContext _cache;
            private readonly IWlodzimierzContext _context;
            private readonly IMapper _mapper;

            public Handler(IWlodzimierzCachingContext cache, IWlodzimierzContext context, IMapper mapper)
            {
                _cache = cache;
                _context = context;
                _mapper = mapper;
            }

            public async Task<ContactDto> Handle(DetailsQuery request, CancellationToken cancellationToken)
            {
                return await _context.Contacts
                    .Where(e => e.ContactId == request.ContactId)
                    .ProjectSingleAsync<ContactDto>(_mapper.ConfigurationProvider);
            }
        }
    }
}