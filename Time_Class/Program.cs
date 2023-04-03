namespace Time_Class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Time time = new Time(5555);
            Console.WriteLine(time);
            TimeOnly tmp = (TimeOnly)time;
            Console.WriteLine(tmp.ToLongTimeString());
        }
    }
}