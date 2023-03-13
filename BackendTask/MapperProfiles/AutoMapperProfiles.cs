using AutoMapper;
using BackendTask.Models.Entities;
using BackendTask.Models.Routs.Responses;

namespace BackendTask.MapperProfiles;

internal class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TreeNode, DataBase.Models.TreeNode>();
        CreateMap<DataBase.Models.TreeNode, TreeNode>();

        CreateMap<DataBase.Models.Exception, ExceptionResponse>();
        CreateMap<ExceptionResponse, DataBase.Models.Exception>();
    }
}