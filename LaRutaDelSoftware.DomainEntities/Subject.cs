using System;
using System.Collections.Generic;

namespace LaRutaDelSoftware.DomainEntities
{
    public class Subject
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Degree { get; set; }
        public virtual string Schedule { get; set; }
        /// <summary>
        /// Fecha en la que se cursa o rinde la materia.
        /// </summary>
        public virtual DateTime Date { get; set; }
    }
}
