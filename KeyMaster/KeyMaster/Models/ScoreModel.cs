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
        public int Wpm { get; set;}
        public int Errors { get; set; }
        public int Consistency { get; set; }
        public int Mark { get; set; }
        public string TextTitle { get; set; }
        
    }
}
