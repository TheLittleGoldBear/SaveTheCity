namespace Projectile.Events
{
	public class ShootedProjectileEvent : IProjectileEvent
	{
		#region Properties

		public ProjectileSystem ProjectileSystem { get; }

		#endregion

		#region Constructors

		public ShootedProjectileEvent(ProjectileSystem projectileSystem)
		{
			ProjectileSystem = projectileSystem;
		}

		#endregion
	}
}
