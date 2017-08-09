using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL.Utilities
{
    public static class Helper
    {
        /// <summary> 
        /// Return the ObjectQuery directly or convert the DbQuery to ObjectQuery. 
        /// </summary> 
        public static ObjectQuery GetObjectQuery<TEntity>(DbContext context, IQueryable query)
            where TEntity : class
        {
            if (query is ObjectQuery)
                return query as ObjectQuery;

            if (context == null)
                throw new ArgumentException("Paramter cannot be null", "context");

            // Use the DbContext to create the ObjectContext 
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
            // Use the DbSet to create the ObjectSet and get the appropriate provider. 
            IQueryable iqueryable = objectContext.CreateObjectSet<TEntity>() as IQueryable;
            IQueryProvider provider = iqueryable.Provider;

            // Use the provider and expression to create the ObjectQuery. 
            return provider.CreateQuery(query.Expression) as ObjectQuery;
        }

        /// <summary> 
        /// Use ObjectQuery to get SqlConnection and SqlCommand. 
        /// </summary> 
        public static void GetSqlCommand(ObjectQuery query, ref SqlConnection connection, ref SqlCommand command)
        {
            if (query == null)
                throw new System.ArgumentException("Paramter cannot be null", "query");

            if (connection == null)
            {
                connection = new SqlConnection(Helper.GetConnectionString(query));
            }

            if (command == null)
            {
                command = new SqlCommand(Helper.GetSqlString(query), connection);

                // Add all the paramters used in query. 
                foreach (ObjectParameter parameter in query.Parameters)
                {
                    command.Parameters.AddWithValue(parameter.Name, parameter.Value);
                }
            }
        }

        /// <summary> 
        /// Use ObjectQuery to get the connection string. 
        /// </summary> 
        public static String GetConnectionString(ObjectQuery query)
        {
            if (query == null)
            {
                throw new ArgumentException("Paramter cannot be null", "query");
            }

            EntityConnection connection = query.Context.Connection as EntityConnection;
            return connection.StoreConnection.ConnectionString;
        }

        /// <summary> 
        /// Use ObjectQuery to get the Sql string. 
        /// </summary> 
        public static String GetSqlString(ObjectQuery query)
        {
            if (query == null)
            {
                throw new ArgumentException("Paramter cannot be null", "query");
            }

            string s = query.ToTraceString();

            return s;
        }

        public static DbGeography GetGeography(double Longitude, double Latitude)
        {
            var pointString = string.Format("POINT({0} {1})", Longitude.ToString(), Latitude.ToString());
            return DbGeography.FromText(pointString);
        }
    }
}
