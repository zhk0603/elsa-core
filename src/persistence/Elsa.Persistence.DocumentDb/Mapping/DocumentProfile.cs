using AutoMapper;
using Elsa.Models;
using Elsa.Persistence.DocumentDb.Documents;
using Elsa.Services;

namespace Elsa.Persistence.DocumentDb.Mapping
{
    public class DocumentProfile : MappingProfile
    {
        public DocumentProfile()
        {
            CreateMap<WorkflowDefinitionVersion, WorkflowDefinitionVersionDocument>().ReverseMap();
            CreateMap<WorkflowInstance, WorkflowInstanceDocument>().ReverseMap();
        }
    }
}
