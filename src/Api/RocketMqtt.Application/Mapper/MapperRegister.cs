using Mapster;
using RocketMqtt.Application.ConnInfos.Result;
using RocketMqtt.Application.Users.Result;
using RocketMqtt.Domain.Domain;

namespace RocketMqtt.Application.Mapper;

public class MapperRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<ConnInfo, ConnInfoResult>()
            .Map(dest => dest.CreateTime, src => src.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
        
        config.ForType<User, UserResult>()
            .Map(dest => dest.UserId, src => src.Id);
    }
}