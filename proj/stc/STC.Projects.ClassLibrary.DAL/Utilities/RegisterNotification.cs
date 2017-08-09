using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL.Utilities
{
    public class ImmediateNotificationRegister<TEntity> : IDisposable
       where TEntity : class
    {
        private SqlConnection connection = null;
        private SqlCommand command = null;
        private IQueryable iquery = null;
        private ObjectQuery oquery = null;

        // Summary: 
        //     Occurs when a notification is received for any of the commands associated 
        //     with this ImmediateNotificationRegister object. 
        public event OnChangeEventHandler OnChanged;
        private SqlDependency dependency = null;

        /// <summary> 
        /// Initializes a new instance of ImmediateNotificationRegister class. 
        /// </summary> 
        /// <param name="query">an instance of ObjectQuery is used to get connection string and  
        /// command string to register SqlDependency nitification. </param> 
        public ImmediateNotificationRegister(ObjectQuery query)
        {
            try
            {
                this.oquery = query;

                Helper.GetSqlCommand(oquery, ref connection, ref command);

                BeginSqlDependency();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Paramter cannot be null", "query", ex);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Fails to initialize a new instance of ImmediateNotificationRegister class.", ex);
            }
        }

        /// <summary> 
        /// Initializes a new instance of ImmediateNotificationRegister class. 
        /// </summary> 
        /// <param name="context">an instance of DbContext is used to get an ObjectQuery object</param> 
        /// <param name="query">an instance of IQueryable is used to get ObjectQuery object, and then get   
        /// connection string and command string to register SqlDependency nitification. </param> 
        public ImmediateNotificationRegister(DbContext context, IQueryable query)
        {
            try
            {
                this.iquery = query;

                // Get the ObjectQuery directly or convert the DbQuery to ObjectQuery. 
                oquery = Helper.GetObjectQuery<TEntity>(context, iquery);

                Helper.GetSqlCommand(oquery, ref connection, ref command);

                BeginSqlDependency();
            }
            catch (ArgumentException ex)
            {
                if (ex.ParamName == "context")
                {
                    throw new ArgumentException("Paramter cannot be null", "context", ex);
                }
                else
                {
                    throw new ArgumentException("Paramter cannot be null", "query", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Fails to initialize a new instance of ImmediateNotificationRegister class.", ex);
            }
        }

        private void BeginSqlDependency()
        {
            // Before start the SqlDependency, stop all the SqlDependency.
            //SqlDependency.Stop(Helper.GetConnectionString(oquery));
            //SqlDependency.Start(Helper.GetConnectionString(oquery)); 
            RegisterSqlDependency();
        }

        private void RegisterSqlDependency()
        {
            if (command == null || connection == null)
            {
                throw new ArgumentException("command and connection cannot be null");
            }

            // Make sure the command object does not already have 
            // a notification object associated with it. 
            command.Notification = null;

            // Create and bind the SqlDependency object to the command object. 
            dependency = new SqlDependency(command);
            dependency.OnChange += new OnChangeEventHandler(DependencyOnChange);

            // After register SqlDependency, the SqlCommand must be executed, or we can't  
            // get the notification. 
            RegisterSqlCommand();
        }

        private void DependencyOnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                // Move the original SqlDependency event handler. 
                SqlDependency dependency = (SqlDependency)sender;
                dependency.OnChange -= DependencyOnChange;

                if (OnChanged != null)
                {
                    OnChanged(this, e);
                }

                // We re-register the SqlDependency. 
                RegisterSqlDependency();
            }
            else
            {
                string lines = "Query Registeration Error";

                // Write the string to a file.
             //   System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\test.txt");
              //  file.WriteLine(lines);

               // file.Close();
            }
        }

        private void RegisterSqlCommand()
        {
            if (connection != null && command != null)
            {
                try
                {
                    if(connection.State != System.Data.ConnectionState.Open)
                        connection.Open();
                    command.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    // log to elmah here
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                        connection.Close();
                }
            }
        }

        /// <summary> 
        /// Releases all the resources by the ImmediateNotificationRegister. 
        /// </summary> 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(Boolean disposed)
        {
            if (disposed)
            {
                if (StopSqlDependency())
                {
                    if (command != null)
                    {
                        command.Dispose();
                        command = null;
                    }

                    if (connection != null)
                    {
                        connection.Dispose();
                        connection = null;
                    }

                    OnChanged = null;
                    iquery = null;
                    dependency.OnChange -= DependencyOnChange;
                    dependency = null;
                }
            }
        }

        /// <summary> 
        /// Stops the notification of SqlDependency. 
        /// </summary> 
        /// <returns>If be success, returns true;If fails, throw the exception</returns> 
        public Boolean StopSqlDependency()
        {
            try
            {
                SqlDependency.Stop(Helper.GetConnectionString(oquery));
                return true;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Parameter cannot be null.", "query", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Fails to Stop the SqlDependency in the ImmediateNotificationRegister class.", ex);
            }
        }

        /// <summary> 
        /// The SqlConnection is got from the Query. 
        /// </summary> 
        public SqlConnection Connection
        { get { return connection; } }

        /// <summary> 
        /// The SqlCommand is got from the Query. 
        /// </summary> 
        public SqlCommand Command
        { get { return command; } }

        /// <summary> 
        /// The ObjectQuery is got from the Query. 
        /// </summary> 
        public ObjectQuery Oquery
        { get { return oquery; } }
    }
} 
