using System;
using System.Collections;
using System.Web;

namespace Ninja.MicroFx.Platform
{
    public static class Local
    {
        static readonly ILocalData current = new LocalData();

        static readonly object LocalDataHashtableKey = new object();

        private class LocalData : ILocalData
        {

            [ThreadStatic]
            static Hashtable thread_hashtable;

            private static Hashtable Local_Hashtable
            {
                get
                {
                    if (!RunningInWeb)
                    {
                        return thread_hashtable ??

                        (
                            thread_hashtable = new Hashtable()
                        );
                    }

                    Hashtable web_hashtable = HttpContext.Current.Items[LocalDataHashtableKey] as Hashtable;

                    if (web_hashtable == null)
                    {
                        HttpContext.Current.Items[LocalDataHashtableKey] = web_hashtable = new Hashtable();

                    }
                    return web_hashtable;
                }
            }

            public object this[object key]
            {
                get { return Local_Hashtable[key]; }
                set { Local_Hashtable[key] = value; }

            }

            public void Clear()
            {
                Local_Hashtable.Clear();
            }
        }


        /// <summary>
        /// Gets the current data

        /// </summary>
        /// <value>The data.</value>

        public static ILocalData Data
        {
            get { return current; }

        }

        /// <summary>
        /// Gets a value indicating whether running in the web context

        /// </summary>
        /// <value><c>true</c> if [running in web]; otherwise, <c>false</c>.</value>

        public static bool RunningInWeb
        {
            get { return HttpContext.Current != null; }

        }
    }

    public interface ILocalData
    {
        object this[object localContainerKey] { get; set; }
    }
}
