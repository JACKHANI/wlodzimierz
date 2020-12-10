using System.Threading;
using System.Threading.Tasks;
using Application.API.Common.Core.Exceptions;
using Application.API.Common.Infrastructure.Persistence;
using Domain.API.Entities;
using MediatR;

namespace Application.API.Storage.Contacts.Commands.Update
{
    public class UpdateCommand : IRequest
    {
        public int ContactId { get; set; }
        public string OwnerUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Photo { get; set; }

        private class Handler : IRequestHandler<UpdateCommand>
        {
            private readonly IWlodzimierzContext _context;

            public Handler(IWlodzimierzContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Contacts.FindAsync(request.ContactId) ??
                             throw new NotFoundException(nameof(Contact), request.ContactId);

                entity.OwnerUserId = request.OwnerUserId;
                entity.FirstName = request.FirstName;
                entity.LastName = request.LastName;
                entity.Email = request.Email;
                entity.Photo = request.Photo;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}