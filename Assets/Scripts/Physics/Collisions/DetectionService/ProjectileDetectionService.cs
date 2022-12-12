using Physics.Collisions.Relay.Trigger2D;
using Projectile;

namespace Physics.Collisions.DetectionService
{
	public class ProjectileDetectionService : AbstractDetectionService<ProjectileSystem>
	{
		#region Constructors

		public ProjectileDetectionService(Trigger2DRelay trigger2DRelay)
			: base(trigger2DRelay)
		{ }

		#endregion
	}
}
