﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Models;

namespace UserManagement.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Role)
            .HasMaxLength(100);
        builder.Property(u => u.RefreshToken)
            .HasMaxLength(64);
    }
}