using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DanielFinal.Data.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? Order { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}
