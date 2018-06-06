using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GT.Utilities.Properties
{
    //class Properties
    //{
    //}
    public class grpcreate
    {
        string _grpCallName = "";
        string _grpCallDate = "";
        string _grpCallTime = "";
        string _grpCallEndDate = "";
        string _grpCallEndTime = "";
        int _SchType = 0;
        int _RecursDays = 0;
        string _DaysInWeek = "";
        int _WeekOfMonth = 0;
        string _DayOfMonth = "";
        int _IsMuteDial = 0;
        int _ReminderMins = 0;
        string _WebListIds = "";
        int _IsOnlyDialIn = 0;
        int _IsAllowNonMembers = 0;
        int _OpenLineBefore = 0;
        string _managerMobile = "";
        public string managerMobile
        {
            get { return _managerMobile; }
            set { _managerMobile = value; }
        }
        public string grpCallName
        {
            get { return _grpCallName; }
            set { _grpCallName = value; }
        }
        public string grpCallDate
        {
            get { return _grpCallDate; }
            set { _grpCallDate = value; }
        }
        public string grpCallTime
        {
            get { return _grpCallTime; }
            set { _grpCallTime = value; }
        }
        public int SchType
        {
            get { return _SchType; }
            set { _SchType = value; }
        }
        public int RecurDays
        {
            get { return _RecursDays; }
            set { _RecursDays = value; }
        }
        public string DaysInWeek
        {
            get { return _DaysInWeek; }
            set { _DaysInWeek = value; }
        }
        public int WeekOfMonth
        {
            get { return _WeekOfMonth; }
            set { _WeekOfMonth = value; }
        }
        public string DayOfMonth
        {
            get { return _DayOfMonth; }
            set { _DayOfMonth = value; }
        }
        public int IsMuteDial
        {
            get { return _IsMuteDial; }
            set { _IsMuteDial = value; }
        }
        public int ReminderMins
        {
            get { return _ReminderMins; }
            set { _ReminderMins = value; }
        }
        public string WebListIds
        {
            get { return _WebListIds; }
            set { _WebListIds = value; }
        }
        public int IsOnlyDialIn
        {
            get { return _IsOnlyDialIn; }
            set { _IsOnlyDialIn = value; }
        }
        public int IsAllowNonMembers
        {
            get { return _IsAllowNonMembers; }
            set { _IsAllowNonMembers = value; }
        }
        public int OpenLineBefore
        {
            get { return _OpenLineBefore; }
            set { _OpenLineBefore = value; }
        }
        public string EndDate
        {
            get { return _grpCallEndDate; }
            set { _grpCallEndDate = value; }
        }

        public string EndTime
        {
            get { return _grpCallEndTime; }
            set { _grpCallEndTime = value; }
        }


    }
    public class grpEdit
    {
        string _grpCallName = "";
        string _grpCallDate = "";
        string _grpCallTime = "";
        string _grpCallEndDate = "";
        string _grpCallEndTime = "";
        int _SchType = 0;
        int _RecursDays = 0;
        string _DaysInWeek = "";
        int _WeekOfMonth = 0;
        string _DayOfMonth = "";
        int _IsMuteDial = 0;
        int _ReminderMins = 0;
        int _grpcallID = 0;
        string _WebListIds = "";
        int _IsOnlyDialIn = 0;
        int _IsAllowNonMembers = 0;
        int _OpenLineBefore = 0;
        string _ManagerMobile = "";
        public string managerMobile
        {
            get { return _ManagerMobile; }
            set { _ManagerMobile = value; }
        }
        public int grpcallID
        {
            get { return _grpcallID; }
            set { _grpcallID = value; }
        }
        public string grpCallName
        {
            get { return _grpCallName; }
            set { _grpCallName = value; }
        }
        public string grpCallDate
        {
            get { return _grpCallDate; }
            set { _grpCallDate = value; }
        }
        public string grpCallTime
        {
            get { return _grpCallTime; }
            set { _grpCallTime = value; }
        }
        public int SchType
        {
            get { return _SchType; }
            set { _SchType = value; }
        }
        public int RecurDays
        {
            get { return _RecursDays; }
            set { _RecursDays = value; }
        }
        public string DaysInWeek
        {
            get { return _DaysInWeek; }
            set { _DaysInWeek = value; }
        }
        public int WeekOfMonth
        {
            get { return _WeekOfMonth; }
            set { _WeekOfMonth = value; }
        }
        public string DayOfMonth
        {
            get { return _DayOfMonth; }
            set { _DayOfMonth = value; }
        }
        public int IsMuteDial
        {
            get { return _IsMuteDial; }
            set { _IsMuteDial = value; }
        }
        public int ReminderMins
        {
            get { return _ReminderMins; }
            set { _ReminderMins = value; }
        }
        public string WebListIds
        {
            get { return _WebListIds; }
            set { _WebListIds = value; }
        }
        public int IsOnlyDialIn
        {
            get { return _IsOnlyDialIn; }
            set { _IsOnlyDialIn = value; }
        }
        public int IsAllowNonMembers
        {
            get { return _IsAllowNonMembers; }
            set { _IsAllowNonMembers = value; }
        }
        public int OpenLineBefore
        {
            get { return _OpenLineBefore; }
            set { _OpenLineBefore = value; }
        }
        public string EndDate
        {
            get { return _grpCallEndDate; }
            set { _grpCallEndDate = value; }
        }

        public string EndTime
        {
            get { return _grpCallEndTime; }
            set { _grpCallEndTime = value; }
        }
    }
    public class grpcall
    {
        string _MobileNumber = "";
        int _ConferenceId;
        string _ConferenceRoom = "";
        int _UserId;
        string _CallUUID = "";
        string _Action = "";
        string _HttpConferenceApiUrl = "";
        string _OriginationUrl = "";
        string _ExtraDialString = "";
        string _CallerIdNumber = "";
        string _BulkDialDelimiter = "";
        string _WelcomeClip = "";
        string _WaitClip = "";
        string _MemberMute = "";
        string _StartConferenceOnEnter = "";
        string _EndConferenceOnExit = "";
        string _Moderator = "";
        string _ConferenceAccessKey = "";
        Boolean _IsValidate;
        string _Direction = "";
        string _ConferenceNumber = "";
        int _TotalNumbers;
        string _MemberName = "";
        Boolean _IsModerator;
        Boolean _IsMute;
        Boolean _IsDeaf;
        Boolean _IsAll;
        string _AutoDialToken = "";
        Boolean _IsRetry;
        int _IsAutodial;
        int _GatewayID;
        string _FromNumber = "";
        DataTable _NodeDependedMobileNumberReportIdTable;
        DataTable _NodeDependendGateWaysTable;
        int _timiLimit = 0;
        bool _isCallFromBonus = false;
        int _isPaidClient = 0;
        bool _isInPrivate = false;
        long _instanceId = 0;

        public long InstanceId
        {
            get { return _instanceId; }
            set { _instanceId = value; }
        }

        public int IsPaidClient
        {
            get { return _isPaidClient; }
            set { _isPaidClient = value; }
        }

        public Boolean IsCallFromBonus {
            get { return _isCallFromBonus; }
            set { _isCallFromBonus = value; }
        }
        public Boolean IsInPrivate
        {
            get { return _isInPrivate; }
            set { _isInPrivate = value; }
        }
        public int TimeLimit
        {
            get { return _timiLimit; }
            set { _timiLimit = value; }
        }
        public string MobileNumber
        {
            get { return _MobileNumber; }
            set { _MobileNumber = value; }
        }
        public int ConferenceId
        {
            get { return _ConferenceId; }
            set { _ConferenceId = value; }
        }
        public string ConferenceRoom
        {
            get { return _ConferenceRoom; }
            set { _ConferenceRoom = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public string CallUUID
        {
            get { return _CallUUID; }
            set { _CallUUID = value; }
        }
        public string ConferenceAction
        {
            get { return _Action.ToUpper(); }
            set { _Action = value.ToUpper(); }
        }
        public string HttpConferenceApiUrl
        {
            get { return _HttpConferenceApiUrl; }
            set { _HttpConferenceApiUrl = value; }
        }
        public string OriginationUrl
        {
            get { return _OriginationUrl; }
            set { _OriginationUrl = value; }
        }
        public string ExtraDialString
        {
            get { return _ExtraDialString; }
            set { _ExtraDialString = value; }
        }
        public string CallerIdNumber
        {
            get { return _CallerIdNumber; }
            set { _CallerIdNumber = value; }
        }
        public string BulkDialDelimiter
        {
            get { return _BulkDialDelimiter; }
            set { _BulkDialDelimiter = value; }
        }
        public string WelcomeClip
        {
            get { return _WelcomeClip; }
            set { _WelcomeClip = value; }
        }
        public string WaitClip
        {
            get { return _WaitClip; }
            set { _WaitClip = value; }
        }
        public string MemberMute
        {
            get { return _MemberMute; }
            set { _MemberMute = value; }
        }
        public string StartConferenceOnEnter
        {
            get { return _StartConferenceOnEnter; }
            set { _StartConferenceOnEnter = value; }
        }
        public string EndConferenceOnExit
        {
            get { return _EndConferenceOnExit; }
            set { _EndConferenceOnExit = value; }
        }
        public string Moderator
        {
            get { return _Moderator; }
            set { _Moderator = value; }
        }
        public string ConferenceAccessKey
        {
            get { return _ConferenceAccessKey; }
            set { _ConferenceAccessKey = value; }
        }
        public Boolean IsValidate
        {
            get { return _IsValidate; }
            set { _IsValidate = value; }
        }
        public string Direction
        {
            get { return _Direction.ToUpper(); }
            set { _Direction = value.ToUpper(); }
        }
        public string ConferenceNumber
        {
            get { return _ConferenceNumber; }
            set { _ConferenceNumber = value; }
        }
        public int TotalNumbers
        {
            get { return _TotalNumbers; }
            set { _TotalNumbers = value; }
        }
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }
        public Boolean IsModerator
        {
            get { return _IsModerator; }
            set { _IsModerator = value; }
        }
        public Boolean IsMute
        {
            get { return _IsMute; }
            set { _IsMute = value; }
        }
        public Boolean IsDeaf
        {
            get { return _IsDeaf; }
            set { _IsDeaf = value; }
        }
        public Boolean IsAll
        {
            get { return _IsAll; }
            set { _IsAll = value; }
        }
        public string AutoDialTocken
        {
            get { return _AutoDialToken; }
            set { _AutoDialToken = value; }
        }
        public Boolean IsRetry
        {
            get { return _IsRetry; }
            set { _IsRetry = value; }
        }
        public int IsAutodial
        {
            get { return _IsAutodial; }
            set { _IsAutodial = value; }
        }
        public int GatewayID
        {
            get { return _GatewayID; }
            set { _GatewayID = value; }
        }
        public string FromNumber
        {
            get { return _FromNumber; }
            set { _FromNumber = value; }
        }
        public DataTable NodeDependedMobileNumberReportIdTable
        {
            get { return _NodeDependedMobileNumberReportIdTable; }
            set { _NodeDependedMobileNumberReportIdTable = value; }
        }
        public DataTable NodeDependendGateWaysTable
        {
            get { return _NodeDependendGateWaysTable; }
            set { _NodeDependendGateWaysTable = value; }
        }
    }

    public class CallBackVariable
    {
        string _calluid = "";
        string _event = "";
        string grpCallDirection = "";
        string fromNumber = "";
        string toNumber = "";
        string grpCallStatus = "";
        string recordUrl = "";
        string recordDuration = "";
        string digits = "";
        string recordFileName = "";
        string MemberId = "";
        string grpCallName = "";
        string grpCallAction = "";
        string requestUId = "";
        string requestUUId = "";
        int startTime = 0;
        int endTime = 0;
        string endReason = "";
        Int64 _seqNumber = 0;
        string conferenceUUID = "";
        Int32 _conferenceSize = 0;
        public Int64 SeqNumber
        {
            get { return _seqNumber; }
            set { _seqNumber = value; }
        }
        public Int32 ConferenceSize
        {
            get { return _conferenceSize; }
            set { _conferenceSize = value; }
        }


        public string CallUUID
        {
            get { return _calluid; }
            set { _calluid = value; }
        }

        public string ConferenceUUID
        {
            get { return conferenceUUID; }
            set { conferenceUUID = value; }
        }
        public string Event
        {
            get { return _event; }
            set { _event = value; }
        }
        public string Direction
        {
            get { return grpCallDirection; }
            set { grpCallDirection = value; }
        }
        public string From
        {
            get { return fromNumber; }
            set { fromNumber = value; }
        }
        public string To
        {
            get { return toNumber; }
            set { toNumber = value; }
        }
        public string CallStatus
        {
            get { return grpCallStatus; }
            set { grpCallStatus = value; }
        }
        public string RecordURL
        {
            get { return recordUrl; }
            set { recordUrl = value; }
        }
        public string RecordDuration
        {
            get { return recordDuration; }
            set { recordDuration = value; }
        }
        public string Digits
        {
            get { return digits; }
            set { digits = value; }
        }
        public string fileName
        {
            get { return recordFileName; }
            set { recordFileName = value; }
        }


        public string grpCallMemberID
        {
            get { return MemberId; }
            set { MemberId = value; }
        }
        public string GrpCallName
        {
            get { return grpCallName; }
            set { grpCallName = value; }
        }
        public string GrpCallAction
        {
            get { return grpCallAction; }
            set { grpCallAction = value; }
        }
        public string GrpCallRequestUUID
        {
            get { return requestUId; }
            set { requestUId = value; }
        }
        public string RequestUUID
        {
            get { return requestUUId; }
            set { requestUUId = value; }
        }
        public int StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        public int EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        public string EndReason
        {
            get { return endReason; }
            set { endReason = value; }
        }
    }

    public class RegistrationDetails
    {
        string _Mobile = "";
        string _DeviceUniqueID = "";
        string _DeviceToken = "";
        Int16 _OsID = 0;
        string _ClientIpAddress = "";
        string _TxnID = "";
        string _IsResend = "";
        string userName = "";
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public String Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        public string DeviceUniqueID
        {
            get { return _DeviceUniqueID; }
            set { _DeviceUniqueID = value; }
        }

        public String DeviceToken
        {
            get { return _DeviceToken; }
            set { _DeviceToken = value; }
        }

        public Int16 OsID
        {
            get { return _OsID; }
            set { _OsID = value; }
        }

        public String ClientIpAddress
        {
            get { return _ClientIpAddress; }
            set { _ClientIpAddress = value; }
        }

        public String TxnID
        {
            get { return _TxnID; }
            set { _TxnID = value; }
        }

        public String IsResend
        {
            get { return _IsResend; }
            set { _IsResend = value; }
        }


    }

    public class PusherNotifier
    {
        int isStarted = 0;
        int grpCallID = 0;
        string conferenceName = "";
        public int IsStarted
        {
            get { return isStarted; }
            set { isStarted = value; }
        }
        public string GrpCallName
        {
            get { return conferenceName; }
            set { conferenceName = value; }
        }
        public int GrpCallID
        {
            get { return grpCallID; }
            set { grpCallID = value; }
        }

    }
    
}
