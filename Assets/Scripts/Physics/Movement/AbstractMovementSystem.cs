using UnityEngine;

namespace Physics.Movement
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

		protected Vector3 m_goalPosition;
		protected bool m_goalReached;

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

		#endregion
	}
}
