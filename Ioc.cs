namespace IOC
{
    public interface IC { }
    public class C : IC
    {
        public C()
        {
            Console.WriteLine("Contructor de C");
        }
    }

    public interface IB
    {

    }

    public class B : IB
    {
        public B(IC c)
        {
            Console.WriteLine("Contructor de B");
        }
    }
}