using System.Linq.Expressions;

namespace Share.QueryFilterings;

public class QueryFilter
{
    // متد اصلی که ورودی آن از نوع IQueryable است و پارامترهای فیلتر را از کلاس فیلتر دریافت می‌کند
    public static IQueryable<T> Filter<T>(IQueryable<T> query, object? filter)
    {
        if (filter == null)
        {
            return query;
        }

        // ساخت پارامتر برای مدل داده‌ای T
        var parameter = Expression.Parameter(typeof(T), "x");

        // ایجاد اولین شرط Expression
        Expression? body = null;

        // دریافت ویژگی‌های کلاس فیلتر
        var properties = filter.GetType().GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(filter)?.ToString();

            // اگر مقدار فیلد null نباشد، فیلتر را اعمال می‌کنیم
            if (value != null)
            {
                var currentCondition = BuildExpression(parameter, property.Name, value);

                // اگر body نال است، برای اولین بار از currentCondition استفاده می‌کنیم
                if (body == null)
                {
                    body = currentCondition;
                }
                else
                {
                    // اگر body موجود است، آن را با AND ترکیب می‌کنیم
                    if (currentCondition != null) body = Expression.AndAlso(body, currentCondition); // برای AND
                }
            }
        }

        // تبدیل body به یک lambda expression
        if (body == null) return query;
        
        var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

        // اعمال فیلتر بر روی IQueryable
        return query.Where(lambda);

    }

    // متد کمکی برای ساختن Expression از رشته فیلتر
    private static Expression? BuildExpression(Expression parameter, string propertyName, string value)
    {
        // پردازش شرط فیلتر (مثل "age>30", "name=john")
        string operatorSymbol;
        
        if (value.Contains('='))
        {
            operatorSymbol = "=";
        }
        else if (value.Contains(">="))
        {
            operatorSymbol = ">=";
        }
        else if (value.Contains("<="))
        {
            operatorSymbol = "<=";
        }
        else if (value.Contains('>'))
        {
            operatorSymbol = ">";
        }
        else if (value.Contains('<'))
        {
            operatorSymbol = "<";
        }
        else
        {
            throw new ArgumentException("Invalid filter operator");
        }
        

        var conditionParts = value.Split(new string[] { operatorSymbol }, StringSplitOptions.None);
        if (conditionParts.Length != 2)
        {
            throw new ArgumentException("Invalid filter condition format");
        }

        var property = Expression.Property(parameter, propertyName);
        var constant = Expression.Constant(Convert.ChangeType(conditionParts[1].Trim(), property.Type));

        // ایجاد Expression برای مقایسه با اپراتور انتخابی
        Expression? comparison = operatorSymbol switch
        {
            "=" => Expression.Equal(property, constant),
            ">" => Expression.GreaterThan(property, constant),
            "<" => Expression.LessThan(property, constant),
            ">=" => Expression.GreaterThanOrEqual(property, constant),
            "<=" => Expression.LessThanOrEqual(property, constant),
            _ => throw new InvalidOperationException($"Operator {operatorSymbol} not supported")
        };

        return comparison;
    }
}