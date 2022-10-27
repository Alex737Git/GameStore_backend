using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects.ForAuth;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;

namespace GameStore;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region User
        CreateMap<UserForRegistrationDto, User>();
        CreateMap<User, UserDto>().ReverseMap();
        #endregion

        #region Game
        CreateMap<Game, GameDto>()
            .ForMember(pd => pd.Categories, p => p.MapFrom(x => x.Categories.Select(s => s.Id)))
            .ForMember(pd => pd.OwnerId, p => p.MapFrom(x => x.User.Id))
            .ReverseMap();
        CreateMap<GameForCreationDto, Game>();
        #endregion

        #region Categories

        CreateMap<Category, CategoryDto>()
            .ReverseMap();
        CreateMap<CategoryForCreationDto, Category>();
        CreateMap<CategoryForUpdateDto, Category>();

        #endregion

        #region Comments

        CreateMap<Comment, CommentDto>()
            .ForPath(cd=>cd.FirstName, c
                =>c.MapFrom(x=>x.User.FirstName))    
            .ForPath(cd=>cd.LastName, c
                =>c.MapFrom(x=>x.User.LastName)) 
            .ForPath(cd=>cd.AvatarUrl, c
                =>c.MapFrom(x=>x.User.AvatarUrl)) 
            .ForPath(cd=>cd.UserId, c
                =>c.MapFrom(x=>x.User.Id))
            .ReverseMap();
        CreateMap<CommentForCreationDto, Comment>()
            .ForPath(c=>c.Game.Id, cf=>cf.MapFrom(x=>x.GameId)).ReverseMap();
        CreateMap<CommentForUpdateDto, Comment>();

        #endregion

        #region Role

        CreateMap<IdentityRole, RoleDto>();

        #endregion
    }
}