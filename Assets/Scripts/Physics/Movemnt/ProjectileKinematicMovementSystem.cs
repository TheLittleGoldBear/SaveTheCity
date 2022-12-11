using Projectile.Events;

namespace Physics
{
	public class ProjectileKinematicMovementSystem : KinematicMovementSystem
	{
		#region PrivateFields

		private ProjectileEventBus m_projectileEventBus;

		#endregion

		#region PublicMethods

		public void Inject(ProjectileEventBus projectileEventBus)
		{
			m_projectileEventBus = projectileEventBus;
		}

		#endregion

		#region ProtectedMethods

		protected override void OnGoalReached()
		{
			base.OnGoalReached();
			m_projectileEventBus.Publish(new ProjectileExplosionEvent());
		}

		#endregion
	}
}
