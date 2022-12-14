// <auto-generated />
using System;
using Factum.Modules.Ledger.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Factum.Modules.Ledger.Infrastructure.EF.Migrations
{
    [DbContext(typeof(LedgerDbContext))]
    partial class LedgerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("ledger")
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Factum.Modules.Ledger.Core.Blocks.Entities.Block", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Confirmation")
                        .HasColumnType("int");

                    b.Property<byte[]>("EntriesRootHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PreviousBlockHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid?>("PreviousBlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId")
                        .IsUnique();

                    b.HasIndex("PreviousBlockId")
                        .IsUnique()
                        .HasFilter("[PreviousBlockId] IS NOT NULL");

                    b.ToTable("Blockchain", "ledger");
                });

            modelBuilder.Entity("Factum.Modules.Ledger.Core.Entries.Entities.Entry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid?>("BlockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("MetadataHash")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.HasIndex("BusinessId")
                        .IsUnique();

                    b.ToTable("Entries", "ledger");
                });

            modelBuilder.Entity("Factum.Modules.Ledger.Core.Entries.Entities.EntryMetadata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("EntryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.HasIndex("Key", "Value");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("Key", "Value"), false);

                    b.ToTable("EntryMetadata", "ledger");
                });

            modelBuilder.Entity("Factum.Shared.Infrastructure.Messaging.Outbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProcessedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReceivedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Inbox", "ledger");
                });

            modelBuilder.Entity("Factum.Shared.Infrastructure.Messaging.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("TraceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Outbox", "ledger");
                });

            modelBuilder.Entity("Factum.Modules.Ledger.Core.Blocks.Entities.Block", b =>
                {
                    b.HasOne("Factum.Modules.Ledger.Core.Blocks.Entities.Block", "PreviousBlock")
                        .WithOne()
                        .HasForeignKey("Factum.Modules.Ledger.Core.Blocks.Entities.Block", "PreviousBlockId")
                        .HasPrincipalKey("Factum.Modules.Ledger.Core.Blocks.Entities.Block", "BusinessId");

                    b.Navigation("PreviousBlock");
                });

            modelBuilder.Entity("Factum.Modules.Ledger.Core.Entries.Entities.Entry", b =>
                {
                    b.HasOne("Factum.Modules.Ledger.Core.Blocks.Entities.Block", "Block")
                        .WithMany("Entries")
                        .HasForeignKey("BlockId")
                        .HasPrincipalKey("BusinessId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Block");
                });

            modelBuilder.Entity("Factum.Modules.Ledger.Core.Entries.Entities.EntryMetadata", b =>
                {
                    b.HasOne("Factum.Modules.Ledger.Core.Entries.Entities.Entry", "Entry")
                        .WithMany("Metadata")
                        .HasForeignKey("EntryId")
                        .HasPrincipalKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");
                });

            modelBuilder.Entity("Factum.Modules.Ledger.Core.Blocks.Entities.Block", b =>
                {
                    b.Navigation("Entries");
                });

            modelBuilder.Entity("Factum.Modules.Ledger.Core.Entries.Entities.Entry", b =>
                {
                    b.Navigation("Metadata");
                });
#pragma warning restore 612, 618
        }
    }
}
