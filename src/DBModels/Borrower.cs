using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationApi
{
    public class Borrower
    {
        [Column("borrower_id")]
        public int BorrowerId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        internal static Action<EntityTypeBuilder<Borrower>>
            EntityBuilder =
                builder =>
                {
                    builder
                        .ToTable("borrower")
                        .HasOne(_ => _.User)
                        .WithOne(_ => _.Borrower)
                        .HasForeignKey<Borrower>(_ => _.UserId);
                };
    }
}