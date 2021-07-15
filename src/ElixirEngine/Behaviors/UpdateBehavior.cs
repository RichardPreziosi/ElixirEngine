using System.Collections.Generic;
using ElixirEngine.Entities;

namespace ElixirEngine.Behaviors
{
    public abstract class UpdateBehavior
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UpdateBehavior" /> class.
        /// </summary>
        /// <param name="updatePriority">
        ///     The update priority.
        /// </param>
        protected UpdateBehavior(UpdatePriority updatePriority)
        {
            UpdatePriority = updatePriority;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UpdateBehavior" /> class.
        /// </summary>
        protected UpdateBehavior()
            : this(UpdatePriority.Normal)
        {
        }

        /// <summary>
        ///     Gets the update priority.
        /// </summary>
        public UpdatePriority UpdatePriority { get; }

        public abstract void Update(IEnumerable<Entity> entities);
    }
}