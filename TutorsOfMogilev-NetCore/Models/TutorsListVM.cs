using System.Collections.Generic;
using Core.Models;
using Data.Entities;

namespace TutorsOfMogilev_NetCore.Models
{
    public class TutorsListVM
    {
        public List<TutorVM> Tutors { get; set; }
        public List<Subject> Subjects { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
        public MenuModel Menu { get; set; }
    }
}