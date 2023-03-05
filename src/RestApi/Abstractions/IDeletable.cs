namespace RestApi.Abstractions
{
    public interface IDeletable
    {
        public bool IsDeleted { get; set; }
    }
}
