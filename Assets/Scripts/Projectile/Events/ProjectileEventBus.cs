using EventBus;

namespace Projectile.Events
{
	public class ProjectileEventBus : AbstractEventBus<IProjectileEvent>
	{ }
}