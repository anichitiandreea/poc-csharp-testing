using System;

namespace UnitTesting.Domain
{
    public class Answer : Entity
    {
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public string Description { get; set; }
    }
}
