using System.Collections.Generic;
using Core.Models;
using Data.DTOs;

namespace TutorsOfMogilev_NetCore.Models
{
    public class TutorsListVM
    {
        public List<TutorVM> Tutors { get; set; }
        public List<SubjectDTO> Subjects { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}