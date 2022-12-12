using Projectile;

namespace Physics
{
	public class ProjectileKinematic2DMovementSystem : Kinematic2DMovementSystem
	{
		#region PrivateFields

		private AbstractProjectileSystem m_abstractProjectileSystem;

		#endregion

		#region PublicMethods

		public void Inject(AbstractProjectileSystem abstractProjectileSystem)
		{
			m_abstractProjectileSystem = abstractProjectileSystem;
		}

		#endregion

		#region ProtectedMethods

		protected override void OnGoalReached()
		{
			base.OnGoalReached();

			m_abstractProjectileSystem.HitExplosion();
		}

		#endregion
	}
}
