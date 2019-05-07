using System.Collections.Generic;
using BLL.App.DTO;

namespace WebApp.ViewModels.Dashboard
{
    public class IndexViewModel
    {
        public List<BLLOrganizationDTO> Organizations { get; set; }
    }
}