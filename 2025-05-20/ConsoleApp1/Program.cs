using System.Runtime.ConstrainedExecution;

namespace InstagramPosts
{
    class Post
    {
        public string? Caption { get; set; }
        public int Likes { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Number of Users : ");
            int n;
            bool f1 = int.TryParse(Console.ReadLine(), out n);
            Post[][] arr = new Post[n][];
            System.Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.Write("Enter Number of Posts for User " + (i + 1) + " : ");
                int p = Convert.ToInt32(Console.ReadLine());
                arr[i] = new Post[p];
                for (int j = 0; j < p; j++)
                {
                    Console.Write($"Enter caption for Post {j + 1} : ");
                    string? caption = Console.ReadLine();
                    Console.Write($"Enter Likes for Post {j + 1} : ");
                    int likes = Convert.ToInt32(Console.ReadLine());
                    arr[i][j] = new Post { Caption = caption, Likes = likes };
                }
                System.Console.WriteLine();
            }

            //Display
            Console.WriteLine("----------Displaying Instagram Posts-----------------------");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"For User {i + 1} : ");
                Console.WriteLine($"Number of Posts : {arr[i].Length}");
                for (int j = 0; j < arr[i].Length; j++)
                {
                    Console.WriteLine($"Post {j + 1} - Caption : {arr[i][j].Caption}\t| Likes : {arr[i][j].Likes}");
                }
                System.Console.WriteLine();
            }
        }
    }
}
