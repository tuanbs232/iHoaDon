<%@ Application Codebehind="Global.asax.cs" Inherits="iHoaDon.Web.MvcApplication" Language="C#" %>
<%@ Import Namespace="iHoaDon.Entities" %>
<%@ Import Namespace="System.Threading" %>
<script RunAt="server">
void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
{
    var ctx = HttpContext.Current;
    if (ctx == null) { return; }

    var usr = ctx.User;
    if (usr == null) { return; }

    var formIdentity = usr.Identity as FormsIdentity;
    if (formIdentity == null) { return; }

    if (!(formIdentity.IsAuthenticated && formIdentity.AuthenticationType == "Forms"))
    {
        return;
    }
    
    var ticket = formIdentity.Ticket;
    if (ticket == null) { return; }

    var tvanId = new iHoaDonIdentity(ticket.Name, ticket.UserData);
    var tvanPrincipal = new iHoaDonPrincipal(tvanId);
    ctx.User = Thread.CurrentPrincipal = tvanPrincipal;
}
</script>
