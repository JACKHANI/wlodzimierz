using System.Linq;
using System.Threading.Tasks;
using Application.Infrastructure.Persistence.API.Interfaces;
using Application.Paging.API.Models;
using Application.Storage.API.Storage.ConversationMessages.Models;
using Application.Storage.API.Storage.Conversations.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Storage.API.Storage.Conversations.Extensions
{
    public static class ConversationExtensions
    {
        public static async Task<PaginatedList<ConversationDto>> ResidualAsync(
            this Task<PaginatedList<ConversationDto>> list, IWlodzimierzContext context, IMapper mapper)
        {
            var awaitedList = await list;

            foreach (var conversation in awaitedList.Items)
                conversation.LastMessage = await context.ConversationMessages
                    .Where(e => e.Conversation.ConversationId == conversation.ConversationId)
                    .OrderByDescending(x => x.Publish)
                    .Take(1)
                    .ProjectTo<ConversationMessageDto>(mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();

            return awaitedList;
        }
    }
}