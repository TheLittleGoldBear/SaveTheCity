using Physics.Collisions.Relay.Collision2D;
using Spawners;
using Turret.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Turret
{
	public class TurretSystem : MonoBehaviour
	{
		#region Properties

		public int ProjectileCount { get; private set; }

		#endregion

		#region SerializeFields

		[SerializeField] private Transform m_spawnPosition;
		[SerializeField] private Collision2DRelay m_collision2DRelay;
		[SerializeField] private Text m_projectileCountText;

		#endregion

		#region PrivateFields

		private ProjectileSpawner m_projectileSpawner;
		private TurretEventBus m_turretEventBus;
		private bool m_registeredToEvents;

		#endregion

		#region PublicMethods

		public TurretSystem Inject(
			ProjectileSpawner projectileSpawner,
			int projectileCount,
			TurretEventBus turretEventBus
		)
		{
			m_projectileSpawner = projectileSpawner;
			ProjectileCount = projectileCount;
			m_turretEventBus = turretEventBus;

			return this;
		}

		public void Initialize()
		{
			UpdateProjectileCountText();

			RegisterToEvents();
		}

		public void OnTearDown()
		{
			UnregisterFromEvents();
		}

		public void SpawnProjectile(Vector3 goalPosition)
		{
			if (ProjectileCount <= 0)
			{
				return;
			}

			Vector3 position = m_spawnPosition.position;
			Vector3 forwardDirection = (goalPosition - position).normalized;

			m_projectileSpawner.SpawnProjectileSystem(
				position,
				forwardDirection,
				goalPosition
			);

			ProjectileCount--;
			UpdateProjectileCountText();
		}

		#endregion

		#region PrivateMethods

		private void RegisterToEvents()
		{
			if (m_registeredToEvents)
			{
				return;
			}

			m_collision2DRelay.CollisionEnter2D += OnTurretHit;

			m_registeredToEvents = true;
		}

		private void UnregisterFromEvents()
		{
			if (!m_registeredToEvents)
			{
				return;
			}

			m_collision2DRelay.CollisionEnter2D -= OnTurretHit;

			m_registeredToEvents = false;
		}

		private void OnTurretHit(Collision2D collision2D)
		{
			ProjectileCount = 0;

			m_turretEventBus.Publish(new TurretHitEvent(this));
			UpdateProjectileCountText();
		}

		private void UpdateProjectileCountText()
		{
			m_projectileCountText.text = ProjectileCount.ToString();
		}

		#endregion
	}
}
