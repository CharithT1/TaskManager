using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using TaskManagement.Application.Configurations;

namespace TaskManagement.API.Validators.Auth
{
    public class BasicAuthorizeAttribute:TypeFilterAttribute
    {
        public BasicAuthorizeAttribute() : base(typeof(BasicAuthorizeAttributeImpl))
        {
        }

        /// <summary>
        /// Implementation
        /// </summary>
        private class BasicAuthorizeAttributeImpl : Attribute, IAsyncResourceFilter, IAuthorizationRequirement
        {
            private readonly IOptions<AuthConfigurations> _authConfig;


            public BasicAuthorizeAttributeImpl(IOptions<AuthConfigurations> authConfig)
            {
                _authConfig = authConfig;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                try
                {
                    var validUserName = _authConfig.Value.BasicAuthUser;
                    var validPassword = _authConfig.Value.BasicAuthPassword;

                    var httpContext = context.HttpContext;
                    string authHeader = httpContext.Request.Headers["Authorization"];

                    if (authHeader != null && authHeader.StartsWith("Basic"))
                    {
                        string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();

                        var encoding = Encoding.GetEncoding("iso-8859-1");
                        string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                        int seperatorIndex = usernamePassword.IndexOf(':');
                        var username = usernamePassword.Substring(0, seperatorIndex);
                        var password = usernamePassword.Substring(seperatorIndex + 1);

                        if (validUserName.Equals(username) && validPassword.Equals(password))
                        {
                            httpContext.Items.Add("UserName", username);
                            await next();
                        }
                    }
                }
                catch (Exception ex)
                {
                }

                context.Result = new JsonResult(new { Title = "Unauthorized", Error = "You do not have permission to complete the action." })
                {
                    StatusCode = Convert.ToInt16(HttpStatusCode.Forbidden)
                };

                return;
            }
        }
    }
}
