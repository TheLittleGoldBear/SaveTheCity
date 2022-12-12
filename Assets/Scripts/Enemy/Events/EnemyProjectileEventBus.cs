using EventBus;

namespace Enemy.Events
{
	public class EnemyProjectileEventBus : AbstractEventBus<IEnemyProjectileEvent>
	{ }
}