using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.Entities;

namespace TutorsOfMogilev_NetCore.Models
{
    public class ResumeVM
    {
        [Required(ErrorMessage = "Введите имя")]
        [MaxLength(50, ErrorMessage = "Слишком длинное имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [MaxLength(50, ErrorMessage = "Слишком длинная фамилия")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Введите отчество")]
        [MaxLength(50, ErrorMessage = "Слишком длинное отчество")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        
        [Required(ErrorMessage = "Введите описание")]
        [MaxLength(1000, ErrorMessage = "Слишком длинное описание")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Введите информацию о своем образовании")]
        [MaxLength(500, ErrorMessage = "Превышен лимит символов")]
        [Display(Name = "Образование")]
        public string Education { get; set; }
        
        [MaxLength(500, ErrorMessage = "Превышен лимит символов")]
        [Display(Name = "Работа")]
        public string Job { get; set; }
        
        [MaxLength(300, ErrorMessage = "Превышен лимит символов")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "Введите опыт работы в годах")]
        [Display(Name = "Опыт")]
        public byte? Experience { get; set; }
        
        [Required(ErrorMessage = "Введите стоимость одного занятия с вами")]
        [DataType(DataType.Currency)]
        [Display(Name = "Стоимость одного занятия")]
        public decimal? Cost { get; set; }
        
        [Required(ErrorMessage = "Введите район в котором вы проживаете")]
        [Display(Name = "Район")]
        public District District { get; set; }
        
        public List<District> Districts { get; set; }
        
        public List<Specialization> Specializations { get; set; }
        
        public List<Subject> Subjects { get; set; }
        
        public List<ContactType> ContactTypes { get; set; }
    }
}