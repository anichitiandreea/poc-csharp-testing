using System;
using System.Collections.Generic;

namespace RestApi.Domain
{
    public class Question : Entity
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Answer> Answers {get; set;}
    }
}
