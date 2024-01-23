using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public enum Priority
    {
        [Display(Name = "Niski")]
        Low,
        [Display(Name = "Normalny")]
        Normal,
        [Display(Name = "Pilny")]
        Urgent
    }
}
