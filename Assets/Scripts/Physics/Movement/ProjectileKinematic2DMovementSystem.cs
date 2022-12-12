using Projectile;

namespace Physics.Movement
{
	public class ProjectileKinematic2DMovementSystem : Kinematic2DMovementSystem
	{
		#region PrivateFields

		private AbstractProjectileSystem m_projectileSystem;

		#endregion

		#region PublicMethods

		public void Inject(AbstractProjectileSystem projectileSystem)
		{
			m_projectileSystem = projectileSystem;
		}

		#endregion

		#region ProtectedMethods

		protected override void OnGoalReached()
		{
			base.OnGoalReached();

			m_projectileSystem.OnExplosion();
		}

		#endregion
	}
}
