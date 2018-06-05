using System.Collections.Generic;

namespace LaRutaDelSoftware.DomainEntities
{
    public class Student 
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Mail { get; set; }
        public virtual User User { get; set; }
    }
}
