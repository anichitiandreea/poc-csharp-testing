using System;

namespace UnitTesting.Abstractions
{
    public interface IIdentifiable
    {
        public Guid Id { get; set; }
    }
}
