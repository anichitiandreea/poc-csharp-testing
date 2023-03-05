using System.Collections.Generic;

namespace RestApi.Domain
{
    public class User : Entity
    {
        public string Name { get; set; }
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
