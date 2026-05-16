namespace Study.Labs.Lab04
{
    public class Car : Vehicle
    {
        public override int GetSpeed()
        {
            Random random = new Random();
            return random.Next(90-151);
        }
    }
}
