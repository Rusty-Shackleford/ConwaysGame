using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGame.Library
{
    public class Timer
    {
        #region [ Members ]
        public TimeSpan Interval { get; set; }
        public TimeSpan CurrentTime { get; protected set; }
        public TimerState State { get; protected set; }
        public TimeSpan TimeRemaining { get; protected set; }
        #endregion

        #region [ Events ]
        public event EventHandler Started;
        public event EventHandler Stopped;
        public event EventHandler Paused;
        public event EventHandler Completed;
        #endregion

       
        #region [ Constructor ]
        public Timer(TimeSpan interval)
        {
            Interval = interval;
        }
        
    
        public Timer(double intervalSeconds) : 
            this(TimeSpan.FromSeconds(intervalSeconds)) { }
        #endregion


        #region [ Start, Stop, Restart, Pause ]
        public void Start()
        {
            State = TimerState.Started;
            Started?.Invoke(this, EventArgs.Empty);
        }


        public void Stop()
        {
            State = TimerState.Stopped;
            CurrentTime = TimeSpan.Zero;
            OnStopped();
            Stopped?.Invoke(this, EventArgs.Empty);
        }


        public void Restart()
        {
            Stop();
            Start();
        }


        public void Pause()
        {
            State = TimerState.Paused;
            Paused?.Invoke(this, EventArgs.Empty);
        }
        #endregion


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
            if (State != TimerState.Started)
                return;

            CurrentTime += gameTime.ElapsedGameTime;
            OnUpdate(gameTime);
        }
        #endregion


        #region [ OnUpdate ]
        public void OnUpdate(GameTime gameTime)
        {
            TimeRemaining = Interval - CurrentTime;

            if (CurrentTime >= Interval)
            {
                State = TimerState.Completed;
                CurrentTime = Interval;
                TimeRemaining = TimeSpan.Zero;
                Completed?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion


        #region [ OnStopped ]
        public void OnStopped()
        {
            State = TimerState.Stopped;
            CurrentTime = TimeSpan.Zero;

        }
        #endregion
    }
}
