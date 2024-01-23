namespace Laboratorium_3.Models
{
    public class LastVisitCookie
    {
        private readonly RequestDelegate _next;
        public static readonly string CookieName = "visit";
        public LastVisitCookie(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? cookie = context.Request.Cookies[CookieName];
            if (cookie is null) 
            {
                context.Items.Add(CookieName, "First visit");
            }
            else
            {
                if(DateTime.TryParse(cookie, out var date))
                {
                    context.Items[CookieName] = date;
                }
                else
                {
                    context.Items[CookieName] = "Unknown date of last visit";
                }
            }
            CookieOptions options = new CookieOptions() { MaxAge = new TimeSpan(400, 0, 0, 0), IsEssential = true };
            context.Response.Cookies.Append(CookieName, DateTime.Now.ToString(), options);
            await _next(context);
        }
    }
}
