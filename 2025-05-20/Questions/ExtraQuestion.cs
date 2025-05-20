namespace ExtraQuestions
{
    class ExtraQuestions
    {
        static Dictionary<string, double> dict = new Dictionary<string, double>();

        static void displayItem()
        {
            System.Console.WriteLine("Enter Product name : ");
            string? pdt =  Console.ReadLine();
            
            if (!dict.ContainsKey(pdt!))
            {
                System.Console.WriteLine("No data found");
                return;
            }
            System.Console.WriteLine(pdt+" "+dict[pdt!]);
        }
        public static void ExtraQ()
        {
            dict.Add("Pen", 10);
            dict.Add("Marker", 30);
            dict.Add("Pencil", 8);
            dict.Add("Box", 100);
            dict.Add("Erase", 5);

            System.Console.WriteLine("Dictonary : ");
            foreach (KeyValuePair<string, double> kv in dict)
            {
                System.Console.WriteLine(kv.Key + "\t" + kv.Value);
            }

            displayItem();
        }
    }
}