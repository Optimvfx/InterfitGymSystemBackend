namespace BLL.Models.Interface;

public interface IEditRequest<T>
{
    T ApplyEdit(T value);
}