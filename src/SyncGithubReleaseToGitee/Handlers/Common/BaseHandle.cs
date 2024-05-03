using System;

namespace SyncGithubReleaseToGitee.Handlers.Common
{

    public abstract class BaseHandle
    {
        public BaseHandle(HandleContext context)
        {
            Context = context;
        }

        public HandleContext Context { get; }

        public void Handle()
        {
            var handlerName = GetType().Name;
            try
            {
                Console.WriteLine($"{handlerName} start");
                HandleInternal();
                Console.WriteLine($"{handlerName} complete");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{handlerName} failed:{ex.Message}");
                throw ex;
            }
        }

        protected abstract void HandleInternal();
    }
}
