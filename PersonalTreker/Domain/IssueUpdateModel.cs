using System;

namespace PersonalTreker.Domain
{
    public class IssueUpdateModel: IIssueIdentity, IBoardContainer
    {
        public String Title { get; set; }
    
        public String Description { get; set; }
    
        public Boolean Status { get; set; }
        
        public int BoardId { get; }
        
        public int Id { get; }
    }
}