using AutoMapper;

namespace Roma.Mail.Utilities
{
    public static class AutoMapExtensions
    {
        /// <summary>
        /// 使用AutoMapper将对象转换为另一个对象 <typeparamref name="TDestination"/>.
        /// 在调用此方法之前，必须在对象之间进行映射。
        /// </summary>
        /// <typeparam name="TDestination">Type of the destination object</typeparam>
        /// <param name="source">Source object</param>
        public static TDestination MapTo<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// 执行从源对象到现有目标对象的映射
        /// 在调用此方法之前，必须在对象之间进行映射。
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
