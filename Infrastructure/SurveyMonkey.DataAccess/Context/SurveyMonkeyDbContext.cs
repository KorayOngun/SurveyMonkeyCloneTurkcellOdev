﻿using Microsoft.EntityFrameworkCore;
using SurveyMonkey.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.DataAccess.Context
{
    public class SurveyMonkeyDbContext : DbContext
    {
        public DbSet<Answer> Answers { get; set; }  
        public DbSet<Choice> Choices { get; set; }
        public DbSet<MultiChoiceAnswer> MultiChoiceAnswers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }  
        public DbSet<SingleChoiceAnswer> SingleChoiceAnswers { get; set; }  
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LineAnswer> LineAnswers { get; set; }  


        public SurveyMonkeyDbContext(DbContextOptions<SurveyMonkeyDbContext> option) : base(option)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MultiChoiceAnswer>(entity =>
            {
                entity.HasKey("ChoiceId", "QuestionId", "AnswerId");

                entity.HasOne<Answer>()
                      .WithMany(a => a.MultiChoiceAnswer)
                      .HasForeignKey(e => e.AnswerId).OnDelete(DeleteBehavior.NoAction);
                
                entity.HasOne<Question>().WithMany().HasForeignKey(e => e.QuestionId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<Choice>().WithMany().HasForeignKey(e => e.ChoiceId).OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<SingleChoiceAnswer>(entity =>
            {
                entity.HasKey("QuestionId", "AnswerId");

                entity.HasOne<Answer>()
                      .WithMany(a => a.SingleChoiceAnswer)
                      .HasForeignKey(e => e.AnswerId).OnDelete(DeleteBehavior.NoAction);

                entity.HasOne<Question>().WithMany().HasForeignKey(e => e.QuestionId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<Choice>().WithMany().HasForeignKey(e => e.ChoiceId).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Choice>(entity =>
            {
                entity.HasOne<Question>()
                      .WithMany(q => q.Choices)
                      .HasForeignKey(c=>c.QuestionId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasOne<Survey>().WithMany(a => a.Questions).HasForeignKey(q => q.SurveyId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(q=>q.QuestionType).WithMany().HasForeignKey(q => q.QuestionTypeId);
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.HasOne(s => s.User).WithMany(u => u.Survey).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Cascade);
                
            });
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasOne<Survey>().WithMany(s => s.Answers).HasForeignKey(a => a.SurveyId).OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<LineAnswer>(entity =>
            {
                entity.HasKey("QuestionId", "AnswerId");
                entity.HasOne<Answer>()
                      .WithMany(a => a.lineAnswers)
                      .HasForeignKey(e => e.AnswerId).OnDelete(DeleteBehavior.NoAction);

                entity.HasOne<Question>().WithMany().HasForeignKey(e => e.QuestionId).OnDelete(DeleteBehavior.Cascade);
                
            });

        }
    }
}
