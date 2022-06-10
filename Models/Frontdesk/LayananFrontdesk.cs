using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAlpha.Models.Frontdesk
{
    public class LayananFrontdesk
    {
        public int LayananFrontdeskID { get; set; }

        public string NamaLayanan { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

    }
}
