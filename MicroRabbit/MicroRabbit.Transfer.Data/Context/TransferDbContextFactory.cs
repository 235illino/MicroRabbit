using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MicroRabbit.Transfer.Data.Context;

public class TransferDbContextFactory : IDesignTimeDbContextFactory<TransferDbContext>
{
    public TransferDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TransferDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=DESKTOP-5ED19CH\\SQLSERVER2022DEV;Database=TransferDb;Trusted_Connection=true;TrustServerCertificate=True"
        );


        return new TransferDbContext(optionsBuilder.Options);
    }
}

