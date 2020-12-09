using System.Threading;
using System.Threading.Tasks;
using Application.API.Common.Infrastructure.Persistence.Interfaces;
using Domain.API.Entities;
using Domain.API.Notifications.Contacts;
using MediatR;

namespace Application.API.Storage.Contacts.Commands.Create
{
    public class CreateCommand : IRequest<int>
    {
        public string OwnerUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Photo { get; set; }

        private class Handler : IRequestHandler<CreateCommand, int>
        {
            private readonly IWlodzimierzContext _context;

            public Handler(IWlodzimierzContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateCommand request, CancellationToken cancellationToken)
            {
                var entity = new Contact
                {
                    OwnerUserId = request.OwnerUserId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Photo = request.Photo
                };

                entity.Notifications.Add(new ContactCreatedNotification(entity));
                await _context.Contacts.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.ContactId;
            }
        }
    }
}