using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WBKNET.Models.Frontdesk.VM
{
    public class ViewModel
    {
        public IEnumerable<Appointment>  Appointments { get; set; }
        public IEnumerable <CountViewModel> CountViewModels { get; set; }
    }
}
