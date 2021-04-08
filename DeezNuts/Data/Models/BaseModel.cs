using System;
using System.ComponentModel.DataAnnotations;

namespace DeezNuts.Data.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public BaseModel()
        {
            this.CreatedDate = DateTime.Now;
        }
    }
}
