using System;

namespace PersonalTreker.Domain
{
    public class BoardUpdateModel : IBoardIdentity, IBoardContainer
    {
        public String Title { get; set; }
        
        public int Id { get; }
        public int? BoardId { get; }
    }
}