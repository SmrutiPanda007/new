﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

public partial class grptalkEntities : DbContext
{
    public grptalkEntities()
        : base("name=grptalkEntities")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }

    public virtual DbSet<Email_Queue_Conference> Email_Queue_Conference { get; set; }
    public virtual DbSet<errors_log> errors_log { get; set; }
    public virtual DbSet<Services_HeartBeat_Info> Services_HeartBeat_Info { get; set; }
    public virtual DbSet<Services_HeartBeat_Info_Conf> Services_HeartBeat_Info_Conf { get; set; }
    public virtual DbSet<SMTP_Queue> SMTP_Queue { get; set; }
    public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    public virtual DbSet<user> users { get; set; }
    public virtual DbSet<AdminUserInfo> AdminUserInfoes { get; set; }
    public virtual DbSet<Agentslist> Agentslists { get; set; }
    public virtual DbSet<BackUpContacts4Aug> BackUpContacts4Aug { get; set; }
    public virtual DbSet<CallerId> CallerIds { get; set; }
    public virtual DbSet<CallerIdsCreateUpdateTracking> CallerIdsCreateUpdateTrackings { get; set; }
    public virtual DbSet<callflow> callflows { get; set; }
    public virtual DbSet<clicktocall_reports> clicktocall_reports { get; set; }
    public virtual DbSet<conference_call_activities> conference_call_activities { get; set; }
    public virtual DbSet<conference_members> conference_members { get; set; }
    public virtual DbSet<conference_members_devicetoken> conference_members_devicetoken { get; set; }
    public virtual DbSet<conference_members_feb_08_2015> conference_members_feb_08_2015 { get; set; }
    public virtual DbSet<conference_membersBackup> conference_membersBackup { get; set; }
    public virtual DbSet<conference_schedules> conference_schedules { get; set; }
    public virtual DbSet<conference_schedules_Trakings> conference_schedules_Trakings { get; set; }
    public virtual DbSet<conference_studio> conference_studio { get; set; }
    public virtual DbSet<ConferenceAutoDialInfo> ConferenceAutoDialInfoes { get; set; }
    public virtual DbSet<ConferenceBalanceDeductionTrack> ConferenceBalanceDeductionTracks { get; set; }
    public virtual DbSet<ConferenceInterConnectNumber> ConferenceInterConnectNumbers { get; set; }
    public virtual DbSet<ConferenceRecording> ConferenceRecordings { get; set; }
    public virtual DbSet<ConferenceRunTrack> ConferenceRunTracks { get; set; }
    public virtual DbSet<contact> contacts { get; set; }
    public virtual DbSet<contacts_feb_08_2015> contacts_feb_08_2015 { get; set; }
    public virtual DbSet<contacts_groups> contacts_groups { get; set; }
    public virtual DbSet<ContactsBackup> ContactsBackups { get; set; }
    public virtual DbSet<CountryMaster> CountryMasters { get; set; }
    public virtual DbSet<CurrencyMaster> CurrencyMasters { get; set; }
    public virtual DbSet<DeleteConferenceMembersTracking> DeleteConferenceMembersTrackings { get; set; }
    public virtual DbSet<deletecontactid> deletecontactids { get; set; }
    public virtual DbSet<DeleteContactsTracking> DeleteContactsTrackings { get; set; }
    public virtual DbSet<email_templates> email_templates { get; set; }
    public virtual DbSet<EmailCampainResponse> EmailCampainResponses { get; set; }
    public virtual DbSet<error_log> error_log { get; set; }
    public virtual DbSet<FeatureMaster> FeatureMasters { get; set; }
    public virtual DbSet<GateWay> GateWays { get; set; }
    public virtual DbSet<LowBalanceAlertsSent> LowBalanceAlertsSents { get; set; }
    public virtual DbSet<OptedInsSet> OptedInsSets { get; set; }
    public virtual DbSet<Optin> Optins { get; set; }
    public virtual DbSet<PulseMaster> PulseMasters { get; set; }
    public virtual DbSet<report> reports { get; set; }
    public virtual DbSet<ScheduleMailTrack> ScheduleMailTracks { get; set; }
    public virtual DbSet<SMS_Details_Conf> SMS_Details_Conf { get; set; }
    public virtual DbSet<SMS_Queue> SMS_Queue { get; set; }
    public virtual DbSet<SMS_Queue_Conf> SMS_Queue_Conf { get; set; }
    public virtual DbSet<SMS_Reports_Conf> SMS_Reports_Conf { get; set; }
    public virtual DbSet<test_usercallerids> test_usercallerids { get; set; }
    public virtual DbSet<testUserpriceInfo> testUserpriceInfoes { get; set; }
    public virtual DbSet<TimeZone> TimeZones { get; set; }
    public virtual DbSet<user_group_contacts> user_group_contacts { get; set; }
    public virtual DbSet<user_group_contactsBackup> user_group_contactsBackup { get; set; }
    public virtual DbSet<user_groups> user_groups { get; set; }
    public virtual DbSet<user_login_activities> user_login_activities { get; set; }
    public virtual DbSet<user_login_activities_mod_mem> user_login_activities_mod_mem { get; set; }
    public virtual DbSet<UserBalanceAddingTrack> UserBalanceAddingTracks { get; set; }
    public virtual DbSet<UserBalanceDepositHistory> UserBalanceDepositHistories { get; set; }
    public virtual DbSet<UserBalanceInfo> UserBalanceInfoes { get; set; }
    public virtual DbSet<UserBalanceInfo_10Apr> UserBalanceInfo_10Apr { get; set; }
    public virtual DbSet<UserBalanceMaster> UserBalanceMasters { get; set; }
    public virtual DbSet<UserCallerId> UserCallerIds { get; set; }
    public virtual DbSet<UserCallerids_10Apr> UserCallerids_10Apr { get; set; }
    public virtual DbSet<UserCallerIdsBackUp> UserCallerIdsBackUps { get; set; }
    public virtual DbSet<UserCallerIDsTriggerTable> UserCallerIDsTriggerTables { get; set; }
    public virtual DbSet<UserContactsTraking> UserContactsTrakings { get; set; }
    public virtual DbSet<UserMissedCallToGetInformation> UserMissedCallToGetInformations { get; set; }
    public virtual DbSet<UserPriceInfo> UserPriceInfoes { get; set; }
    public virtual DbSet<UserPriceInfo_10Apr> UserPriceInfo_10Apr { get; set; }
    public virtual DbSet<UserPriceInfoHistory> UserPriceInfoHistories { get; set; }
    public virtual DbSet<UserPriceMaster> UserPriceMasters { get; set; }
    public virtual DbSet<UserSubAccount> UserSubAccounts { get; set; }
    public virtual DbSet<UserToken> UserTokens { get; set; }
    public virtual DbSet<UserUnauthorizedAction> UserUnauthorizedActions { get; set; }
    public virtual DbSet<xl_upload_logs> xl_upload_logs { get; set; }
    public virtual DbSet<yconference_email_list> yconference_email_list { get; set; }
    public virtual DbSet<yconference_Emails_list> yconference_Emails_list { get; set; }
    public virtual DbSet<yconference_mails_list> yconference_mails_list { get; set; }
}
