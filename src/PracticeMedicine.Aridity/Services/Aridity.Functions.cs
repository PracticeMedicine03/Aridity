using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;

using PracticeMedicine.Aridity.Util;
using PracticeMedicine.Aridity.Services;

namespace PracticeMedicine.Aridity
{
    public static class AridityF
    {
        /// <summary>
		/// Gets the main service container for Aridity.
		/// </summary>
		public static IServiceContainer Services
        {
            get { return GetRequiredService<IServiceContainer>(); }
        }

        /// <summary>
        /// Initializes the services for unit testing.
        /// This will replace the whole service container with a new container that
        /// contains only the following services:
        /// - ILoggingService (logging to Diagnostics.Trace)
        /// - IMessageService (writing to Console.Out)
        /// - IPropertyService (empty in-memory property container)
        /// - AddInTree (empty tree with no AddIns loaded)
        /// </summary>
        /// 
        /// <summary>
		/// Gets a service. Returns null if service is not found.
		/// </summary>
		//public static T GetService<T>() where T : class
  //      {
  //          return ServiceSingleton.ServiceProvider.GetService<T>();
  //      }

        /// <summary>
        /// Gets a service. Returns null if service is not found.
        /// </summary>
        public static T GetRequiredService<T>() where T : class
        {
            return ServiceSingleton.GetRequiredService<T>();
        }

        /// <summary>
        /// Returns a task that gets completed when the service is initialized.
        /// 
        /// This method does not try to initialize the service -- if no other code forces the service
        /// to be initialized, the task will never complete.
        /// </summary>
        /// <remarks>
        /// This method can be used to solve cyclic dependencies in service initialization.
        /// </remarks>
        public static Task<T> GetFutureService<T>() where T : class
        {
            return GetRequiredService<AridityServiceContainer>().GetFutureService<T>();
        }

        /// <inheritdoc see="ILoggingService"/>
		public static ILoggingService Log
        {
            get { return GetRequiredService<ILoggingService>(); }
        }
    }
}
