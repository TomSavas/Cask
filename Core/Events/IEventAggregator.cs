using System;

namespace Core.Events
{
    public interface IEventAggregator
    {        
        void PublishEvent<TEventType>(TEventType eventToPublish);
        void SubsribeEvent(Object subscriber);
    }
}
