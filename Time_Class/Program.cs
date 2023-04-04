namespace Time_Class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Time time = new Time();
            time.Reset();
            Time time2 = new Time(Time.ToSeconds(time.Hours,time.Minutes,time.Seconds));
            do
            {
                Console.SetCursorPosition(0,0);
                Console.WriteLine($"   Time1           Time2          Time1 - Time2          Time1 + Time2");
                Console.WriteLine($"  {++time}        {--time2}           {time2 - time}               {time2 + time}");
                Console.WriteLine("\n\n Press Escape to exit...");
                Thread.Sleep(1000);
            } while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape));
        }
    }
}