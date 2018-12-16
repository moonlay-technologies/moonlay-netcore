namespace Moonlay.Domain
{
    using MediatR;
    using System;
    using System.Collections.Generic;

    public abstract class Entity
    {
        private int? _requestedHashCode;
        private Guid _Identity;

        public virtual Guid Identity
        {
            get
            {
                return _Identity;
            }
            protected set
            {
                _Identity = value;
            }
        }

        private List<INotification> _domainEvents;

        private bool _transient = false;

        protected virtual void MarkTransient()
        {
            _transient = true;
            _modified = false;
            _deleted = false;
        }

        public virtual bool IsTransient()
        {
            return _transient;
        }

        private bool _modified = false;

        protected virtual void MarkModified()
        {
            _modified = true;
        }

        public virtual bool IsModified()
        {
            return _modified;
        }

        private bool _deleted = false;

        protected virtual void MarkRemoved()
        {
            _deleted = true;
            _transient = false;
            _modified = false;
        }

        public virtual bool IsRemoved()
        {
            return _deleted;
        }

        public virtual void Remove()
        {
            MarkRemoved();
        }

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Identity == this.Identity;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Identity.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}