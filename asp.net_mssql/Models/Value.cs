using System.ComponentModel.DataAnnotations;
using System;
namespace Meaurse.Models
{
     public class Value
    {
        //public Data() {}
        [Key] public int Id { get; set; }
        [Required]  public float  temperature {set; get;}
        [Required]  public float  humidity {set; get;}

        [Required] public DateTime time {set; get;}
    }
}