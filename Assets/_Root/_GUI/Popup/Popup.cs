using System.Collections.Generic;

namespace Gamee.Hiuk.Popup
{
    public class Popup
    {
        /// <summary>
        /// stack contains all popup (LIFO)
        /// </summary>
        private readonly Stack<IPopupHandler> _stacks = new Stack<IPopupHandler>();

        /// <summary>
        /// hide popup in top stack
        /// </summary>
        public void Hide()
        {
            if(_stacks.Count > 0) _stacks.Pop().Close();
            var orderOfBoard = 0;
            if (_stacks.Count > 1)
            {
                var stop = _stacks.Peek();
                orderOfBoard = stop.Canvas.sortingOrder - 10;
            }
        }

        /// <summary>
        /// hide all popup in top stack
        /// </summary>
        public void HideAll()
        {
            var count = _stacks.Count;
            for (int i = 0; i < count; i++)
            {
                _stacks.Pop().ThisGameObject.SetActive(false);
            }
        }

        /// <summary>
        /// show popup
        /// </summary>
        /// <param name="uniPopupHandler">popup wanna show</param>
        public void Show(IPopupHandler uniPopupHandler)
        {
            var lastOrder = 0;
            if (_stacks.Count > 0)
            {
                var top = _stacks.Peek();
                lastOrder = top.Canvas.sortingOrder;
            }

            uniPopupHandler.UpdateSortingOrder(lastOrder + 10);
            _stacks.Push(uniPopupHandler);
            uniPopupHandler.Show(); // show
        }

        /// <summary>
        /// show popup and hide previous popup
        /// </summary>
        /// <param name="uniPopupHandler">popup wanna show</param>
        /// <param name="number">number previous popup wanna hide</param>
        public void Show(IPopupHandler uniPopupHandler,
            int number)
        {
            if (number > _stacks.Count)
            {
                number = _stacks.Count;
            }

            for (int i = 0; i < number; i++)
            {
                var p = _stacks.Pop();
                p.Close();
            }

            Show(uniPopupHandler);
        }

        /// <summary>
        /// show popup and hide all previous popup
        /// </summary>
        /// <param name="uniPopupHandler">popup wanna show</param>
        public void ShowAndHideAll(IPopupHandler uniPopupHandler)
        {
            Show(uniPopupHandler, _stacks.Count);
        }

        /// <summary>
        /// check has exist <paramref name="uniPopupHandler"/> in active stack
        /// </summary>
        /// <param name="uniPopupHandler"></param>
        /// <returns></returns>
        public bool HasPoup(IPopupHandler uniPopupHandler)
        {
            foreach (var handler in _stacks)
            {
                if (handler == uniPopupHandler)
                {
                    return true;
                }
            }

            return false;
        }
    }
}