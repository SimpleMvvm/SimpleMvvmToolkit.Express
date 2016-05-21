using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SimpleMvvmToolkit.Express
{
    /// <summary>
    /// Extension methods for model entities.
    /// </summary>
    public static class ModelExtensions
    {
        /// <summary>
        /// Performs a deep copy using Json Serializer.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="obj">Entity object</param>
        /// <returns>Cloned entity</returns>
        public static T Clone<T>(this T obj)
        {
            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All
            };
            var json = JsonConvert.SerializeObject(obj, settings);
            var result = JsonConvert.DeserializeObject<T>(json, settings);
            return result;
        }

        /// <summary>
        /// Performs a deep copy using DatacontractSerializer.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="obj">Entity object</param>
        /// <returns>Cloned entity</returns>
        public static T CloneXml<T>(this T obj)
        {
            T copy;
            using (var stream = new MemoryStream())
            {
                var settings = new DataContractSerializerSettings {PreserveObjectReferences = true};
                var ser = new DataContractSerializer(typeof(T), settings);
                ser.WriteObject(stream, obj);
                stream.Position = 0;
                copy = (T)ser.ReadObject(stream);
            }
            return copy;
        }

        /// <summary>
        /// Performs a shallow copy of all properties
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="source">Source entity object</param>
        /// <param name="dest">Destination entity object</param>
        public static void CopyValuesTo<T>(this T source, T dest)
        {
            var properties = typeof(T).GetRuntimeProperties()
                .Where(p => p.CanRead && p.CanWrite);

            foreach (var property in properties)
            {
                if (property.SetMethod == null) continue;
                property.SetValue(dest, property.GetValue(source, null), null);
            }
        }

        /// <summary>
        /// Determines equality based on property hash codes.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="source">Source entity object</param>
        /// <param name="item">Object to compare</param>
        /// <param name="excludeProps">Properties excluded from comparison</param>
        /// <returns>True if object properties are the same</returns>
        public static bool AreSame<T>(this T source, T item, params string[] excludeProps)
            where T : class
        {
            int hashCode1 = GetObjectHashCode(source, excludeProps);
            int hashCode2 = GetObjectHashCode(item, excludeProps);
            return hashCode1 == hashCode2;
        }

        // Calculates object has code based on property hash codes
        private static int GetObjectHashCode(object item, params string[] excludeProps)
        {
            int hashCode = 0;
            foreach (var prop in item.GetType().GetRuntimeProperties())
            {
                if (!excludeProps.Contains(prop.Name))
                {
                    object propVal = prop.GetValue(item, null);
                    if (propVal != null)
                    {
                        hashCode = hashCode ^ propVal.GetHashCode();
                    }
                }
            }
            return hashCode;
        }

        /// <summary>
        /// Convert an enum into a list of value / string pairs for 
        /// showing in list controls
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <returns>Sequence of enum / name pairs</returns>
        public static IEnumerable<EnumItem<TEnum>> GetEnumItems<TEnum>()
            where TEnum : struct
        {
            // Get TEnum type and verify it's an enum
            var enumType = typeof(TEnum);
            if (!IsEnum(enumType))
                throw new ArgumentException($"Type {enumType.Name} is not an enum");

            // Project enum values and names
            var items = from f in enumType.GetRuntimeFields()
                        where f.IsPublic && f.IsStatic && f.IsLiteral
                        let val = (TEnum)f.GetValue(enumType)
                        let name = f.Name
                        select new EnumItem<TEnum>(val, name);
            return items;
        }

        /// <summary>
        /// Convert an enum into a list of value / string pairs for 
        /// showing in list controls
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <typeparam name="TValue">Enum base type</typeparam>
        /// <returns>Sequence of value / name pairs</returns>
        public static IEnumerable<EnumItem<TValue>> GetEnumItems<TEnum, TValue>()
            where TEnum : struct
            where TValue : struct
        {
            // Get TEnum type and verify it's an enum
            var enumType = typeof(TEnum);
            if (!IsEnum(enumType))
                throw new ArgumentException($"Type {enumType.Name} is not an enum");

            // Verify TValue is an allowed numeric type
            var valType = typeof(TValue);
            if (!IsValidEnumBase(valType))
                throw new ArgumentException($"Type {enumType.Name} is not a valid enum base type");

            // Project enum values and names
            var items = from f in enumType.GetRuntimeFields()
                        where f.IsPublic && f.IsStatic && f.IsLiteral
                        let val = (TValue)f.GetValue(enumType)
                        let name = f.Name
                        select new EnumItem<TValue>(val, name);
            return items;
        }

        private static bool IsEnum(Type enumType)
        {
            return typeof(Enum).GetTypeInfo().IsAssignableFrom(enumType.GetTypeInfo());
        }

        private static bool IsValidEnumBase(Type valType)
        {
            return valType == typeof(byte)
                || valType == typeof(short)
                || valType == typeof(int)
                || valType == typeof(long);
        }
    }

    /// <summary>
    /// Name-value pair for enum-based lists.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class EnumItem<TValue>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public EnumItem() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">Enum value.</param>
        /// <param name="name">Enum name.</param>
        public EnumItem(TValue value, string name)
        {
            Value = value;
            Name = name;
        }

        /// <summary>
        /// Enum value.
        /// </summary>
        public TValue Value { get; private set; }

        /// <summary>
        /// Enum name.
        /// </summary>
        public string Name { get; private set; }
    }
}
