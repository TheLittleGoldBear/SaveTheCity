using System;
using UnityEngine;

namespace Utils
{
	public class Timer
	{
		#region Events

		public event Action Completed;

		#endregion

		#region Properties

		public bool IsCompleted { get; private set; }
		public bool IsPaused { get; private set; }
		public float ElapsedTime { get; private set; }
		public float ElapsedTimeNormalized => Duration <= 0.0f ? 0.0f : ElapsedTime / Duration;
		public float ElapsedTimeOffset => Duration <= 0.0f ? ElapsedTime : ElapsedTime % Duration;
		public float Duration { get; private set; }

		#endregion

		#region Constructors

		public Timer()
		{
			IsCompleted = true;
		}

		#endregion

		#region PublicMethods

		public void StartTimer(float duration)
		{
			IsPaused = false;
			ElapsedTime = 0.0f;

			if (duration <= 0.0f)
			{
				if (duration < 0.0f)
				{
					Debug.LogError("Timer duration shouldn't be less than 0.");
				}

				Duration = Mathf.Max(0.0f, duration);
				IsCompleted = true;
				Completed?.Invoke();

				return;
			}

			Duration = duration;
			IsCompleted = false;
		}

		public void PauseTimer()
		{
			if (IsCompleted)
			{
				return;
			}

			IsPaused = true;
		}

		public void ResumeTimer()
		{
			if (IsCompleted)
			{
				return;
			}

			IsPaused = false;
		}

		public void CancelTimer()
		{
			IsCompleted = true;
			IsPaused = false;
			ElapsedTime = 0.0f;
		}

		public void UpdateTimer(float deltaTime)
		{
			if (IsCompleted || IsPaused)
			{
				return;
			}

			ElapsedTime += deltaTime;

			if (ElapsedTime >= Duration)
			{
				IsCompleted = true;
				Completed?.Invoke();
			}
		}

		#endregion
	}
}
