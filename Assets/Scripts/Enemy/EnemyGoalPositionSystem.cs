using Building;
using Turret;
using UnityEngine;

namespace Enemy
{
	public class EnemyGoalPositionSystem
	{
		#region PrivateFields

		private BuildingManager m_buildingManager;
		private TurretManager m_turretManager;

		#endregion

		#region Constructors

		public EnemyGoalPositionSystem(BuildingManager buildingManager, TurretManager turretManager)
		{
			m_buildingManager = buildingManager;
			m_turretManager = turretManager;
		}

		#endregion

		#region PublicMethods

		public Vector3 GetGoalLocation()
		{
			return m_buildingManager.GetRandomBuildingLocation();
		}

		#endregion
	}
}
