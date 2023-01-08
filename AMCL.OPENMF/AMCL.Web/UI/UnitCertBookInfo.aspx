<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitCertBookInfo.aspx.cs" Inherits="UI_UnitCertBookInfo" Title=" Cert Book Entry Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />  
    <script language="javascript" type="text/javascript"> 
    
   
    function fnCheckAll()
    {
        for (Looper=0; Looper < document.forms[0].length ; Looper++ )
        {
            var strType = document.forms[0].elements[Looper].type;
            if (strType=="checkbox")
            {
                
                document.forms[0].elements[Looper].checked=true;
             
                if(document.forms[0].elements[Looper].id.indexOf("ACheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"A");
                }
                if(document.forms[0].elements[Looper].id.indexOf("BCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"B");
                }
                if(document.forms[0].elements[Looper].id.indexOf("CCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"C");
                }
                if(document.forms[0].elements[Looper].id.indexOf("DCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"D");
                }
                if(document.forms[0].elements[Looper].id.indexOf("ECheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"E");
                }
                if(document.forms[0].elements[Looper].id.indexOf("FCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"F");
                }
                if(document.forms[0].elements[Looper].id.indexOf("GCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"G");
                }
                if(document.forms[0].elements[Looper].id.indexOf("HCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"H");
                }
                if(document.forms[0].elements[Looper].id.indexOf("ICheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"I");
                }
                if(document.forms[0].elements[Looper].id.indexOf("JCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"J");
                }
                                
                if(document.forms[0].elements[Looper].id.indexOf("KCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"K");
                }
            }   
        }
        
    }
    
   
     
     
    function fnUnCheckAll()
    {
        for (Looper=0; Looper < document.forms[0].length ; Looper++ )
        {
            var strType = document.forms[0].elements[Looper].type;
            if (strType=="checkbox")
            {
                
                document.forms[0].elements[Looper].checked=false;
              
                if(document.forms[0].elements[Looper].id.indexOf("ACheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"A");
                }
                if(document.forms[0].elements[Looper].id.indexOf("BCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"B");
                }
                if(document.forms[0].elements[Looper].id.indexOf("CCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"C");
                }
                if(document.forms[0].elements[Looper].id.indexOf("DCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"D");
                }
                if(document.forms[0].elements[Looper].id.indexOf("ECheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"E");
                }
                if(document.forms[0].elements[Looper].id.indexOf("FCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"F");
                }
                if(document.forms[0].elements[Looper].id.indexOf("GCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"G");
                }
                if(document.forms[0].elements[Looper].id.indexOf("HCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"H");
                }
                if(document.forms[0].elements[Looper].id.indexOf("ICheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"I");
                }
                if(document.forms[0].elements[Looper].id.indexOf("JCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"J");
                }
                 if(document.forms[0].elements[Looper].id.indexOf("KCheckBox")!=-1)
                {
                    fnEnable(document.forms[0].elements[Looper],"K");
                }
                
          }   
        }
       
    }
    function fnReset()
    {
        var con=confirm("Are You Sure To Reset ?");
         if(con)
         {
            document.getElementById("<%=fundNameDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=branchNameDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=DeliveryDateTextBox.ClientID%>").value ="";
            document.getElementById("<%=IssueLetterNoTextBox.ClientID%>").value ="";
            document.getElementById("<%=ReqLetterNoTextBox.ClientID%>").value ="";
            
            fnUnCheckAll();
           
            for(Looper=0; Looper < document.forms[0].length ; Looper++)
            {
                var strType = document.forms[0].elements[Looper].type;
                if (strType=="text")
                {
                        if(document.forms[0].elements[Looper].id.indexOf("BookStartNoTextBox")!=-1)
                        {
                           document.forms[0].elements[Looper].value="";
                        } 
                        else if(document.forms[0].elements[Looper].id.indexOf("BookEndNoTextBox")!=-1)        
                        {
                         document.forms[0].elements[Looper].value="";    
                        }  
                        else if(document.forms[0].elements[Looper].id.indexOf("NoBooksTextBox")!=-1)        
                        {
                         document.forms[0].elements[Looper].value="";    
                        }    
                        else if(document.forms[0].elements[Looper].id.indexOf("CertStartNoTextBox")!=-1)        
                        {
                         document.forms[0].elements[Looper].value="";    
                        }   
                        else if(document.forms[0].elements[Looper].id.indexOf("CertEndNoTextBox")!=-1)        
                        {
                         document.forms[0].elements[Looper].value="";    
                        }  
                        else if(document.forms[0].elements[Looper].id.indexOf("BookNoOpenTextBox")!=-1)        
                        {
                         document.forms[0].elements[Looper].value="";    
                        }    
                        else if(document.forms[0].elements[Looper].id.indexOf("BookNoBalanceTextBox")!=-1)        
                        {
                         document.forms[0].elements[Looper].value="";    
                        }                                                        
                }
            }
            return false;
         }
     }
     
   function fnCheqeInput()//Before Save Checking
     {
      
          if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
                alert("Please Select Fund Name");
                return false;
            }
            if(document.getElementById("<%=branchNameDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=branchNameDropDownList.ClientID%>").focus();
                alert("Please Select Branch Name");
                return false;
            }
            if(document.getElementById("<%=DeliveryDateTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=DeliveryDateTextBox.ClientID%>").focus();
                alert("Please Select Delivary Date");
                return false;
            }
            if(document.getElementById("<%=DeliveryDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=DeliveryDateTextBox.ClientID%>").value))
                    {
                     document.getElementById("<%=DeliveryDateTextBox.ClientID%>").focus();
                     alert("Plese Select Date From The Calender");
                     return false;
                    }
            }
            if(document.getElementById("<%=perCertBookAmtTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=perCertBookAmtTextBox.ClientID%>").focus();
                alert("Please Select Enter Per Book Certifiacte Amount");
                return false;
            }
            if(document.getElementById("<%=ACheckBox.ClientID%>").checked)
            {                                                           
              if(document.getElementById("<%=ABookStartNoTextBox.ClientID%>").value ==""||document.getElementById("<%=ABookStartNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=ABookStartNoTextBox.ClientID%>").focus();
                    alert("Book Statr Number Can not be Empty or Zero");
                    return false;
                }
               if(document.getElementById("<%=ABookEndNoTextBox.ClientID%>").value ==""||document.getElementById("<%=ABookEndNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=ABookEndNoTextBox.ClientID%>").focus();
                    alert("Book End Number Can not be Empty or Zero");
                    return false;
                }
                if(document.getElementById("<%=ANoBooksTextBox.ClientID%>").value ==""||document.getElementById("<%=ANoBooksTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=ANoBooksTextBox.ClientID%>").focus();
                    alert("Book Amount Can not be Empty or Zero");
                    return false;
                }               
               if(parseInt(document.getElementById("<%=ABookNoBalanceTextBox.ClientID%>").value)<0)
                {
                    document.getElementById("<%=ABookEndNoTextBox.ClientID%>").focus();
                    alert("Book Balance Can not be Negative Value");
                    return false;
                }
            }
              
            if(document.getElementById("<%=BCheckBox.ClientID%>").checked)
            {                                                           
              if(document.getElementById("<%=BBookStartNoTextBox.ClientID%>").value ==""||document.getElementById("<%=BBookStartNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=BBookStartNoTextBox.ClientID%>").focus();
                    alert("Book Statr Number Can not be Empty or Zero");
                    return false;
                }
               if(document.getElementById("<%=BBookEndNoTextBox.ClientID%>").value ==""||document.getElementById("<%=BBookEndNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=BBookEndNoTextBox.ClientID%>").focus();
                    alert("Book End Number Can not be Empty or Zero");
                    return false;
                }
                if(document.getElementById("<%=BNoBooksTextBox.ClientID%>").value ==""||document.getElementById("<%=BNoBooksTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=BNoBooksTextBox.ClientID%>").focus();
                    alert("Book Amount Can not be Empty or Zero");
                    return false;
                }               
               if(parseInt(document.getElementById("<%=BBookNoBalanceTextBox.ClientID%>").value)<0)
                {
                    document.getElementById("<%=BBookEndNoTextBox.ClientID%>").focus();
                    alert("Book Balance Can not be Negative Value");
                    return false;
                }
            } 
           if(document.getElementById("<%=CCheckBox.ClientID%>").checked)
            {                                                           
              if(document.getElementById("<%=CBookStartNoTextBox.ClientID%>").value ==""||document.getElementById("<%=CBookStartNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=CBookStartNoTextBox.ClientID%>").focus();
                    alert("Book Statr Number Can not be Empty or Zero");
                    return false;
                }
               if(document.getElementById("<%=CBookEndNoTextBox.ClientID%>").value ==""||document.getElementById("<%=CBookEndNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=CBookEndNoTextBox.ClientID%>").focus();
                    alert("Book End Number Can not be Empty or Zero");
                    return false;
                }
                if(document.getElementById("<%=CNoBooksTextBox.ClientID%>").value ==""||document.getElementById("<%=CNoBooksTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=CNoBooksTextBox.ClientID%>").focus();
                    alert("Book Amount Can not be Empty or Zero");
                    return false;
                }               
               if(parseInt(document.getElementById("<%=CBookNoBalanceTextBox.ClientID%>").value)<0)
                {
                    document.getElementById("<%=CBookEndNoTextBox.ClientID%>").focus();
                    alert("Book Balance Can not be Negative Value");
                    return false;
                }
            }   
           if(document.getElementById("<%=DCheckBox.ClientID%>").checked)
            {                                                           
              if(document.getElementById("<%=DBookStartNoTextBox.ClientID%>").value ==""||document.getElementById("<%=DBookStartNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=DBookStartNoTextBox.ClientID%>").focus();
                    alert("Book Statr Number Can not be Empty or Zero");
                    return false;
                }
               if(document.getElementById("<%=DBookEndNoTextBox.ClientID%>").value ==""||document.getElementById("<%=DBookEndNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=DBookEndNoTextBox.ClientID%>").focus();
                    alert("Book End Number Can not be Empty or Zero");
                    return false;
                }
                if(document.getElementById("<%=DNoBooksTextBox.ClientID%>").value ==""||document.getElementById("<%=DNoBooksTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=DNoBooksTextBox.ClientID%>").focus();
                    alert("Book Amount Can not be Empty or Zero");
                    return false;
                }               
               if(parseInt(document.getElementById("<%=DBookNoBalanceTextBox.ClientID%>").value)<0)
                {
                    document.getElementById("<%=DBookEndNoTextBox.ClientID%>").focus();
                    alert("Book Balance Can not be Negative Value");
                    return false;
                }
            }    
            if(document.getElementById("<%=ECheckBox.ClientID%>").checked)
            {                                                           
              if(document.getElementById("<%=EBookStartNoTextBox.ClientID%>").value ==""||document.getElementById("<%=EBookStartNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=EBookStartNoTextBox.ClientID%>").focus();
                    alert("Book Statr Number Can not be Empty or Zero");
                    return false;
                }
               if(document.getElementById("<%=EBookEndNoTextBox.ClientID%>").value ==""||document.getElementById("<%=EBookEndNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=EBookEndNoTextBox.ClientID%>").focus();
                    alert("Book End Number Can not be Empty or Zero");
                    return false;
                }
                if(document.getElementById("<%=ENoBooksTextBox.ClientID%>").value ==""||document.getElementById("<%=ENoBooksTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=ENoBooksTextBox.ClientID%>").focus();
                    alert("Book Amount Can not be Empty or Zero");
                    return false;
                }               
               if(parseInt(document.getElementById("<%=EBookNoBalanceTextBox.ClientID%>").value)<0)
                {
                    document.getElementById("<%=EBookEndNoTextBox.ClientID%>").focus();
                    alert("Book Balance Can not be Negative Value");
                    return false;
                }
            }    
           if(document.getElementById("<%=FCheckBox.ClientID%>").checked)
            {                                                           
              if(document.getElementById("<%=FBookStartNoTextBox.ClientID%>").value ==""||document.getElementById("<%=FBookStartNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=FBookStartNoTextBox.ClientID%>").focus();
                    alert("Book Statr Number Can not be Empty or Zero");
                    return false;
                }
               if(document.getElementById("<%=FBookEndNoTextBox.ClientID%>").value ==""||document.getElementById("<%=FBookEndNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=FBookEndNoTextBox.ClientID%>").focus();
                    alert("Book End Number Can not be Empty or Zero");
                    return false;
                }
                if(document.getElementById("<%=FNoBooksTextBox.ClientID%>").value ==""||document.getElementById("<%=FNoBooksTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=FNoBooksTextBox.ClientID%>").focus();
                    alert("Book Amount Can not be Empty or Zero");
                    return false;
                }               
               if(parseInt(document.getElementById("<%=FBookNoBalanceTextBox.ClientID%>").value)<0)
                {
                    document.getElementById("<%=FBookEndNoTextBox.ClientID%>").focus();
                    alert("Book Balance Can not be Negative Value");
                    return false;
                }
            }     
            if(document.getElementById("<%=GCheckBox.ClientID%>").checked)
            {                                                           
              if(document.getElementById("<%=GBookStartNoTextBox.ClientID%>").value ==""||document.getElementById("<%=GBookStartNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=GBookStartNoTextBox.ClientID%>").focus();
                    alert("Book Statr Number Can not be Empty or Zero");
                    return false;
                }
               if(document.getElementById("<%=GBookEndNoTextBox.ClientID%>").value ==""||document.getElementById("<%=GBookEndNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=GBookEndNoTextBox.ClientID%>").focus();
                    alert("Book End Number Can not be Empty or Zero");
                    return false;
                }
                if(document.getElementById("<%=GNoBooksTextBox.ClientID%>").value ==""||document.getElementById("<%=GNoBooksTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=GNoBooksTextBox.ClientID%>").focus();
                    alert("Book Amount Can not be Empty or Zero");
                    return false;
                }               
               if(parseInt(document.getElementById("<%=GBookNoBalanceTextBox.ClientID%>").value)<0)
                {
                    document.getElementById("<%=GBookEndNoTextBox.ClientID%>").focus();
                    alert("Book Balance Can not be Negative Value");
                    return false;
                }
            }   
            if(document.getElementById("<%=HCheckBox.ClientID%>").checked)
            {                                                           
              if(document.getElementById("<%=HBookStartNoTextBox.ClientID%>").value ==""||document.getElementById("<%=HBookStartNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=HBookStartNoTextBox.ClientID%>").focus();
                    alert("Book Statr Number Can not be Empty or Zero");
                    return false;
                }
               if(document.getElementById("<%=HBookEndNoTextBox.ClientID%>").value ==""||document.getElementById("<%=HBookEndNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=HBookEndNoTextBox.ClientID%>").focus();
                    alert("Book End Number Can not be Empty or Zero");
                    return false;
                }
                if(document.getElementById("<%=HNoBooksTextBox.ClientID%>").value ==""||document.getElementById("<%=HNoBooksTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=HNoBooksTextBox.ClientID%>").focus();
                    alert("Book Amount Can not be Empty or Zero");
                    return false;
                }               
               if(parseInt(document.getElementById("<%=HBookNoBalanceTextBox.ClientID%>").value)<0)
                {
                    document.getElementById("<%=HBookEndNoTextBox.ClientID%>").focus();
                    alert("Book Balance Can not be Negative Value");
                    return false;
                }
            }   
           if(document.getElementById("<%=ICheckBox.ClientID%>").checked)
            {                                                           
              if(document.getElementById("<%=IBookStartNoTextBox.ClientID%>").value ==""||document.getElementById("<%=IBookStartNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=IBookStartNoTextBox.ClientID%>").focus();
                    alert("Book Statr Number Can not be Empty or Zero");
                    return false;
                }
               if(document.getElementById("<%=IBookEndNoTextBox.ClientID%>").value ==""||document.getElementById("<%=IBookEndNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=IBookEndNoTextBox.ClientID%>").focus();
                    alert("Book End Number Can not be Empty or Zero");
                    return false;
                }
                if(document.getElementById("<%=INoBooksTextBox.ClientID%>").value ==""||document.getElementById("<%=INoBooksTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=INoBooksTextBox.ClientID%>").focus();
                    alert("Book Amount Can not be Empty or Zero");
                    return false;
                }               
               if(parseInt(document.getElementById("<%=IBookNoBalanceTextBox.ClientID%>").value)<0)
                {
                    document.getElementById("<%=IBookEndNoTextBox.ClientID%>").focus();
                    alert("Book Balance Can not be Negative Value");
                    return false;
                }
            }  
            if(document.getElementById("<%=JCheckBox.ClientID%>").checked)
            {                                                           
              if(document.getElementById("<%=JBookStartNoTextBox.ClientID%>").value ==""||document.getElementById("<%=JBookStartNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=JBookStartNoTextBox.ClientID%>").focus();
                    alert("Book Statr Number Can not be Empty or Zero");
                    return false;
                }
               if(document.getElementById("<%=JBookEndNoTextBox.ClientID%>").value ==""||document.getElementById("<%=JBookEndNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=JBookEndNoTextBox.ClientID%>").focus();
                    alert("Book End Number Can not be Empty or Zero");
                    return false;
                }
                if(document.getElementById("<%=JNoBooksTextBox.ClientID%>").value ==""||document.getElementById("<%=JNoBooksTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=JNoBooksTextBox.ClientID%>").focus();
                    alert("Book Amount Can not be Empty or Zero");
                    return false;
                }               
               if(parseInt(document.getElementById("<%=JBookNoBalanceTextBox.ClientID%>").value)<0)
                {
                    document.getElementById("<%=JBookEndNoTextBox.ClientID%>").focus();
                    alert("Book Balance Can not be Negative Value");
                    return false;
                }
            }   
            
            if(document.getElementById("<%=KCheckBox.ClientID%>").checked)
            {                                                           
              if(document.getElementById("<%=KBookStartNoTextBox.ClientID%>").value ==""||document.getElementById("<%=KBookStartNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=KBookStartNoTextBox.ClientID%>").focus();
                    alert("Book Statr Number Can not be Empty or Zero");
                    return false;
                }
               if(document.getElementById("<%=KBookEndNoTextBox.ClientID%>").value ==""||document.getElementById("<%=KBookEndNoTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=KBookEndNoTextBox.ClientID%>").focus();
                    alert("Book End Number Can not be Empty or Zero");
                    return false;
                }
                if(document.getElementById("<%=KNoBooksTextBox.ClientID%>").value ==""||document.getElementById("<%=KNoBooksTextBox.ClientID%>").value =="0")
                {
                    document.getElementById("<%=KNoBooksTextBox.ClientID%>").focus();
                    alert("Book Amount Can not be Empty or Zero");
                    return false;
                }               
               if(parseInt(document.getElementById("<%=KBookNoBalanceTextBox.ClientID%>").value)<0)
                {
                    document.getElementById("<%=JBookEndNoTextBox.ClientID%>").focus();
                    alert("Book Balance Can not be Negative Value");
                    return false;
                }
            }                                                       
         
     }
     
      
    function fncInputNumericValuesOnly()
	{
		if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		{
			alert("Please Enter Numaric Value Only");
			    event.returnValue=false;
		}
	}
	
    function fnEnable(chkBox,dino)
        {
            if(dino.indexOf("A")==0)
            {   
                if(chkBox.checked)
                {                   
                    document.getElementById("<%=ABookStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=ABookEndNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=ANoBooksTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=ACertStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=ACertEndNoTextBox.ClientID%>").disabled=false;     
                    document.getElementById("<%=ABookNoBalanceTextBox.ClientID%>").disabled=false;                      
                    document.getElementById("<%=ABookEndNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {                    
                    document.getElementById("<%=ABookStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=ABookEndNoTextBox.ClientID%>").disabled=true; 
                    document.getElementById("<%=ANoBooksTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=ACertStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=ACertEndNoTextBox.ClientID%>").disabled=true;     
                    document.getElementById("<%=ABookNoBalanceTextBox.ClientID%>").disabled=true;                      
                }
              }
             else if(dino.indexOf("B")==0)
             {
                 if(chkBox.checked)
                {                   
                    document.getElementById("<%=BBookStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=BBookEndNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=BNoBooksTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=BCertStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=BCertEndNoTextBox.ClientID%>").disabled=false;     
                    document.getElementById("<%=BBookNoBalanceTextBox.ClientID%>").disabled=false;                       
                    document.getElementById("<%=BBookEndNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {                    
                    document.getElementById("<%=BBookStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=BBookEndNoTextBox.ClientID%>").disabled=true;     
                    document.getElementById("<%=BNoBooksTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=BCertStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=BCertEndNoTextBox.ClientID%>").disabled=true;     
                    document.getElementById("<%=BBookNoBalanceTextBox.ClientID%>").disabled=true;                 
                }
             }
              else if(dino.indexOf("C")==0)
             {
                 if(chkBox.checked)
                {                   
                    document.getElementById("<%=CBookStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=CBookEndNoTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=CNoBooksTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=CCertStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=CCertEndNoTextBox.ClientID%>").disabled=false;     
                    document.getElementById("<%=CBookNoBalanceTextBox.ClientID%>").disabled=false;                        
                    document.getElementById("<%=CBookEndNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {                    
                    document.getElementById("<%=CBookStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=CBookEndNoTextBox.ClientID%>").disabled=true;     
                     document.getElementById("<%=CNoBooksTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=CCertStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=CCertEndNoTextBox.ClientID%>").disabled=true;     
                    document.getElementById("<%=CBookNoBalanceTextBox.ClientID%>").disabled=true;                   
                }
             }
              else if(dino.indexOf("D")==0)
             {
                 if(chkBox.checked)
                {                   
                    document.getElementById("<%=DBookStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=DBookEndNoTextBox.ClientID%>").disabled=false;    
                    document.getElementById("<%=DNoBooksTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=DCertStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=DCertEndNoTextBox.ClientID%>").disabled=false;     
                    document.getElementById("<%=DBookNoBalanceTextBox.ClientID%>").disabled=false;                      
                    document.getElementById("<%=DBookEndNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {                    
                    document.getElementById("<%=DBookStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=DBookEndNoTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=DNoBooksTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=DCertStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=DCertEndNoTextBox.ClientID%>").disabled=true;     
                    document.getElementById("<%=DBookNoBalanceTextBox.ClientID%>").disabled=true;                      
                }
             }
             else if(dino.indexOf("E")==0)
             {
                 if(chkBox.checked)
                {                   
                    document.getElementById("<%=EBookStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=EBookEndNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=ENoBooksTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=ECertStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=ECertEndNoTextBox.ClientID%>").disabled=false;     
                    document.getElementById("<%=EBookNoBalanceTextBox.ClientID%>").disabled=false;                       
                    document.getElementById("<%=EBookEndNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {                    
                    document.getElementById("<%=EBookStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=EBookEndNoTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=ENoBooksTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=ECertStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=ECertEndNoTextBox.ClientID%>").disabled=true;     
                    document.getElementById("<%=EBookNoBalanceTextBox.ClientID%>").disabled=true;                    
                }
             }
             else if(dino.indexOf("F")==0)
             {
                 if(chkBox.checked)
                {                   
                    document.getElementById("<%=FBookStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=FBookEndNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=FNoBooksTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=FCertStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=FCertEndNoTextBox.ClientID%>").disabled=false;     
                    document.getElementById("<%=FBookNoBalanceTextBox.ClientID%>").disabled=false;                       
                    document.getElementById("<%=FBookEndNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {                    
                    document.getElementById("<%=FBookStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=FBookEndNoTextBox.ClientID%>").disabled=true;  
                     document.getElementById("<%=FNoBooksTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=FCertStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=FCertEndNoTextBox.ClientID%>").disabled=true;     
                    document.getElementById("<%=FBookNoBalanceTextBox.ClientID%>").disabled=true;                    
                }
             }
             else if(dino.indexOf("G")==0)
             {
                 if(chkBox.checked)
                {                   
                    document.getElementById("<%=GBookStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=GBookEndNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=GNoBooksTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=GCertStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=GCertEndNoTextBox.ClientID%>").disabled=false;     
                    document.getElementById("<%=GBookNoBalanceTextBox.ClientID%>").disabled=false;                       
                    document.getElementById("<%=GBookEndNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {                    
                    document.getElementById("<%=GBookStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=GBookEndNoTextBox.ClientID%>").disabled=true;
                     document.getElementById("<%=GNoBooksTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=GCertStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=GCertEndNoTextBox.ClientID%>").disabled=true;     
                    document.getElementById("<%=GBookNoBalanceTextBox.ClientID%>").disabled=true;                          
                }
             }
             else if(dino.indexOf("H")==0)
             {
                 if(chkBox.checked)
                {                   
                    document.getElementById("<%=HBookStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=HBookEndNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=HNoBooksTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=HCertStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=HCertEndNoTextBox.ClientID%>").disabled=false;     
                    document.getElementById("<%=HBookNoBalanceTextBox.ClientID%>").disabled=false;                       
                    document.getElementById("<%=HBookEndNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {                    
                    document.getElementById("<%=HBookStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=HBookEndNoTextBox.ClientID%>").disabled=true; 
                     document.getElementById("<%=HNoBooksTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=HCertStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=HCertEndNoTextBox.ClientID%>").disabled=true;     
                    document.getElementById("<%=HBookNoBalanceTextBox.ClientID%>").disabled=true;                       
                }
             }
             else if(dino.indexOf("I")==0)
             {
                 if(chkBox.checked)
                {                   
                    document.getElementById("<%=IBookStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=IBookEndNoTextBox.ClientID%>").disabled=false;      
                    document.getElementById("<%=INoBooksTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=ICertStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=ICertEndNoTextBox.ClientID%>").disabled=false;     
                    document.getElementById("<%=IBookNoBalanceTextBox.ClientID%>").disabled=false;                    
                    document.getElementById("<%=IBookEndNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {                    
                    document.getElementById("<%=IBookStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=IBookEndNoTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=INoBooksTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=ICertStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=ICertEndNoTextBox.ClientID%>").disabled=true;     
                    document.getElementById("<%=IBookNoBalanceTextBox.ClientID%>").disabled=true;                           
                }
             }
             else if(dino.indexOf("J")==0)
             {
                 if(chkBox.checked)
                {                   
                    document.getElementById("<%=JBookStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=JBookEndNoTextBox.ClientID%>").disabled=false;      
                    document.getElementById("<%=JNoBooksTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=JCertStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=JCertEndNoTextBox.ClientID%>").disabled=false;     
                    document.getElementById("<%=JBookNoBalanceTextBox.ClientID%>").disabled=false;                    
                    document.getElementById("<%=JBookEndNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {                    
                    document.getElementById("<%=JBookStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=JBookEndNoTextBox.ClientID%>").disabled=true;    
                     document.getElementById("<%=JNoBooksTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=JCertStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=JCertEndNoTextBox.ClientID%>").disabled=true;     
                    document.getElementById("<%=JBookNoBalanceTextBox.ClientID%>").disabled=true;                  
                }
             }
             else if(dino.indexOf("K")==0)
             {
                 if(chkBox.checked)
                {                   
                    document.getElementById("<%=KBookStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=KBookEndNoTextBox.ClientID%>").disabled=false;      
                    document.getElementById("<%=KNoBooksTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=KCertStartNoTextBox.ClientID%>").disabled=false;   
                    document.getElementById("<%=KCertEndNoTextBox.ClientID%>").disabled=false;     
                    document.getElementById("<%=KBookNoBalanceTextBox.ClientID%>").disabled=false;                    
                    document.getElementById("<%=KBookEndNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {                    
                    document.getElementById("<%=KBookStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=KBookEndNoTextBox.ClientID%>").disabled=true;    
                     document.getElementById("<%=KNoBooksTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=KCertStartNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=KCertEndNoTextBox.ClientID%>").disabled=true;     
                    document.getElementById("<%=KBookNoBalanceTextBox.ClientID%>").disabled=true;                  
                }
             }
             
             
        }
      function fnCalculateNoBooks(dino)
      {                
                if(dino.indexOf("A")==0)
                {
                    var perBookCertAmount=parseInt(document.getElementById("<%=perCertBookAmtTextBox.ClientID%>").value);
                    var bookStartNo=parseInt(document.getElementById("<%=ABookStartNoTextBox.ClientID%>").value);
                    var bookEndNo=parseInt(document.getElementById("<%=ABookEndNoTextBox.ClientID%>").value);
                    var bookOpenBalance=parseInt(document.getElementById("<%=ABookNoOpenTextBox.ClientID%>").value);
                    var certNoStart;
                    if((bookEndNo-bookStartNo)<0)
                    {
                        alert("Book End Number can not be smaller than Start Number");
                        document.getElementById("<%=ABookEndNoTextBox.ClientID%>").focus();                    
                        
                    }
                    else if((bookEndNo-bookStartNo)>=0)
                    {
                        document.getElementById("<%=ANoBooksTextBox.ClientID%>").value=(bookEndNo-bookStartNo)+1; 
                       
                        certNoStart=parseInt(((bookStartNo-1)*perBookCertAmount)+1);
                        
                        document.getElementById("<%=ACertStartNoTextBox.ClientID%>").value=certNoStart;     
                        document.getElementById("<%=ACertEndNoTextBox.ClientID%>").value=bookEndNo*perBookCertAmount;                                        
                        document.getElementById("<%=ABookNoBalanceTextBox.ClientID%>").value=bookOpenBalance-parseInt( document.getElementById("<%=ANoBooksTextBox.ClientID%>").value);          
                        
                    }
                }
                else if(dino.indexOf("B")==0)
                 {
                    var perBookCertAmount=parseInt(document.getElementById("<%=perCertBookAmtTextBox.ClientID%>").value);
                    var bookStartNo=parseInt(document.getElementById("<%=BBookStartNoTextBox.ClientID%>").value);
                    var bookEndNo=parseInt(document.getElementById("<%=BBookEndNoTextBox.ClientID%>").value);
                    var bookOpenBalance=parseInt(document.getElementById("<%=BBookNoOpenTextBox.ClientID%>").value);
                    var certNoStart;
                    if((bookEndNo-bookStartNo)<0)
                    {
                        alert("Book End Number can not be smaller than Start Number");
                        document.getElementById("<%=BBookEndNoTextBox.ClientID%>").focus();                    
                        
                    }
                    else if((bookEndNo-bookStartNo)>=0)
                    {
                        document.getElementById("<%=BNoBooksTextBox.ClientID%>").value=(bookEndNo-bookStartNo)+1; 
                        certNoStart=parseInt(((bookStartNo-1)*perBookCertAmount)+1);
                        
                        document.getElementById("<%=BCertStartNoTextBox.ClientID%>").value=certNoStart;     
                        document.getElementById("<%=BCertEndNoTextBox.ClientID%>").value=bookEndNo*perBookCertAmount;                                        
                        document.getElementById("<%=BBookNoBalanceTextBox.ClientID%>").value=bookOpenBalance-parseInt( document.getElementById("<%=BNoBooksTextBox.ClientID%>").value);          
                        
                    }
                }
                 else if(dino.indexOf("C")==0)
                 {
                    var perBookCertAmount=parseInt(document.getElementById("<%=perCertBookAmtTextBox.ClientID%>").value);
                    var bookStartNo=parseInt(document.getElementById("<%=CBookStartNoTextBox.ClientID%>").value);
                    var bookEndNo=parseInt(document.getElementById("<%=CBookEndNoTextBox.ClientID%>").value);
                    var bookOpenBalance=parseInt(document.getElementById("<%=CBookNoOpenTextBox.ClientID%>").value);
                    var certNoStart;
                    if((bookEndNo-bookStartNo)<0)
                    {
                        alert("Book End Number can not be smaller than Start Number");
                        document.getElementById("<%=CBookEndNoTextBox.ClientID%>").focus();                    
                        
                    }
                    else if((bookEndNo-bookStartNo)>=0)
                    {
                        document.getElementById("<%=CNoBooksTextBox.ClientID%>").value=(bookEndNo-bookStartNo)+1; 
                       certNoStart=parseInt(((bookStartNo-1)*perBookCertAmount)+1);
                        document.getElementById("<%=CCertStartNoTextBox.ClientID%>").value=certNoStart;     
                        document.getElementById("<%=CCertEndNoTextBox.ClientID%>").value=bookEndNo*perBookCertAmount;                                        
                        document.getElementById("<%=CBookNoBalanceTextBox.ClientID%>").value=bookOpenBalance-parseInt( document.getElementById("<%=CNoBooksTextBox.ClientID%>").value);          
                        
                    }
                }
                 else if(dino.indexOf("D")==0)
                 {
                    var perBookCertAmount=parseInt(document.getElementById("<%=perCertBookAmtTextBox.ClientID%>").value);
                    var bookStartNo=parseInt(document.getElementById("<%=DBookStartNoTextBox.ClientID%>").value);
                    var bookEndNo=parseInt(document.getElementById("<%=DBookEndNoTextBox.ClientID%>").value);
                    var bookOpenBalance=parseInt(document.getElementById("<%=DBookNoOpenTextBox.ClientID%>").value);
                    var certNoStart;
                    if((bookEndNo-bookStartNo)<0)
                    {
                        alert("Book End Number can not be smaller than Start Number");
                        document.getElementById("<%=DBookEndNoTextBox.ClientID%>").focus();                    
                        
                    }
                    else if((bookEndNo-bookStartNo)>=0)
                    {
                        document.getElementById("<%=DNoBooksTextBox.ClientID%>").value=(bookEndNo-bookStartNo)+1; 
                        certNoStart=parseInt(((bookStartNo-1)*perBookCertAmount)+1);
                        document.getElementById("<%=DCertStartNoTextBox.ClientID%>").value=certNoStart;     
                        document.getElementById("<%=DCertEndNoTextBox.ClientID%>").value=bookEndNo*perBookCertAmount;                                        
                        document.getElementById("<%=DBookNoBalanceTextBox.ClientID%>").value=bookOpenBalance-parseInt( document.getElementById("<%=DNoBooksTextBox.ClientID%>").value);          
                        
                    }
                }
                 else if(dino.indexOf("E")==0)
                 {
                    var perBookCertAmount=parseInt(document.getElementById("<%=perCertBookAmtTextBox.ClientID%>").value);
                    var bookStartNo=parseInt(document.getElementById("<%=EBookStartNoTextBox.ClientID%>").value);
                    var bookEndNo=parseInt(document.getElementById("<%=EBookEndNoTextBox.ClientID%>").value);
                    var bookOpenBalance=parseInt(document.getElementById("<%=EBookNoOpenTextBox.ClientID%>").value);
                    var certNoStart;
                    if((bookEndNo-bookStartNo)<0)
                    {
                        alert("Book End Number can not be smaller than Start Number");
                        document.getElementById("<%=EBookEndNoTextBox.ClientID%>").focus();                    
                        
                    }
                    else if((bookEndNo-bookStartNo)>=0)
                    {
                        document.getElementById("<%=ENoBooksTextBox.ClientID%>").value=(bookEndNo-bookStartNo)+1; 
                        certNoStart=parseInt(((bookStartNo-1)*perBookCertAmount)+1);
                        document.getElementById("<%=ECertStartNoTextBox.ClientID%>").value=certNoStart;     
                        document.getElementById("<%=ECertEndNoTextBox.ClientID%>").value=bookEndNo*perBookCertAmount;                                        
                        document.getElementById("<%=EBookNoBalanceTextBox.ClientID%>").value=bookOpenBalance-parseInt( document.getElementById("<%=ENoBooksTextBox.ClientID%>").value);          
                        
                    }
                }
                 else if(dino.indexOf("F")==0)
                 {
                    var perBookCertAmount=parseInt(document.getElementById("<%=perCertBookAmtTextBox.ClientID%>").value);
                    var bookStartNo=parseInt(document.getElementById("<%=FBookStartNoTextBox.ClientID%>").value);
                    var bookEndNo=parseInt(document.getElementById("<%=FBookEndNoTextBox.ClientID%>").value);
                    var bookOpenBalance=parseInt(document.getElementById("<%=FBookNoOpenTextBox.ClientID%>").value);
                    var certNoStart;
                    if((bookEndNo-bookStartNo)<0)
                    {
                        alert("Book End Number can not be smaller than Start Number");
                        document.getElementById("<%=FBookEndNoTextBox.ClientID%>").focus();                    
                        
                    }
                    else if((bookEndNo-bookStartNo)>=0)
                    {
                        document.getElementById("<%=FNoBooksTextBox.ClientID%>").value=(bookEndNo-bookStartNo)+1; 
                       certNoStart=parseInt(((bookStartNo-1)*perBookCertAmount)+1);
                        document.getElementById("<%=FCertStartNoTextBox.ClientID%>").value=certNoStart;     
                        document.getElementById("<%=FCertEndNoTextBox.ClientID%>").value=bookEndNo*perBookCertAmount;                                        
                        document.getElementById("<%=FBookNoBalanceTextBox.ClientID%>").value=bookOpenBalance-parseInt( document.getElementById("<%=FNoBooksTextBox.ClientID%>").value);          
                        
                    }
                }
                 else if(dino.indexOf("G")==0)
                 {
                    var perBookCertAmount=parseInt(document.getElementById("<%=perCertBookAmtTextBox.ClientID%>").value);
                    var bookStartNo=parseInt(document.getElementById("<%=GBookStartNoTextBox.ClientID%>").value);
                    var bookEndNo=parseInt(document.getElementById("<%=GBookEndNoTextBox.ClientID%>").value);
                    var bookOpenBalance=parseInt(document.getElementById("<%=GBookNoOpenTextBox.ClientID%>").value);
                    var certNoStart;
                    if((bookEndNo-bookStartNo)<0)
                    {
                        alert("Book End Number can not be smaller than Start Number");
                        document.getElementById("<%=GBookEndNoTextBox.ClientID%>").focus();                    
                        
                    }
                    else if((bookEndNo-bookStartNo)>=0)
                    {
                        document.getElementById("<%=GNoBooksTextBox.ClientID%>").value=(bookEndNo-bookStartNo)+1; 
                       certNoStart=parseInt(((bookStartNo-1)*perBookCertAmount)+1);
                        document.getElementById("<%=GCertStartNoTextBox.ClientID%>").value=certNoStart;     
                        document.getElementById("<%=GCertEndNoTextBox.ClientID%>").value=bookEndNo*perBookCertAmount;                                        
                        document.getElementById("<%=GBookNoBalanceTextBox.ClientID%>").value=bookOpenBalance-parseInt( document.getElementById("<%=GNoBooksTextBox.ClientID%>").value);          
                        
                    }
                }
                 else if(dino.indexOf("H")==0)
                 {
                    var perBookCertAmount=parseInt(document.getElementById("<%=perCertBookAmtTextBox.ClientID%>").value);
                    var bookStartNo=parseInt(document.getElementById("<%=HBookStartNoTextBox.ClientID%>").value);
                    var bookEndNo=parseInt(document.getElementById("<%=HBookEndNoTextBox.ClientID%>").value);
                    var bookOpenBalance=parseInt(document.getElementById("<%=HBookNoOpenTextBox.ClientID%>").value);
                    var certNoStart;
                    if((bookEndNo-bookStartNo)<0)
                    {
                        alert("Book End Number can not be smaller than Start Number");
                        document.getElementById("<%=HBookEndNoTextBox.ClientID%>").focus();                    
                        
                    }
                    else if((bookEndNo-bookStartNo)>=0)
                    {
                        document.getElementById("<%=HNoBooksTextBox.ClientID%>").value=(bookEndNo-bookStartNo)+1; 
                        certNoStart=parseInt(((bookStartNo-1)*perBookCertAmount)+1);
                        document.getElementById("<%=HCertStartNoTextBox.ClientID%>").value=certNoStart;     
                        document.getElementById("<%=HCertEndNoTextBox.ClientID%>").value=bookEndNo*perBookCertAmount;                                        
                        document.getElementById("<%=HBookNoBalanceTextBox.ClientID%>").value=bookOpenBalance-parseInt( document.getElementById("<%=HNoBooksTextBox.ClientID%>").value);          
                        
                    }
                }
                 else if(dino.indexOf("I")==0)
                 {
                    var perBookCertAmount=parseInt(document.getElementById("<%=perCertBookAmtTextBox.ClientID%>").value);
                    var bookStartNo=parseInt(document.getElementById("<%=IBookStartNoTextBox.ClientID%>").value);
                    var bookEndNo=parseInt(document.getElementById("<%=IBookEndNoTextBox.ClientID%>").value);
                    var bookOpenBalance=parseInt(document.getElementById("<%=IBookNoOpenTextBox.ClientID%>").value);
                    var certNoStart;
                    if((bookEndNo-bookStartNo)<0)
                    {
                        alert("Book End Number can not be smaller than Start Number");
                        document.getElementById("<%=IBookEndNoTextBox.ClientID%>").focus();                    
                        
                    }
                    else if((bookEndNo-bookStartNo)>=0)
                    {
                        document.getElementById("<%=INoBooksTextBox.ClientID%>").value=(bookEndNo-bookStartNo)+1; 
                        certNoStart=parseInt(((bookStartNo-1)*perBookCertAmount)+1);
                        document.getElementById("<%=ICertStartNoTextBox.ClientID%>").value=certNoStart;     
                        document.getElementById("<%=ICertEndNoTextBox.ClientID%>").value=bookEndNo*perBookCertAmount;                                        
                        document.getElementById("<%=IBookNoBalanceTextBox.ClientID%>").value=bookOpenBalance-parseInt( document.getElementById("<%=INoBooksTextBox.ClientID%>").value);          
                        
                    }
                }
                 else if(dino.indexOf("J")==0)
                 {
                    var perBookCertAmount=parseInt(document.getElementById("<%=perCertBookAmtTextBox.ClientID%>").value);
                    var bookStartNo=parseInt(document.getElementById("<%=JBookStartNoTextBox.ClientID%>").value);
                    var bookEndNo=parseInt(document.getElementById("<%=JBookEndNoTextBox.ClientID%>").value);
                    var bookOpenBalance=parseInt(document.getElementById("<%=JBookNoOpenTextBox.ClientID%>").value);
                    var certNoStart;
                    if((bookEndNo-bookStartNo)<0)
                    {
                        alert("Book End Number can not be smaller than Start Number");
                        document.getElementById("<%=JBookEndNoTextBox.ClientID%>").focus();                    
                        
                    }
                    else if((bookEndNo-bookStartNo)>=0)
                    {
                        document.getElementById("<%=JNoBooksTextBox.ClientID%>").value=(bookEndNo-bookStartNo)+1; 
                        certNoStart=parseInt(((bookStartNo-1)*perBookCertAmount)+1);
                        document.getElementById("<%=JCertStartNoTextBox.ClientID%>").value=certNoStart;     
                        document.getElementById("<%=JCertEndNoTextBox.ClientID%>").value=bookEndNo*perBookCertAmount;                                        
                        document.getElementById("<%=JBookNoBalanceTextBox.ClientID%>").value=bookOpenBalance-parseInt( document.getElementById("<%=JNoBooksTextBox.ClientID%>").value);          
                        
                    }
                }
                
                 else if(dino.indexOf("K")==0)
                 {
                    var perBookCertAmount=parseInt(document.getElementById("<%=perCertBookAmtTextBox.ClientID%>").value);
                    var bookStartNo=parseInt(document.getElementById("<%=KBookStartNoTextBox.ClientID%>").value);
                    var bookEndNo=parseInt(document.getElementById("<%=KBookEndNoTextBox.ClientID%>").value);
                    var bookOpenBalance=parseInt(document.getElementById("<%=KBookNoOpenTextBox.ClientID%>").value);
                    var certNoStart;
                    if((bookEndNo-bookStartNo)<0)
                    {
                        alert("Book End Number can not be smaller than Start Number");
                        document.getElementById("<%=KBookEndNoTextBox.ClientID%>").focus();                    
                        
                    }
                    else if((bookEndNo-bookStartNo)>=0)
                    {
                        document.getElementById("<%=KNoBooksTextBox.ClientID%>").value=(bookEndNo-bookStartNo)+1; 
                        certNoStart=parseInt(((bookStartNo-1)*perBookCertAmount)+1);
                        document.getElementById("<%=KCertStartNoTextBox.ClientID%>").value=certNoStart;     
                        document.getElementById("<%=KCertEndNoTextBox.ClientID%>").value=bookEndNo*perBookCertAmount;                                        
                        document.getElementById("<%=KBookNoBalanceTextBox.ClientID%>").value=bookOpenBalance-parseInt( document.getElementById("<%=KNoBooksTextBox.ClientID%>").value);          
                        
                    }
                }
                 
       
      }
    </script>
    <style type="text/css">
      .style1
        {
           border: 1px solid #800080;
             text-align:center;
        }
        .style2
        {
            border: 1px solid #800080;
            text-align:center;
            background-color:White;
        }
         .style2
        {
            height:15px;
        }
    
        .style4
        {
            width: 74px;
        }
        .style5
        {
            width: 77px;
        }
    
     </style>
<br />

 <table align="center">
        <tr>
            <td class="FormTitle" align="center">
                Certificate Book Issue Form
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
<br />
<div id="dvUpdatePanel" runat="server">
<asp:UpdatePanel ID="CertInfoUpdatePanel" runat="server">
<ContentTemplate>
<table width="400" align="center" cellpadding="0" cellspacing="0" >
<colgroup width="150"></colgroup>

    <tr>
        <td align="left">Fund Name:</td>
        <td align="left">
            <asp:DropDownList ID="fundNameDropDownList" runat="server" 
                    TabIndex="1" meta:resourcekey="fundNameDropDownListResource1" 
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged" 
                AutoPostBack="True"></asp:DropDownList><span class="star">*</span></td>
        
    </tr>    
     
    <tr>
       <td align="left">Branch Name:</td>
        <td align="left">
            <asp:DropDownList ID="branchNameDropDownList" runat="server" 
                    TabIndex="2" meta:resourcekey="branchNameDropDownListResource1" 
               ></asp:DropDownList><span class="star">*</span></td>
      
    </tr>
    <tr>
            <td align="left"  >Delivary Date:</td>
            <td align="left" >
                <asp:TextBox ID="DeliveryDateTextBox" runat="server" CssClass="textInputStyleDate" 
                    TabIndex="3" meta:resourcekey="saleDateTextBoxResource1"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                    TargetControlID="DeliveryDateTextBox" PopupButtonID="RegDateImageButton" 
                    Format="dd-MMM-yyyy" Enabled="True"/>
                <asp:ImageButton ID="RegDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                    meta:resourcekey="RegDateImageButtonResource1" />
                <span class="star">* </span></td>
          
       
           
        </tr>
     <tr>
            <td align="left"  >Issue Letter No:</td>
            <td align="left" >                
                                <asp:TextBox ID="IssueLetterNoTextBox" runat="server" 
                    CssClass="textInputStyle2" TabIndex="4" Width="195px"></asp:TextBox>
                                </td>
          
       
           
        </tr>
        <tr>
            <td align="left"  >Request Letter No:</td>
            <td align="left" >
                <asp:TextBox ID="ReqLetterNoTextBox" runat="server" 
                    CssClass="textInputStyle2" TabIndex="6" Width="195px"></asp:TextBox></td>
          
       
           
        </tr>
         <tr>
            <td align="left"  >Per Book Certifiacte Amount</td>
            <td align="left" >
                <asp:TextBox ID="perCertBookAmtTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="6" Width="85px"></asp:TextBox></td>
          
       
           
        </tr>
    </table>
    <br />
    <div  style="border-style: solid; border-color: inherit; border-width: 1px; width:1100px; height:330px; overflow:auto; text-align:left" 
                            ID="dvMenue" runat="server">
  <table width="1100" align="center" cellpadding="0" cellspacing="0">
    <colgroup width="100"></colgroup>
    <colgroup width="150"></colgroup>
    <colgroup width="140"></colgroup>
    <colgroup width="140"></colgroup>
    <colgroup width="140"></colgroup>
    <colgroup width="140"></colgroup>
    <colgroup width="150"></colgroup>
    
    <tr>
        <td  class="style1"><b >CERT TYPE</b></td> <td class="style1"><b>BOOK STARTING NO.</b></td> 
        <td class="style1"><b>BOOK ENDING NO.</td> <td class="style1"><b>NUMBER OF BOOKS</b></td>
        <td class="style1"><b>CERT&nbsp; STARTING NO. </b> </td> <td class="style1"><b>CERT ENDING NO.</b></td> 
        <td class="style1"><b>BOOK NO. OPENING</b></td><td class="style1"> <b>BOOK NO.&nbsp; BALANCE</b></td>
        
    </tr>
    <tr>
        <td  class="style2">
            <asp:CheckBox  CssClass="checkbox" ID="ACheckBox" runat="server"  onclick="fnEnable(this,'A');" />A
            </td>            
            <td class="style2">
                <asp:TextBox ID="ABookStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2"><asp:TextBox ID="ABookEndNoTextBox" runat="server" 
                    onkeypress= "fncInputNumericValuesOnly()"  
                    onBlur="Javascript:fnCalculateNoBooks('A');"    CssClass="textInputStyle2" 
                    TabIndex="7" CausesValidation="True" 
                    Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="ANoBooksTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="ACertStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="ACertEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="ABookNoOpenTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"> </asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="ABookNoBalanceTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" Enabled="False"></asp:TextBox></td>
         
       
        
    </tr>
    <tr>
        <td  class="style2">
            <asp:CheckBox  CssClass="checkbox" ID="BCheckBox" runat="server"  
                onclick="fnEnable(this,'B');" />B
            </td>            
            <td class="style2">
                <asp:TextBox ID="BBookStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2"><asp:TextBox ID="BBookEndNoTextBox" runat="server" 
                    onkeypress= "fncInputNumericValuesOnly()"  
                    onBlur="Javascript:fnCalculateNoBooks('B');"    CssClass="textInputStyle2" 
                    TabIndex="8" CausesValidation="True" 
                    Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="BNoBooksTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="BCertStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="BCertEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="BBookNoOpenTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="BBookNoBalanceTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" Enabled="False"></asp:TextBox></td>                        
    </tr>
    <tr>
        <td  class="style2">
            <asp:CheckBox  CssClass="checkbox" ID="CCheckBox" runat="server"  onclick="fnEnable(this,'C');" />C
            </td>            
            <td class="style2">
                <asp:TextBox ID="CBookStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2"><asp:TextBox ID="CBookEndNoTextBox" runat="server" 
                    onkeypress= "fncInputNumericValuesOnly()"  
                    onBlur="Javascript:fnCalculateNoBooks('C');"    CssClass="textInputStyle2" 
                    TabIndex="9" CausesValidation="True" 
                    Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="CNoBooksTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="CCertStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="CCertEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="CBookNoOpenTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="CBookNoBalanceTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" Enabled="False"></asp:TextBox></td>                        
    </tr>
    <tr>
        <td  class="style2">
            <asp:CheckBox  CssClass="checkbox" ID="DCheckBox" runat="server"  onclick="fnEnable(this,'D');" />D
            </td>            
            <td class="style2">
                <asp:TextBox ID="DBookStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2"><asp:TextBox ID="DBookEndNoTextBox" runat="server" 
                    onkeypress= "fncInputNumericValuesOnly()"  
                    onBlur="Javascript:fnCalculateNoBooks('D');"    CssClass="textInputStyle2" 
                    TabIndex="10" CausesValidation="True" 
                    Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="DNoBooksTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="DCertStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="DCertEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="DBookNoOpenTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="DBookNoBalanceTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>                        
    </tr>
    <tr>
        <td  class="style2">
            <asp:CheckBox  CssClass="checkbox" ID="ECheckBox" runat="server"  onclick="fnEnable(this,'E');" />E
            </td>            
            <td class="style2">
                <asp:TextBox ID="EBookStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"> </asp:TextBox></td>
            <td class="style2"><asp:TextBox ID="EBookEndNoTextBox" runat="server" 
                    onkeypress= "fncInputNumericValuesOnly()"  
                    onBlur="Javascript:fnCalculateNoBooks('E');"    CssClass="textInputStyle2" 
                    TabIndex="11" CausesValidation="True" 
                    Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="ENoBooksTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="ECertStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="ECertEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="EBookNoOpenTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="EBookNoBalanceTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()" TabIndex="0"
                    CssClass="textInputStyle2" Enabled="False"></asp:TextBox></td>                        
    </tr>
    <tr>
        <td  class="style2">
            <asp:CheckBox  CssClass="checkbox" ID="FCheckBox" runat="server"  onclick="fnEnable(this,'F');" />F
            </td>            
            <td class="style2">
                <asp:TextBox ID="FBookStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2"><asp:TextBox ID="FBookEndNoTextBox" runat="server" 
                    onkeypress= "fncInputNumericValuesOnly()"  
                    onBlur="Javascript:fnCalculateNoBooks('F');"    CssClass="textInputStyle2" TabIndex="12" 
                    Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="FNoBooksTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="FCertStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"> </asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="FCertEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="FBookNoOpenTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="FBookNoBalanceTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()" TabIndex="0"
                    CssClass="textInputStyle2" Enabled="False"></asp:TextBox></td>                        
    </tr>
    <tr>
        <td  class="style2">
            <asp:CheckBox  CssClass="checkbox" ID="GCheckBox" runat="server"  onclick="fnEnable(this,'G');" />G
            </td>            
            <td class="style2">
                <asp:TextBox ID="GBookStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2"><asp:TextBox ID="GBookEndNoTextBox" runat="server" 
                    onkeypress= "fncInputNumericValuesOnly()"  
                    onBlur="Javascript:fnCalculateNoBooks('G');"    CssClass="textInputStyle2" 
                    TabIndex="13" CausesValidation="True" 
                    Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="GNoBooksTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="GCertStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="GCertEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="GBookNoOpenTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="GBookNoBalanceTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>                        
    </tr>
    <tr>
        <td  class="style2">
            <asp:CheckBox  CssClass="checkbox" ID="HCheckBox" runat="server"  onclick="fnEnable(this,'H');" />H
            </td>            
            <td class="style2">
                <asp:TextBox ID="HBookStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2"><asp:TextBox ID="HBookEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"  onBlur="Javascript:fnCalculateNoBooks('H');"    CssClass="textInputStyle2" TabIndex="14" CausesValidation="True" 
                    Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="HNoBooksTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="HCertStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="HCertEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="HBookNoOpenTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="HBookNoBalanceTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>                        
    </tr>
    <tr>
        <td  class="style2">
            <asp:CheckBox  CssClass="checkbox" ID="ICheckBox" runat="server"  onclick="fnEnable(this,'I');" />I
            </td>            
            <td class="style2">
                <asp:TextBox ID="IBookStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2"><asp:TextBox ID="IBookEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"  onBlur="Javascript:fnCalculateNoBooks('I');"    CssClass="textInputStyle2" TabIndex="15" CausesValidation="True" 
                    Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="INoBooksTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="ICertStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="ICertEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="IBookNoOpenTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="IBookNoBalanceTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>                        
    </tr>
    <tr>
        <td  class="style2">
            <asp:CheckBox  CssClass="checkbox" ID="JCheckBox" runat="server"  onclick="fnEnable(this,'J');" />J
            </td>            
            <td class="style2">
                <asp:TextBox ID="JBookStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2"><asp:TextBox ID="JBookEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"  onBlur="Javascript:fnCalculateNoBooks('J');"    CssClass="textInputStyle2" TabIndex="16" CausesValidation="True" 
                    Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="JNoBooksTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="JCertStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="JCertEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="JBookNoOpenTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="JBookNoBalanceTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>                        
    </tr>
    <tr>
        <td  class="style2">
            <asp:CheckBox  CssClass="checkbox" ID="KCheckBox" runat="server"  onclick="fnEnable(this,'K');" />K
            </td>            
            <td class="style2">
                <asp:TextBox ID="KBookStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2"><asp:TextBox ID="KBookEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"  onBlur="Javascript:fnCalculateNoBooks('K');"    CssClass="textInputStyle2" TabIndex="16" CausesValidation="True" 
                    Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="KNoBooksTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="KCertStartNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="KCertEndNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="KBookNoOpenTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>
            <td class="style2">
                <asp:TextBox ID="KBookNoBalanceTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass="textInputStyle2" TabIndex="0" Enabled="False"></asp:TextBox></td>                        
    </tr>
        
</table>

</div>
</ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="fundNameDropDownList" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
</div>
<table width="1100">
 <tr>
    <td class="style4"><a href="#" onclick="fnCheckAll();">Select All</a> </td>
     <td class="style5"><a href="#" onclick="fnUnCheckAll();">Deselect All</a> </td>
        <td align="left" colspan="6">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Button ID="Button1" runat="server" Text="Save" AccessKey="s" CssClass="buttoncommon" OnClientClick="return fnCheqeInput();" onclick="saveButton_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Reset" CssClass="buttoncommon" AccessKey="r" OnClientClick="return fnReset();"   />&nbsp;&nbsp;  
         <asp:Button ID="Button3" runat="server" Text="Close" CssClass="buttoncommon"  onclick="regCloseButton_Click" AccessKey="c" /></td>
    </tr>
</table>
   
    <br />
    <br />
    <br />
</asp:Content>

