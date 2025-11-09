using AutoMapper;
using Portfolyo.Business.Features.Experience.CreateExperience;
using Portfolyo.Business.Features.Experience.UpdateExperience;
using Portfolyo.Business.Features.Profiles.CreateProfile;
using Portfolyo.Business.Features.Profiles.UpdateProfile;
using Portfolyo.Business.Features.Projects.CreateProject;
using Portfolyo.Business.Features.Projects.UpdateProject;
using Portfolyo.Business.Features.Skills.CreateSkill;
using Portfolyo.Business.Features.Skills.UpdateSkill;
using Portfolyo.Entities.Models;

namespace Portfolyo.Business.Mapping
{
    internal sealed class MappingProfile: AutoMapper.Profile
    {

        public MappingProfile()
        {
            CreateMap<CreateProfileCommand, Portfolyo.Entities.Models.Profile>();

            CreateMap<UpdateProfileCommand, Portfolyo.Entities.Models.Profile>();

            CreateMap<CreateProjectCommand, Project>();

            CreateMap<UpdateProjectCommand, Project>();

            CreateMap<CreateSkillCommand, Skill>();

            CreateMap<UpdateSkillCommand, Skill>();

            CreateMap<CreateExperienceCommand, Experience>();
            CreateMap<UpdateExperienceCommand, Experience>();

        }


    }
}
