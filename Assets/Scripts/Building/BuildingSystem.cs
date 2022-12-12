using Building.Events;
using Physics.Collisions.Relay;
using Physics.Collisions.Relay.Collision2D;
using UnityEngine;

namespace Building
{
	public class BuildingSystem : MonoBehaviour
	{
		#region SerializeFields

		[SerializeField] private Collision2DRelay m_collision2DRelay;

		#endregion

		#region PrivateFields

		private BuildingEventBus m_buildingEventBus;
		private bool m_registeredToEvents;

		#endregion

		#region UnityMethods

		#endregion

		#region PublicMethods

		public BuildingSystem Inject(BuildingEventBus buildingEventBus)
		{
			m_buildingEventBus = buildingEventBus;

			return this;
		}

		public void Initialize()
		{
			RegisterToEvents();
		}

		public void OnTearDown()
		{
			UnregisterFromEvents();
		}

		#endregion

		#region PrivateMethods

		private void RegisterToEvents()
		{
			if (m_registeredToEvents)
			{
				return;
			}

			m_collision2DRelay.CollisionEnter2D += OnDestroyBuilding;

			m_registeredToEvents = true;
		}

		private void UnregisterFromEvents()
		{
			if (!m_registeredToEvents)
			{
				return;
			}

			m_collision2DRelay.CollisionEnter2D -= OnDestroyBuilding;

			m_registeredToEvents = false;
		}

		private void OnDestroyBuilding(Collision2D obj)
		{
			m_buildingEventBus.Publish(
				new
					BuildingDestroyedEvent(this)
			);
		}

		#endregion
	}
}
