using System;
using System.IO.Compression;
using System.Runtime.Intrinsics.Arm;

namespace ConsoleApp1
{
    class Program
    {
        // 1) create a program that will take name from user and greet the user
        static void Q1()
        {
            string? input;
            Console.Write("Enter User name : ");
            input = Console.ReadLine();
            Console.WriteLine("Welcome! " + input);
        }

        // 2) Take 2 numbers from user and print the largest
        static void Q2()
        {
            Console.WriteLine("Enter Two Numbers : ");
            int n1 = Convert.ToInt32(Console.ReadLine());
            int n2 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Largest number is ");
            Console.WriteLine(n1 > n2 ? n1 : n2);
        }

        // 3) Take 2 numbers from user, check the operation user wants to perform (+,-,*,/). Do the operation and print the result

        static void Q3()
        {
            Console.Write("Enter first numbers : ");
            int n1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter second numbers : ");
            int n2 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter operation to be performed (+,-,*,/) : ");
            char op = Convert.ToChar(Console.ReadLine()!);
            Console.Write("Output : ");
            switch (op)
            {
                case '+':
                    Console.WriteLine(n1 + n2);
                    break;
                case '-':
                    Console.WriteLine(n1 - n2);
                    break;
                case '*':
                    Console.WriteLine(n1 * n2);
                    break;
                case '/':
                    if (n2 != 0)
                    {
                        Console.WriteLine(n1 / n2);
                    }
                    else
                    {
                        throw new DivideByZeroException();
                    }
                    break;
                default:
                    Console.WriteLine("Invalid Operator");
                    break;

            }
        }

        // 4) Take username and password from user. Check if user name is "Admin" and password is "pass" if yes then print success message. 
        // Give 3 attempts to user. In the end of eh 3rd attempt if user still is unable to provide valid creds then exit the application after print the message "Invalid attempts for 3 times. Exiting...."

        static void Q4()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Write("Enter username : ");
                string? u = Console.ReadLine();
                Console.Write("Enter password : ");
                string? p = Console.ReadLine();

