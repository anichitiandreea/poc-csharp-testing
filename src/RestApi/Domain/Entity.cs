using System;
using RestApi.Abstractions;

namespace RestApi.Domain
{
    public abstract class Entity : IIdentifiable, IDeletable
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
