using Projectile;
using Projectile.Events;

namespace Physics
{
	public class ProjectileKinematic2DMovementSystem : Kinematic2DMovementSystem
	{
		#region PrivateFields

		private ProjectileSystem m_projectileSystem;

		#endregion

		#region PublicMethods

		public void Inject(ProjectileSystem projectileSystem)
		{
			m_projectileSystem = projectileSystem;
		}

		#endregion

		#region ProtectedMethods

		protected override void OnGoalReached()
		{
			base.OnGoalReached();

			m_projectileSystem.HitExplosion();
		}

		#endregion
	}
}
