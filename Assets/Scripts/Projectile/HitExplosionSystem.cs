using Physics.Collisions.Relay.Collision2D;
using UnityEngine;

namespace Projectile
{
	public class HitExplosionSystem
	{
		#region PrivateFields

		private AbstractProjectileSystem m_projectileSystem;

		private Collision2DRelay m_collision2DRelay;
		private bool m_registeredToEvents;

		#endregion

		#region Constructors

		public HitExplosionSystem(AbstractProjectileSystem projectileSystem, Collision2DRelay collision2DRelay)
		{
			m_projectileSystem = projectileSystem;
			m_collision2DRelay = collision2DRelay;

			RegisterToEvents();
		}

		#endregion

		#region PublicMethods

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

			m_collision2DRelay.CollisionEnter2D += OnCollision2DEntered;

			m_registeredToEvents = true;
		}

		private void UnregisterFromEvents()
		{
			if (!m_registeredToEvents)
			{
				return;
			}

			m_collision2DRelay.CollisionEnter2D -= OnCollision2DEntered;

			m_registeredToEvents = false;
		}

		private void OnCollision2DEntered(Collision2D collision2D)
		{
			m_projectileSystem.OnExplosion();
		}

		#endregion
	}
}
