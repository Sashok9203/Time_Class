using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_Class
{
    internal class Time
    {
		private uint _hours;
        private uint _minutes;
        private uint _seconds;
			
        public Time():this(0,0,0) {}
        public Time(uint hours,uint minutes,uint seconds)
        {
            Hours = hours;
			Minutes = minutes;	
			Seconds = seconds;
        }
        public Time(uint seconds) => FromSeconds(seconds, out _hours, out _minutes, out _seconds);
        
        public uint Hours
		{
			get => _hours;
			private set
			{
				if (value > 23) throw new ApplicationException($" Invalid hours value \"{value}\"");
				_hours = value;
			}
		}
		public uint Minutes
		{
			get => _minutes;
			private set
			{
                if (value > 59) throw new ApplicationException($" Invalid minutes value \"{value}\"");
                _minutes = value; 
			}
		}
		public uint Seconds
		{
			get => _seconds;
			private set 
			{
                if (value > 59) throw new ApplicationException($" Invalid seconds value \"{value}\"");
                _seconds = value;
			}
		}

		public void Reset()
		{
            DateTime time = DateTime.Now;
			_hours   = (uint)time.Hour;
			_minutes = (uint)time.Minute;
			_seconds = (uint)time.Second;	
        }
		public override string ToString()
		{
			return $"{(_hours < 10 ? "0" + _hours : _hours)}" +
				   $":{(_minutes < 10 ? "0" + _minutes : _minutes)}" +
				   $":{(_seconds < 10 ? "0" + _seconds : _seconds)}";
		}
        public override bool Equals(object? obj)
        {
            return obj is Time time &&
                   _hours == time._hours &&
                   _minutes == time._minutes &&
                   _seconds == time._seconds;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_hours, _minutes, _seconds);
        }

        public static uint ToSeconds(uint hours, uint minutes, uint seconds) => hours * 3600 + minutes * 60 + seconds;
        public static void FromSeconds(uint sec, out uint hours, out uint minutes, out uint seconds)
        {
            hours = sec / 3600 % 24;
            minutes = sec % 3600 / 60;
            seconds = sec % 3600 % 60;
        }

        public static Time operator ++(Time time)
		{
			if (time._seconds < 59) ++time._seconds;
			else
			{
				time._seconds = 0;
				if (time._minutes < 59) ++time._minutes;
				else
				{
					time._minutes = 0;
					if (time._hours < 23) ++time._hours;
					else time._hours = 0;
				}
			}
            return time;
		}
        public static Time operator --(Time time)
        {
            if (time._seconds > 0) --time._seconds;
            else
            {
                time._seconds = 59;
                if (time._minutes > 0) --time._minutes;
                else
                {
                    time._minutes = 59;
                    if (time._hours > 0) --time._hours;
                    else time._hours = 23;
                }
            }
            return time;
        }

		public static Time operator +(Time time1, Time time2)
		{
			return new Time(ToSeconds(time1._hours, time1._minutes, time1._seconds) + 
				            ToSeconds(time2._hours, time2._minutes, time2._seconds));
		}
        public static Time operator -(Time time1, Time time2)
        {
            const uint _day_seconds = 86400;
            uint sec1 = ToSeconds(time1._hours, time1._minutes, time1._seconds);
			uint sec2 = ToSeconds(time2._hours, time2._minutes, time2._seconds);
			if (sec1 < sec2)
		     	return new Time(_day_seconds - (sec2 - sec1) % _day_seconds);
			return new Time(sec1 - sec2);
        }

        public static bool operator ==(Time time1, Time time2) => time1.Equals(time2);
		public static bool operator !=(Time time1, Time time2) => !(time1 == time2);
		public static bool operator >(Time time1, Time time2)
		{
			return ToSeconds(time1._hours, time1._minutes, time1._seconds) >
				   ToSeconds(time2._hours, time2._minutes, time2._seconds);
		}
		public static bool operator <(Time time1, Time time2)
		{
			return ToSeconds(time1._hours, time1._minutes, time1._seconds) <
				   ToSeconds(time2._hours, time2._minutes, time2._seconds);
		}
        public static bool operator <=(Time time1, Time time2) => !(time1 > time2);
        public static bool operator >=(Time time1, Time time2) => !(time1 < time2);

		public static bool operator true(Time time1)
		{
			DateTime tmp = DateTime.Now;
			return time1 >= new Time((uint)tmp.Hour, (uint)tmp.Second, (uint)tmp.Minute);
		}
		public static bool operator false(Time time1)
        {
            DateTime tmp = DateTime.Now;
            return time1 < new Time((uint)tmp.Hour, (uint)tmp.Second, (uint)tmp.Minute);
        }

		public static explicit operator TimeOnly(Time time1) => new TimeOnly((int)time1._hours, (int)time1._minutes, (int)time1._seconds);
    }
}
