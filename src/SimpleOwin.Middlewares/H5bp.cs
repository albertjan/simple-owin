﻿namespace SimpleOwin.Middlewares
{
    using System;
    using SimpleOwin.Extensions;

    using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

    public class H5bp
    {
        /// <summary>
        /// Send the IE=Edge and chrome=1 headers for IE browsers on html/htm requests.
        /// </summary>
        /// <returns></returns>
        public static Func<AppFunc, AppFunc> IeEdgeChromeFrameHeader()
        {
            return
                next =>
                env =>
                {
                    var userAgent = env.GetOwinRequestHeaderValue("user-agent");
                    if (!string.IsNullOrWhiteSpace(userAgent) && userAgent.IndexOf("MSIE", StringComparison.Ordinal) > 1)
                    {
                        // todo: only for html/htm requests
                        env.GetOwinResponseHeaders()
                            .SetOwinHeader("X-UA-Compatible", "IE=Edge,chrome=1");
                    }

                    return next(env);
                };
        }

        public static Func<AppFunc, AppFunc> RemovePoweredBy()
        {
            return
                next =>
                env =>
                {
                    env.GetOwinResponseHeaders()
                        .RemoveOwinHeader("X-Powered-By");

                    return next(env);
                };
        }
    }
}