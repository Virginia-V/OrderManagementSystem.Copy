using OMS.Domain.Interfaces;

namespace OMS.Domain
{
    public abstract class Entity : IEntity
    {
        protected Entity() { }

        public int Id { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj.GetType() != GetType()) return false;
            var entity = obj as Entity;
            return entity?.Id == Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
