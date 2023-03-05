using System;

namespace RestApi.Abstractions
{
    public interface IIdentifiable
    {
        public Guid Id { get; set; }
    }
}
