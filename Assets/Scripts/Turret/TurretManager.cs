using System.Collections.Generic;
using Spawners;
using Turret.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Turret
{
	public class TurretManager : MonoBehaviour
	{
		#region Properties

		public int TurretsCount => m_turretSystems.Count;

		#endregion

		#region SerializeFields

		[SerializeField] private List<TurretSystem> m_turretSystems;
		[SerializeField] private Camera m_camera;
		[SerializeField] private ProjectileSpawner m_projectilePool;
		[SerializeField] private int m_projectileCount;

		#endregion

		#region PrivateFields

		private TurretEventBus m_turretEventBus;
		private bool m_registeredToEvents;

		#endregion

		#region UnityMethods

		private void Update()
		{
			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
				if (m_turretSystems.Count == 0)
				{
					return;
				}

				Vector3 position = m_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
				position.z = 0.0f;

				TurretSystem turretSystem = GetClosetsTurret(position);

				turretSystem.SpawnProjectile(position);

				if (turretSystem.ProjectileCount <= 0)
				{
					m_turretSystems.Remove(turretSystem);
				}
			}
		}

		#endregion

		#region PublicMethods

		public void Initialize()
		{
			m_turretEventBus = new TurretEventBus();

			for (int i = 0; i < m_turretSystems.Count; i++)
			{
				m_turretSystems[i]
					.Inject(
						m_projectilePool,
						m_projectileCount,
						m_turretEventBus
					)
					.Initialize();
			}

			RegisterToEvents();
		}

		public void OnTearDown()
		{
			for (int i = 0; i < m_turretSystems.Count; i++)
			{
				m_turretSystems[i].OnTearDown();
			}

			UnregisterFromEvents();
		}

		public int GetNotUsedProjectile()
		{
			int notUsedProjectiles = 0;

			for (int i = 0; i < m_turretSystems.Count; i++)
			{
				notUsedProjectiles += m_turretSystems[i].ProjectileCount;
			}

			return notUsedProjectiles;
		}

		public Vector3 GetRandomTurretLocation()
		{
			if (m_turretSystems.Count == 0)
			{
				return Vector3.zero;
			}

			int index = Random.Range(0, m_turretSystems.Count);

			return m_turretSystems[index]
				.transform
				.position;
		}

		#endregion

		#region PrivateMethods

		private void RegisterToEvents()
		{
			if (m_registeredToEvents)
			{
				return;
			}

			m_turretEventBus.Subscribe<TurretHitEvent>(OnTurretHit);

			m_registeredToEvents = true;
		}

		private void UnregisterFromEvents()
		{
			if (!m_registeredToEvents)
			{
				return;
			}

			m_turretEventBus.Unsubscribe<TurretHitEvent>(OnTurretHit);

			m_registeredToEvents = false;
		}

		private void OnTurretHit(TurretHitEvent turretHitEvent)
		{
			TurretSystem turretSystem = turretHitEvent.TurretSystem;

			turretSystem.OnTearDown();
			m_turretSystems.Remove(turretSystem);
		}

		private TurretSystem GetClosetsTurret(Vector3 position)
		{
			TurretSystem turretSystem = null;
			float closestDistance = float.MaxValue;

			for (int i = 0; i < m_turretSystems.Count; i++)
			{
				float distance = Vector3.Distance(position, m_turretSystems[i].transform.position);

				if (distance < closestDistance)
				{
					closestDistance = distance;
					turretSystem = m_turretSystems[i];
				}
			}

			return turretSystem;
		}

		#endregion
	}
}
