namespace ConsoleApp1.Exceptions
{
    class CollectionEmptyException : Exception
    {
        string _message;
        public CollectionEmptyException(string msg)
        {
            _message = msg;
        }
        public override string Message => _message;
    }
}