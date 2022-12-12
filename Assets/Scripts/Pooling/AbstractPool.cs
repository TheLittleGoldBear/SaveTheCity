using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
	public abstract class AbstractPool<T> : MonoBehaviour, IPool<IPoolable>
		where T : MonoBehaviour, IPoolable
	{
		#region SerializeFields

		[SerializeField] protected Transform m_poolRoot;
		[SerializeField] protected int m_capacity;
		[SerializeField] protected bool m_isExpandable;

		#endregion

		#region PrivateFields

		private bool m_initialized;

		private List<T> m_activeObjects = new();
		private Stack<T> m_inactiveObjects = new();

		#endregion

		#region InterfaceImplementations

		public void Initialize()
		{
			if (m_initialized)
			{
				return;
			}

			for (int i = 0; i < m_capacity; i++)
			{
				Insert(CreateObjectPool());
			}

			m_initialized = true;
		}

		public IPoolable Get()
		{
			T poolable;

			if (m_inactiveObjects.Count > 0)
			{
				poolable = m_inactiveObjects.Pop();
				poolable.OnGetFromPool();
			}
			else
			{
				if (m_isExpandable)
				{
					poolable = CreateObjectPool();
					poolable.OnInsertToPool();
				}
				else
				{
					if (m_activeObjects.Count == 0)
					{
						Debug.LogError("There ain't any active objects and the pool is not expendable");

						return null;
					}

					poolable = m_activeObjects[0];
					poolable.OnReleaseToPool();
				}
			}

			poolable.OnGetFromPool();
			m_activeObjects.Add(poolable);

			return poolable;
		}

		public void Insert(IPoolable objectToInsert)
		{
			if (objectToInsert == null)
			{
				Debug.LogWarning("The object to insert is null");

				return;
			}

			objectToInsert.OnInsertToPool();
			m_inactiveObjects.Push((T)objectToInsert);
		}

		public void Release(IPoolable objectToRelease)
		{
			if (!m_initialized)
			{
				return;
			}

			if (objectToRelease == null)
			{
				Debug.LogWarning("The object to Release is null");

				return;
			}

			if (objectToRelease.IsInPool)
			{
				return;
			}

			if (m_inactiveObjects.Contains((T)objectToRelease))
			{
				Debug.LogWarning("Unable to return an object. It is already on the stack");
			}
			else
			{
				objectToRelease.OnReleaseToPool();
				m_inactiveObjects.Push((T)objectToRelease);
			}
		}

		public void ReleaseAll()
		{
			for (int i = 0; i < m_activeObjects.Count; i++)
			{
				m_activeObjects[i].ReleaseToPool();
			}
		}

		public void Shutdown()
		{
			for (int i = 0; i < m_activeObjects.Count; i++)
			{
				m_activeObjects[i].OnPoolShutdown();
			}

			if (m_inactiveObjects.Count == 0)
			{
				return;
			}

			T[] inActiveObjectsArray = m_inactiveObjects.ToArray();

			for (int i = 0; i < inActiveObjectsArray.Length; i++)
			{
				inActiveObjectsArray[i].OnPoolShutdown();
			}
		}

		#endregion

		#region ProtectedMethods

		protected abstract T CreateObjectPool();

		#endregion
	}
}
