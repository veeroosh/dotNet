using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalTreker.DataAccess.Entities
{
    public partial class IssueEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public BoardEntity Board { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public Boolean Status { get; set; }
        public int BoardId { get; set; }
    }
}