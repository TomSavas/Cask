using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading;
using Core.Events;

namespace Cask.Events
{
    public class EventAggregator : IEventAggregator
    {

        private Dictionary<Type, List<WeakReference>> eventSubsribers = new Dictionary<Type, List<WeakReference>>();

        private readonly object lockSubscriberDictionary = new object();

        #region IEventAggregator Members              

        /// <summary>
        /// Publish an event
        /// </summary>
        /// <typeparam name="TEventType"></typeparam>
        /// <param name="eventToPublish"></param>
        public void PublishEvent<TEventType>(TEventType eventToPublish)
        {
            var subsriberType = typeof(ISubscriber<>).MakeGenericType(typeof(TEventType));

            var subscribers = GetSubscriberList(subsriberType);
            
            List<WeakReference> subsribersToBeRemoved = new List<WeakReference>();

            foreach (var weakSubsriber in subscribers)
            {
                if (weakSubsriber.IsAlive)
                {
                    var subscriber = (ISubscriber<TEventType>)weakSubsriber.Target;

                    InvokeSubscriberEvent<TEventType>(eventToPublish, subscriber);
                }
                else
                {
                    subsribersToBeRemoved.Add(weakSubsriber);

                }

            }


            if (subsribersToBeRemoved.Any())
            {
                lock (lockSubscriberDictionary)
                {
                    foreach (var remove in subsribersToBeRemoved)
                    {
                        subscribers.Remove(remove);

                    }

                }

            }
        }
     
        /// <summary>
        /// Subribe to an event.
        /// </summary>
        /// <param name="subscriber"></param>
        public void SubsribeEvent(object subscriber)
        {
            lock (lockSubscriberDictionary)
            {
                var subsriberTypes = subscriber.GetType().GetInterfaces()
                                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));

                WeakReference weakReference = new WeakReference(subscriber);

                foreach (var subsriberType in subsriberTypes)
                {
                    List<WeakReference> subscribers = GetSubscriberList(subsriberType);

                    subscribers.Add(weakReference);

                }

            }
        }

      

        #endregion

        private void InvokeSubscriberEvent<TEventType>(TEventType eventToPublish, ISubscriber<TEventType> subscriber)
        {
            

            SynchronizationContext syncContext = SynchronizationContext.Current;

            if (syncContext == null)
            {
                syncContext = new SynchronizationContext();

            }

            syncContext.Post(s => subscriber.OnEventHandler(eventToPublish), null);
        }

        private List<WeakReference> GetSubscriberList(Type subsriberType)
        {
            List<WeakReference> subsribersList = null;

            lock (lockSubscriberDictionary)
            {
                bool found = this.eventSubsribers.TryGetValue(subsriberType, out subsribersList);

                if (!found)
                {
                    //First time create the list.

                    subsribersList = new List<WeakReference>();

                    this.eventSubsribers.Add(subsriberType, subsribersList);

                }

            }

            return subsribersList;
        }
    }
}
