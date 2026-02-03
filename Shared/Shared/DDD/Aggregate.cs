namespace Shared.DDD;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
    //we use list bc sequence is important
    //prive is for deny changing out of here
    //readonly for aware changing the list's reference, but we can change the 
    //this list will save domain events 
    private readonly List<IDomainEvent> _domainEvents = new();

    //for reading DomainEvents will return this : _domainEvents.AsReadOnly();
    //AsReadonly will create an wrapper that not allow to change 
    //usage : out of this class just can see events, not changing them
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    //when sth important happened, aggregate will call it
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent); 
    }

    //after saving, will give events and then clear them
    public IDomainEvent[] ClearDomainEvents()
    {
        //the reason of using array is bc its unchangable and its safe for publish
        IDomainEvent[] dequeuedEvents = _domainEvents.ToArray(); //copy from list and cast it to array

        _domainEvents.Clear(); //clear internal list and stop publish it again
        return dequeuedEvents; //will give events to dispatcher
    }
}
