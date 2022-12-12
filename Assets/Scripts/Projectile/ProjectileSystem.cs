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

		private PropagateExplosionSystem m_propagateExplosionSystem;

		#endregion

		#region PublicMethods

		public override void Initialize()
		{
			base.Initialize();
			m_propagateExplosionSystem = new PropagateExplosionSystem(m_detectionTrigger2DRelay);
		}

		public ProjectileSystem Inject(ProjectilePool projectilePool)
		{
			base.Inject(projectilePool);

			return this;
		}

		public override void OnExplosion()
		{
			m_propagateExplosionSystem.OnPropagateExplosion();
			ReleaseToPool();
		}

		#endregion

		#region ProtectedMethods

		protected override void OnTearDown()
		{
			base.OnTearDown();

			m_propagateExplosionSystem.OnTearDown();
		}

		protected override void CallOnPoolShutdown()
		{
			base.CallOnPoolShutdown();

			OnTearDown();
		}

		#endregion
	}
}
