using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SLAwareWorkerService.Entities.SLAware;

public partial class slaware_dataContext : DbContext
{
    public slaware_dataContext(DbContextOptions<slaware_dataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClientTier> ClientTiers { get; set; }

    public virtual DbSet<ClientTierCommunicationType> ClientTierCommunicationTypes { get; set; }

    public virtual DbSet<ClientTierSeverityLevel> ClientTierSeverityLevels { get; set; }

    public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SlaSeverityLevel> SlaSeverityLevels { get; set; }

    public virtual DbSet<SlaSeverityLevelRule> SlaSeverityLevelRules { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketActivityLog> TicketActivityLogs { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<TicketMessage> TicketMessages { get; set; }

    public virtual DbSet<TicketNotification> TicketNotifications { get; set; }

    public virtual DbSet<TicketSeverityLevel> TicketSeverityLevels { get; set; }

    public virtual DbSet<TicketSlaTracking> TicketSlaTrackings { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    public virtual DbSet<TicketSubCategory> TicketSubCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientTier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Client_Tiers");

            entity.ToTable("client_tiers");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("Created_By");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("Updated_By");
        });

        modelBuilder.Entity<ClientTierCommunicationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Client_Tier_Communication_Types");

            entity.ToTable("client_tier_communication_types");

            entity.Property(e => e.ClientTierId).HasColumnName("Client_Tier_Id");
            entity.Property(e => e.CommunicationTypeId).HasColumnName("Communication_Type_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("Created_By");

            entity.HasOne(d => d.ClientTier).WithMany(p => p.ClientTierCommunicationTypes)
                .HasForeignKey(d => d.ClientTierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_client_tier_communication_types_client_tiers");
        });

        modelBuilder.Entity<ClientTierSeverityLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Client_Tier_Severity_Levels");

            entity.ToTable("client_tier_severity_levels");

            entity.Property(e => e.ClientTierId).HasColumnName("Client_Tier_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(1000)
                .HasColumnName("Created_By");
            entity.Property(e => e.SlaSeverityLevelId).HasColumnName("SLA_Severity_Level_Id");
        });

        modelBuilder.Entity<CommunicationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Communication_Types");

            entity.ToTable("communication_types");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("Created_By");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("Updated_By");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Roles");

            entity.ToTable("roles");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<SlaSeverityLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SLA_Policy");

            entity.ToTable("sla_severity_levels");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("Created_By");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("Updated_By");
        });

        modelBuilder.Entity<SlaSeverityLevelRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SLA_Policy_Rules");

            entity.ToTable("sla_severity_level_rules");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("Created_By");
            entity.Property(e => e.InitialResponseHours).HasColumnName("Initial_Response_Hours");
            entity.Property(e => e.SlaSeverityLevelId).HasColumnName("SLA_Severity_Level_Id");
            entity.Property(e => e.TargetResolutionHours).HasColumnName("Target_Resolution_Hours");

            entity.HasOne(d => d.SlaSeverityLevel).WithMany(p => p.SlaSeverityLevelRules)
                .HasForeignKey(d => d.SlaSeverityLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sla_severity_level_rules_sla_severity_levels");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ticket");

            entity.ToTable("ticket");

            entity.Property(e => e.AssignedToId).HasColumnName("Assigned_To_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedById).HasColumnName("Created_By_Id");
            entity.Property(e => e.SubCategoryId).HasColumnName("Sub_Category_Id");
            entity.Property(e => e.Subject).HasMaxLength(50);
            entity.Property(e => e.TicketNumber).HasColumnName("Ticket_Number");
            entity.Property(e => e.TicketSeverityLevelId).HasColumnName("Ticket_Severity_Level_Id");
            entity.Property(e => e.TicketStatusId).HasColumnName("Ticket_Status_Id");

            entity.HasOne(d => d.TicketSeverityLevel).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TicketSeverityLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_ticket");
        });

        modelBuilder.Entity<TicketActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ticket_Activity_Log");

            entity.ToTable("ticket_activity_log");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("Created_By");
            entity.Property(e => e.NewTicketStatusId).HasColumnName("New_Ticket_Status_Id");
            entity.Property(e => e.OldTicketStatusId).HasColumnName("Old_Ticket_Status_Id");
            entity.Property(e => e.TicketId).HasColumnName("Ticket_Id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("Updated_By");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.NewTicketStatus).WithMany(p => p.TicketActivityLogNewTicketStatuses)
                .HasForeignKey(d => d.NewTicketStatusId)
                .HasConstraintName("FK_ticket_activity_log_ticket_status");

            entity.HasOne(d => d.OldTicketStatus).WithMany(p => p.TicketActivityLogOldTicketStatuses)
                .HasForeignKey(d => d.OldTicketStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_activity_log_ticket_status1");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketActivityLogs)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_activity_log_ticket");

            entity.HasOne(d => d.User).WithMany(p => p.TicketActivityLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_activity_log_users");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.ToTable("ticket_categories");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("Created_By");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("Updated_By");
        });

        modelBuilder.Entity<TicketMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ticket_Messages");

            entity.ToTable("ticket_messages");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.IsRead).HasColumnName("Is_Read");
            entity.Property(e => e.MessageContent).HasColumnName("Message_Content");
            entity.Property(e => e.RepliedAt)
                .HasColumnType("datetime")
                .HasColumnName("Replied_At");
            entity.Property(e => e.TicketId).HasColumnName("Ticket_Id");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketMessages)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_messages_ticket");
        });

        modelBuilder.Entity<TicketNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ticket_Notifications");

            entity.ToTable("ticket_notifications");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("Created_By");
            entity.Property(e => e.IsRead).HasColumnName("Is_Read");
            entity.Property(e => e.TicketId).HasColumnName("Ticket_Id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("Updated_By");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketNotifications)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_notifications_ticket");

            entity.HasOne(d => d.User).WithMany(p => p.TicketNotifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_notifications_users");
        });

        modelBuilder.Entity<TicketSeverityLevel>(entity =>
        {
            entity.ToTable("ticket_severity_levels");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("Created_By");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("Updated_By");
        });

        modelBuilder.Entity<TicketSlaTracking>(entity =>
        {
            entity.ToTable("ticket_sla_tracking");

            entity.HasIndex(e => e.Id, "IX_ticket_sla_tracking");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.FirstResponseAt)
                .HasColumnType("datetime")
                .HasColumnName("First_Response_At");
            entity.Property(e => e.IsResolutionSlaBreach)
                .HasColumnType("datetime")
                .HasColumnName("IsResolution_SLA_Breach");
            entity.Property(e => e.IsResponseSlaBreach)
                .HasColumnType("datetime")
                .HasColumnName("IsResponse_SLA_Breach");
            entity.Property(e => e.PausedDtm)
                .HasColumnType("datetime")
                .HasColumnName("Paused_DTM");
            entity.Property(e => e.RemainingResolutionDueTime).HasColumnName("Remaining_Resolution_Due_Time");
            entity.Property(e => e.RemainingResponseDueTime).HasColumnName("Remaining_Response_Due_Time");
            entity.Property(e => e.ResolutionDueDtm)
                .HasColumnType("datetime")
                .HasColumnName("Resolution_Due_DTM");
            entity.Property(e => e.ResolvedDtm)
                .HasColumnType("datetime")
                .HasColumnName("Resolved_DTM");
            entity.Property(e => e.ResponseDueDtm)
                .HasColumnType("datetime")
                .HasColumnName("Response_Due_DTM");
            entity.Property(e => e.SlaSeverityLevelId).HasColumnName("SLA_Severity_Level_Id");
            entity.Property(e => e.TicketId).HasColumnName("Ticket_Id");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.TicketSlaTracking)
                .HasForeignKey<TicketSlaTracking>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_sla_tracking_sla_severity_levels");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketSlaTrackings)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_sla_tracking_ticket");
        });

        modelBuilder.Entity<TicketStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ticket_Status");

            entity.ToTable("ticket_status");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("Created_By");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TicketSubCategory>(entity =>
        {
            entity.ToTable("ticket_sub_categories");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("Created_By");
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("Updated_By");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Users");

            entity.ToTable("users");

            entity.Property(e => e.ClientTierId).HasColumnName("Client_Tier_Id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_Name");
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");

            entity.HasOne(d => d.ClientTier).WithMany(p => p.Users)
                .HasForeignKey(d => d.ClientTierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_users_client_tiers");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_users_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
