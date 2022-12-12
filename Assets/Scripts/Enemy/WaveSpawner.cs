using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Enemy
{
	public abstract class WaveSpawner : MonoBehaviour
	{
		#region SerializeFields

		[SerializeField] private Transform m_boundaryA;
		[SerializeField] private Transform m_boundaryB;
		[SerializeField] private float m_minPositionOffset;
		[SerializeField] private int m_requiredWaveCount;
		[SerializeField] private int m_minWaveObjects;
		[SerializeField] private int m_maxWaveObjects;
		[SerializeField] private Vector2 m_minMaxWaveDelay;

		#endregion

		#region PrivateFields

		private Timer m_timer;
		private int m_waveCount;
		private bool m_initialized;
		private bool m_registeredToEvents;

		#endregion

		#region UnityMethods

		private void Update()
		{
			if (!m_initialized)
			{
				return;
			}

			m_timer.UpdateTimer(Time.deltaTime);
		}

		#endregion

		#region PublicMethods

		public void Initialize()
		{
			m_timer = new Timer();

			RegisterToEvents();
			SpawnNextWave();

			m_initialized = true;
		}

		public void OnTearDown()
		{
			UnregisterFromEvents();
		}

		#endregion

		#region ProtectedMethods

		protected abstract void OnSpawnObject(Vector3 position);

		#endregion

		#region PrivateMethods

		private void RegisterToEvents()
		{
			if (m_registeredToEvents)
			{
				return;
			}

			m_timer.Completed += SpawnNextWave;

			m_registeredToEvents = true;
		}

		private void UnregisterFromEvents()
		{
			if (!m_registeredToEvents)
			{
				return;
			}

			m_timer.Completed -= SpawnNextWave;

			m_registeredToEvents = false;
		}

		private void SpawnNextWave()
		{
			if (m_waveCount >= m_requiredWaveCount)
			{
				return;
			}

			float delay = Random.Range(m_minMaxWaveDelay.x, m_minMaxWaveDelay.y);
			m_timer.StartTimer(delay);

			int objectsCount = Random.Range(m_minWaveObjects, m_maxWaveObjects);
			List<Vector3> spawnPositions = GetRandomPosition(objectsCount);

			for (int i = 0; i < spawnPositions.Count; i++)
			{
				OnSpawnObject(spawnPositions[i]);
			}

			m_waveCount++;
		}

		private List<Vector3> GetRandomPosition(int count)
		{
			List<Vector3> randomPositions = new();

			while (randomPositions.Count < count)
			{
				float t = Random.Range(0.0f, 1.0f);
				Vector3 position = Vector3.Lerp(m_boundaryA.position, m_boundaryB.position, t);

				if (PositionIsFarEnough(randomPositions, position))
				{
					randomPositions.Add(position);
				}
			}

			return randomPositions;
		}

		private bool PositionIsFarEnough(IReadOnlyList<Vector3> positionsArray, Vector3 position)
		{
			for (int i = 0; i < positionsArray.Count; i++)
			{
				float distance = Vector3.Distance(position, positionsArray[i]);

				if (distance < m_minPositionOffset)
				{
					return false;
				}
			}

			return true;
		}

		#endregion
	}
}
