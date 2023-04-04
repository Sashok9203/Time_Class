namespace Time_Class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool exit = false;
            Time time = new Time();
            time.Reset();
            Time time2 = new Time(Time.ToSeconds(time.Hours,time.Minutes,time.Seconds));
            do
            {
                Console.Clear();
                Console.WriteLine($"   Time1           Time2          Time1 - Time2          Time1 + Time2");
                Console.WriteLine($"  {++time}        {--time2}           {time2 - time}               {time2 + time}");
                Thread.Sleep(1000);
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape) exit = true;
            } while (!exit);


        }
    }
}