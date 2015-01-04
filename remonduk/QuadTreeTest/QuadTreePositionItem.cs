using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Remonduk.QuadTreeTest;

namespace remonduk.QuadTreeTest
{
    public class QuadTreePositionItem<T>
    {
        #region Events and Event Handlers

        /// <summary>
        /// A movement handler delegate
        /// </summary>
        /// <param name="positionItem">The item that fired the event</param>
        public delegate void MoveHandler(QuadTreePositionItem<T> positionItem);

        /// <summary>
        /// A destruction handler delegate - fired when the item is destroyed
        /// </summary>
        /// <param name="positionItem">The item that fired the event</param>
        public delegate void DestroyHandler(QuadTreePositionItem<T> positionItem);

        /// <summary>
        /// Event handler for the move action
        /// </summary>
        public event MoveHandler Move;

        /// <summary>
        /// Event handler for the destroy action
        /// </summary>
        public event DestroyHandler Destroy;

        /// <summary>
        /// Handles the move event
        /// </summary>
        protected void OnMove()
        {
            // Update rectangles
            rect.TopLeft = new Tuple<double, double>(position.Item1 - (size.Item1 * .5), position.Item2 - (size.Item2 * .5));
            rect.BottomRight = new Tuple<double, double>(position.Item1 + (size.Item1 * .5), position.Item2 + (size.Item2 * .5));
            //rect.TopLeft = position - (size * .5f);
            //rect.BottomRight = position + (size * .5f);

            // Call event handler
            if (Move != null) Move(this);
        }

        /// <summary>
        /// Handles the destroy event
        /// </summary>
        protected void OnDestroy()
        {
            if (Destroy != null) Destroy(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// The center position of this item
        /// </summary>
        private Tuple<double, double> position;

        /// <summary>
        /// Gets or sets the center position of this item
        /// </summary>
        public Tuple<double, double> Position
        {
            get { return position; }
            set
            {
                position = value;
                OnMove();
            }
        }

        /// <summary>
        /// The size of this item
        /// </summary>
        private Tuple<double, double> size;

        /// <summary>
        /// Gets or sets the size of this item
        /// </summary>
        public Tuple<double, double> Size
        {
            get { return size; }
            set
            {
                size = value;
                rect.TopLeft = new Tuple<double, double>(position.Item1 - (size.Item1 * .5), rect.TopLeft.Item2 - (size.Item2 * .5));
                rect.BottomRight = new Tuple<double, double>(position.Item1 + (size.Item1 * .5), rect.TopLeft.Item2 + (size.Item2 * .5));
                //rect.TopLeft = position - (size / 2f);
                //rect.BottomRight = position + (size / 2f);
                OnMove();
            }
        }

        /// <summary>
        /// The rectangle containing this item
        /// </summary>
        private FRect rect;

        /// <summary>
        /// Gets a rectangle containing this item
        /// </summary>
        public FRect Rect
        {
            get { return rect; }
        }

        /// <summary>
        /// The parent of this item
        /// </summary>
        /// <remarks>The Parent accessor is used to gain access to the item controlling this position item</remarks>
        private T parent;

        /// <summary>
        /// Gets the parent of this item
        /// </summary>
        public T Parent
        {
            get { return parent; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Creates a position item in a QuadTree
        /// </summary>
        /// <param name="parent">The parent of this item</param>
        /// <param name="position">The position of this item</param>
        /// <param name="size">The size of this item</param>
        public QuadTreePositionItem(T parent, Tuple<double, double> position, Tuple<double, double> size)
        {
            this.rect = new FRect(0f, 0f, 1f, 1f);

            this.parent = parent;
            this.position = position;
            this.size = size;
            OnMove();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Destroys this item and removes it from the QuadTree
        /// </summary>
        public void Delete()
        {
            OnDestroy();
        }

        #endregion
    }
}
