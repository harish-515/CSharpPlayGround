namespace CSharpPlayGrond
{
    public class user
    {
        private static string dept = "1";

        public int ID { get; set; }

        public string Dosomething()
        {
            return user.dept;
        }
    }

    public class Digit
    {
        public int Number { get; set; }
        public int[,] DigitMatrix { get; set; }

        public Digit(int number)
        {
            this.Number = number;
            this.DigitMatrix = GetMatrix(number);
        }

        private int[,] GetMatrix(int number)
        {
            int[,] matrix = new int[3, 5];
            return matrix;
        }
    }
}