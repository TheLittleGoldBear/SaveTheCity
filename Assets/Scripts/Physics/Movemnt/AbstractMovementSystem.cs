using UnityEngine;

namespace Physics
{
	public abstract class AbstractMovementSystem : MonoBehaviour
	{
		#region Properties

		public bool IsEnabled { set; get; }

		#endregion

		#region SerializeFields

		[SerializeField] protected float m_speed;

		#endregion

		#region ProtectedFields

		protected bool m_goalReached;

		protected Vector3 m_goalPosition;

		#endregion

		#region UnityMethods

		private void Awake()
		{
			IsEnabled = false;
			OnAwake();
		}

		#endregion

		#region PublicMethods

		public void Setup(Vector3 goalPosition)
		{
			m_goalPosition = goalPosition;
			m_goalReached = false;
			IsEnabled = true;
		}

		#endregion

		#region ProtectedMethods

		protected virtual void OnGoalReached()
		{
			m_goalReached = true;
			IsEnabled = false;
		}

		protected abstract void MoveToGoalPosition(float deltaTime);
		protected abstract void CheckIfGoalReached();

		protected virtual void OnAwake()
		{ }

		#endregion
	}
}
