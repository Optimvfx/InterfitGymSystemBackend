namespace Common.Extensions;

public static class TypeExtensions
{
    public static Type GetBaseType(this Type type)
    {
        while (type.BaseType != null && type.BaseType != typeof(object))
        {
            type = type.BaseType;
        }

        return type;
    }

    public static bool Is(this Type type, Type other)
    {
        return type == other;
    }
    
    public static bool IsNot(this Type type, Type other)
    {
        return type != other;
    }
}