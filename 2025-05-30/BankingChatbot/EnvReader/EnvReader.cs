namespace BankingChatbot.EnvReader
{
    public class EnvReader
    {
        public static void Read()
        {
            
            String[] env = File.ReadAllLines(".env");
            for (int i= 0;i< env.Length;i++)
            {
                string[] keyValuePair= env[i].Trim().Split("=");
                Environment.SetEnvironmentVariable(keyValuePair[0], keyValuePair[1]);
            }
        }
    }
}