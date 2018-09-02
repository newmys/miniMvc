using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Artech.MiniMvc
{
    /// <summary>
    /// ASP.NET MVC 默认的路由实现
    /// </summary>
    public class Route : RouteBase
    {
        public IRouteHandler RouteHandler { get; set; }
        public Route()
        {
            this.DataTokens = new Dictionary<string, object>();
            this.RouteHandler = new MvcRouteHandler();
        }
        /// <summary>
        /// 从当前 URL 中提取路由的 Key-Value 并封装到 RouteData 中
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            IDictionary<string, object> variables;
            if (this.Match(httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2), out variables))
            {
                RouteData routeData = new RouteData();
                foreach (var item in variables)
                {
                    routeData.Values.Add(item.Key, item.Value);
                }
                foreach (var item in DataTokens)
                {
                    routeData.DataTokens.Add(item.Key, item.Value);
                }
                routeData.RouteHandler = this.RouteHandler;
                return routeData;
            }
            return null;
        }

        public string Url { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public object Defaults { get; set; }

        public IDictionary<string, object> DataTokens { get; set; }

        protected virtual bool Match(string requestUrl, out IDictionary<string,object> variables)
        {
            variables = new Dictionary<string,object>();
            string[] strArray1 = requestUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string[] strArray2 = this.Url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray1.Length != strArray2.Length)
            {
                return false;
            }

            for (int i = 0; i < strArray2.Length; i++)
            { 
                if(strArray2[i].StartsWith("{") && strArray2[i].EndsWith("}"))
                {
                    variables.Add(strArray2[i].Trim("{}".ToCharArray()),strArray1[i]);
                }
            }
            return true;
        }

        private IDictionary<string, object> _defaultValueDictionary;

        /// <summary>
        /// 默认值集合
        /// </summary>
        protected IDictionary<string, object> DefaultValueDictionary
        {
            get
            {
                if (_defaultValueDictionary != null)
                {
                    return _defaultValueDictionary;
                }
                _defaultValueDictionary = new Dictionary<string, object>();
                if (Defaults != null)
                {
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(Defaults))
                    {
                        _defaultValueDictionary.Add(descriptor.Name, descriptor.GetValue(Defaults));
                    }
                }
                return _defaultValueDictionary;
            }
        }
    }

    public class RegexRoute : Route
    {
        private List<string> _routeParameterList;

        /// <summary>
        /// 得到当前注册的路由中的参数
        /// </summary>
        protected List<string> RouteParameterList
        {
            get
            {
                if (_routeParameterList != null)
                {
                    return _routeParameterList;
                }
                Regex reg1 = new Regex(@"\{(.+?)\}", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
                MatchCollection matchCollection = reg1.Matches(this.Url);
                _routeParameterList = new List<string>();
                if (matchCollection.Count == 0)
                {
                    return _routeParameterList;
                }
                foreach (Match matchItem in matchCollection)
                {
                    string value = matchItem.Groups[1].Value;
                    if (!string.IsNullOrEmpty(value))
                    {
                        _routeParameterList.Add(Regex.Escape(value));
                    }
                }
                return _routeParameterList;
            }
        }

        private string _urlRegexPattern;

        protected string UrlRegexPattern
        {
            get
            {
                if (_urlRegexPattern != null)
                {
                    return _urlRegexPattern;
                }
                _urlRegexPattern = Regex.Escape(this.Url).Replace("\\{", "{").Replace(@"\\}", "}");
                foreach (string param in RouteParameterList)
                {
                    _urlRegexPattern = _urlRegexPattern.Replace("{" + param + "}", @"(?<" + param + ">.*?)");
                }
                _urlRegexPattern = "^" + _urlRegexPattern + "/?$";
                return _urlRegexPattern; // 比如： ^(?<controller>.*?)/(?<action>.*?)$
            }
        }

        protected override bool Match(string requestUrl, out IDictionary<string, object> variables)
        {
            variables = new Dictionary<string, object>();

            int tempIndex = requestUrl.IndexOf("?");
            if (tempIndex > -1)
            {
                if (tempIndex > 0)
                {
                    requestUrl = requestUrl.Substring(0, tempIndex);
                }
                else
                {
                    requestUrl = string.Empty;
                }
            }
            if (!requestUrl.EndsWith("/"))
            {
                requestUrl += "/";
            }

            Regex routeRegex = new Regex(UrlRegexPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match match = routeRegex.Match(requestUrl);
            if (!match.Success)
            {
                return false;
            }

            foreach (string item in RouteParameterList)
            {
                string value = match.Groups[item].Value.ToLower();
                if (string.IsNullOrEmpty(value) && DefaultValueDictionary.ContainsKey(item))
                {
                    value = DefaultValueDictionary[item].ToString();
                }
                variables.Add(item, value);
            }
            if (!variables.ContainsKey("controller"))
            {
                throw new HttpException("从当前路由中没有找到 controller.");
            }
            if (!variables.ContainsKey("action"))
            {
                throw new HttpException("从当前路由中没有找到 action.");
            }
            return true;
        }
    }
}
