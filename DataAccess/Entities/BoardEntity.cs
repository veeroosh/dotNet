using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalTreker.DataAccess.Entities
{
    public partial class BoardEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Title { get; set; }
        
        public virtual ICollection<IssueEntity> Issue { get; set; }
        
    }
}