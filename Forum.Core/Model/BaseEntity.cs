using Forum.Core.Interfaces;

namespace Forum.Core.Model
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
