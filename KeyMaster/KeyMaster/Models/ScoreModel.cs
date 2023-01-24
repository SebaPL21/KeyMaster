using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeyMaster.Models
{
    public class ScoreModel
    {
        [Key]
        public int ScoresID { get; set; }
        [ForeignKey("StandardRefId")]
        public int UserId { get; set; }
        public int Cpm { get; set;}
        public int Errors { get; set; }
        public int Accuracy { get; set; }
        public string TextTitle { get; set; }
        
    }
}
