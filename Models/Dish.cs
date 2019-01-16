using System.ComponentModel.DataAnnotations;
using System;
namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int ID{get;set;}
        
        [Required(ErrorMessage="Who made the dish?")]
        [MinLength(3,ErrorMessage="Chef's full name, please")]
        [MaxLength(20, ErrorMessage="Uhh, what's short for that?")]
        [Display(Name="Chef's Name: ")]
        public string ChefName {get;set;}

        [Required (ErrorMessage="What do you call your dish?")]
        [MinLength(3, ErrorMessage="That's not a real name")]
        [MaxLength(20, ErrorMessage="Too much name! make it punchier")]
        [Display(Name="Name of Dish: ")]
        
        public string DishName{get;set;}

        [Required(ErrorMessage="How Many Calories?")]
        [MinValue(0, ErrorMessage="Negative Calories? No Way!")]
        [Display(Name="# of Calories: ")]
        public int Calories{get;set;}

        [Required(ErrorMessage="How was it?")]
        [Range(0,6, ErrorMessage="Be real, how was it, though?")]
        [Display(Name="Tastiness: ")]
        public int Tastiness{get;set;}

        [Required (ErrorMessage="Tell us about your dish")]
        [MinLength(8, ErrorMessage="Tell us more...")]
        public string Description{get;set;}
        public DateTime created_at {get;set;} = DateTime.Now;
        public DateTime updated_at{get;set;} = DateTime.Now;
    }
        public class MaxValueAttribute : ValidationAttribute
        {
            private readonly int _maxValue;
            public MaxValueAttribute(int maxValue)
            {
                _maxValue = maxValue;
            }
            public override bool IsValid(object value)
            {
                return (int)value <= _maxValue;
            }
        }
        public class MinValueAttribute : ValidationAttribute
        {
            private readonly int _minValue;
            public MinValueAttribute(int minValue)
            {
                _minValue = minValue;
            }
            public override bool IsValid(object value)
            {
                return (int)value >= _minValue;
            }
        }
}