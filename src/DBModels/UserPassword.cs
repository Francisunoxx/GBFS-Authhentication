using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationApi
{
    public class UserPassword
    {
        [Column("password_id")]
        public int UserPasswordId { get; set; }

        [Column("password")]
        public string Password { get; set; }
        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }
        public User User { get; set; }

        internal static Action<EntityTypeBuilder<UserPassword>>
            EntityBuilder =
                builder =>
                {
                    builder
                        .ToTable("password")
                        .HasOne(_ => _.User)
                        .WithOne(_ => _.UserPassword)
                        .HasForeignKey<User>(_ => _.PasswordId);
                };
    }
}