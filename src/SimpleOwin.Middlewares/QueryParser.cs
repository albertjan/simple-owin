﻿namespace SimpleOwin.Middlewares
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SimpleOwin.Extensions;

    using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

    public class QueryParser
    {
        public static Func<AppFunc, AppFunc> Middleware(Func<string, string> urlDecoder = null)
        {
            if (urlDecoder == null)
            {
                // todo: set default url decoder
                urlDecoder = str => str;
            }

            return
                next =>
                env =>
                {
                    env["simpleOwin.query"] = new Lazy<IDictionary<string, string[]>>(() => GetQuerystring(env, urlDecoder));
                    return next(env);
                };
        }

        private static IDictionary<string, string[]> GetQuerystring(IDictionary<string, object> env, Func<string, string> urlDecoder)
        {
            var querystring = env.GetOwinRequestQueryString();

            var queryDictionary = new Dictionary<string, List<string>>();

            foreach (var kvp in querystring.Split('&'))
            {
                var parts = kvp.Split('=');
                if (!queryDictionary.ContainsKey(parts[0]))
                    queryDictionary.Add(parts[0], new List<string>());

                queryDictionary[parts[0]].Add(parts.Length == 2 ? urlDecoder(parts[1]) : string.Empty);
            }

            return queryDictionary.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray());
        }
    }
}