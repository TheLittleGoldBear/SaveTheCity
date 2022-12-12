using Physics.Collisions.Relay.Trigger2D;
using Projectile.Pool;
using UnityEngine;

namespace Projectile
{
	public class ProjectileSystem : AbstractProjectileSystem
	{
		#region SerializeFields

		[SerializeField] private Trigger2DRelay m_detectionTrigger2DRelay;

		#endregion

		#region PrivateFields

		private ProjectileExplosionSystem m_projectileExplosionSystem;

		#endregion

		#region PublicMethods

		public void Initialize()
		{
			m_projectileExplosionSystem = new ProjectileExplosionSystem(this, m_collision2DRelay, m_detectionTrigger2DRelay);
		}
		
		public void OnTearDown()
		{
			m_projectileExplosionSystem.OnTearDown();
		}

		public ProjectileSystem Inject(ProjectilePool projectilePool)
		{
			base.Inject(projectilePool);

			return this;
		}

		#endregion
	}
}
