using System;
using System.Collections;
using System.Reflection;

namespace NHLClientTests
{
    internal class Common
    {
        internal static void LayoutObject(object o, bool loadMetaData = true, bool loadMethodInfo = false, int depth = 0)
        {
            if (o != null)
            {
                Type type = o.GetType();
                PropertyInfo[] properties = type.GetProperties();

                string spacer = string.Empty;

                for (int i = 0; i < depth; i++) { spacer += "    "; }

                if (loadMetaData == true)
                {
                    Console.WriteLine("{0}depth: {1}", spacer, depth);
                    Console.WriteLine("{0}object type: {1}", spacer, type.ToString());
                    Console.WriteLine("{0}isArray: {1}", spacer, o is Array);
                    Console.WriteLine("{0}isList: {1}", spacer, o is IList);
                    Console.WriteLine("{0}isDictionary: {1}", spacer, o is IDictionary);
                }

                foreach (PropertyInfo property in properties)
                {
                    if (property.Name != "Item")
                    {
                        if (property.GetValue(o) == null)
                        {
                            Console.WriteLine("{0}{1}: [null]", spacer, property.Name);
                        }
                        else
                        {
                            Console.WriteLine("{0}{1}: {2}", spacer, property.Name, property.GetValue(o));
                        }

                        if (property.GetValue(o) is IList || property.GetValue(o) is Array)
                        {
                            HandleCollection(property.GetValue(o), property.GetValue(o).GetType(), loadMetaData, loadMethodInfo, depth);
                        }
                        else if (property.GetValue(o).GetType().IsClass && !(property.GetValue(o) is string))
                        {
                            LayoutObject(property.GetValue(o), loadMethodInfo, loadMethodInfo, depth + 1);
                        }
                    }
                }

                Console.WriteLine();

                if (loadMethodInfo)
                {
                    Console.WriteLine();

                    MethodInfo[] methods = type.GetMethods();
                    foreach (MethodInfo method in methods)
                    {
                        Console.WriteLine("{0}{1}", spacer, method.Name);

                        ParameterInfo[] parameters = method.GetParameters();
                        if (parameters != null)
                        {
                            foreach (ParameterInfo parameter in parameters)
                            {
                                Console.WriteLine("{0}    {1}:", spacer, parameter.ParameterType);
                            }
                        }
                    }
                }

                if (o is IList || o is Array || o is IDictionary)
                {
                    HandleCollection(o, type, loadMetaData, loadMethodInfo, depth);
                }
            }
            else
            {
                Console.WriteLine("Null object found!");
            }
        }

        private static void HandleCollection(object o, Type type, bool loadMetaData = true, bool loadMethodInfo = false, int depth = 0)
        {
            object child;

            if (o is Array)
            {
                int length = Convert.ToInt32(type.GetProperty("Length").GetValue(o));

                if (length > 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        child = type.GetMethod("Get").Invoke(o, new object[] { i });

                        Console.WriteLine();

                        LayoutObject(child, loadMetaData, loadMethodInfo, depth + 1);
                    }
                }
            }
            else if (o is IList)
            {
                int count = Convert.ToInt32(type.GetProperty("Count").GetValue(o));

                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        child = type.GetProperty("Item").GetValue(o, new object[] { i });

                        Console.WriteLine();

                        LayoutObject(child, loadMetaData, loadMethodInfo, depth + 1);
                    }
                }
            }
            else if (o is IDictionary)
            {
                int count = Convert.ToInt32(type.GetProperty("Count").GetValue(o));

                if (count > 0)
                {
                    IEnumerable dic = (IEnumerable)o;

                    foreach (var d in dic)
                    {
                        Console.WriteLine();

                        LayoutObject(d, loadMetaData, loadMethodInfo, depth + 1);
                    }
                }
            }
        }
    }
}
