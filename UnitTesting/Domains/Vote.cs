namespace UnitTesting.Domains
{
    public class Vote : Entity
    {
        public bool IsLike { get; set; }
        public bool IsDislike { get; set; }
        public Question Question { get; set; }
    }
}
