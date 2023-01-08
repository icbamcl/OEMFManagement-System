<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitSaleEdit.aspx.cs" Inherits="UI_UnitSaleEdit" Title=" Unit Sale Edit Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
     function fnReset()
    {
        var Confrm=confirm("Are Sure To Resete");
        if(Confrm)
        {
            document.getElementById("<%=saleDateTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=unitQtyTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderTelphoneTextBox.ClientID%>").value =""; 
            document.getElementById("<%=regNoTextBox.ClientID%>").value =""; 
            document.getElementById("<%=saleRateTextBox.ClientID%>").value ="";          
            document.getElementById("<%=saleRemarksTextBox.ClientID%>").value =""; 
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
            {
               document.getElementById("<%=dinoGridView.ClientID%>").style.visibility='hidden';
            }
          
       
            

         document.getElementById("<%=tdCIP.ClientID%>").innerHTML ="";
         document.getElementById("<%=SignImage.ClientID%>").src ="";
        
            return false;
        }
        else
        {
         return false;
        }
       
    }
    
    
     function fnResetAll()
    {
       
              
            document.getElementById("<%=saleDateTextBox.ClientID%>").value ="";
            
            
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderTextBox.ClientID%>").value ="";
            
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=saleNumberTextBox.ClientID%>").value ="";
            document.getElementById("<%=unitQtyTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderTelphoneTextBox.ClientID%>").value ="";          
            document.getElementById("<%=saleRemarksTextBox.ClientID%>").value =""; 
            document.getElementById("<%=saleDateTextBox.ClientID%>").value="";
            document.getElementById("<%=saleRateTextBox.ClientID%>").value="";
            document.getElementById("<%=unitQtyTextBox.ClientID%>").value ="";
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
            {
               document.getElementById("<%=dinoGridView.ClientID%>").style.visibility='hidden';
            }
            

             document.getElementById("<%=tdCIP.ClientID%>").innerHTML ="";
             document.getElementById("<%=SignImage.ClientID%>").src ="";
             
             
             
             alert("Invalid Registration Number");
             return false;
      
    }
     function fnCheqInput()
        {
        
            //         if(1==1)
            //         {
            //            alert("You can not edit any Sale please Contract with Admin");
            //           return false;
            //         }
        
              //Input Text Checking
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number");
                return false;
                
            }
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=regNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                    alert("Please Enter Valid Registration Number");
                    return false;
                }
            }
            
           if(document.getElementById("<%=saleDateTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=saleDateTextBox.ClientID%>").focus();
                alert("Please Enter Sale Date");
                return false;
                
            }
            if(document.getElementById("<%=saleDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=saleDateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=saleDateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
            if(document.getElementById("<%=saleNumberTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=saleNumberTextBox.ClientID%>").focus();
                alert("Please Enter Sale Number");
                return false;
                
            }
            
          
            if(document.getElementById("<%=saleRateTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=saleRateTextBox.ClientID%>").focus();
                alert("Please Enter Sale Rate");
                return false;
                
            }
            if(document.getElementById("<%=unitQtyTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=unitQtyTextBox.ClientID%>").focus();
                alert("Please Enter Unit Quantity");
                return false;
                
            }
          
            if(!document.getElementById("<%=dinoGridView.ClientID%>"))
            {
               alert("Plese Add Certificate to The List");
                     return false;
            }
          
           //Data Grid Checking
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
           {
                    var inputs = document.getElementById("<%=dinoGridView.ClientID%>").getElementsByTagName("input");
                    
                    var wightNo=0;
                    wightNo=parseInt(wightNo);
                    var dinotxt = "";
                    var weighttxt = "";
                   

                   for(var i=0;i<inputs.length;i++)
                    {
                        if(inputs[i].type =="text")
                        {
                            if(inputs[i].id.indexOf("weightTextBox") != -1)
                            {
                              
                               
                                wightNo=wightNo+parseInt(inputs[i].value)
                            }
                          }
                    }
                    var unitQty=document.getElementById("<%=unitQtyTextBox.ClientID%>").value
                    if(wightNo!=parseInt(unitQty))
                    {
                        document.getElementById("<%=unitQtyTextBox.ClientID%>").focus();
                        alert(" Save Failed:Unit Quantity and List Quantity is not Equal");
                        return false;
                    }
             }
        
        }
        
        
        
       
       
         
       function  fnFindChek()//check Find Button validation
       {
       
           if(document.getElementById("<%=saleNumberTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=saleNumberTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=saleNumberTextBox.ClientID%>").focus();
                    alert("Please Enter Valid Sale Number");
                    return false;
                }
            }
              if(document.getElementById("<%=dinoGridView.ClientID%>"))
            {
               document.getElementById("<%=dinoGridView.ClientID%>").style.visibility='hidden';
            }
           
       }
       
       
       
       function fnValidateWeight(dinoObj)
       {
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
           {
                var inputs = document.getElementById("<%=dinoGridView.ClientID%>").getElementsByTagName("input");
                
                var wightNo=0;
                var dinotxt = "";
                var weighttxt = "";
               
                
                for(var i=0;i<inputs.length;i++)
                {
                    if(inputs[i].type =="text")
                    {
                        if(inputs[i].id.indexOf("dinoTextBox") != -1)
                        {
                            dinotxt = inputs[i];
                            if(dinoObj.id==dinotxt.id)
                            { 
                             wightNo=i+2;
                            }
                            
                        }
                      }
                }
                
               if(dinoObj.value.indexOf("K")==0 ||dinoObj.value.indexOf("k")==0)
                  {
                    if(document.getElementById("<%=fundCodeTextBox.ClientID%>").value=="BDF")
                    {
                        var weight=inputs[wightNo].value;
                        if(weight!=10000)
                        {
                           inputs[wightNo].focus();
                           alert("Invalid Certificate Weight");
                           return false;
                        }
                    }
                    else
                    {
                         alert("Invalid Dinomination");
                           return false;
                    }
                  }  
                else if (dinoObj.value.indexOf("J")==0 ||dinoObj.value.indexOf("j")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=5000)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
                else if(dinoObj.value.indexOf("i")==0 ||dinoObj.value.indexOf("I")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=1000)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("h")==0 ||dinoObj.value.indexOf("H")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=500)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
              else if(dinoObj.value.indexOf("g")==0 ||dinoObj.value.indexOf("G")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=250)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
              else if(dinoObj.value.indexOf("f")==0 ||dinoObj.value.indexOf("F")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=100)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("e")==0 ||dinoObj.value.indexOf("E")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=50)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("d")==0 ||dinoObj.value.indexOf("D")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=20)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("c")==0 ||dinoObj.value.indexOf("C")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=10)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("b")==0 ||dinoObj.value.indexOf("B")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=5)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("a")==0 ||dinoObj.value.indexOf("A")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=1)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
                else
                {
                   alert("Invalid Certificate Dinomination");
                       return false;  
                }
            }
            
       }
      
       function fnValidateDino(weightObj)
       {
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
           {
                var inputs = document.getElementById("<%=dinoGridView.ClientID%>").getElementsByTagName("input");
                
                var dinoNo=0;
                var dinotxt = "";
                var weighttxt = "";
                
                    for(var i=0;i<inputs.length;i++)
                    {
                        if(inputs[i].type =="text")
                        {
                            if(inputs[i].id.indexOf("weightTextBox") != -1)
                            {
                                weighttxt = inputs[i];
                                if(weightObj.id==weighttxt.id)
                                { 
                                 dinoNo=i-2;
                                }
                                
                            }                           
                         }
                    }
                    if(weightObj.value==10000)
                    {
                     if(document.getElementById("<%=fundCodeTextBox.ClientID%>").value=="BDF")
                     {
                        var dino=inputs[dinoNo].value;
                       
                       if(dino.toUpperCase()!="K")
                        {
                           inputs[dinoNo].focus();
                           alert("Invalid Dinomination");
                           return false;
                        }
                      }
                      else
                      {
                         alert("Invalid Weight");
                           return false;
                      }
                    }
                    else if(weightObj.value==5000)
                    {
                        var dino=inputs[dinoNo].value;
                       
                       if(dino.toUpperCase()!="J")
                        {
                           inputs[dinoNo].focus();
                           alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==1000)
                    {
                       var dino=inputs[dinoNo].value;
                         if(dino.toUpperCase()!="I")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==500)
                    {
                        var dino=inputs[dinoNo].value;
                         if(dino.toUpperCase()!="H")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==250)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="G")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==100)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="F")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==50)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="E")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                  else if(weightObj.value==20)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="D")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                 else if(weightObj.value==10)
                    {
                        var dino=inputs[dinoNo].value;
                       if(dino.toUpperCase()!="C")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                   else if(weightObj.value==5)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="B")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                   else if(parseInt(weightObj.value)==1)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="A")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else
                    {
                        alert("Invalid Weight");
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
	  
  function fnSelectedTotalUnit()
  {
    if(document.getElementById("<%=dinoGridView.ClientID%>"))
            {      
                var datagrid=document.getElementById("<%=dinoGridView.ClientID%>")               
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
               
            }  
        }
function CheckAllDataGridWarrantNo(checkVal)
     {
            if(document.getElementById("<%=dinoGridView.ClientID%>"))
            {  
                
                var datagrid=document.getElementById("<%=dinoGridView.ClientID%>")
                   
                var check = 0;                
                
                for( var rowCount = 0; rowCount < datagrid.rows.length; rowCount++)
                {
                  var tr = datagrid.rows[rowCount];
                  var td= tr.childNodes[0]; 
                  var item = td.firstChild; 
                  var strType=item.type;
                  if(strType=="checkbox")
                  {
                        item.checked = checkVal; 
                  }
                  fnSelectedTotalUnit();
                }
            }
     }
  function fncheck()
       {
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
           {
           
           
           var inputs = document.getElementById("<%=dinoGridView.ClientID%>").getElementsByTagName("input");
                
                var dinoNo=0;
                var dinotxt = "";
                var weighttxt = "";
                
                    for(var i=0;i<inputs.length;i++)
                    {
                        if(inputs[i].type =="checkbox")
                        {
                          inputs[i].checked = true;
                          
                                
                        }
                    }
              }
        }
        
        function fncheqBox(txtObj)
        {
            var textBoxid=txtObj.id;
            var checkBoxID=textBoxid.replace("certNoTextBox","leftCheckBox");
                                                         
              var inputs = document.getElementById("<%=dinoGridView.ClientID%>").getElementsByTagName("input");
                
               
                
                    for(var i=0;i<inputs.length;i++)
                    {
                        if(inputs[i].id ==checkBoxID)
                        {
                          inputs[i].checked = true;
                          fnSelectedTotalUnit();
                                
                        }
                    }
              
        }
        
        
       function fncheqBoxStatus(txtObj)
        {
            var textBoxid=txtObj.id;
            var checkBoxID=textBoxid.replace("statusTextBox","leftCheckBox");
               
            
        
           
           
              var inputs = document.getElementById("<%=dinoGridView.ClientID%>").getElementsByTagName("input");
                
               
                
                    for(var i=0;i<inputs.length;i++)
                    {
                        if(inputs[i].id ==checkBoxID)
                        {
                          inputs[i].checked = true;
                          fnSelectedTotalUnit();
                                
                        }
                    }
              
        }
        
        function fnConfirmDelete()
        {
            var isConfirm=confirm("Are you sure to DELETE this sale record");
            if(isConfirm)
            {
                return true;
             }
             else
             {
                return false;
             }
        }
</script>
 <style type="text/css">
      .style6
        {
            font-size: smaller;
            font-weight: bold;
            color: #009933;
        }
      .style11
     {
         height: 22px;
     }
     .style18
     {
         width: 153px;
     }
     </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />                
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           Unit Holder Sale Edit Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
      <br />
    
    <br />
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="1100">
        <colgroup width="120">
        </colgroup>
        <colgroup width="320">
        </colgroup>
        <colgroup width="160">
        </colgroup>
        <colgroup width="160">
        </colgroup>
        <tr>
            <td align="right">
                <b>Sale No:</b></td>
            <td align="left">
                <asp:TextBox ID="saleNumberTextBox" runat="server" 
                    CssClass="textInputStyleDate" MaxLength="6" 
                    meta:resourcekey="saleNumberTextBoxResource1" 
                    onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                <span class="star">*<span class="star" __designer:mapid="6ac"><asp:Button ID="findButton0" 
                    runat="server" AccessKey="f" CssClass="buttonmid" 
                    meta:resourcekey="findButtonResource1" onclick="findButton_Click" 
                    onclientclick="return fnFindChek();" TabIndex="2" Text="Find" />
                </span></span>
            </td>
            <td align="center" colspan="2">
                <span class="style6" style="border:1px solid">Signature and Photo</span></td>
        </tr>
        <tr>
            <td align="right">
                Money Receipt No:</td>
            <td align="left">
                <asp:TextBox ID="MoneyReceiptNoTextBox" runat="server" 
                    CssClass="textInputStyleDate" meta:resourcekey="saleNumberTextBoxResource1" 
                    onkeypress="fncInputNumericValuesOnly()" Enabled="False"></asp:TextBox>
            </td>
            <td align="center" colspan="2" rowspan="10">
                <asp:Image ID="SignImage" runat="server" Height="202px" 
                    meta:resourcekey="SignImageResource1" Width="286px" />
            </td>
        </tr>
        <tr>
            <td align="right">
                Registration No:</td>
            <td align="left">
                <asp:TextBox ID="fundCodeTextBox" runat="server" CssClass="TextInputStyleSmall" 
                    Enabled="False" meta:resourcekey="fundCodeTextBoxResource1"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="branchCodeTextBox" runat="server" 
                    CssClass="TextInputStyleSmall" Enabled="False" 
                    meta:resourcekey="branchCodeTextBoxResource1"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="regNoTextBox" runat="server"
                    CssClass="TextInputStyleSmall" MaxLength="8" 
                    meta:resourcekey="regNoTextBoxResource1" 
                                  Width="95px"></asp:TextBox>
                <span class="star">*</span>&nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                Payment Type:</td>
            <td align="left">
                <table>
                    <tr>
                        <td align="center">
                            <asp:RadioButton ID="ChqRadioButton" runat="server" AutoPostBack="True" 
                                Checked="True" GroupName="payType" 
                                oncheckedchanged="ChqRadioButton_CheckedChanged" TabIndex="2" Text="CHQ" />
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ChequeTypeDropDownList" runat="server" 
                                CssClass="DropDownList" Height="27px" 
                                meta:resourcekey="saleTypeDropDownListResource1">
                                <asp:ListItem Selected="True" Value="CHQ">CHQ</asp:ListItem>
                                <asp:ListItem Value="DD">DD</asp:ListItem>
                                <asp:ListItem Value="PO">PO</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:RadioButton ID="CashRadioButton" runat="server" AutoPostBack="True" 
                                GroupName="payType" oncheckedchanged="CashRadioButton_CheckedChanged" 
                                TabIndex="3" Text="CASH" />
                        </td>
                        <td align="center">
                            <asp:RadioButton ID="BothRadioButton" runat="server" AutoPostBack="True" 
                                GroupName="payType" oncheckedchanged="BothRadioButton_CheckedChanged" 
                                TabIndex="4" Text="BOTH" />
                        </td>
                        <td align="center">
                            <asp:RadioButton ID="MultiRadioButton" runat="server" AutoPostBack="True" 
                                GroupName="payType" oncheckedchanged="MultiRadioButton_CheckedChanged" 
                                TabIndex="5" Text="MULTIPLE" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                Cheque/DD/PO No:</td>
            <td align="left">
                <table>
                    <tr>
                        <td class="style18">
                            <asp:TextBox ID="CHQDDNoRemarksTextBox" runat="server" 
                                CssClass="TextInputStyleLarge" MaxLength="55" 
                                meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="6" Width="148px"></asp:TextBox>
                        </td>
                        <td>
                            Date:
                        </td>
                        <td align="left" class="style11">
                            <asp:TextBox ID="chequeDateTextBox" runat="server" 
                                CssClass="textInputStyleDate" meta:resourcekey="saleDateTextBoxResource1" 
                                TabIndex="6"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="chequeDateImageButton" 
                                TargetControlID="chequeDateTextBox" />
                            <asp:ImageButton ID="chequeDateImageButton" runat="server" 
                                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                                TabIndex="10" />
                            <span class="star">* </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                Bank Name:</td>
            <td align="left">
                <asp:DropDownList ID="bankNameDropDownList" runat="server" AutoPostBack="True" 
                    meta:resourcekey="bankNameDropDownListResource1" 
                    onselectedindexchanged="bankNameDropDownList_SelectedIndexChanged" TabIndex="7">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Branch Name:</td>
            <td align="left">
                <asp:DropDownList ID="branchNameDropDownList" runat="server" 
                    meta:resourcekey="branchNameDropDownListResource1" TabIndex="8">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Cash Amount:</td>
            <td align="left">
                <asp:TextBox ID="CashAmountTextBox" runat="server" 
                    CssClass="TextInputStyleLarge" MaxLength="55" 
                    meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="9"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Multiple Payment:</td>
            <td align="left">
                <asp:TextBox ID="MultiplePayTypeTextBox" runat="server" 
                    CssClass="TextInputStyleLarge" MaxLength="256" 
                    meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="10" Width="280px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Sale Date:</td>
            <td align="left">
                <asp:TextBox ID="saleDateTextBox" runat="server" CssClass="textInputStyleDate" 
                    meta:resourcekey="saleDateTextBoxResource1" TabIndex="11"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                    Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton" 
                    TargetControlID="saleDateTextBox" />
                <asp:ImageButton ID="RegDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                    meta:resourcekey="RegDateImageButtonResource1" TabIndex="10" />
                <span class="star">* </span>
            </td>
        </tr>
        <tr>
            <td align="right">
                Sale Type:</td>
            <td align="left">
                <asp:DropDownList ID="saleTypeDropDownList" runat="server" 
                    CssClass="DropDownList" meta:resourcekey="saleTypeDropDownListResource1" 
                    TabIndex="12">
                    <asp:ListItem meta:resourcekey="ListItemResource1" Selected="True" Value="SL">SALE</asp:ListItem>
                    <asp:ListItem meta:resourcekey="ListItemResource2" Value="CIP">CIP</asp:ListItem>
                </asp:DropDownList>
                <span class="star">*</span></td>
        </tr>
        <tr>
            <td align="right">
                Sale Rate:</td>
            <td style="text-align: left">
                <asp:TextBox ID="saleRateTextBox" runat="server" CssClass="textInputStyleDate" 
                    MaxLength="6" meta:resourcekey="saleRateTextBoxResource1" TabIndex="13"></asp:TextBox>
                <span class="star">*</span></td>
            <td style="text-align: right">
                <span class="style20"><b style="text-align: right">CIP :&nbsp; </b></span>
            </td>
            <td id="tdCIP" runat="server" style="text-align: left; color:Red">
            </td>
        </tr>
        <tr>
            <td align="right">
                Unit Quantity:</td>
            <td style="text-align: left">
                <asp:TextBox ID="unitQtyTextBox" runat="server" CssClass="textInputStyleDate" 
                    MaxLength="10" meta:resourcekey="unitQtyTextBoxResource1" 
                    onkeypress="fncInputNumericValuesOnly()" TabIndex="14"></asp:TextBox>
                <span class="star">*</span></td>
            <td align="right">
                Name of Holder :</td>
            <td align="left">
                <asp:TextBox ID="holderNameTextBox" runat="server" 
                    CssClass="TextInputStyleLarge" MaxLength="55" 
                    meta:resourcekey="holderNameTextBoxResource1" TabIndex="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Remarks:</td>
            <td style="text-align: left">
                <asp:TextBox ID="saleRemarksTextBox" runat="server" 
                    CssClass="TextInputStyleLarge" MaxLength="55" 
                    meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="15" Width="278px"></asp:TextBox>
            </td>
            <td align="right">
                Name of Joint Holder :</td>
            <td align="left">
                <asp:TextBox ID="jHolderTextBox" runat="server" CssClass="TextInputStyleLarge" 
                    MaxLength="55" meta:resourcekey="jHolderTextBoxResource1" TabIndex="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td align="right">
                Address1 :</td>
            <td align="left">
                <asp:TextBox ID="holderAddress1TextBox" runat="server" 
                    CssClass="TextInputStyleLarge" MaxLength="55" 
                    meta:resourcekey="jHolderTextBoxResource1" TabIndex="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Address2 :</td>
            <td align="left">
                <asp:TextBox ID="holderAddress2TextBox" runat="server" 
                    CssClass="TextInputStyleLarge" MaxLength="55" 
                    meta:resourcekey="jHolderTextBoxResource1" TabIndex="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td align="right">
                Telephone/Mobile :</td>
            <td align="left">
                <asp:TextBox ID="holderTelphoneTextBox" runat="server" 
                    CssClass="TextInputStyleLarge" MaxLength="55" 
                    meta:resourcekey="jHolderTextBoxResource1" TabIndex="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" rowspan="10">
                <div style="text-align: center; display: block; overflow: auto; width:302; height:150px;">
                <asp:DataGrid ID="dinoGridView" runat="server" AutoGenerateColumns="False" 
                    meta:resourcekey="dinoGridViewResource1"  >
                <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                <ItemStyle CssClass="TableText"></ItemStyle>
                <HeaderStyle CssClass="TableHeader2"></HeaderStyle>
                <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                <Columns>
                
                <asp:TemplateColumn>
                 <HeaderTemplate>
                                    <input id="chkAllWarrant" type="checkbox" 
                                        onclick="CheckAllDataGridWarrantNo(this.checked)"> 
                   </HeaderTemplate>
                    <ItemTemplate> 
                         <asp:CheckBox ID="leftCheckBox" runat="server" 
                             onclick="fnSelectedTotalUnit();" ></asp:CheckBox> 
                    </ItemTemplate>                    
                    </asp:TemplateColumn>       
                 <asp:TemplateColumn HeaderText="Denomination" >
                        <ItemTemplate>
                            <asp:TextBox ID="dinoTextBox" runat="server"  Enabled="false"
                                Text='<%# DataBinder.Eval(Container.DataItem,"dino") %>' Width="50px" 
                                onBlur="Javascript:fnValidateWeight(this);" 
                                meta:resourcekey="dinoTextBoxResource1"></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Font-Bold="False" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderText="Cert.No" >
                        <ItemTemplate>
                            <asp:TextBox ID="certNoTextBox" runat="server"  
                                Text='<%# DataBinder.Eval(Container.DataItem,"cert_no") %>' Width="80px" 
                                onkeypress= "fncInputNumericValuesOnly()"   onBlur="Javascript:fncheqBox(this);"
                                meta:resourcekey="certNoTextBoxResource1"></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"  Font-Bold="True" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderText="Weight"  >
                        <ItemTemplate>
                            <asp:TextBox ID="weightTextBox" runat="server"  Enabled="false"
                                Text='<%# DataBinder.Eval(Container.DataItem,"cert_weight") %>' 
                                Width="70px" onkeypress= "fncInputNumericValuesOnly()"  
                                onBlur="Javascript:fnValidateDino(this);" 
                                meta:resourcekey="weightTextBoxResource1"></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Font-Bold="True" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>                    
                 <asp:TemplateColumn HeaderText="Status"  >
                        <ItemTemplate>
                            <asp:TextBox ID="statusTextBox" runat="server"   Enabled="false"
                                Text='<%# DataBinder.Eval(Container.DataItem,"status_flag") %>' 
                                Width="20px" onBlur="Javascript:fncheqBoxStatus(this);" ></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Font-Bold="True" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderText="RowID"  Visible="false" >
                        <ItemTemplate>
                            <asp:TextBox ID="RowIDTextBox" runat="server"  
                                Text='<%# DataBinder.Eval(Container.DataItem,"ROWID") %>'  Visible="false" 
                                Enabled="false" ></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Font-Bold="True" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                  
                  
                </Columns>
            </asp:DataGrid>
                    <br />
                </div>
            </td>
            <td align="right">
            </td>
            <td align="left">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table align="center" cellpadding="0" cellspacing="0" style="width: 1243px">
     <tr>
        <td align="right">
        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                onclick="SaveButton_Click" AccessKey="s" 
                meta:resourcekey="SaveButtonResource1"/>&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="a" 
                meta:resourcekey="ResetButtonResource2" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="DeleteButton" runat="server" Text="Delete" OnClientClick="return fnConfirmDelete();"
                CssClass="buttoncommon"  AccessKey="" 
                meta:resourcekey="CloseButtonResource1" onclick="DeleteButton_Click" 
                  />
                &nbsp;&nbsp;&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" meta:resourcekey="CloseButtonResource1" 
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

