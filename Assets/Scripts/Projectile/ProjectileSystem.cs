using Physics;
using Physics.Collisions.Relay.Collision2D;
using Physics.Collisions.Relay.Trigger2D;
using Pooling;
using Projectile.Events;
using UnityEngine;

namespace Projectile
{
	public class ProjectileSystem : AbstractMonoBehaviourPoolable<ProjectilePool>
	{
		#region SerializeFields

		[SerializeField] private ProjectileKinematic2DMovementSystem m_kinematic2DMovementSystem;
		[SerializeField] private Collision2DRelay m_collision2DRelay;
		[SerializeField] private Trigger2DRelay m_detectionTrigger2DRelay;

		#endregion

		#region PrivateFields

		private ProjectileExplosionSystem m_projectileExplosionSystem;
		private ProjectileEventBus m_projectileEventBus;

		#endregion

		#region PublicMethods

		public void Setup(Vector3 goalPosition)
		{
			m_kinematic2DMovementSystem.Setup(goalPosition);
			m_kinematic2DMovementSystem.IsEnabled = true;
		}

		public void HitExplosion()
		{
			m_projectileEventBus.Publish(new ProjectileExplosionEvent(this));
			ReleaseToPool();
		}
		

		#endregion

		// private void OnProjectileReachedGoalPosition(ProjectileExplosionEvent projectileExplosionEvent)
		// {
		// 	Explode();
		// 	
		// }

		#region UnityMethods

		public void Initialize()
		{
			// m_projectileEventBus = new ProjectileEventBus();

			m_projectileExplosionSystem = new ProjectileExplosionSystem(m_collision2DRelay, m_detectionTrigger2DRelay);

			m_kinematic2DMovementSystem.Inject(this);

			// m_projectileEventBus.Subscribe<ProjectileExplosionEvent>(OnProjectileReachedGoalPosition);
		}

		public ProjectileSystem Inject(ProjectileEventBus projectileEventBus, ProjectilePool projectilePool)
		{
			base.Inject(projectilePool);

			m_projectileEventBus = projectileEventBus;

			return this;
		}

		private void OnDestroy()
		{
			m_projectileExplosionSystem.OnTearDown();

			// m_projectileEventBus.Unsubscribe<ProjectileExplosionEvent>(OnProjectileReachedGoalPosition);
		}

		#endregion
	}
}
