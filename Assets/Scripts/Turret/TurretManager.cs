using System.Collections.Generic;
using Projectile;
using Spawners;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Turret
{
	public class TurretManager : MonoBehaviour
	{
		#region SerializeFields

		[SerializeField] private List<TurretSystem> m_turretSystems;
		[SerializeField] private Camera m_camera;
		[SerializeField] private ProjectileSpawner m_projectilePool;
		[SerializeField] private int m_projectileCount;

		#endregion

		#region UnityMethods

		
		public void Initialize()
		{
			for (int i = 0; i < m_turretSystems.Count; i++)
			{
				m_turretSystems[i]
					.Inject(m_projectilePool, m_projectileCount)
					.Initialize();
			}
		}

		public int GetNotUsedProjectile()
		{
			int notUsedProjectiles = 0;

			for (int i = 0; i < m_turretSystems.Count; i++)
			{
				notUsedProjectiles += m_turretSystems[i].ProjectileCount;
			}

			return notUsedProjectiles;
		}

		private void Update()
		{
			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
				if (m_turretSystems.Count ==0)
				{
					return;
				}
				
				Vector3 position = m_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
				position.z = 0.0f;

				TurretSystem turretSystem = GetClosetsTurret(position);
				
				turretSystem.SpawnProjectile(position);

				if (turretSystem.ProjectileCount <= 0)
				{
					m_turretSystems.Remove(turretSystem);
				}
			}
		}

		#endregion

		#region PrivateMethods

		private TurretSystem GetClosetsTurret(Vector3 position)
		{
			TurretSystem turretSystem = null;
			float closestDistance = float.MaxValue;

			for (int i = 0; i < m_turretSystems.Count; i++)
			{
				float distance = Vector3.Distance(position, m_turretSystems[i].transform.position);

				if (distance < closestDistance)
				{
					closestDistance = distance;
					turretSystem = m_turretSystems[i];
				}
			}

			return turretSystem;
		}

		#endregion
	}
}
