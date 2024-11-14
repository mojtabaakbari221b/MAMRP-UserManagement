using System.Linq.Expressions;

namespace Share.Ordering;

public class QueryOrdering
{
    public static IQueryable<T> ApplyOrdering<T>(IQueryable<T> query, object? orderBy)
    {
        if (orderBy == null)
        {
            return query;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        IOrderedQueryable<T>? orderedQuery = null;

        // دریافت ویژگی‌های کلاس مرتب‌سازی
        var properties = orderBy.GetType().GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(orderBy)?.ToString();

            // اگر مقدار فیلد null نباشد و مشخصات مرتب‌سازی معتبر باشند، مرتب‌سازی را اعمال می‌کنیم
            if (value == null) continue;
            var direction = value.Contains("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
            var propertyName = property.Name; // نام ویژگی خود کلاس مرتب‌سازی (مثلاً "Name" یا "Age")

            // ساخت Expression برای ویژگی مورد نظر
            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(T).Name}'.");
            }

            var propertyAccess = Expression.Property(parameter, propertyInfo);
            var lambda = Expression.Lambda(propertyAccess, parameter);

            // تعیین متد مرتب‌سازی (OrderBy یا OrderByDescending)
            var methodName = direction == "ASC" ? "OrderBy" : "OrderByDescending";
            var method = typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), propertyInfo.PropertyType);

            // اعمال مرتب‌سازی به query
            orderedQuery = orderedQuery == null
                ? (IOrderedQueryable<T>)method.Invoke(null, new object[] { query, lambda })!
                : (IOrderedQueryable<T>)method.Invoke(null, new object[] { orderedQuery, lambda })!;
        }

        return orderedQuery ?? query;
    }
}