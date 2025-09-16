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

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientTier> ClientTiers { get; set; }

    public virtual DbSet<ClientTierCommunicationType> ClientTierCommunicationTypes { get; set; }

    public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SlaSeverityLevel> SlaSeverityLevels { get; set; }

    public virtual DbSet<SlaSeverityLevelTimer> SlaSeverityLevelTimers { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketActivityLog> TicketActivityLogs { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<TicketMessage> TicketMessages { get; set; }

    public virtual DbSet<TicketNotification> TicketNotifications { get; set; }

    public virtual DbSet<TicketSlaTracking> TicketSlaTrackings { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    public virtual DbSet<TicketSubCategory> TicketSubCategories { get; set; }

    public virtual DbSet<TicketTag> TicketTags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientTierId).HasColumnName("client_tier_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.ClientTier).WithMany(p => p.Clients)
                .HasForeignKey(d => d.ClientTierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_client_client_tiers");

            entity.HasOne(d => d.User).WithMany(p => p.Clients)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_client_users");
        });

        modelBuilder.Entity<ClientTier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Client_Tiers");

            entity.ToTable("client_tiers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<ClientTierCommunicationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Client_Tier_Communication_Types");

            entity.ToTable("client_tier_communication_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.ClientTierId).HasColumnName("client_tier_id");
            entity.Property(e => e.CommunicationTypeId).HasColumnName("communication_type_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.ClientTier).WithMany(p => p.ClientTierCommunicationTypes)
                .HasForeignKey(d => d.ClientTierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_client_tier_communication_types_client_tiers");

            entity.HasOne(d => d.CommunicationType).WithMany(p => p.ClientTierCommunicationTypes)
                .HasForeignKey(d => d.CommunicationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_client_tier_communication_types_client_tier_communication_types");
        });

        modelBuilder.Entity<CommunicationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Communication_Types");

            entity.ToTable("communication_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_By");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_At");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_By");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Roles");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<SlaSeverityLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SLA_Policy");

            entity.ToTable("sla_severity_levels");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.InitialReponseHours).HasColumnName("initial_reponse_hours");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.TargetResolutionHours).HasColumnName("target_resolution_hours");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<SlaSeverityLevelTimer>(entity =>
        {
            entity.ToTable("sla_severity_level_timers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.InitialReponseHours).HasColumnName("initial_reponse_hours");
            entity.Property(e => e.SlaSeverityLevelId).HasColumnName("sla_severity_level_id");
            entity.Property(e => e.TargetResolutionHours).HasColumnName("target_resolution_hours");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ticket");

            entity.ToTable("ticket");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignedToId).HasColumnName("assigned_to_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedById).HasColumnName("created_by_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Subject)
                .HasMaxLength(50)
                .HasColumnName("subject");
            entity.Property(e => e.TicketCategoryId).HasColumnName("ticket_category_id");
            entity.Property(e => e.TicketNumber).HasColumnName("ticket_number");
            entity.Property(e => e.TicketSeverityLevelId).HasColumnName("ticket_severity_level_id");
            entity.Property(e => e.TicketStatusId).HasColumnName("ticket_status_id");
            entity.Property(e => e.TicketSubCategoryId).HasColumnName("ticket_sub_category_id");

            entity.HasOne(d => d.TicketCategory).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TicketCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_ticket_category");
        });

        modelBuilder.Entity<TicketActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ticket_Activity_Log");

            entity.ToTable("ticket_activity_log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.NewTicketStatusId).HasColumnName("new_ticket_status_id");
            entity.Property(e => e.OldTicketStatusId).HasColumnName("old_ticket_status_id");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

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
            entity.HasKey(e => e.Id).HasName("PK_ticket_categories");

            entity.ToTable("ticket_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<TicketMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ticket_Messages");

            entity.ToTable("ticket_messages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.MessageContent).HasColumnName("message_content");
            entity.Property(e => e.RepliedAt)
                .HasColumnType("datetime")
                .HasColumnName("replied_at");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketMessages)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_messages_ticket");
        });

        modelBuilder.Entity<TicketNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ticket_Notifications");

            entity.ToTable("ticket_notifications");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketNotifications)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_notifications_ticket");

            entity.HasOne(d => d.User).WithMany(p => p.TicketNotifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_notifications_users");
        });

        modelBuilder.Entity<TicketSlaTracking>(entity =>
        {
            entity.ToTable("ticket_sla_tracking");

            entity.HasIndex(e => e.Id, "IX_ticket_sla_tracking");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FirstResponseAt)
                .HasColumnType("datetime")
                .HasColumnName("first_response_at");
            entity.Property(e => e.IsResolutionSlaBreach).HasColumnName("is_resolution_sla_breach");
            entity.Property(e => e.IsResponseSlaBreach).HasColumnName("is_response_sla_breach");
            entity.Property(e => e.PausedDtm)
                .HasColumnType("datetime")
                .HasColumnName("paused_dtm");
            entity.Property(e => e.RemainingResolutionDueTime).HasColumnName("remaining_resolution_due_time");
            entity.Property(e => e.RemainingResponseDueTime).HasColumnName("remaining_response_due_time");
            entity.Property(e => e.ResolutionDueDtm)
                .HasColumnType("datetime")
                .HasColumnName("resolution_due_dtm");
            entity.Property(e => e.ResolvedDtm)
                .HasColumnType("datetime")
                .HasColumnName("resolved_dtm");
            entity.Property(e => e.ResponseDueDtm)
                .HasColumnType("datetime")
                .HasColumnName("response_due_dtm");
            entity.Property(e => e.SlaSeverityLevelId).HasColumnName("sla_severity_level_Id");
            entity.Property(e => e.TicketId).HasColumnName("ticket_Id");

            entity.HasOne(d => d.SlaSeverityLevel).WithMany(p => p.TicketSlaTrackings)
                .HasForeignKey(d => d.SlaSeverityLevelId)
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

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<TicketSubCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ticket_sub_categories");

            entity.ToTable("ticket_sub_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.TicketCategoryId).HasColumnName("ticket_category_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Tag).WithMany(p => p.TicketSubCategories)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_sub_category_ticket_tags");

            entity.HasOne(d => d.TicketCategory).WithMany(p => p.TicketSubCategories)
                .HasForeignKey(d => d.TicketCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_sub_category_ticket_sub_category");
        });

        modelBuilder.Entity<TicketTag>(entity =>
        {
            entity.ToTable("ticket_tags");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.SlaSeverityLevelId).HasColumnName("sla_severity_level_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
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
