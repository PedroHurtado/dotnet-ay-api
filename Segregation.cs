namespace Segregation{
    public interface IGet<T,ID>{

    }

    public interface IAdd<T>{

    }

    public interface IUpdate<T,ID>:IGet<T,ID>{

    }
    public interface IRemove<T,ID>:IGet<T,ID>{

    }

    public interface IAll<T,ID>:IAdd<T>,IUpdate<T,ID>, IRemove<T,ID>{

    }

    public class Repository<T,ID>:IAll<T,ID>{

    }

    public class Customer{

    }

    public class CustomerRepository:Repository<Customer,int>{

    }

}