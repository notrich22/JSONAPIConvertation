namespace JSONConvertationAPI
{
    public class NSSolvator : ISolvator
    {
        public Messages.CalcOutputMessage Solve(Messages.CalcInputMessage Input)
        {
            int x = Input.X;
            int numberSystem = Input.NumberSystem;
            if (x <= 0)
            {
                throw new Exception("Enter an X");
            }
            string result = Convert.ToString(x, numberSystem);
            return new Messages.CalcOutputMessage(numberSystem, x, result);
        }
    }
}
