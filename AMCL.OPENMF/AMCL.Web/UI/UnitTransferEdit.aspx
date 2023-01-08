<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitTransferEdit.aspx.cs" Inherits="UI_UnitTransferEdit" Title="Unit Transfer Edit Form (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
  function fnReset()
  {
      var Confrm=confirm("Are Sure To Resete");
        if(confirm)
        {
             document.getElementById("<%=transferNoTextBox.ClientID%>").value ="";
             document.getElementById("<%=transferDateTextBox.ClientID%>").value ="";
             document.getElementById("<%=tferorRegNoTextBox.ClientID%>").value ="";
             document.getElementById("<%=tferorHolderNameTextBox.ClientID%>").value ="";
             document.getElementById("<%=tferorjHolderNameTextBox.ClientID%>").value ="";
             document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").value ="";
             document.getElementById("<%=tfereeHolderNameTextBox.ClientID%>").value ="";
             document.getElementById("<%=tfereejHolderNameTextBox.ClientID%>").value ="";
             document.getElementById("<%=tdTferor.ClientID%>").innerHTML ="";
             document.getElementById("<%=tdTferee.ClientID%>").innerHTML ="";
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
          if(document.getElementById("<%=transferNoTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=transferNoTextBox.ClientID%>").focus();
                alert("Please Enter Transfer Number ");
                return false;
               }
        
          if(document.getElementById("<%=transferDateTextBox.ClientID%>").value =="")
           {
            document.getElementById("<%=transferDateTextBox.ClientID%>").focus();
            alert("Please Enter Date of Transfer  ");
            return false;
           }
          if(document.getElementById("<%=tferorRegNoTextBox.ClientID%>").value =="")
           {
            document.getElementById("<%=tferorRegNoTextBox.ClientID%>").focus();
            alert("Please Enter Registration Number of Transferor  ");
            return false;
           }
          if(document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").value =="")
           {
            document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").focus();
            alert("Please Enter Registration Number of Transferee  ");
            return false;
           }
           
           
           
           
           
             if(document.getElementById("<%=transferNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=transferNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=transferNoTextBox.ClientID%>").focus();
                    alert("Please Enter Numaric value for Transfer  Number");
                    return false;
                }
            }
           
            if(document.getElementById("<%=tferorRegNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=tferorRegNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=tferorRegNoTextBox.ClientID%>").focus();
                    alert("Please Enter Numaric value for Transferor Registration Number");
                    return false;
                }
            }
            
           if(document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").focus();
                    alert("Please Enter Numaric value for Transferee Registration Number");
                    return false;
                }
            }
      
      
      
        if(document.getElementById("<%=transferDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=transferDateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=transferDateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
      
      
        if(document.getElementById("<%=leftDataGrid.ClientID%>"))
        {
          if(document.getElementById("<%=tferorRegNoTextBox.ClientID%>").value ==document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").value )
            {
               alert("Transfer IN and OUT could not be Same Registration");
                return false;
            }
        
            var leftDatagrid=document.getElementById("<%=leftDataGrid.ClientID%>")
            var saleCert="";    
            var chek=0;                
            var tferorRegNo=document.getElementById("<%=tferorRegNoTextBox.ClientID%>").value;
            var tfereeRegNo=document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").value;
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
                var msg="Are You Sure to Transform The Following Sale Certificate?"+'\n';
                msg=msg+" Registration No: "+tferorRegNo+" to "+tfereeRegNo+'\n';
                msg=msg+" Sale Certificates:"+'\n'+saleCert;  
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
                alert("Please Select Any Sale Number To Transfer");
                return false;
            }
  
        }
        else
        {
          alert("No Units to Transfer");
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
        .style4
        {
            height: 38px;
        }
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
           Unit Holder Transfer Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>

      <br />
      <div>
      <asp:UpdatePanel ID="HolderInfoUpdatePanel" runat="server">
     <ContentTemplate>
      <div id="dvContent" runat="server"  style="width:658px; padding-left:80px; padding-right:100px; height: 271px;" >                 
     
          <div  id="dvLeftContent" style=" float:left; width:430;" runat="server">
          <table width="420" align="center" cellpadding="0" cellspacing="0" border="0" >
    <colgroup width="120"></colgroup>
    <colgroup width="300"></colgroup>
        <tr>
            <td colspan="2" align="left">&nbsp;</td>
            
        </tr>
        <tr>
            <td align="left" >Transfer No:</td>
            <td align="left">
                <asp:TextBox ID="transferNoTextBox" runat="server"  MaxLength="6" 
                    CssClass= "TextInputStyleSmall" TabIndex="1" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
                                    <span class="star">*</span></td>
            
           
        </tr>
        <tr>
             <td align="left"  >&nbsp;Date of Transfer:</td>
            <td align="left" >
                <asp:TextBox ID="transferDateTextBox" runat="server" CssClass="textInputStyleDate" 
                    TabIndex="2"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                    TargetControlID="transferDateTextBox" PopupButtonID="RegDateImageButton" 
                    Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="RegDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                    TabIndex="10" />
                <span class="star">* </span></td>
             
        </tr>
        <tr>
            <td align="left" >Transferor Reg No:</td>
            <td align="left">
                <asp:TextBox ID="tferorFundCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" TabIndex="3"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="tferorBranchCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" TabIndex="4"></asp:TextBox>
                &nbsp;
                                    <asp:TextBox ID="tferorRegNoTextBox" runat="server"  MaxLength="8" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass= "TextInputStyleSmall" TabIndex="5" 
                    ontextchanged="tferorRegNoTextBox_TextChanged" AutoPostBack="True" 
                    Width="86px"></asp:TextBox>
                                    <span class="star">*</span>&nbsp;</td>
            
           
           
        </tr>
         
        <tr>
            <td align="left" >Name of Transferor:</td>
            <td align="left">
                <asp:TextBox ID="tferorHolderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="6"></asp:TextBox>
            </td>
        </tr>
        
       
            
        <tr>
            <td align="left">&nbsp;</td>
            <td align="left">
                 <asp:TextBox ID="tferorjHolderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="7"></asp:TextBox></td>
                                     
        </tr>
        <tr>
                  <td align="left" >&nbsp;</td>
           
        </tr>   
         <tr>
            <td align="left" >Transferee Reg No:</td>
            <td align="left">
                <asp:TextBox ID="tfereeFundCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" AutoPostBack="True" 
                    TabIndex="8" Enabled="False"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="tfereeBranhCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" TabIndex="9"></asp:TextBox>
                &nbsp;
                                    <asp:TextBox ID="tfereeRegNoTextBox" runat="server"  MaxLength="8" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass= "TextInputStyleSmall" TabIndex="9" 
                    ontextchanged="tfereeRegNoTextBox_TextChanged" AutoCompleteType="Disabled" 
                    AutoPostBack="True" Width="86px"></asp:TextBox>
                                    <span class="star">*</span>&nbsp;</td>
            
           
           
        </tr>
         
        <tr>
            <td align="left" >Name of Transferee:</td>
            <td align="left">
                <asp:TextBox ID="tfereeHolderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="10"></asp:TextBox>
            </td>
        </tr>
        
       
            
        <tr>
            <td align="left">&nbsp;</td>
            <td align="left">
                 <asp:TextBox ID="tfereejHolderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="11"></asp:TextBox></td>
                                     
        </tr>
            
        </table>
        </div>
          <div style="border-style: solid; border-color: inherit; border-width: 1px; width:222px; float:left; height:259px;">
                <table align="center" width="164" cellpadding="0" cellspacing="0" 
                    style="height: 261px">
                <tr style=" height:15px;">
                   <td align="center" ><span   style="border:1px solid">Signature of Transferor    </span></td></tr>
                   <tr>
                    <td align="left" id="tdTferor" runat="server" class="style4">
                  <asp:Image ID="SignImage" runat="server" Height="104px" Width="200px"  />
                    </td>    
                </tr>
                 <tr style=" height:15px;">
                   <td align="center" ><span   style="border:1px solid">Signature of Transferee</span></td>
                </tr>
                 <tr style="height:85px;">
                    <td align="left" id="tdTferee" runat="server">
                  <asp:Image ID="PhotoImage" runat="server" Height="115px" Width="200px"  />
                  </td>
                 </tr>
                </table>
           </div>
          
    </div>    
    
    <br />
    <table align="center" width="400" cellpadding="0" cellspacing="0">
     <colgroup width="250"></colgroup>
    
        <tr >
           <td align="left" ><b>Total Holding&nbsp; Units:            <td align="left">
                <asp:TextBox ID="TotalUnitHoldingTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True" ForeColor="#009933"></asp:TextBox>
            </td>           
        </tr>
         <tr >
           <td align="left" ><b>Selected Total Units for Transfer:</b></td>
            <td align="left">
                <asp:TextBox ID="TotalUnitRepurchaseTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True" ForeColor="#990033"></asp:TextBox>
            </td>           
        </tr>
        
              
      </table>
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
                <asp:AsyncPostBackTrigger ControlID="tferorRegNoTextBox" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="tfereeHolderNameTextBox" EventName="TextChanged" />
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

