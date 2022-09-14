using Factum.Modules.Access.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Modules.Access.Core.Entities
{
    internal class Access
    {
        public int Id { get; set; }
        public Guid BusinessId { get; set; }
        public DocumentId DocumentId { get; set; }
        public AccessType Type { get; set; }
        public UserId GrantedBy { get; set; }
        public UserId GrantedTo { get; set; }

        private Access()
        {

        }

        public Access(DocumentId documentId, AccessType type, UserId grantedBy, UserId grantedTo)
        {
            BusinessId = Guid.NewGuid();
            DocumentId = documentId;
            Type = type;
            GrantedBy = grantedBy;
            GrantedTo = grantedTo;
        }
    }
}
