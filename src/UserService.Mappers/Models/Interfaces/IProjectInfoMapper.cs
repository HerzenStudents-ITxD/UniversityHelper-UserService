using HerzenHelper.Core.Attributes;
using HerzenHelper.Models.Broker.Models.Project;
using HerzenHelper.UserService.Models.Dto.Models;

namespace HerzenHelper.UserService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IProjectInfoMapper
  {
    ProjectInfo Map(ProjectData projectData, ProjectUserData projectUser);
  }
}
