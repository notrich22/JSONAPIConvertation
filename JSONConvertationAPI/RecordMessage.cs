namespace JSONConvertationAPI
{
    public class RecordMessage
    {
        public record StatusMessage(string Message, DateTime Time);
        public record CalcInputMessage(int numberSystem, int x);
        public record CalcOutputMessage(int numberSystem, int num, string result);
        public record ErrorMessage(string Error);
    }
}
