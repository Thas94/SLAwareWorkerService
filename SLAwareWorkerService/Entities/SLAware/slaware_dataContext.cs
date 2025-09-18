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

    public virtual DbSet<SlaSeverityLevelRule> SlaSeverityLevelRules { get; set; }

    public virtual DbSet<SubCategorySeverityLevel> SubCategorySeverityLevels { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketActivityLog> TicketActivityLogs { get; set; }

    public virtual DbSet<TicketBreachLog> TicketBreachLogs { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<TicketMessage> TicketMessages { get; set; }

    public virtual DbSet<TicketNotification> TicketNotifications { get; set; }

    public virtual DbSet<TicketSeverityLevel> TicketSeverityLevels { get; set; }

    public virtual DbSet<TicketSlaTracking> TicketSlaTrackings { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    public virtual DbSet<TicketSubCategory> TicketSubCategories { get; set; }

    public virtual DbSet<TicketTag> TicketTags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCompany> UserCompanies { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientTierId).HasColumnName("client_tier_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");

            entity.HasOne(d => d.ClientTier).WithMany(p => p.Clients)
                .HasForeignKey(d => d.ClientTierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_client_client_tiers");
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
            entity.HasKey(e => e.Id).HasName("PK__sla_seve__3213E83FADCFCAD3");

            entity.ToTable("sla_severity_levels");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Severity)
                .HasMaxLength(10)
                .HasColumnName("severity");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<SlaSeverityLevelRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sla_seve__3213E83FC0D22A40");

            entity.ToTable("sla_severity_level_rules");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.InitialResponseHours).HasColumnName("initial_response_hours");
            entity.Property(e => e.SlaSeverityLevelId).HasColumnName("sla_severity_level_id");
            entity.Property(e => e.TargetResolutionHours).HasColumnName("target_resolution_hours");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.SlaSeverityLevel).WithMany(p => p.SlaSeverityLevelRules)
                .HasForeignKey(d => d.SlaSeverityLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sla_severity_level_rules");
        });

        modelBuilder.Entity<SubCategorySeverityLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sub_cate__3213E83F15CA460D");

            entity.ToTable("sub_category_severity_level");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.SlaSeverityLevelId).HasColumnName("sla_severity_level_id");
            entity.Property(e => e.SubCategoryId).HasColumnName("sub_category_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.SlaSeverityLevel).WithMany(p => p.SubCategorySeverityLevels)
                .HasForeignKey(d => d.SlaSeverityLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sub_categ__sla_s__75A278F5");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.SubCategorySeverityLevels)
                .HasForeignKey(d => d.SubCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sub_categ__sub_c__74AE54BC");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ticket");

            entity.ToTable("ticket");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignedToId).HasColumnName("assigned_to_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedById).HasColumnName("created_by_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.SeverityLevelId).HasColumnName("severity_level_id");
            entity.Property(e => e.SubCategoryId).HasColumnName("sub_category_id");
            entity.Property(e => e.Subject)
                .HasMaxLength(50)
                .HasColumnName("subject");
            entity.Property(e => e.TicketNumber)
                .HasMaxLength(50)
                .HasColumnName("ticket_number");
            entity.Property(e => e.TicketStatusId).HasColumnName("ticket_status_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_ticket_category");

            entity.HasOne(d => d.SeverityLevel).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeverityLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tickets_severity_level");
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

        modelBuilder.Entity<TicketBreachLog>(entity =>
        {
            entity.ToTable("ticket_breach_logs");

            entity.Property(e => e.ResolutionBreachedDate)
                .HasColumnType("datetime")
                .HasColumnName("resolution_breached_date");
            entity.Property(e => e.ResolutionDueDtm)
                .HasColumnType("datetime")
                .HasColumnName("resolution_due_dtm");
            entity.Property(e => e.ResolutionRemainingTime).HasColumnName("resolution_remaining_time");
            entity.Property(e => e.ResponseBreachedDate)
                .HasColumnType("datetime")
                .HasColumnName("response_breached_date");
            entity.Property(e => e.ResponseDueDtm)
                .HasColumnType("datetime")
                .HasColumnName("response_due_dtm");
            entity.Property(e => e.ResponseRemainingTime).HasColumnName("response_remaining_time");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ticket_c__3213E83F264495A0");

            entity.ToTable("ticket_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
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

        modelBuilder.Entity<TicketSeverityLevel>(entity =>
        {
            entity.ToTable("ticket_severity_levels");

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
            entity.Property(e => e.ResolutionSlaBreachDtm)
                .HasColumnType("datetime")
                .HasColumnName("resolution_sla_breach_dtm");
            entity.Property(e => e.ResolvedDtm)
                .HasColumnType("datetime")
                .HasColumnName("resolved_dtm");
            entity.Property(e => e.ResponseDueDtm)
                .HasColumnType("datetime")
                .HasColumnName("response_due_dtm");
            entity.Property(e => e.ResponseSlaBreachDtm)
                .HasColumnType("datetime")
                .HasColumnName("response_sla_breach_dtm");
            entity.Property(e => e.SlaSeverityLevelId).HasColumnName("sla_severity_level_Id");
            entity.Property(e => e.TicketId).HasColumnName("ticket_Id");

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
            entity.HasKey(e => e.Id).HasName("PK__ticket_s__3213E83F0E50E4B1");

            entity.ToTable("ticket_sub_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.TicketCategoryId).HasColumnName("ticket_category_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.TicketCategory).WithMany(p => p.TicketSubCategories)
                .HasForeignKey(d => d.TicketCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ticket_sub_category_ticket_category");
        });

        modelBuilder.Entity<TicketTag>(entity =>
        {
            entity.ToTable("ticket_tags");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
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

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<UserCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_com__3213E83F1F98F811");

            entity.ToTable("user_company");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserCompanies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCompany_Users");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_rol__3213E83FF25189E7");

            entity.ToTable("user_roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasColumnName("created_by");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
