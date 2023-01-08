<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportLetterLienMark.aspx.cs" Inherits="UI_UnitReportLetterLienMark" Title="Unit Letter for Lien Mark  (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
 function fnReset()
  {
      var Confrm=confirm("Are Sure To Resete");
        if(confirm)
        {                       
            document.getElementById("<%=LienMarkDropDownList.ClientID%>").value ="0";           
            document.getElementById("<%=LienbankNameDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=LienbranchNameDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=DesignationDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=SignatoryDropDownList.ClientID%>").value ="0";            
            document.getElementById("<%=RegNoTextBox.ClientID%>").value ="";
            document.getElementById("<%=HolderNameTextBox.ClientID%>").value ="";             
            document.getElementById("<%=HolderJNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=LienReqRefTextBox.ClientID%>").value ="";
            document.getElementById("<%=toTextBox.ClientID%>").value ="";             
            document.getElementById("<%=DivisionTextBox.ClientID%>").value ="";
            document.getElementById("<%=Address1TextBox.ClientID%>").value ="";
            document.getElementById("<%=Address2TextBox.ClientID%>").value ="";             
            document.getElementById("<%=Address3TextBox.ClientID%>").value ="";
            document.getElementById("<%=LienReqRefTextBox.ClientID%>").value ="";
            document.getElementById("<%=LienReqDateTextBox.ClientID%>").value ="";             
           
            
             return false;
        }
  }
  function findCheqInput()
  {
    if( document.getElementById("<%=RegNoTextBox.ClientID%>").value =="")
    {
        document.getElementById("<%=RegNoTextBox.ClientID%>").focus();
        alert("Please Enter Registration Number");
        return false;                
    }
  }
 
    
    
   function fnCheckInput()
    {
       
        
               
           if(document.getElementById("<%=LienbankNameDropDownList.ClientID%>").value =="0")
               {
                document.getElementById("<%=LienbankNameDropDownList.ClientID%>").focus();
                alert("Please Select Lien Institution  ");
                return false;
               }
                
          if(document.getElementById("<%=LienbranchNameDropDownList.ClientID%>").value =="0")
               {
                document.getElementById("<%=LienbranchNameDropDownList.ClientID%>").focus();
                alert("Please Select Lien Institution Branch  ");
                return false;
               }
        
        

           
          if(document.getElementById("<%=RegNoTextBox.ClientID%>").value =="")
           {
            document.getElementById("<%=RegNoTextBox.ClientID%>").focus();
            alert("Please Enter Registration Number ");
            return false;
           }
        if(document.getElementById("<%=RegNoTextBox.ClientID%>").value !="")
        {
            var digitCheck = /^\d+$/;
            if(!digitCheck.test(document.getElementById("<%=RegNoTextBox.ClientID%>").value))
            {
                document.getElementById("<%=RegNoTextBox.ClientID%>").focus();
                alert("Please Enter Numaric value for Registration Number");
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
  
</script>
 


    <style type="text/css">
        .style6
        {
            font-family: "Times New Roman";
            font-weight: bold;
        }
        .style11
        {
            height: 20px;            
        }
         .style12
        {
            width: 323px;
        }
        .style13
        {
            height: 20px;
            width: 323px;
        }
         </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
      

      
    
   
   
     
    <table align="left" cellpadding="0" cellspacing="0" border="0"  >
    <colgroup width="200"></colgroup>
    <colgroup width="300"></colgroup>
      <tr>
           <td class="FormTitle" align="center" colspan="2">
           Unit Lien Mark Confirm Letter Form (<span id="span1" runat="server"></span>)
            </td> 
            
        </tr>
        <tr>
            <td colspan="2" align="left">&nbsp;</td>
            
        </tr>
           <tr>
            <td align="left" class="style12" >Registration No:</td>
            <td align="left">
                <asp:TextBox ID="FundCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" TabIndex="3"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="BranchCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" TabIndex="4"></asp:TextBox>
                &nbsp;
                                    <asp:TextBox ID="RegNoTextBox" runat="server"  MaxLength="8" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass= "TextInputStyleSmall" TabIndex="1" 
                    
                    Width="86px"></asp:TextBox>
                                    <span class="star">*</span>&nbsp;<asp:Button 
                    ID="findButton" runat="server" AccessKey="f" CssClass="buttonmid" 
                    meta:resourcekey="findButtonResource1" onclick="findButton_Click" 
                    onclientclick="return findCheqInput();" TabIndex="2" Text="Find" />
               </td>
            
           
           
        </tr>
        
        <tr>
            <td align="left" class="style12" >Lien Mark No:</td>
            <td align="left">
                <asp:DropDownList ID="LienMarkDropDownList" runat="server"  TabIndex="3" 
                    AutoPostBack="True" 
                    onselectedindexchanged="LienMarkDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
                <span class="star">*</span></td>
                                   
                       
        </tr>
      
        <tr>
            <td align="left" class="style13" >Lien Mark&nbsp; Request Date:</td>
            <td align="left" class="style11">
                <span class="star">
                <asp:TextBox ID="LienReqDateTextBox" runat="server" CssClass="textInputStyleDate" 
                    TabIndex="4"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                    TargetControlID="LienReqDateTextBox" PopupButtonID="RegDateImageButton" 
                    Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="RegDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                    TabIndex="10" />
                * </span></td>
            
           
        </tr>
        <tr>
            <td align="left" class="style12" >Lien Institution:</td>
            <td align="left">
               
                <asp:DropDownList ID="LienbankNameDropDownList" runat="server"                    
                    TabIndex="5" Enabled="False">
                </asp:DropDownList>
               
                <span class="star">*</span></td>
            
           
        </tr>
        <tr>
            <td align="left" class="style12" >Lien Institution Branch:</td>
            <td align="left">
               
                <asp:DropDownList ID="LienbranchNameDropDownList" runat="server"   TabIndex="6" 
                    Enabled="False">
                </asp:DropDownList>
               
                <span class="star">*</span></td>
           
        </tr>
        <tr>
             <td align="left" class="style13"  >Lien Request Reference:</td>
            <td align="left" class="style11" >
                <asp:TextBox ID="LienReqRefTextBox" runat="server" 
                    CssClass="TextInputStyleLarge" MaxLength="55" TabIndex="7"></asp:TextBox>
             </td>
             
        </tr>
        
         
        <tr>
            <td align="left" class="style12" >Name of Holder:</td>
            <td align="left">
                <asp:TextBox ID="HolderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="8"></asp:TextBox>
            </td>
        </tr>
        
       
            
        <tr>
            <td align="left" class="style12">Name of The Joint Holder:</td>
            <td align="left">
                 <asp:TextBox ID="HolderJNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="9"></asp:TextBox></td>
                                     
        </tr>
        <tr>
            <td align="left" class="style12">To:</td>
            <td align="left">
                 <asp:TextBox ID="toTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="9"></asp:TextBox>
                <span class="star">*</span></td>
                                     
        </tr>
                                <tr>
                      <td align="left" class="style12">
                          Division/Department of Lien Institution:</td>
                      <td align="left">
                          <asp:TextBox ID="DivisionTextBox" runat="server" CssClass="TextInputStyleLarge" 
                              TabIndex="11" Width="333px"></asp:TextBox>
                      </td>
                  </tr>
                 
                    
                          `<tr>
                              <td align="left" class="style12">
                                  Address1 of Lien Institution:</td>
                              <td align="left">
                                  <asp:TextBox ID="Address1TextBox" runat="server" CssClass="TextInputStyleLarge" 
                                      MaxLength="55" TabIndex="12" Width="333px"></asp:TextBox>
                              </td>
                          </tr>
                          `<tr>
                              <td align="left" class="style12">
                                  Address2 of Lien Institution:</td>
                              <td align="left">
                                  <asp:TextBox ID="Address2TextBox" runat="server" CssClass="TextInputStyleLarge" 
                                      MaxLength="55" TabIndex="12" Width="333px"></asp:TextBox>
                              </td>
                          </tr>
                          `<tr>
                              <td align="left" class="style12">
                                  Address3 of Lien Institution:</td>
                              <td align="left">
                                  <asp:TextBox ID="Address3TextBox" runat="server" CssClass="TextInputStyleLarge" 
                                      MaxLength="55" TabIndex="12" Width="333px"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td align="left" class="style12">
                                  Name of The Signatory</td>
                              <td align="left">
                                  <asp:DropDownList ID="SignatoryDropDownList" runat="server">
                                  </asp:DropDownList>
                                  <span class="star">*</span></td>
                          </tr>
                            <tr>
                              <td align="left" class="style12">
                                 Designation of The Signatory</td>
                              <td align="left">
                                  <asp:DropDownList ID="DesignationDropDownList" runat="server">
                                  <asp:ListItem Value="0" Selected="True">Chief Executive Officer</asp:ListItem>
                                  <asp:ListItem Value="1" >Depty Chief Executive Officer</asp:ListItem>
                                  <asp:ListItem Value="2" >Senior Principal Officer</asp:ListItem>
                                  <asp:ListItem Value="3" >Senior Executive Officer</asp:ListItem>
                                  <asp:ListItem Value="4" >System Analyst</asp:ListItem>
                                  <asp:ListItem Value="5" >Principal Officer</asp:ListItem>
                                  <asp:ListItem Value="6" >Executive Officer</asp:ListItem>
                                  <asp:ListItem Value="7" >Programmer</asp:ListItem>
                                  </asp:DropDownList>
                                  <span class="star">*</span></td>
                          </tr>
                          <tr>
                              <td colspan="2">
                                  &nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                          </tr>
                          <tr>
                              <td align="center" colspan="2">
                                  <asp:Button ID="PrintButton" runat="server" AccessKey="p" 
                                      CssClass="buttoncommon" onclick="PrintButton_Click" 
                                      OnClientClick="return fnCheckInput();" TabIndex="13" Text="Print" />
                                  &nbsp;
                                  <asp:Button ID="ResetButton" runat="server" AccessKey="r" 
                                      CssClass="buttoncommon" OnClientClick="return fnReset();" Text="Reset" />
                                  &nbsp;
                                  <asp:Button ID="CloseButton" runat="server" AccessKey="c" 
                                      CssClass="buttoncommon" onclick="CloseButton_Click" Text="Close" />
                              </td>
                          </tr>
                          <tr>
                              <td colspan="2">
                                  &nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                          </tr>
                          <tr>
                              <td colspan="2">
                                  &nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                          </tr>
       
             
        </table>
    
 
    
    <br>
    
            
    <br />   
   
    <br />
    <br />
    <br />
    <br />
    <br />   
   
    <br />
    <br />
    <br />
    <br />
</asp:Content>

