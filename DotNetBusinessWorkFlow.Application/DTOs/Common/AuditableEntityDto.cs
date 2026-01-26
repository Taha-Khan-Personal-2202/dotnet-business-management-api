using DotNetBusinessWorkFlow.Application.DTOs.Common;

namespace DotNetBusinessWorkFlow.Application.Common;

public class AuditableEntityDto : BaseEntityDto
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}
