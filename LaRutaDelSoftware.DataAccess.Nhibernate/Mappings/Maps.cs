using FluentNHibernate.Mapping;
using LaRutaDelSoftware.DomainEntities;

namespace LaRutaDelSoftware.DataAccess.Nhibernate.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("LRDS_USER");
            Id(x => x.Id, "ID").GeneratedBy.Native("SEQ_LRDSUSER");
            Map(x => x.CurrentSessionToken, "CURRENT_SESSION_TOKEN");
            Map(x => x.SessionStart, "SESSION_START");
            Map(x => x.Password, "PASSWORD");
            Map(x => x.UserName, "USERNAME");
            Map(x => x.CreationDate, "CREATION_DATE");
            Map(x => x.IsActive, "ISACTIVE");
            Map(x => x.DateOfBlock, "DATEOFBLOCK");
        }
    }

    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Table("STUDENT");
            Id(x => x.Id, "ID").GeneratedBy.Native("SEQ_STUDENT");
            Map(x => x.FirstName, "FIRST_NAME");
            Map(x => x.Surname, "SURNAME");
            Map(x => x.Mail, "MAIL");
            References(x => x.User, "USER_ID");
        }
    }


    public class SubjectMap : ClassMap<Subject>
    {
        public SubjectMap()
        {
            Table("SUBJECT");
            Id(x => x.Id, "ID").GeneratedBy.Native("SEQ_SUBJECT");
            Map(x => x.Name, "NAME");
            Map(x => x.Degree, "DEGREE");
            Map(x => x.Date, "NEXT_FINAL_DATE");
            Map(x => x.Schedule, "SCHEDULE");
        }
    }

    public class StudentSubjectMap : ClassMap<StudentSubject>
    {
        public StudentSubjectMap()
        {
            Table("STUDENT_SUBJECT");
            Id(x => x.Id, "ID").GeneratedBy.Native("SEQ_STUDENT_SUBJECT");
            References(x => x.Student, "STUDENT_ID");
            References(x => x.Subject, "SUBJECT_ID");
            Map(x => x.Registered, "IS_REGISTERED");
        }
    }
}
