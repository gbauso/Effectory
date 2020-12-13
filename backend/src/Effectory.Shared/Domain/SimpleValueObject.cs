namespace Effectory.Shared.Domain
{
    public abstract class SimpleValueObject<T> : IValueObject
    {
        public T Value { get; protected set; }

    }
}
