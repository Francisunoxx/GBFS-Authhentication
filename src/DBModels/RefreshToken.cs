using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationApi
{
    public class RefreshToken
    {
        [Column("refresh_token_id")]
        public int RefreshTokenId { get; set; }
        [Column("token")]
        public string Token { get; set; }
        public User Usr { get; set; }
        internal static Action<EntityTypeBuilder<RefreshToken>>
            EntityBuilder =
                builder =>
                {
                    builder
                        .ToTable("refresh_token")
                        .HasOne(_ => _.Usr)
                        .WithOne(_ => _.RefTok)
                        .HasForeignKey<User>(_ => _.RefreshTokenId);
                };
    }
}