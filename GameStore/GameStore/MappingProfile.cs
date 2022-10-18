using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.ForAuth;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;

namespace GameStore;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>();

            CreateMap<User,UserDto>();
            // CreateMap<User, UserForUpdateMe>();
            //
            //
            CreateMap<Game, GameDto>()
                 .ForMember(pd => pd.Categories, p => p.MapFrom(x => x.Categories.Select(s=>s.Id)))
                .ForMember(pd=>pd.OwnerId,p=>p.MapFrom(x=>x.User.Id))
                .ReverseMap();
             CreateMap<GameForCreationDto, Game>();
                
             // CreateMap<GameForUpdateDto, Game>();
            
            CreateMap<Category, CategoryDto>()
                .ReverseMap();
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<CategoryForUpdateDto, Category>();
            //
            // CreateMap<Comment, CommentDto>();



        }
        
       
    }
