using MediatR;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Services;
using MicroRabbit.Banking.Data.Repository;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace MicroRabbit.Infra.IoC;

public static class BankingDependencyContainer
{
    public static void RegisterBankingServices(IServiceCollection services)
    {
        // Domain Bus
        services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
        {
            var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
            var mediator = sp.GetRequiredService<IMediator>();
            return new RabbitMQBus(mediator, scopeFactory);
        });


        // Application Layer
        services.AddTransient<IAccountService, AccountService>();

        // Data Layer
        services.AddTransient<IAccountRepository, AccountRepository>();
    }
}

