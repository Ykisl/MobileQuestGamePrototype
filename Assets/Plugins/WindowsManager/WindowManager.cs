using UnityEngine;

namespace Plugins.WindowsManager
{
    public class WindowManager : WindowManagerBase
    {
        [SerializeField] private Canvas canvas;

        protected override int StartCanvasSortingOrder => canvas.sortingOrder;
    }
}
