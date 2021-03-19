using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace CodeGenerator.Infra.Common.Extensions
{
    public static class TypeExtension
    {
        /// <summary>
        ///     A Type extension method that creates an instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The new instance.</returns>
        public static T CreateInstance<T>([NotNull] this Type @this) => (T)Activator.CreateInstance(@this);

        /// <summary>
        ///     A Type extension method that creates an instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The new instance.</returns>
        public static T CreateInstance<T>([NotNull] this Type @this, params object[] args) => (T)Activator.CreateInstance(@this, args);

        /// <summary>
        /// if a type has empty constructor
        /// </summary>
        /// <param name="type">type</param>
        /// <returns></returns>
        public static bool HasEmptyConstructor([NotNull] this Type type)
            => type.GetConstructors(BindingFlags.Instance).Any(c => c.GetParameters().Length == 0);

        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        private static readonly ConcurrentDictionary<Type, object> _defaultValues =
            new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// 根据 Type 获取默认值，实现类似 default(T) 的功能
        /// </summary>
        /// <param name="type">type</param>
        /// <returns></returns>
        public static object GetDefaultValue([NotNull] this Type type) =>
            type.IsValueType && type != typeof(void) ? _defaultValues.GetOrAdd(type, Activator.CreateInstance) : null;

        /// <summary>
        /// GetUnderlyingType if nullable else return self
        /// </summary>
        /// <param name="type">type</param>
        /// <returns></returns>
        public static Type Unwrap([NotNull] this Type type)
            => Nullable.GetUnderlyingType(type) ?? type;

        /// <summary>
        /// GetUnderlyingType
        /// </summary>
        /// <param name="type">type</param>
        /// <returns></returns>
        public static Type GetUnderlyingType([NotNull] this Type type)
            => Nullable.GetUnderlyingType(type);
        public static string GetFullNameWithAssemblyName(this Type type)
        {
            return type.FullName + ", " + type.Assembly.GetName().Name;
        }

        /// <summary>
        /// Determines whether an instance of this type can be assigned to
        /// an instance of the <typeparamref name="TTarget"></typeparamref>.
        ///
        /// Internally uses <see cref="Type.IsAssignableFrom"/>.
        /// </summary>
        /// <typeparam name="TTarget">Target type</typeparam> (as reverse).
        public static bool IsAssignableTo<TTarget>([NotNull] this Type type)
        {
            Check.NotNull(type, nameof(type));

            return type.IsAssignableTo(typeof(TTarget));
        }

        /// <summary>
        /// Determines whether an instance of this type can be assigned to
        /// an instance of the <paramref name="targetType"></paramref>.
        ///
        /// Internally uses <see cref="Type.IsAssignableFrom"/> (as reverse).
        /// </summary>
        /// <param name="type">this type</param>
        /// <param name="targetType">Target type</param>
        public static bool IsAssignableTo([NotNull] this Type type, [NotNull] Type targetType)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(targetType, nameof(targetType));

            return targetType.IsAssignableFrom(type);
        }

        /// <summary>
        /// Gets all base classes of this type.
        /// </summary>
        /// <param name="type">The type to get its base classes.</param>
        /// <param name="includeObject">True, to include the standard <see cref="object"/> type in the returned array.</param>
        public static Type[] GetBaseClasses([NotNull] this Type type, bool includeObject = true)
        {
            Check.NotNull(type, nameof(type));

            var types = new List<Type>();
            AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject);
            return types.ToArray();
        }

        private static void AddTypeAndBaseTypesRecursively(
            [NotNull] List<Type> types,
            [CanBeNull] Type type,
            bool includeObject)
        {
            Check.NotNull(types, nameof(types));

            if (type == null)
            {
                return;
            }

            if (!includeObject && type == typeof(object))
            {
                return;
            }

            AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject);
            types.Add(type);
        }
    }
}
