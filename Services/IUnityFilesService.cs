using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Qrnick.FileServer.Models;

namespace Qrnick.FileServer.Services
{
    public interface IUnityFilesService
    {
        PhysicalFileResult GetDataFile(string gameId);
        PhysicalFileResult GetFrameworkFile(string gameId);
        PhysicalFileResult GetLoaderFile(string gameId);
        PhysicalFileResult GetWasmFile(string gameId);
    }
}