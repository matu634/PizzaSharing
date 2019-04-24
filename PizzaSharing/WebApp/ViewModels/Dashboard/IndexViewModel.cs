using System.Collections.Generic;
using BLL.App.DTO;

namespace WebApp.ViewModels.Dashboard
{
    public class IndexViewModel
    {
        public List<BLLOrganizationMinDTO> Organizations { get; set; }
    }
}