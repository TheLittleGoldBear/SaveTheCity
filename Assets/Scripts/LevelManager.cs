using System;
using Projectile;
using Turret;
using UnityEngine;

namespace DefaultNamespace
{
	public class LevelManager : MonoBehaviour
	{
		[SerializeField] private TurretManager m_turretManager;
		[SerializeField] private ProjectilePool m_projectilePool;

		private void Awake()
		{
			m_projectilePool.Initialize();

			m_turretManager.OnInitialize();
		}
	}
}
