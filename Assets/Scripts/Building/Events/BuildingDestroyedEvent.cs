namespace Building.Events
{
	public class BuildingDestroyedEvent : IBuildingEvent
	{
		#region Properties

		public BuildingSystem BuildingSystem { get; }

		#endregion

		#region Constructors

		public BuildingDestroyedEvent(BuildingSystem buildingSystem)
		{
			BuildingSystem = buildingSystem;
		}

		#endregion
	}
}
