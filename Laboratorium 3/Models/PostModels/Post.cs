using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Reflection;
using Data.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Laboratorium_3.Models.PostModels
{
    public class Post
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Proszę podać zawartość")]
        [StringLength(maximumLength: 5000, ErrorMessage = "Zbyt dlugie imie, podaj mniejsze")]
        [Display(Name = "Zawartość")] 
        public string Content { get; set; }

        [Required(ErrorMessage = "Proszę podać imię")]
        [Display(Name = "Imie utora")]
        public string AuthorName { get; set; }

        [EmailAddress]
        [Display(Name = "Adres emailu autora")]
        public string AuthorEmail { get; set; }
        
        [Phone]
        [Display(Name = "Telefon autora")]
        [DataType(DataType.PhoneNumber)]
        public string AuthorPhone { get; set; }
        
        [Required(ErrorMessage = "Proszę podać datę publikacji")]
        [DataType(DataType.Date)]
        [Display(Name = "Data publikacji")]
        public DateTime PublicationDate { get; set; }

        [RegularExpression(@"#\w+", ErrorMessage = "Proszę podać tag na początku ze znakiem #...")]
        [Display(Name = "Tagi")]

        public string? Tags { get; set; }

        [Display(Name = "Komentarz")]
        public string? Comment { get; set;}

        [Display(Name = "Adres")]
        public string Adress { get; set; }

        [Display(Name = "Miasto")]
        public string Miasto { get; set; }

        [HiddenInput]

        [Display(Name = "Autor")]
        public int ContactId { get; set; }
        public string ContactName { get; set; }

        [ValidateNever]
        public List<SelectListItem> ContactList { get; set; }

    }
}


