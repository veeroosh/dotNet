using System;

namespace PersonalTreker.Domain
{
    public class BoardUpdateModel : IBoardIdentity
    {
        public String Title { get; set; }
        
        public int Id { get; }
    }
}