using System;
using System.Collections.Generic;
using Autofac;

namespace Ninja.MicroFx.Platform
{
    public static class IoC
    {
        private static IContainer container;
        private static readonly object localContainerKey = new object();


        public static void Initialize(IContainer intellifloContainer)
        {
            GlobalContainer = intellifloContainer;
        }

        public static object Resolve(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }
        
        /// <summary>

        /// Tries to resolve the component, but return null
        /// instead of throwing if it is not there.

        /// Useful for optional dependencies.
        /// </summary>

        /// <typeparam name="T"></typeparam>

        /// <returns></returns>
        public static T TryResolve<T>()
        {
            return TryResolve(default(T));
        }

        /// <summary>

        /// Tries to resolve the compoennt, but return the default 
        /// value if could not find it, instead of throwing.

        /// Useful for optional dependencies.
        /// </summary>

        /// <typeparam name="T"></typeparam>

        /// <param name="defaultValue">The default value.</param>

        /// <returns></returns>
        public static T TryResolve<T>(T defaultValue)
        {
            if (Container.Resolve(typeof(T)) == null)
                return defaultValue;

            return Container.Resolve<T>();
        }

        /// <summary>

        /// Resolves objects of type <see cref="T"/>

        /// </summary>
        /// <typeparam name="T"></typeparam>

        /// <returns></returns>
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
        
        /// <summary>

        /// Returns the local container if there is one otherwise the global container
        /// </summary>

        public static IContainer Container
        {
            get
            {
                var result = LocalContainer ?? GlobalContainer;
                if (result == null)

                    throw new InvalidOperationException("The container has not been initialized!");

                return result;
            }
        }

        private static IContainer LocalContainer
        {
            get
            {
                if (LocalContainerStack.Count == 0)
                    return null;

                return LocalContainerStack.Peek();
            }
        }

        private static Stack<IContainer> LocalContainerStack
        {
            get
            {
                var stack = Local.Data[localContainerKey] as Stack<IContainer>;

                if (stack == null)
                    Local.Data[localContainerKey] = stack = new Stack<IContainer>();

                return stack;
            }
        }
        /// <summary>

        /// Returns <c>true</c> if the container is initialised.

        /// </summary>
        public static bool IsInitialized
        {
            get { return GlobalContainer != null; }
        }

        internal static IContainer GlobalContainer
        {
            get
            {
                return container;
            }
            set
            {
                container = value;
            }
        }

        /// <summary>

        /// This allows you to override the global container locally
        /// Useful for scenarios where you are replacing the global container

        /// but needs to do some initializing that relies on it.
        /// </summary>

        /// <param name="localContainer"></param>

        /// <returns></returns>
        public static IDisposable UseLocalContainer(IContainer localContainer)
        {
            LocalContainerStack.Push(localContainer);
            return new DisposableAction(() => Reset(localContainer));

        }

        /// <summary>
        /// Resets the container by removing from the stack and setting it to null

        /// </summary>
        public static void Reset(IContainer containerToReset)
        {
            if (containerToReset == null)
                return;
            if (ReferenceEquals(LocalContainer, containerToReset))
            {
                LocalContainerStack.Pop();
                if (LocalContainerStack.Count == 0)
                    Local.Data[localContainerKey] = null;

                return;
            }
            if (ReferenceEquals(GlobalContainer, containerToReset))
            {
                GlobalContainer = null;

            }
        }

        /// <summary>
        /// Resets the container by removing from the stack and setting it to null

        /// </summary>
        public static void Reset()
        {
            var thisContainer = LocalContainer ?? GlobalContainer;
            Reset(thisContainer);
        }
    }

    public class DisposableAction : IDisposable
    {
        private Action action;

        public DisposableAction(Action action)
        {
            this.action = action;
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (action != null)
                action = () => Dispose();
        }

        #endregion
    }
}
