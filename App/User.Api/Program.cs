using User.Api.AppStartup;

(WebApplicationBuilder builder, WebApplication app) = Startup.StartApp(args);

app.Run();