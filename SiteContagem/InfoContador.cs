using System.Runtime.InteropServices;

namespace SiteContagem;

public class InfoContador
{
    public static string Local { get; }
    public static string Kernel { get; }
    public static string Framework { get; }

    static InfoContador()
    {
        Local = Environment.MachineName;
        Kernel = Environment.OSVersion.VersionString;
        Framework = RuntimeInformation.FrameworkDescription;
    }
}