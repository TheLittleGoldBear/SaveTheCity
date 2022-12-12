using Building;
using Turret;
using UI;

namespace Level
{
	public class PointsSystem
	{
		#region Constants

		private const int BUILDING_POINTS = 100;
		private const int NOT_USED_PROJECTILES_POINTS = 5;
		private const int SHOOTED_PROJECTILE_POINTS = 50;

		#endregion

		#region PrivateFields

		private int m_points;

		private BuildingManager m_buildingManager;
		private TurretManager m_turretManager;
		private UISystem m_uiSystem;

		#endregion

		#region Constructors

		public PointsSystem(
			BuildingManager buildingManager,
			TurretManager turretManager,
			UISystem uiSystem
		)
		{
			m_buildingManager = buildingManager;
			m_turretManager = turretManager;
			m_uiSystem = uiSystem;
		}

		#endregion

		#region PublicMethods

		public void ShootedProjectile()
		{
			m_points += SHOOTED_PROJECTILE_POINTS;

			m_uiSystem.UpdatePoints(m_points);
		}

		public void SummarizePoints()
		{
			m_points += m_buildingManager.GetBuildingCount() * BUILDING_POINTS;
			m_points += m_turretManager.GetNotUsedProjectile() * NOT_USED_PROJECTILES_POINTS;

			m_uiSystem.UpdatePoints(m_points);
		}

		#endregion
	}
}
