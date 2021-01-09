namespace LTBs.System
{
    public interface IFactory<T> 
    {
        T Create(string id = null);    
    }
}