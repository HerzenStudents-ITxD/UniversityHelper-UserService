﻿using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Models.Project;
using UniversityHelper.UserService.Models.Dto.Models;

namespace UniversityHelper.UserService.Mappers.Models.Interfaces;

[AutoInject]
public interface IProjectInfoMapper
{
  ProjectInfo Map(ProjectData projectData, ProjectUserData projectUser);
}
