namespace ConsoleDI.Example;

internal sealed class ServiceLifetimeReporter(
    IExampleTransientService transientService,
    IExampleScopedService scopedService,
    IExampleSingletonService singletonService
)
{
    public void ReportServiceLifetimeDetails(string lifetimeDetails)
    {
        Console.WriteLine(lifetimeDetails);

        LogService(transientService, "Zawsze inne/rózne");
        LogService(scopedService, "Zmiana z nowe odwolanie");
        LogService(singletonService, "zawsze takie samo");
    }

    private static void LogService<T>(T service, string message) where T : IReportServiceLifetime =>
        Console.WriteLine(
            $"    {typeof(T).Name}: {service.Id} ({message})");
}