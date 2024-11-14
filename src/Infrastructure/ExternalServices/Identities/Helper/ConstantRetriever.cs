namespace UserManagement.Infrastructure.ExternalServices.Identities.Helper;



public static class ConstantRetriever
{
    public static IEnumerable<(string Name, string Value)> GetConstants(Type type)
    {
        var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static);

        return fieldInfos.Select(x => (Name: x.Name, Value: x.GetRawConstantValue().ToString()));
    }
}