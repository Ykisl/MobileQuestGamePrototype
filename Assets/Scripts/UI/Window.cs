using System;
using Plugins.WindowsManager;
using UnityEngine;
using Zenject;

namespace UI
{
	public class Window<TDerived> : Window where TDerived : Window<TDerived>
	{
		private bool _isClosed;
		private ActivatableState _activatableState = ActivatableState.Inactive;

		public override void ResetClosed()
		{
			_isClosed = false;
		}

		public override string WindowId => GetType().Name;
		public override bool HasOwnCanvas => false;

		public override void Activate(bool immediately = false)
		{
			throw new NotImplementedException();
		}

		public override void Deactivate(bool immediately = false)
		{
			throw new NotImplementedException();
		}

		public override ActivatableState ActivatableState
		{
			get => _activatableState;
			protected set
			{
				if (value == _activatableState) return;
				var args = new ActivatableStateChangedEventArgs(value, _activatableState);
				_activatableState = value;
				ActivatableStateChangedEvent?.Invoke(this, args);
				TriggerEvents(value);
			}
		}

		private void TriggerEvents(ActivatableState value)
		{
			switch (value)
			{
				case ActivatableState.Active:
					WindowOpen();
					break;
				case ActivatableState.Inactive:
					WindowClose();
					break;
			}
		}

		private void WindowOpen()
		{
			//Placeholder
		}

		private void WindowClose()
		{
			//Placeholder
		}

		public override event EventHandler<ActivatableStateChangedEventArgs> ActivatableStateChangedEvent;

		public override bool Close(bool immediately = false)
		{
			if (_isClosed || this.IsInactiveOrDeactivated()) return false;

			if (!this.IsActive() && this.IsActiveOrActivated())
			{
				Debug.LogWarningFormat("Trying to close window {0} before it was activated.", GetType().FullName);

				void OnActivatableStateChanged(object sender, EventArgs args)
				{
					var activatableStateChangedEventArgs = (ActivatableStateChangedEventArgs) args;
					if (activatableStateChangedEventArgs.CurrentState != ActivatableState.Active) return;
					ActivatableStateChangedEvent -= OnActivatableStateChanged;
					Close(immediately);
				}

				ActivatableStateChangedEvent += OnActivatableStateChanged;
				return true;
			}

			_isClosed = true;

			var resultData = GetWindowResultData();
			var resultEventArgs = new WindowResultEventArgs(resultData);

			CloseWindowEvent?.Invoke(this, resultEventArgs);
			Deactivate(immediately);
			return true;
		}
		
		protected virtual object GetWindowResultData()
        {
			return null;
        }

		protected virtual void OnDestroy()
		{
			ActivatableStateChangedEvent = null;
			CloseWindowEvent = null;

			var resultData = GetWindowResultData();
			var resultEventArgs = new WindowResultEventArgs(resultData);

			DestroyWindowEvent?.Invoke(this, resultEventArgs);
			DestroyWindowEvent = null;

			WindowDestroy();
		}

		protected virtual void WindowDestroy() { }

		public override void SetArgs(object[] args)
		{
			throw new NotImplementedException();
		}

		public override event EventHandler<WindowResultEventArgs> CloseWindowEvent;

		public override event EventHandler<WindowResultEventArgs> DestroyWindowEvent;
	}
}