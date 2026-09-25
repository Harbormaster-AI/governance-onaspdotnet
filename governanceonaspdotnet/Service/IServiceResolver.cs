namespace governanceonaspdotnet.Service;

public interface IServiceResolver
{
    T Get<T>() where T : notnull;
}