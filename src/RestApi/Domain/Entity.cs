using System;
using UnitTesting.Abstractions;

namespace UnitTesting.Domain
{
    public abstract class Entity : IIdentifiable, IDeletable
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
