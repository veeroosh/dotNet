using System;

namespace PersonalTreker.Domain
{
    public class Issue : IBoardContainer
    {
        public int Id { get; set; }

        public Board Board { get; set; }

        public String Title { get; set; }
    
        public String Description { get; set; }
    
        public String Epic { get; set; }
    
        public Boolean Status { get; set; }

        int IBoardContainer.BoardId => this.Board.Id;
    }
    
}