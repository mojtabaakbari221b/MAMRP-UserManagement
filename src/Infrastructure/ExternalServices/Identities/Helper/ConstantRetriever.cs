namespace UserManagement.Infrastructure.ExternalServices.Identities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



public static class ConstantRetriever
{
    public static IEnumerable<(string Name, string Value)> GetConstants(Type type)
    {
        var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static);

        return fieldInfos.Select(x => (Name: x.Name, Value: x.GetRawConstantValue().ToString()));
    }
}