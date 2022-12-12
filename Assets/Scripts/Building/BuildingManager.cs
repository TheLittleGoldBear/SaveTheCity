using System.Collections.Generic;
using Building.Events;
using UnityEngine;

namespace Building
{
	public class BuildingManager : MonoBehaviour
	{
		#region SerializeFields

		[SerializeField] private List<BuildingSystem> m_buildingSystems;

		#endregion

		#region PrivateFields

		private BuildingEventBus m_buildingEventBus;
		private bool m_registeredToEvents;

		#endregion

		#region PublicMethods

		public void Initialize()
		{
			m_buildingEventBus = new BuildingEventBus();

			for (int i = 0; i < m_buildingSystems.Count; i++)
			{
				m_buildingSystems[i]
					.Inject(m_buildingEventBus)
					.Initialize();
			}

			RegisterToEvents();
		}

		public void OnTearDown()
		{
			UnregisterFromEvents();
		}

		public Vector3 GetRandomBuildingLocation()
		{
			if (m_buildingSystems.Count == 0)
			{
				return Vector3.zero;
			}

			int index = Random.Range(0, m_buildingSystems.Count);

			return m_buildingSystems[index]
				.transform
				.position;
		}

		public int GetBuildingCount()
		{
			return m_buildingSystems.Count;
		}

		#endregion

		#region PrivateMethods

		private void RegisterToEvents()
		{
			if (m_registeredToEvents)
			{
				return;
			}

			m_buildingEventBus.Subscribe<BuildingDestroyedEvent>(OnBuildingDestroyedEvent);

			m_registeredToEvents = true;
		}

		private void UnregisterFromEvents()
		{
			if (!m_registeredToEvents)
			{
				return;
			}

			m_buildingEventBus.Unsubscribe<BuildingDestroyedEvent>(OnBuildingDestroyedEvent);

			m_registeredToEvents = false;
		}

		private void OnBuildingDestroyedEvent(BuildingDestroyedEvent buildingDestroyedEvent)
		{
			BuildingSystem destroyedBuildingSystem = buildingDestroyedEvent.BuildingSystem;
			m_buildingSystems.Remove(destroyedBuildingSystem);
			destroyedBuildingSystem.OnTearDown();
			Destroy(destroyedBuildingSystem.gameObject);
		}

		#endregion
	}
}
