using AutoMapper;
using Ninja.MicroFx.Domain;
using Ninja.MicroFx.Platform;
using Ninja.MicroFx.v1.Contracts;

namespace Ninja.MicroFx.v1.Impl
{
    public class AutoMapperModule : IAutoMapperModule
    {
        public void Load()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Book, BookDocument>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Id)));
        }
    }
}
