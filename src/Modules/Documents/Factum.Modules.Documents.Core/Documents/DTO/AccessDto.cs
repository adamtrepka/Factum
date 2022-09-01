using System;

namespace Factum.Modules.Documents.Core.Documents.DTO
{
    public class AccessDto
    {
        public Guid DocumentId { get; set; }
        public string Type { get; set; }
        public Guid GrantedBy { get; set; }
        public Guid GrantedTo { get; set; }
    }
}
