using System;
using Microsoft.Extensions.DependencyInjection;

namespace ElixirEngine.Behaviors
{
    /// <summary>
    ///     Represents a factory that provides access to instances of <see cref="UpdateBehavior" /> and
    ///     <see cref="DrawBehavior" /> classes.
    /// </summary>
    internal class BehaviorResolver
    {
        /// <summary>
        ///     The service provider.
        /// </summary>
        private readonly ServiceProvider _serviceProvider;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BehaviorResolver" /> class.
        /// </summary>
        /// <param name="serviceProvider">
        ///     The service provider.
        /// </param>
        public BehaviorResolver(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        ///     Resolves an instance of the <see cref="DrawBehavior" /> class matching the provided <see cref="Type" />.
        /// </summary>
        /// <param name="type">
        ///     The <see cref="Type" /> of <see cref="DrawBehavior" /> class instance to resolve.
        /// </param>
        /// <returns>
        ///     The resolved instance of the <see cref="DrawBehavior" /> class.
        /// </returns>
        public DrawBehavior ResolveDrawBehavior(Type type)
        {
            return _serviceProvider.GetService(type) as DrawBehavior;
        }

        /// <summary>
        ///     Resolves an instance of the <see cref="UpdateBehavior" /> class matching the provided <see cref="Type" />.
        /// </summary>
        /// <param name="type">
        ///     The <see cref="Type" /> of <see cref="UpdateBehavior" /> class instance to resolve.
        /// </param>
        /// <returns>
        ///     The resolved instance of the <see cref="UpdateBehavior" /> class.
        /// </returns>
        public UpdateBehavior ResolveUpdateBehavior(Type type)
        {
            return _serviceProvider.GetService(type) as UpdateBehavior;
        }
    }
}