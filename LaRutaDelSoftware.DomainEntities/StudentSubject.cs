namespace LaRutaDelSoftware.DomainEntities
{
    public class StudentSubject
    {
        public virtual int Id { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual bool Registered { get; set; }
    }
}
