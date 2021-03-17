using DanielFinal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanielFinal.Data
{
    public class DanielFinalDbContext : DbContext
    {
        public DanielFinalDbContext(DbContextOptions<DanielFinalDbContext> options)
        : base(options)
        {
        }

        //public DbSet<Answers> Answerss { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }
        //public DbSet<SurveyUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Option>(option =>
            {
                option.Property(p => p.Text).IsRequired();
                option.Property(p => p.Id).IsRequired();
                option.HasKey(p => p.Id);
                option.Property(p => p.Order).IsRequired();
                option.Property(p => p.Text).HasMaxLength(600).IsRequired();
                option.Ignore(p => p.QuestionId);
            });



            modelBuilder.Entity<Question>(question =>
            {
                question.Property(p => p.Id).IsRequired();
                question.HasKey(p => p.Id);
                question.Property(p => p.Description).HasMaxLength(800).IsRequired();
               // question.HasMany(p => p.Options);
               // question.ToTable("Question");
            });

        /*
            modelBuilder.Entity<Answers>(answer =>
            {
                answer.Property(a => a.Id).IsRequired();
                answer.HasKey(a => a.Id);
                //a.Property(a => a.Score).IsRequired();

                answer.HasOne(a => a.Option)
                    .WithMany(p => p.Options)
                    .HasForeignKey(a => a.PlayerId);

                answer.HasOne(a => a.SurveyUser)
                    .WithMany(s => s.Users)
                    .HasForeignKey(a => a.MatchId);
            });

            modelBuilder.Entity<SurveyUser>(user =>
            {
                user.Property(s => s.Id).IsRequired();
                user.HasKey(s => s.Id);
                user.Property(s => s.FirstName).IsRequired();
                user.Property(s => s.LastName).IsRequired();
                user.Property(s => s.Country).IsRequired();
                user.ToTable("Users");

            });
        */
        }
    }
}
