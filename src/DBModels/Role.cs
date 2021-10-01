using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationApi
{
    public class Role
    {
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("type")]
        public string Type { get; set; }
        public User User { get; set; }
        internal static Action<EntityTypeBuilder<Role>>
            EntityBuilder =
                builder =>
                {
                    builder
                        .ToTable("role")
                        .HasOne(_ => _.User)
                        .WithOne(_ => _.Role)
                        .HasForeignKey<User>(_ => _.RoleId);
                };
    }
}