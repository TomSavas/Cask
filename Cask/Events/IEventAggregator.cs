using System;

namespace Cask.Events
{
    public interface IEventAggregator
    {        
        void PublishEvent<TEventType>(TEventType eventToPublish);
        void SubsribeEvent(Object subscriber);
    }
}
