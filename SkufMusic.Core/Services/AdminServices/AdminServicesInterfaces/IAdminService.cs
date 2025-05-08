using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkufMusic.Core.Services.AdminServices.AdminServicesInterfaces
{
    public interface IAdminService
    {
        Task ExportProductsToCsvAsync(string filePath);
    }

}
