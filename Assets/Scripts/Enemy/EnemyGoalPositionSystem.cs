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

		public bool NoValidLocationAvailable()
		{
			return m_buildingManager.BuildingsCount == 0 && m_turretManager.TurretsCount == 0;
		}

		public Vector3 GetGoalLocation()
		{
			if (m_buildingManager.BuildingsCount == 0)
			{
				return m_turretManager.GetRandomTurretLocation();
			}

			if (m_turretManager.TurretsCount == 0)
			{
				return m_buildingManager.GetRandomBuildingLocation();
			}

			int number = Random.Range(0, 2);

			return number == 0
				? m_buildingManager.GetRandomBuildingLocation()
				: m_turretManager.GetRandomTurretLocation();
		}

		#endregion
	}
}
