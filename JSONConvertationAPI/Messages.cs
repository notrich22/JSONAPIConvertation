namespace JSONConvertationAPI
{
    public class Messages
    {
        public class StatusMessage
        {
            public string Message { get; set; }
            public DateTime Time { get; set; }
            public StatusMessage(string message, DateTime time)
            {
                Message = message;
                Time = time;
            }
            public StatusMessage(string message) : this(message, DateTime.Now) { }

            public override string ToString()
            {
                return $"{Message} - {Time}";
            }
        }

        public class CalcInputMessage
        {
            public int NumberSystem { get; set; }
            public int X { get; set; } //Number to convert
            public CalcInputMessage(int numberSystem, int x)
            {
                NumberSystem = numberSystem;
                X = x;
            }
            public override string ToString()
            {
                return $"Trying to convert {X} in {NumberSystem} Number system;";
            }
        }
        public class CalcOutputMessage
        {
            public int NumberSystem { get; set; }
            public int Num { get; set; }
            public string Result { get; set; }
            public CalcOutputMessage(int numberSystem, int num, string result)
            {
                NumberSystem = numberSystem;
                Num = num;
                Result = result;
            }
            public override string ToString()
            {
                return $"{Num} in {NumberSystem} number system is: {Result}";
            }
        }

        public class ErrorMessage
        {
            public string Error { get; set; }
            public ErrorMessage(string error)
            {
                Error = error;
            }
            public override string ToString()
            {
                return Error;
            }
        }
    }
}
