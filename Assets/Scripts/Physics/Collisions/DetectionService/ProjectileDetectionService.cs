using Physics.Collisions.Relay;
using Projectile;

namespace Physics.Collisions.DetectionService
{
	public class ProjectileDetectionService : AbstractDetectionService<ProjectileSystem>
	{
		#region Constructors

		public ProjectileDetectionService(TriggerRelay triggerRelay)
			: base(triggerRelay)
		{ }

		#endregion
	}
}
