using Quoting.Infrastructure.Bus.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Quoting.Domain.Seedworking
{
    public abstract class ConsistentModel : IConsistentModel, IEventProducer
    {
        private List<IModelInconsistency> _modelInconsistencies = new List<IModelInconsistency>();
        public IReadOnlyCollection<IModelInconsistency> ModelInconsistecies => _modelInconsistencies;

        private List<IEvent> _events = new List<IEvent>();
        public IReadOnlyCollection<IEvent> Events => _events;

        public int Id { get; set; }


        public bool IsTransient() => Id == default(int);
        public virtual bool IsConsistent()
            => !_modelInconsistencies.Any(n => n.Error);
        protected void AddInconsitency(string error)
            => _modelInconsistencies.Add(new DefaultInconsistency(error));
        protected void AddInconsitencies(IEnumerable<IModelInconsistency> inconsitencies)
            => _modelInconsistencies.AddRange(inconsitencies);
        protected void CheckChildConsistency(ConsistentModel child, string childName)
        {
            if (child == null)
                AddInconsitency($"Invalid {childName}.");
            else if (!child.IsConsistent())
                AddInconsitencies(child.ModelInconsistecies);
        }
        protected void ResetModelState()
            => _modelInconsistencies.Clear();

        public void Raise(IEvent notification)
            => _events.Add(notification);
        
    }
}
