using HerzenHelper.Models.Broker.Models.Project;
using HerzenHelper.UserService.Mappers.Models.Interfaces;
using HerzenHelper.UserService.Models.Dto.Models;

namespace HerzenHelper.UserService.Mappers.Models
{
  public class ProjectInfoMapper : IProjectInfoMapper
  {
    public ProjectInfo Map(ProjectData projectData, ProjectUserData projectUser)
    {
      return projectData is null
        ? default
        : new ProjectInfo
        {
          Id = projectData.Id,
          Name = projectData.Name,
          ShortDescription = projectData.ShortDescription,
          ShortName = projectData.ShortName,
          Status = projectData.Status,
          User = projectUser is null
            ? default
            : new ProjectUserInfo()
            {
              IsActive = projectUser.IsActive
            }
        };
    }
  }
}
