using Physics;
using Physics.Collisions.Relay.Collision2D;
using Pooling;
using Projectile.View;
using UnityEngine;

namespace Projectile
{
	public abstract class AbstractProjectileSystem: AbstractMonoBehaviourPoolable<IPool<IPoolable>>
	{
		#region SerializeFields

		[SerializeField] protected ProjectileKinematic2DMovementSystem m_kinematic2DMovementSystem;
		[SerializeField] protected Collision2DRelay m_collision2DRelay;

		[SerializeField] private ProjectileViewSystem m_projectileViewSystem;

		#endregion

		#region PublicMethods

		public void Setup(Vector3 goalPosition)
		{
			m_kinematic2DMovementSystem.Setup(goalPosition);
			m_kinematic2DMovementSystem.IsEnabled = true;
		}

		public virtual void HitExplosion()
		{
			ReleaseToPool();
		}

		#endregion

		#region ProtectedMethods

		protected new AbstractProjectileSystem Inject(IPool<IPoolable> projectilePool)
		{
			base.Inject(projectilePool);

			m_kinematic2DMovementSystem.Inject(this);

			return this;
		}

		protected override void CallOnReleaseToPool()
		{
			base.CallOnReleaseToPool();

			m_projectileViewSystem.ClearView();
		}

		#endregion
	}
}
