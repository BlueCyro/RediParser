namespace RediParser;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Invalid data");
            return;
        }


        IRespData something = RespDataParsingHelpers.ParseRespInput(args[0]);

        Console.WriteLine($"{((SimpleString)something).String}");
    }
}
