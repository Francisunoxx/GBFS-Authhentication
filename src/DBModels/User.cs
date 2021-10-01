using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationApi
{
    public class User
    {
        [Column("user_id")]
        public int Id { get; set; }
        [Column("password_id")]
        public int PasswordId { get; set; }
        [Column("refresh_token_id")]
        public int RefreshTokenId { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("first_name")]
        public string Firstname { get; set; }
        [Column("last_name")]
        public string Lastname { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("has_reset")]
        public bool HasReset { get; set; }
        [Column("password_reset_token")]
        public string PasswordResetToken { get; set; }

        public RefreshToken RefTok { get; set; }
        public Role Role { get; set; }
        public UserPassword UserPassword { get; set; }
        public Borrower Borrower { get; set; }

        internal static Action<EntityTypeBuilder<User>>
            EntityBuilder =
                builder =>
                {
                    builder
                        .ToTable("user")
                        .HasOne(_ => _.RefTok)
                        .WithOne(_ => _.Usr)
                        .HasForeignKey<RefreshToken>(_ => _.RefreshTokenId);

                    builder
                        .ToTable("user")
                        .HasOne(_ => _.Role)
                        .WithOne(_ => _.User)
                        .HasForeignKey<Role>(_ => _.RoleId);

                    builder
                        .ToTable("user")
                        .HasOne(_ => _.UserPassword)
                        .WithOne(_ => _.User)
                        .HasForeignKey<UserPassword>(_ => _.UserPasswordId);

                    builder
                        .ToTable("user")
                        .HasOne(_ => _.Borrower)
                        .WithOne(_ => _.User)
                        .HasForeignKey<Borrower>(_ => _.UserId);
                };
    }
}
