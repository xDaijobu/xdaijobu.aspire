var builder = DistributedApplication.CreateBuilder(args);

// MySql container is configured with an auto-generated password by default
// and supports setting the default database name via an environment variable & running *.sql/*.sh scripts in a bind mount.
var catalogDbName = "xdaijobu"; // MySql database & table names are case-sensitive on non-Windows.
var xdaijobuDb = builder.AddMySqlContainer("mysql")
    // Set the name of the database to auto-create on container startup.
    .WithEnvironment("MYSQL_DATABASE", catalogDbName)
    // Mount the SQL scripts directory into the container so that the init scripts run.
    .WithVolumeMount("../xdaijobu.ApiService/data/mysql", "/docker-entrypoint-initdb.d", VolumeMountType.Bind)
    // Add the database to the application model so that it can be referenced by other resources.
    .AddDatabase(catalogDbName);


var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.xdaijobu_ApiService>("apiservice");

builder.AddProject<Projects.xdaijobu_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(xdaijobuDb)
    .WithReference(apiService);
    

builder.Build().Run();
