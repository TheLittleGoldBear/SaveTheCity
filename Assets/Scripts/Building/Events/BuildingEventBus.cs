using EventBus;
using Projectile.Events;

namespace Building.Events
{
	public class BuildingEventBus : AbstractEventBus<IBuildingEvent>
	{ }
}
