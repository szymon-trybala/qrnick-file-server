using System;
using Microsoft.Extensions.Configuration;

namespace Qrnick.FileServer.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetUnityFilesPath(this IConfiguration configuration)
        {
            var unityFilesPath = configuration.GetValue<string>("UnityFilesPath");
            if (string.IsNullOrEmpty(unityFilesPath))
                throw new Exception();

            return unityFilesPath;
        }
    }
}