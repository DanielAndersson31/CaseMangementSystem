
namespace CaseMangementSystem.Services
{
    //CommonService or General purpose
    public class CS
    {
        public static int CheckIfIntOrNot()
        {
            int n;
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("You entered an invalid number");
                Console.WriteLine("Please try again!");
            }

            return n;
        }

        public static string CheckIfNullOrEmpty()
        {
            string input;

            while (true)
            {
                input = Console.ReadLine() ?? null!;

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("User input is null or empty\n");

                    Console.WriteLine("Enter a new value that is not null or empty: ");
                    input = Console.ReadLine() ?? "";
                }
                else
                {
                    return input;
                }
            }

        }

    }
}
