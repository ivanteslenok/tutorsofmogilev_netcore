using Data.DTOs;
using System.Collections.Generic;

namespace TutorsOfMogilev_NetCore.Models
{
    public class ResumeContactsVM
    {
        public IList<PhoneDTO> Phones { get; set; }
        public IList<ContactDTO> Contacts { get; set; }
    }
}
