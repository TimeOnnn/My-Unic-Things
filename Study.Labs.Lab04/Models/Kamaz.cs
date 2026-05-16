namespace Study.Labs.Lab04
{
    public class Kamaz : Vehicle
    {
        public override int GetSpeed()
        {
            Random random = new Random();
            return random.Next(70-101);
        }
    }
}
