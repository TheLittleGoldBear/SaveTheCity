using System;

namespace EventBus
{
	public interface IEventBus<in T> where T : IEvent
	{
		#region PublicMethods

		void Subscribe<TU>(Action<TU> action) where TU : T;
		void Unsubscribe<TU>(Action<TU> action) where TU : T;
		void Publish<TU>(TU @event) where TU : T;

		#endregion
	}
}
