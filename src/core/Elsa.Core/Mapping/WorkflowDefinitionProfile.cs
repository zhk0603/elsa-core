using AutoMapper;
using Elsa.Models;
using Elsa.Services;

namespace Elsa.Mapping
{
    public class WorkflowDefinitionProfile : MappingProfile
    {
        public WorkflowDefinitionProfile()
        {
            CreateMap<WorkflowDefinitionVersion, WorkflowDefinitionVersion>();
        }
    }
}