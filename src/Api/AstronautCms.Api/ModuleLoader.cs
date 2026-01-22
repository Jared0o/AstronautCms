using System.Reflection;
using AstronautCms.Shared.Abstract.Modules;



namespace AstronautCms.Api;

internal static class ModuleLoader
{
    public static IList<IModule> LoadModules()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic)
            .ToArray();

        var types = assemblies.SelectMany(a =>
            {
                try { return a.GetTypes(); }
                catch (ReflectionTypeLoadException ex) { return ex.Types.Where(t => t != null)!; }
            })
            .Where(t => typeof(IModule).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .Distinct()
            .OrderBy(t => t.Name);

        return types
            .Select(t => Activator.CreateInstance(t) as IModule)
            .Where(m => m != null)
            .Cast<IModule>()
            .ToList();
    }
}