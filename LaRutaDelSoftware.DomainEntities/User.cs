using System;

namespace LaRutaDelSoftware.DomainEntities
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual string CurrentSessionToken { get; set; }
        public virtual DateTime? SessionStart { get; set; }

        public virtual bool IsActive { get; set; }
    }
}