                if (string.Equals(u, "Admin") && p == "pass")
                {
                    Console.WriteLine("Successful Login!");
                    return;
                }
                Console.WriteLine("Invalid Credentials, Try Again!");
            }
            Console.WriteLine("Invalid attempts for 3 times. Exiting....");
        }

        // 5) Take 10 numbers from user and print the number of numbers that are divisible by 7
        static void Q5()
        {
            List<int> l = new List<int>();
            Console.WriteLine("Enter 10 numbers : ");
            for (int i = 0; i < 10; i++)
            {
                int n = Convert.ToInt32(Console.ReadLine());
                if (n % 7 == 0) l.Add(n);
            }
            Console.WriteLine("Numbers divisible by 7 : ");
            foreach (int i in l)
            {
                Console.Write(i + " ");
            }
        }

        //TryParse
        static void TryParseInt()
        {
            int num;
            Console.Write("Enter a number : ");
            string? str = Console.ReadLine();
            while (int.TryParse(str, out num) == false)
            {
                Console.Write("Enter a number : ");
                str = Console.ReadLine();
            }
            Console.WriteLine("Number is " + num);
            

        }


        // 6) Count the Frequency of Each Element
        // Given an array, count the frequency of each element and print the result.
        // Input: {1, 2, 2, 3, 4, 4, 4}
        // output
        // 1 occurs 1 times  
        // 2 occurs 2 times  
        // 3 occurs 1 times  
        // 4 occurs 3 times

        static void Q6()
        {
            int[] arr = { 1, 2, 2, 3, 4, 4, 4 };
            foreach (string s in arr.Distinct().Select(a => a + " occurs " + arr.Count(c => c == a)+" times")){
                Console.WriteLine(s);
            }
        }

        // 7) create a program to rotate the array to the left by one position.
        // Input: {10, 20, 30, 40, 50}
        // Output: {20, 30, 40, 50, 10}

        static void Q7()
        {
            int[] arr = { 10, 20, 30, 40, 50 };
            int t = arr[0];
            for (int i = 1; i < arr.Count(); i++)
            {
                arr[i - 1] = arr[i];
            }
            arr[arr.Count() - 1] = t;

            foreach (int i in arr)
                Console.Write(i+" ");
        }

        // 8) Given two integer arrays, merge them into a single array.
        // Input: {1, 3, 5} and {2, 4, 6}
        // Output: {1, 3, 5, 2, 4, 6}

        static void Q8()
        {
            int[] arr1 = { 1, 3, 5 };
            int[] arr2 = { 2, 4, 6 };

            int[] newArr = new int[arr1.Length + arr2.Length];
            int z = 0;
            foreach (int i in arr1)
                newArr[z++] = i;
            foreach (int i in arr2)
                newArr[z++] = i;
            foreach (int i in newArr)
                Console.Write(i+" ");
            

        }


        // 9) Write a program that:
        // Has a predefined secret word (e.g., "GAME").
        // Accepts user input as a 4-letter word guess.
        // Compares the guess to the secret word and outputs:
        // X Bulls: number of letters in the correct position.
        // Y Cows: number of correct letters in the wrong position.
        // Continues until the user gets 4 Bulls (i.e., correct guess).
        // Displays the number of attempts.
        // Bull = Correct letter in correct position.
        // Cow = Correct letter in wrong position.
        // Secret Word	User Guess	Output	Explanation
        // GAME	GAME	4 Bulls, 0 Cows	Exact match
        // GAME	MAGE	1 Bull, 3 Cows	A in correct position, MGE misplaced
        // GAME	GUYS	1 Bull, 0 Cows	G in correct place, rest wrong
        // GAME	AMGE	2 Bulls, 2 Cows	A, E right; M, G misplaced
        // NOTE	TONE	2 Bulls, 2 Cows	O, E right; T, N misplaced

        static bool isAlpha(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsLetter(c)) return false;
            }
            return true;
        }
        static int Q9_Check(string s1, string s2)
        {
            int cnt = 0;
            for (int i = 0; i < 4; i++)
            {
                if (s1[i] == s2[i]) cnt++;
            }
            return cnt;
        }
        static void Q9()
        {
            string ans = "GAME";
            Console.WriteLine("Enter your guess : ");
            string? guess = Console.ReadLine();
            int tries = 1;
            while (string.IsNullOrEmpty(guess) ||guess.ToUpper() != ans)
            {
                if (string.IsNullOrEmpty(guess)|| !isAlpha(guess)) Console.WriteLine("Enter a Valid Input");
                else if (guess.Length != 4) Console.WriteLine("Secret word's length is 4");
                else
                {
                    int cnt = Q9_Check(ans, guess.ToUpper());
                    System.Console.WriteLine($"{cnt} Bulls, {4 - cnt} Cows");
                }

                Console.WriteLine("Enter your guess : ");
                guess = Console.ReadLine();
                tries++;
            }
            Console.WriteLine($"4 Bulls, 0 Cows");
            System.Console.WriteLine($"Found after {tries} tries");
            
        }

        // 10) write a program that accepts a 9-element array representing a Sudoku row.
        // Validates if the row:
        // Has all numbers from 1 to 9.
        // Has no duplicates.
        // Displays if the row is valid or invalid.

        static bool checkRow(int[] row)
        {
            for (int i=1;i < 10; i++)
            {
                if (row[i] != 1)
                    return false;
            }
            return true;
        }

        static bool Q10()
        {
            System.Console.WriteLine("Enter 9 values of a Soduku Row : ");
            int[] arr = new int[10];
            for (int i = 0; i < 9; i++)
            {

                int v = Convert.ToInt32(Console.ReadLine());
                if (v > 9 || v <= 0)
                {
                    System.Console.WriteLine("Invalid Value");
                    return false;
                }
                arr[v]++;
            }
            if (checkRow(arr))
            {
                System.Console.WriteLine("Valid Row");
                return true;
            }
            else System.Console.WriteLine("Invalid Row");
            return false;
        }

        // 11) In the question ten extend it to validate a sudoku game. 
        // Validate all 9 rows (use int[,] board = new int[9,9])

        // static void Q11()
        // {
        //     for (int i = 1; i < 10; i++)
        //     {
        //         Console.WriteLine($"For Row {i}");
        //         bool flag = Q10();
        //         if (!flag)
        //         {
        //             Console.WriteLine("Invalid Sudoku Game");
        //             return;
        //         }
        //     }
        //     Console.WriteLine("valid Sudoku Game");
        // }

        static bool check_row(int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                bool[] visited = new bool[10];
                for (int j = 0; j < 9; j++)
                {
                    if (visited[board[i, j]]) return false;
                    visited[board[i, j]] = true;
                }
            }
            return true;
        }
        static bool check_col(int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                bool[] visited = new bool[10];
                for (int j = 0; j < 9; j++)
                {
                    if (visited[board[j,i]]) return false;
                    visited[board[j,i]] = true;
                }
            }
            return true;
        }
        static bool check_sub(int[,] board)
        {
            for (int i = 0; i < 9; i += 3)
            {
                for (int j = 0; j < 9; j += 3)
                {
                    bool[] visited = new bool[10];
                    for (int k = i; k < i + 3; k++)
                    {
                        for (int l = j; l < j + 3; l++)
                        {
                            if (visited[board[k,l]]) return false;
                            visited[board[k,l]] = true;
                        }
                    }
                }
            }
            return true;
        }
        static void Q11()
        {
            int[,] board = {
            { 5, 3, 4, 6, 7, 8, 9, 1, 2 },
            { 6, 7, 2, 1, 9, 5, 3, 4, 8 },
            { 1, 9, 8, 3, 4, 2, 5, 6, 7 },
            { 8, 5, 9, 7, 6, 1, 4, 2, 3 },
            { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
            { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
            { 9, 6, 1, 5, 3, 7, 2, 8, 4 },
            { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
            { 3, 4, 5, 2, 8, 6, 1, 7, 9 }
        };
            if (check_row(board) && check_col(board) && check_sub(board))
                Console.WriteLine("Valid Sudoku Game");
            else
                Console.WriteLine("Invalid Sudoku Game");
        }


        // 12) Write a program that:
        // Takes a message string as input (only lowercase letters, no spaces or symbols).
        // Encrypts it by shifting each character forward by 3 places in the alphabet.
        // Decrypts it back to the original message by shifting backward by 3.
        // Handles wrap-around, e.g., 'z' becomes 'c'.

        // Examples
        // Input:     hello
        // Encrypted: khoor
        // Decrypted: hello
        // -------------
        // Input:     xyz
        // Encrypted: abc
        // Test cases
        // | Input | Shift | Encrypted | Decrypted |
        // | ----- | ----- | --------- | --------- |
        // | hello | 3     | khoor     | hello     |
        // | world | 3     | zruog     | world     |
        // | xyz   | 3     | abc       | xyz       |
        // | apple | 1     | bqqmf     | apple     |

        static string encrypt(String s)
        {
            char[] arr = s.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (char)(arr[i] + 3);
                if (arr[i] > 'z') arr[i] = (char)(arr[i] - 26);
            }
            return new String(arr);
        }

        static string decrypt(String s)
        {
            char[] arr = s.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (char)(arr[i] - 3);
                if (arr[i] < 'a') arr[i] = (char)(arr[i] + 26);
            }
            return new String(arr);
        }
        static void Q12()
        {
            System.Console.Write("Enter a string : ");
            string? s = Console.ReadLine();
            if (string.IsNullOrEmpty(s)|| !isAlpha(s) || s.Contains(' ') || s.ToLower() != s)
            {
                Console.WriteLine("Enter a valid input");
                return;
            }
            string enc = encrypt(s);
            string dec = decrypt(enc);
            Console.WriteLine($"Encrypted Text : {enc}");
            Console.WriteLine($"Decrypted Text : {dec}");
        }

        static void Main(string[] args)
        {
            // Q1();
            // Q2();
            // Q3();
            // Q4();
            // Q5();
            // TryParseInt();
            // Q6();
            // Q7();
            // Q8();
            // Q9();
            // Q11();
            // Q12();
        }
    }
}