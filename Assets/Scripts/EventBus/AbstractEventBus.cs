using System;
using System.Collections.Generic;

namespace EventBus
{
	public class AbstractEventBus<T> : IEventBus<T> where T : IEvent
	{
		#region PrivateFields

		private readonly Dictionary<Type, List<Delegate>> m_subscriptions = new();
		private readonly object m_lock = new();

		#endregion

		#region InterfaceImplementations

		public void Subscribe<TU>(Action<TU> action) where TU : T
		{
			Type type = typeof(TU);

			lock (m_lock)
			{
				if (!m_subscriptions.ContainsKey(type))
				{
					m_subscriptions.Add(type, new List<Delegate>());
				}

				m_subscriptions[type].Add(action);
			}
		}

		public void Unsubscribe<TU>(Action<TU> action) where TU : T
		{
			Type type = typeof(TU);

			lock (m_lock)
			{
				if (m_subscriptions.TryGetValue(type, out List<Delegate> actions))
				{
					for (int i = actions.Count - 1; i >= 0; i--)
					{
						if (actions[i].Equals(action))
						{
							actions.RemoveAt(i);
						}
					}
				}
			}
		}

		public void Publish<TU>(TU @event) where TU : T
		{
			Type type = typeof(TU);

			lock (m_lock)
			{
				if (m_subscriptions.TryGetValue(type, out List<Delegate> actions))
				{
					for (int i = actions.Count - 1; i >= 0; i--)
					{
						((Action<TU>)actions[i])?.Invoke(@event);
					}
				}
			}
		}

		#endregion
	}
}
