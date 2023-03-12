using AutoMapper;
using BackendTask.Models.Entities;

namespace BackendTask.MapperProfiles;

internal class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TreeNode, DataBase.Models.TreeNode>();
        CreateMap<DataBase.Models.TreeNode, TreeNode>();
    }
}