namespace YANAN.Mail.Client
{
    //using AutoMapper;
    using YANAN.Mail.Entity;
    /// <summary>
    /// 
    /// </summary>
    public  class AutoMapperConfig
    {
        /// <summary>
        /// AutoMapper 对象映射初始化
        /// </summary>
        public static void Initialize()
        {
            ////配置AutoMapper  属性不区分大小写
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<MailFilterCondition, MailFilterConditionDto>();
            //    cfg.CreateMap<MailFilterConditionDto, MailFilterCondition>();

            //    //Mapper.AssertConfigurationIsValid();//验证机制，用来判断Destination类中的所有属性是否都被映射，如果存在未被映射的属性，则抛出异常
            //});
        }
    }
}
