using System;

namespace Plugins.WindowsManager
{
    public enum ActivatableState
    {
        Inactive,
        Active,
        ToActive,
        ToInactive
    }

    public class ActivatableStateChangedEventArgs : EventArgs
    {
        public ActivatableState CurrentState { get; }
        public ActivatableState PreviousState { get; }

        public ActivatableStateChangedEventArgs(ActivatableState currentState, ActivatableState previousState)
        {
            CurrentState = currentState;
            PreviousState = previousState;
        }
    }

    public interface IActivatable
    {
        /// <summary>
        /// Текущее состояние активируемого объекта.
        /// </summary>
        ActivatableState ActivatableState { get; }

        /// <summary>
        /// Событие изменения текущего состояния.
        /// </summary>
        event EventHandler<ActivatableStateChangedEventArgs> ActivatableStateChangedEvent;

        /// <summary>
        /// Активировать объект.
        /// </summary>
        /// <param name="immediately">Флаг, указывающий активировать объект немедленно.</param>
        void Activate(bool immediately = false);

        /// <summary>
        /// Деактивировать объект.
        /// </summary>
        /// <param name="immediately">Флаг, указывающий деактивировать объект немедленно.</param>
        void Deactivate(bool immediately = false);
    }
}
