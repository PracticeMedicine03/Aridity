using System;
namespace PracticeMedicine.Aridity
{
    public class Service
    {
        public static string SvcName { get; set; }
        public static Service SvcClass { get; set; }
        public static bool IsOnServiceCreatedInitalized;
        public static bool IsShuttingDown;

        public Service() 
        {
            OnServiceCreated(SvcClass);

            for(; ;)
            {
                OnServiceUpdate(SvcClass);

                if (IsShuttingDown)
                    break;
            }
        }

        protected virtual void OnServiceCreated(Service service)
        {
            if (service == null)
            {
                if(SvcClass == null)
                {
                    throw new ArgumentNullException("You must select a service class.");
                }
            }
            else if (SvcClass == null)
            {
                throw new ArgumentNullException("You must select a service class with SvcClass");
            }

            if(SvcName  == null)
            {
                throw new ArgumentNullException("Service must contain a name.");
            }

            IsOnServiceCreatedInitalized = true;
        }

        protected virtual void OnServiceUpdate(Service service)
        {
            if(!IsOnServiceCreatedInitalized)
            {
                throw new Exception("OnServiceCreated must be initalized first before executing the update function");
            }
        }

        protected virtual void OnServiceShutdown(Service service)
        {
            if(!IsOnServiceCreatedInitalized)
            {
                throw new Exception("OnServiceCreated must be initialized first/Service must be initialized first before executing the shutdown function.");
            }
            else
            {
                IsShuttingDown = true;
            }
        }
    }
}
