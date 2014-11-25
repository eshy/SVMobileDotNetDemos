using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using PicShare.Data.Repositories;

namespace PicShare.Api.Middleware
{
    public class BasicAuthenticationMiddleware : OwinMiddleware
    {
        public BasicAuthenticationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var header = context.Request.Headers["Authorization"];
            if (!string.IsNullOrWhiteSpace(header))
            {
                var authHeader = System.Net.Http.Headers
                               .AuthenticationHeaderValue.Parse(header);
                if ("Basic".Equals(authHeader.Scheme, StringComparison.OrdinalIgnoreCase))
                {
                    var parameter = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter));
                    var parts = parameter.Split(':');
                    var userName = parts[0];
                    var password = parts[1];

                    using (var repo = new AuthRepository())
                    {
                        var userIsValid = await repo.IsUserValidAsync(userName, password);

                        if (userIsValid)
                        {
                            var claims = new[]
                            {
                                new Claim(ClaimTypes.Name, userName)
                            };
                            var identity = new ClaimsIdentity(claims, "Basic");

                            context.Request.User = new ClaimsPrincipal(identity);
                        }
                    }
                }
            }

            await Next.Invoke(context);
        }
    }
}
