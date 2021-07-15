using System;
using System.Threading;
using ElixirEngine.Behaviors;
using ElixirEngine.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ElixirEngine
{
    /// <inheritdoc cref="IApplication" />
    public abstract class Application : IApplication, IDisposable
    {
        /// <summary>
        ///     The global time.
        /// </summary>
        private readonly GlobalTime _globalTime;

        /// <summary>
        ///     The keyboard.
        /// </summary>
        private readonly Keyboard _keyboard;

        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        ///     The mouse.
        /// </summary>
        private readonly Mouse _mouse;

        /// <summary>
        ///     The service provider.
        /// </summary>
        private readonly ServiceProvider _serviceProvider;

        /// <summary>
        ///     The settings.
        /// </summary>
        private readonly ISettings _settings;

        /// <summary>
        ///     The time.
        /// </summary>
        private readonly ITime _time;

        /// <summary>
        ///     The window.
        /// </summary>
        private readonly Window _window;

        /// <summary>
        ///     The accumulated sleep milliseconds.
        /// </summary>
        private float _accumulatedSleepMilliseconds;

        /// <summary>
        ///     The sleep microseconds per frame.
        /// </summary>
        private float _sleepMicrosecondsPerFrame = -1.0f;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Application" /> class.
        /// </summary>
        protected Application()
        {
            _serviceProvider = CreateServiceProvider();

            _settings = _serviceProvider.GetService<ISettings>();
            _window = _serviceProvider.GetService<IWindow>() as Window;
            _globalTime = _serviceProvider.GetService<IGlobalTime>() as GlobalTime;
            _time = _serviceProvider.GetService<ITime>();
            _keyboard = _serviceProvider.GetService<IKeyboard>() as Keyboard;
            _mouse = _serviceProvider.GetService<IMouse>() as Mouse;
            _logger = _serviceProvider.GetService<ILogger<IApplication>>();

            ServiceProvider CreateServiceProvider()
            {
                ServiceCollection serviceCollection = new();

                serviceCollection.AddSingleton<IApplication>(this);
                serviceCollection.AddSingleton<ISettings, Settings>();
                serviceCollection.AddSingleton<IWindow, Window>();
                serviceCollection.AddSingleton<IGlobalTime, GlobalTime>();
                serviceCollection.AddSingleton<ITime, Time>();
                serviceCollection.AddSingleton<IKeyboard, Keyboard>();
                serviceCollection.AddSingleton<IMouse, Mouse>();
                serviceCollection.AddSingleton<BehaviorResolver>();
                serviceCollection.AddLogging(OnConfigureLoggers);

                OnConfigureServices(serviceCollection);

                return serviceCollection.BuildServiceProvider();
            }
        }

        /// <summary>
        ///     Gets the service provider.
        /// </summary>
        protected IServiceProvider ServiceProvider => _serviceProvider;

        /// <summary>
        ///     Runs the application and begins the game loop.
        /// </summary>
        public virtual void Run()
        {
            _window.Open();

            do
            {
                // TODO: Clear the Graphics Device
                _globalTime.Update();

                // TODO: Update Entities
                // TODO: Draw Entities
                // TODO: Present the Graphics Device
                ProcessEvents();
                _keyboard.Update();
                _mouse.Update();
                Sleep();
            } while (!_window.IsClosing);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _serviceProvider?.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Register loggers into the provided <see cref="ILoggingBuilder" />.
        /// </summary>
        /// <param name="loggingBuilder">
        ///     The <see cref="ILoggingBuilder" /> to add the loggers to.
        /// </param>
        protected virtual void OnConfigureLoggers(ILoggingBuilder loggingBuilder)
        {
        }

        /// <summary>
        ///     Register services into the provided <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="serviceCollection">
        ///     The <see cref="IServiceCollection" /> to add the services to.
        /// </param>
        protected virtual void OnConfigureServices(IServiceCollection serviceCollection)
        {
        }

        /// <summary>
        ///     Processes <see cref="SDL.SDL_EventType" /> type events.
        /// </summary>
        private void ProcessEvents()
        {
            while (SDL.SDL_PollEvent(out SDL.SDL_Event e) != 0)
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_FIRSTEVENT:
                        break;
                    case SDL.SDL_EventType.SDL_QUIT:
                        break;
                    case SDL.SDL_EventType.SDL_APP_TERMINATING:
                        break;
                    case SDL.SDL_EventType.SDL_APP_LOWMEMORY:
                        break;
                    case SDL.SDL_EventType.SDL_APP_WILLENTERBACKGROUND:
                        break;
                    case SDL.SDL_EventType.SDL_APP_DIDENTERBACKGROUND:
                        break;
                    case SDL.SDL_EventType.SDL_APP_WILLENTERFOREGROUND:
                        break;
                    case SDL.SDL_EventType.SDL_APP_DIDENTERFOREGROUND:
                        break;
                    case SDL.SDL_EventType.SDL_LOCALECHANGED:
                        break;
                    case SDL.SDL_EventType.SDL_DISPLAYEVENT:
                        break;
                    case SDL.SDL_EventType.SDL_WINDOWEVENT:
                        _window.ProcessEvent(e.window);
                        break;
                    case SDL.SDL_EventType.SDL_SYSWMEVENT:
                        break;
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        _keyboard.ProcessKeyDownEvent(e.key);
                        break;
                    case SDL.SDL_EventType.SDL_KEYUP:
                        _keyboard.ProcessKeyUpEvent(e.key);
                        break;
                    case SDL.SDL_EventType.SDL_TEXTEDITING:
                        break;
                    case SDL.SDL_EventType.SDL_TEXTINPUT:
                        break;
                    case SDL.SDL_EventType.SDL_KEYMAPCHANGED:
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEMOTION:
                        _mouse.ProcessMouseMotionEvent(e.motion);
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        _mouse.ProcessKeyDownEvent(e.button);
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        _mouse.ProcessKeyUpEvent(e.button);
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEWHEEL:
                        _mouse.ProcessMouseWheelEvent(e.wheel);
                        break;
                    case SDL.SDL_EventType.SDL_JOYAXISMOTION:
                        break;
                    case SDL.SDL_EventType.SDL_JOYBALLMOTION:
                        break;
                    case SDL.SDL_EventType.SDL_JOYHATMOTION:
                        break;
                    case SDL.SDL_EventType.SDL_JOYBUTTONDOWN:
                        break;
                    case SDL.SDL_EventType.SDL_JOYBUTTONUP:
                        break;
                    case SDL.SDL_EventType.SDL_JOYDEVICEADDED:
                        break;
                    case SDL.SDL_EventType.SDL_JOYDEVICEREMOVED:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERAXISMOTION:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERBUTTONUP:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERDEVICEADDED:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERDEVICEREMAPPED:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERTOUCHPADDOWN:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERTOUCHPADMOTION:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERTOUCHPADUP:
                        break;
                    case SDL.SDL_EventType.SDL_CONTROLLERSENSORUPDATE:
                        break;
                    case SDL.SDL_EventType.SDL_FINGERDOWN:
                        break;
                    case SDL.SDL_EventType.SDL_FINGERUP:
                        break;
                    case SDL.SDL_EventType.SDL_FINGERMOTION:
                        break;
                    case SDL.SDL_EventType.SDL_DOLLARGESTURE:
                        break;
                    case SDL.SDL_EventType.SDL_DOLLARRECORD:
                        break;
                    case SDL.SDL_EventType.SDL_MULTIGESTURE:
                        break;
                    case SDL.SDL_EventType.SDL_CLIPBOARDUPDATE:
                        break;
                    case SDL.SDL_EventType.SDL_DROPFILE:
                        break;
                    case SDL.SDL_EventType.SDL_DROPTEXT:
                        break;
                    case SDL.SDL_EventType.SDL_DROPBEGIN:
                        break;
                    case SDL.SDL_EventType.SDL_DROPCOMPLETE:
                        break;
                    case SDL.SDL_EventType.SDL_AUDIODEVICEADDED:
                        break;
                    case SDL.SDL_EventType.SDL_AUDIODEVICEREMOVED:
                        break;
                    case SDL.SDL_EventType.SDL_SENSORUPDATE:
                        break;
                    case SDL.SDL_EventType.SDL_RENDER_TARGETS_RESET:
                        break;
                    case SDL.SDL_EventType.SDL_RENDER_DEVICE_RESET:
                        break;
                    case SDL.SDL_EventType.SDL_USEREVENT:
                        break;
                    case SDL.SDL_EventType.SDL_LASTEVENT:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }

        /// <summary>
        ///     Sleeps for any remaining time for the current frame.
        /// </summary>
        private void Sleep()
        {
            if (_settings.TargetFrameRate <= 0 || _globalTime.Milliseconds < 1000) return;

            if (_sleepMicrosecondsPerFrame < 0 && _globalTime.FramesPerSecond > _settings.TargetFrameRate)
                _sleepMicrosecondsPerFrame =
                    1.0f / _settings.TargetFrameRate - 1.0f / _globalTime.FramesPerSecond;

            if (_sleepMicrosecondsPerFrame < 0) return;

            if (_time.CheckEvery(2.0f) && Math.Abs(_globalTime.FramesPerSecond - _settings.TargetFrameRate)
                > _settings.TargetFrameRate / 20)
                _sleepMicrosecondsPerFrame +=
                    (1.0f / _settings.TargetFrameRate - 1.0f / _globalTime.FramesPerSecond) / 2;

            _accumulatedSleepMilliseconds += _sleepMicrosecondsPerFrame * 1000.0f;
            int sleepTime = (int) _accumulatedSleepMilliseconds;

            if (sleepTime <= 0) return;

            Thread.Sleep(sleepTime);
            _accumulatedSleepMilliseconds -= sleepTime;
        }
    }
}