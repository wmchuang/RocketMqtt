using Mapster;
using RocketMqtt.Application.ConnInfos.Command;
using RocketMqtt.Domain.Domain;

namespace RocketMqtt.Application.Mapper;

public class MapperRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateConnInfoCommand, ConnInfo>()
            .Map(dest => dest.Endpoint, src => src.Endpoint1);
    }
}