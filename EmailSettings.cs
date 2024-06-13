using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBooking.Application.DTOs
{
    public class EmailSettings
    {
        public string ServidorSMTP {  get; set; }

        public int PortaSMTP {  get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }
    }
}
