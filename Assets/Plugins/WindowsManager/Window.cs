using System;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.WindowsManager
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Canvas))]
	public abstract class Window : MonoBehaviour, IWindow
	{
		private Canvas _canvas;
		private CanvasScaler _canvasScaler;
		private GraphicRaycaster _graphicRaycaster;

		public Canvas Canvas => _canvas ? _canvas : _canvas = GetComponent<Canvas>();
		public abstract void ResetClosed();
		public abstract string WindowId { get; }
		public abstract bool HasOwnCanvas { get; }
		public abstract void Activate(bool immediately = false);
		public abstract void Deactivate(bool immediately = false);
		public abstract ActivatableState ActivatableState { get; protected set; }
		public abstract event EventHandler<ActivatableStateChangedEventArgs> ActivatableStateChangedEvent;
		public abstract bool Close(bool immediately = false);
		public abstract void SetArgs(object[] args);
		public abstract event EventHandler<WindowResultEventArgs> CloseWindowEvent;
		public abstract event EventHandler<WindowResultEventArgs> DestroyWindowEvent;
	}
}