using SubContractors.Common.EfCore.Contracts;

namespace SubContractors.Common.EfCore
{
    public abstract class Entity<T> : IEntity<T>, IDeletable
    {
        public virtual T Id { get; protected set; }

        public bool IsDeleted { get; set; } = false;

        protected Entity()
        { }

        protected Entity(T id)
        {
            Id = id;
        }

        protected virtual object Actual => this;

        public override bool Equals(object obj)
        {
            var other = obj as Entity<T>;

            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (Actual.GetType() != other.Actual.GetType())
            {
                return false;
            }


            return (object) Id == (object) other.Id;
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (Actual.GetType()
                          .ToString() + Id).GetHashCode();
        }

    }

    public interface IEntity<out T>
    {
        T Id { get; }
    }
}