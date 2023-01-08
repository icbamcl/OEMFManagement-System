using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SIP
/// </summary>
public class UnitSIPGetSet
{
    public UnitSIPGetSet()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private int sipNumber;
    private decimal installAmount;
    private string autoRenewal;
    private string payFrequency;
    private string monhlySIPDate;
    private string eftDebitStart;
    private string eftDebitEnd;
    private int durationInMonth;
    private string stepUpOption;
    private decimal stepUpAmount;
    private string routingNo;
    private string bankAccNumber;
    private string eftDate;
    private string eftAccType;
    private string scheduleCreateBy;
    private string scheduleCreateDate;

    public int SipNumber
    {
        get
        {
            return sipNumber;
        }

        set
        {
            sipNumber = value;
        }
    }

    public decimal InstallAmount
    {
        get
        {
            return installAmount;
        }

        set
        {
            installAmount = value;
        }
    }

    public string AutoRenewal
    {
        get
        {
            return autoRenewal;
        }

        set
        {
            autoRenewal = value;
        }
    }

    public string PayFrequency
    {
        get
        {
            return payFrequency;
        }

        set
        {
            payFrequency = value;
        }
    }

    public string MonhlySIPDate
    {
        get
        {
            return monhlySIPDate;
        }

        set
        {
            monhlySIPDate = value;
        }
    }

    public string EftDebitStart
    {
        get
        {
            return eftDebitStart;
        }

        set
        {
            eftDebitStart = value;
        }
    }

    public string EftDebitEnd
    {
        get
        {
            return eftDebitEnd;
        }

        set
        {
            eftDebitEnd = value;
        }
    }

    public int DurationInMonth
    {
        get
        {
            return durationInMonth;
        }

        set
        {
            durationInMonth = value;
        }
    }

    public decimal StepUpAmount
    {
        get
        {
            return stepUpAmount;
        }

        set
        {
            stepUpAmount = value;
        }
    }

    public string RoutingNo
    {
        get
        {
            return routingNo;
        }

        set
        {
            routingNo = value;
        }
    }

    public string BankAccNumber
    {
        get
        {
            return bankAccNumber;
        }

        set
        {
            bankAccNumber = value;
        }
    }

    public string EftDate
    {
        get
        {
            return eftDate;
        }

        set
        {
            eftDate = value;
        }
    }

    public string EftAccType
    {
        get
        {
            return eftAccType;
        }

        set
        {
            eftAccType = value;
        }
    }

    public string ScheduleCreateDate
    {
        get
        {
            return scheduleCreateDate;
        }

        set
        {
            scheduleCreateDate = value;
        }
    }

    public string ScheduleCreateBy
    {
        get
        {
            return scheduleCreateBy;
        }

        set
        {
            scheduleCreateBy = value;
        }
    }
}