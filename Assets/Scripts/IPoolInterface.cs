public interface IpoolInterface<U> where U : class, IpoolInterface<U>
{

    void SetPool(S_Pool<U> pool);

    
}
