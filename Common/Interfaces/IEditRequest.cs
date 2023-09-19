namespace Common.Interfaces;

public interface IEditRequest<T>
{
    T ApplyEdit(T value);
}