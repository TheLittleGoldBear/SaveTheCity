using Physics;
using Physics.Collisions.Relay;
using Pooling;
using Projectile.Events;
using UnityEngine;

namespace Projectile
{
	public class ProjectileSystem :  AbstractMonoBehaviourPoolable<ProjectilePool>
	{
		#region SerializeFields

		[SerializeField] private ProjectileKinematicMovementSystem m_kinematicMovementSystem;
		[SerializeField] private CollisionRelay m_collisionRelay;
		[SerializeField] private TriggerRelay m_detectionTriggerRelay;

		#endregion

		#region PrivateFields

		private ProjectileExplosionSystem m_projectileExplosionSystem;
		private ProjectileEventBus m_projectileEventBus;

		#endregion

		#region UnityMethods
		

		public void OnInitialize()
		{
			m_projectileEventBus = new ProjectileEventBus();

			m_projectileExplosionSystem = new ProjectileExplosionSystem(
				m_projectileEventBus,
				m_collisionRelay,
				m_detectionTriggerRelay
			);

			m_kinematicMovementSystem.Inject(m_projectileEventBus);

			m_projectileEventBus.Subscribe<ProjectileExplosionEvent>(OnProjectileReachedGoalPosition);
		}

		private void OnDestroy()
		{
			m_projectileExplosionSystem.OnTearDown();
			
			m_projectileEventBus.Unsubscribe<ProjectileExplosionEvent>(OnProjectileReachedGoalPosition);
		}

		#endregion

		#region PublicMethods

		public void Setup(Vector3 goalPosition)
		{
			m_kinematicMovementSystem.Setup(goalPosition);
			m_kinematicMovementSystem.IsEnabled = true;
		}

		public void Explode()
		{
			m_projectileExplosionSystem.OnExploded();
			ReleaseToPool();
		}

		#endregion

		#region PrivateMethods

		private void OnProjectileReachedGoalPosition(ProjectileExplosionEvent projectileExplosionEvent)
		{
			Explode();
		}

		#endregion
	}
}
