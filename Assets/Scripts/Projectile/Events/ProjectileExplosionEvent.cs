namespace Projectile.Events
{
	public class ProjectileExplosionEvent : IProjectileEvent
	{
		#region Properties

		public ProjectileSystem ProjectileSystem { get; }

		#endregion

		#region Constructors

		public ProjectileExplosionEvent(ProjectileSystem projectileSystem)
		{
			ProjectileSystem = projectileSystem;
		}

		#endregion
	}
}
