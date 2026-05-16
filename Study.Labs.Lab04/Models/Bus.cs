namespace Study.Labs.Lab04
{
    public class Bus : Vehicle
    {
        public override int GetSpeed()
        {
            Random random = new Random();
            return random.Next(80 - 111);
        }
    }
}
