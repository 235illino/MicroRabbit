using MediatR;
using MicroRabbit.Banking.Domain.CommandHandlers;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Application.Services;
using MicroRabbit.Transfer.Data.Repository;
using MicroRabbit.Transfer.Domain.EventHandlers;
using MicroRabbit.Transfer.Domain.Events;
using MicroRabbit.Transfer.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MicroRabbit.Infra.IoC;

public static class TransferDependencyContainer
{
    public static void RegisterTransferServices(IServiceCollection services)
    {
        // Domain Bus
        services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
        {
            var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
            var mediator = sp.GetRequiredService<IMediator>();
            return new RabbitMQBus(mediator, scopeFactory);
        });

        // Subscription Manager
        services.AddTransient<TransferEventHandler>();

        // Domain Transfer Events
        services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

        // Application Layer
        services.AddTransient<ITransferService, TransferService>();

        // Data Layer
        services.AddTransient<ITransferRepository, TransferRepository>();

        // Domain Transfer Commands
        services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();
    }
}

