using NHibernate.Driver;
using System.Data.Common;

namespace LaRutaDelSoftware.DataAccess.Nhibernate
{
    public class LoggerClientDriver : OracleClientDriver
    {
        /// <summary>
        /// Warning: This method is executed twice! 
        /// (see: https://github.com/nhibernate/nhibernate-core/blob/master/src/NHibernate/Driver/DriverBase.cs)
        /// </summary>
        public override void AdjustCommand(DbCommand command)
        {
            ////log here
            //var query = command.CommandText;
            //foreach (DbParameter parameter in command.Parameters)
            //{
            //    query = query.Replace(parameter.ParameterName, parameter.Value.ToString());
            //}
            return;
        }
    }
}
