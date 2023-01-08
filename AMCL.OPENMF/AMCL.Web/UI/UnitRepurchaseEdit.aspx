<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitRepurchaseEdit.aspx.cs" Inherits="UI_UnitRepurchaseEdit" Title="Unit Repurchase Edit (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
 function fnReset()
  {
      var Confrm=confirm("Are Sure To Resete");
        if(confirm)
        {
             document.getElementById("<%=RepNoTextBox.ClientID%>").value ="";
             document.getElementById("<%=RepRateTextBox.ClientID%>").value ="";
             document.getElementById("<%=RepDateTextBox.ClientID%>").value ="";
             document.getElementById("<%=RegNoTextBox.ClientID%>").value ="";
             document.getElementById("<%=HolderNameTextBox.ClientID%>").value ="";             
             document.getElementById("<%=HolderJNameTextBox.ClientID%>").value ="";
             document.getElementById("<%=TotalUnitHoldingTextBox.ClientID%>").value ="";
             document.getElementById("<%=TotalUnitRepurchaseTextBox.ClientID%>").value ="";
             document.getElementById("<%=tdSign.ClientID%>").innerHTML ="";
            
             if(document.getElementById("<%=leftDataGrid.ClientID%>"))
             {
                document.getElementById("<%=divLeftgrid.ClientID%>").innerHTML ="";
             }  
             return false;
        }
  }
 function fnCheckAll()
    {
        for (Looper=0; Looper < document.forms[0].length ; Looper++ )
        {
            var strType = document.forms[0].elements[Looper].type;
            if (strType=="checkbox")
            {
                
                document.forms[0].elements[Looper].checked=true;
            }   
        }
        fnSelectedTotalUnit();
    }
    function fnUnCheckAll()
    {
        for (Looper=0; Looper < document.forms[0].length ; Looper++ )
        {
            var strType = document.forms[0].elements[Looper].type;
            if (strType=="checkbox")
            {
                
                document.forms[0].elements[Looper].checked=false;
          }   
        }
        fnSelectedTotalUnit();
    }
    
    
    function fnCheckInput()
    {
          if(document.getElementById("<%=RepNoTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=RepNoTextBox.ClientID%>").focus();
                alert("Please Enter Repurchase Number ");
                return false;
               }
               
           if(document.getElementById("<%=RepRateTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=RepRateTextBox.ClientID%>").focus();
                alert("Please Enter Repurchase Rate ");
                return false;
               }
        
          if(document.getElementById("<%=RepDateTextBox.ClientID%>").value =="")
           {
            document.getElementById("<%=RepDateTextBox.ClientID%>").focus();
            alert("Please Enter Date of Repurchase  ");
            return false;
           }

            if(document.getElementById("<%=RepNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=RepNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=RepNoTextBox.ClientID%>").focus();
                    alert("Please Enter Numaric value for Repurchase  Number");
                    return false;
                }
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
       
      
      
        if(document.getElementById("<%=RepDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=RepDateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=RepDateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
      
      
        if(document.getElementById("<%=leftDataGrid.ClientID%>"))
        {
        
            var leftDatagrid=document.getElementById("<%=leftDataGrid.ClientID%>")
            var saleCert="";    
            var chek=0;                         
            for( var rowCount = 1; rowCount < leftDatagrid.rows.length; rowCount++)
            {
              var tr = leftDatagrid.rows[rowCount];
              var td= tr.childNodes[0]; 
              var item = td.firstChild; 
              var strType=item.type;
              if(strType=="checkbox")
              {
                if(item.checked)
                {
                 chek=chek+1;
                    if(saleCert=="")
                    {                       
                        saleCert=leftDatagrid.rows[rowCount].cells[1].innerHTML+"::"+leftDatagrid.rows[rowCount].cells[2].innerHTML;
                     }
                     else
                     {
                        if(leftDatagrid.rows[rowCount].cells[1].innerHTML==leftDatagrid.rows[rowCount-1].cells[1].innerHTML)
                        {
                            saleCert=saleCert+","+leftDatagrid.rows[rowCount].cells[2].innerHTML;
                         }
                         else
                         {
                            saleCert=saleCert+"\n"+leftDatagrid.rows[rowCount].cells[1].innerHTML+"::"+leftDatagrid.rows[rowCount].cells[2].innerHTML;
                         }
                     }
                }
              }
            } 
            
           if(chek>0)
            {
                var msg="Are You Sure to Surrender The Following Sale Certificate?"+'\n';
                msg=msg+" Sale Certificates: "+'\n'+saleCert;  
                var  conformMsg=confirm(msg);       
                if(conformMsg)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                alert("Please Select Any Sale Number To Surrender");
                return false;
            }
  
        }
        else
        {
          alert("No Units to Surrender");
          return false;
        }  
     }
  
  function fnSelectedTotalUnit()
  {
    if(document.getElementById("<%=leftDataGrid.ClientID%>"))
            {      
                var datagrid=document.getElementById("<%=leftDataGrid.ClientID%>")
                var sum = 0;    
                var check = 0;                
                
                for( var rowCount = 0; rowCount < datagrid.rows.length; rowCount++)
                {
                  var tr = datagrid.rows[rowCount];
                  var td= tr.childNodes[0]; 
                  var item = td.firstChild; 
                  var strType=item.type;
                  if(strType=="checkbox")
                  {
                    if(item.checked)
                    {
                    datagrid.rows[rowCount].style.backgroundColor='#DDAAFF';
                     check = check +1;
                        
                     sum = sum + parseInt(datagrid.rows[rowCount].cells[3].innerHTML);
                       
                    }
                    else
                    {
                        if(rowCount%2==0)
                        {
                            datagrid.rows[rowCount].style.backgroundColor='#D5E0E6';
                        }
                        else
                        {
                            datagrid.rows[rowCount].style.backgroundColor='#DBEAF5';
                        }
                    }
                  }
                }
                document.getElementById("<%=TotalUnitRepurchaseTextBox.ClientID%>").value=parseInt(sum);
            }  
        }
        
     function PopupLienDetails()
        {
            if(document.getElementById("<%=RegNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=RegNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number");
                return false;
                
            }
              if(document.getElementById("<%=TotalLienUnitHoldingTextBox.ClientID%>").value =="0"||document.getElementById("<%=TotalLienUnitHoldingTextBox.ClientID%>").value =="")
            {
                
                alert("No Lien Units to Show Details");
                return false;
                
            }
            
           if(document.getElementById("<%=RegNoTextBox.ClientID%>").value !="")
            {
                 var fundCode=document.getElementById("<%=FundCodeTextBox.ClientID%>").value;
                 var reg=document.getElementById("<%=RegNoTextBox.ClientID%>").value;
                 var Branch=document.getElementById("<%=BranchCodeTextBox.ClientID%>").value;
                 var url='Popup/ViewLienDetails.aspx?reg='+reg+'&fund='+fundCode+'&branch='+Branch;
                 var ViewLienDetails= window.open(url,'ViewLienDetails');
                 ViewLienDetails.focus();
                  return false;
                
              
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
        .style8
        {
            font-size: 14px;
        }
        .style9
        {
            width: 75px;
        }
        .style10
        {
            width: 79px;
        }
    </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           Unit Holder Repurchase Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>

      <br />
      <div id="dvUpdatePanel" runat="server">
      <asp:UpdatePanel ID="holderInfoUpdatePanel" runat="server">
      <ContentTemplate>
      <div id="dvContent" runat="server" 
        style="width:658px; padding-left:80px; padding-right:100px; height: 161px;" >
          <div  id="dvLeftContent" style=" float:left; width:430;">
          <table width="420" align="center" cellpadding="0" cellspacing="0" border="0" >
    <colgroup width="120"></colgroup>
    <colgroup width="300"></colgroup>
        <tr>
            <td colspan="2" align="left">&nbsp;</td>
            
        </tr>
        <tr>
            <td align="left" >Repurchase No:</td>
            <td align="left">
                <asp:TextBox ID="RepNoTextBox" runat="server"  MaxLength="6" 
                    CssClass= "TextInputStyleSmall" TabIndex="2" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
                                    <span class="star">*<span class="star"><asp:Button 
                    ID="findButton" runat="server" AccessKey="f" CssClass="buttonmid" 
                    meta:resourcekey="findButtonResource1" onclick="findButton_Click" 
                    onclientclick="return fnFindChek();" TabIndex="2" Text="Find" />
                </span></span></td>
                       
        </tr>
        <tr>
            <td align="left" >Repurchase Date</td>
            <td align="left">
                <span class="star">
                <asp:TextBox ID="RepDateTextBox" runat="server" CssClass="textInputStyleDate" 
                    TabIndex="4"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                    TargetControlID="RepDateTextBox" PopupButtonID="RegDateImageButton" 
                    Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="RegDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                    TabIndex="10" />
                * </span></td>
            
           
        </tr>
        <tr>
             <td align="left"  >Repurchase Rate:</td>
            <td align="left" >
                <asp:TextBox ID="RepRateTextBox" runat="server"  MaxLength="6" 
                    CssClass= "TextInputStyleSmall" TabIndex="3"></asp:TextBox>
                                    <span class="star">*</span></td>
             
        </tr>
        <tr>
            <td align="left" >&nbsp;Reg No :</td>
            <td align="left">
                <asp:TextBox ID="FundCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" TabIndex="3"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="BranchCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" TabIndex="4"></asp:TextBox>
                &nbsp;
                                    <asp:TextBox ID="RegNoTextBox" runat="server"  MaxLength="8" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass= "TextInputStyleSmall" TabIndex="1" 
                    ontextchanged="RegNoTextBox_TextChanged" AutoPostBack="True" 
                    Width="86px"></asp:TextBox>
                                    <span class="star">*</span>&nbsp;</td>
            
           
           
        </tr>
         
        <tr>
            <td align="left" >Name of Holder:</td>
            <td align="left">
                <asp:TextBox ID="HolderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="6"></asp:TextBox>
            </td>
        </tr>
        
       
            
        <tr>
            <td align="left">&nbsp;</td>
            <td align="left">
                 <asp:TextBox ID="HolderJNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="7"></asp:TextBox></td>
                                     
        </tr>

        </table>
        </div>
          <div style="border-style: solid; border-color: inherit; border-width: 1px; width:229px; float:left; height:153px;">
                <table align="center" width="164" cellpadding="0" cellspacing="0" 
                    style="height: 98px">
                <tr style=" height:15px;">
                   <td align="center" ><span   style="border:1px solid"><b>Signature and Photo</b>&nbsp;    &nbsp;    </span></td></tr>
                   <tr>
                    <td align="left" id="tdSign" runat="server">
                  <asp:Image ID="SignImage" runat="server" Height="136px" Width="222px"  />
                    </td>    
                </tr>
              
                </table>
           </div>
    </div>
            
    <br />
     <table align="center" cellpadding="0" cellspacing="0" style="width: 467px">
     <colgroup width="250"></colgroup>
    
        <tr >
           <td align="left" ><b>Total Holding&nbsp; Units:            <td align="left">
                <asp:TextBox ID="TotalUnitHoldingTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True" ForeColor="#009933"></asp:TextBox>
            </td>           
        </tr>
        <tr >
           <td align="left" ><b>Total Liened Units:</b><td align="left">
               <asp:TextBox ID="TotalLienUnitHoldingTextBox" runat="server" 
                   CssClass="TextInputStyleSmall" Enabled="false" Font-Bold="True" 
                   ForeColor="#009933" Width="100px"></asp:TextBox>&nbsp;
               <asp:Button ID="ShowLienDatailsButton" runat="server" AccessKey="l" 
                   CssClass="buttoncommon" EnableTheming="True" 
                   OnClientClick=" return PopupLienDetails();" Text="Lien Details" 
                   Height="18px" Width="87px" />
               </td>
        </tr>
         <tr >
           <td align="left" ><b>Selected Total Units for Repurchase:</b></td>
            <td align="left">
                <asp:TextBox ID="TotalUnitRepurchaseTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True" ForeColor="#990033"></asp:TextBox>
            </td>           
        </tr>
        
              
      </table>
    <br />
    <br />
   
    <div id="dvContentBottom" runat="server"  style="width:840px; height: 266px;">
    <div id="divLeftgrid" runat="server" 
            style="width:845px; float:left; text-align:center;">
    <table align="center" style="width: 427px; height: 20px;">
        <tr class="style6">
            <td align="center" class="style9"><a href="#" onclick="fnCheckAll();">Select All</a> </td>
            <td><span class="style8">Sale No</span></td>
            <td><span class="style8">Certificate</span></td>
            <td><span class="style8">Weight</span></td>
        </tr>
    </table>
    <table align="center" style="width: 430">
         <tr>
         <td> 
             <div style="text-align: center; display: block; overflow: auto; width:430px; height:200px;">

               <asp:DataGrid ID="leftDataGrid" runat="server" AutoGenerateColumns="False" 
                   Width="403px"  >
                   <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                    <ItemStyle CssClass="TableText"></ItemStyle>
                    <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                    <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                    <Columns>
                    <asp:TemplateColumn HeaderText="">
                    <ItemTemplate> 
                         <asp:CheckBox ID="leftCheckBox" runat="server" onclick="fnSelectedTotalUnit();" ></asp:CheckBox> 
                    </ItemTemplate>                    
                    </asp:TemplateColumn>                    
                    <asp:BoundColumn DataField="SL_NO"></asp:BoundColumn>                                                                                              
                    <asp:BoundColumn DataField="CERTIFICATE"></asp:BoundColumn>                                                                                              
                    <asp:BoundColumn  DataField="QTY"></asp:BoundColumn> 
                                                             
                </Columns>
            </asp:DataGrid>              
                </div>
           <table align="center" style="width: 427px; height: 20px;">
        <tr class="style6">
            <td align="center" class="style10"><a href="#" onclick="fnUnCheckAll();">Deselect All</a></td>
            <td><span class="style8">Sale No</span></td>
            <td><span class="style8">Certificate</span></td>
            <td><span class="style8">Weight</span></td>
        </tr>
    </table>
           </td>
         </tr>
    </table>
    </div>
   
  
    </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="RegNoTextBox" EventName="TextChanged" />
    </Triggers>
    </asp:UpdatePanel>
    </div>
    <br />
    
    
    <table align="center" cellpadding="0" cellspacing="0" style="width: 464px">
     <tr>
        <td align="right">
        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheckInput();"
                 AccessKey="s" onclick="SaveButton_Click"/>&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" 
                  />
        
        </td>
        <td>
        &nbsp;
        </td>
    </tr>
   </table>
   
    <br />
    <br />
    <br />
    <br />
</asp:Content>

