using AutoMapper;

namespace Application.API.Common.Mappings.Interfaces
{
    public interface IMapFrom<T>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}