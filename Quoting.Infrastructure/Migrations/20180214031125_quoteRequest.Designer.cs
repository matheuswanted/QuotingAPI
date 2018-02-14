﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Quoting.Domain.Models;
using Quoting.Infrastructure;
using System;

namespace Quoting.Infrastructure.Migrations
{
    [DbContext(typeof(QuotingDbContext))]
    [Migration("20180214031125_quoteRequest")]
    partial class quoteRequest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("Relational:Sequence:.seq_base_price_rules", "'seq_base_price_rules', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:.seq_customers", "'seq_customers', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:.seq_price_modifier_rules", "'seq_price_modifier_rules', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:.seq_quote_requests", "'seq_quote_requests', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:.seq_vehicles", "'seq_vehicles', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Quoting.Domain.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "seq_customers")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<string>("SSN")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Quoting.Domain.Models.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerId");

                    b.Property<int?>("RequestId");

                    b.Property<int>("Status");

                    b.Property<decimal>("Value");

                    b.Property<int?>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RequestId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("Quoting.Domain.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "seq_vehicles")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<int?>("CustomerId")
                        .IsRequired();

                    b.Property<string>("Make")
                        .IsRequired();

                    b.Property<int>("ManufacturingYear");

                    b.Property<string>("Model")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Quoting.Domain.ValueObjects.BasePriceRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "seq_base_price_rules")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<decimal>("BasePrice");

                    b.Property<string>("Make");

                    b.Property<string>("Model");

                    b.Property<string>("Type");

                    b.Property<int?>("Year");

                    b.HasKey("Id");

                    b.ToTable("BasePriceRules");
                });

            modelBuilder.Entity("Quoting.Domain.ValueObjects.PriceModifierRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "seq_price_modifier_rules")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("Gender");

                    b.Property<decimal>("Modifier");

                    b.HasKey("Id");

                    b.ToTable("PriceModifierRules");
                });

            modelBuilder.Entity("Quoting.Domain.ValueObjects.QuoteRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "seq_quote_requests")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.HasKey("Id");

                    b.ToTable("QuoteRequests");
                });

            modelBuilder.Entity("Quoting.Domain.Models.Quote", b =>
                {
                    b.HasOne("Quoting.Domain.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("Quoting.Domain.ValueObjects.QuoteRequest", "Request")
                        .WithMany()
                        .HasForeignKey("RequestId");

                    b.HasOne("Quoting.Domain.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("Quoting.Domain.Models.Vehicle", b =>
                {
                    b.HasOne("Quoting.Domain.Models.Customer")
                        .WithMany("Vehicles")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Quoting.Domain.ValueObjects.PriceModifierRule", b =>
                {
                    b.OwnsOne("Quoting.Domain.ValueObjects.Range", "AgeRange", b1 =>
                        {
                            b1.Property<int>("PriceModifierRuleId");

                            b1.Property<int?>("End");

                            b1.Property<int?>("Start");

                            b1.ToTable("PriceModifierRules");

                            b1.HasOne("Quoting.Domain.ValueObjects.PriceModifierRule")
                                .WithOne("AgeRange")
                                .HasForeignKey("Quoting.Domain.ValueObjects.Range", "PriceModifierRuleId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("Quoting.Domain.ValueObjects.QuoteRequest", b =>
                {
                    b.OwnsOne("Quoting.Domain.ValueObjects.QuoteRequestCustomer", "Customer", b1 =>
                        {
                            b1.Property<int>("QuoteRequestId");

                            b1.Property<string>("Address");

                            b1.Property<DateTime>("BirthDate");

                            b1.Property<string>("Email");

                            b1.Property<string>("Gender");

                            b1.Property<string>("Phone");

                            b1.Property<string>("SSN");

                            b1.ToTable("QuoteRequests");

                            b1.HasOne("Quoting.Domain.ValueObjects.QuoteRequest")
                                .WithOne("Customer")
                                .HasForeignKey("Quoting.Domain.ValueObjects.QuoteRequestCustomer", "QuoteRequestId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("Quoting.Domain.ValueObjects.QuoteRequestVehicle", "Vehicle", b1 =>
                        {
                            b1.Property<int>("QuoteRequestId");

                            b1.Property<string>("Make");

                            b1.Property<int>("ManufacturingYear");

                            b1.Property<string>("Model");

                            b1.Property<string>("Type");

                            b1.ToTable("QuoteRequests");

                            b1.HasOne("Quoting.Domain.ValueObjects.QuoteRequest")
                                .WithOne("Vehicle")
                                .HasForeignKey("Quoting.Domain.ValueObjects.QuoteRequestVehicle", "QuoteRequestId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}