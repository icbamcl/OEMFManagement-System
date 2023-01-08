<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitSaleEdit111.aspx.cs" Inherits="UI_UnitSaleEdit" Title=" Unit Sale Edit Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
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
     .style7
     {
         font-size: 12px;
         font-weight: bold;
         color:Red; 
     }
      .style8
     {
     	border:1px solid #006633;
     
     }
      .style9
     {
     	border-right:1px solid #006633;
     	border-left:1px solid #006633;
     	
     }
      .style10
     {
     	border-right:1px solid #006633;
     	border-left:1px solid #006633;
     	border-bottom:1px solid #006633;
     	
     }
     .style11
     {
         height: 22px;
     }
     .style12
     {
         border-right: 1px solid #006633;
         border-left: 1px solid #006633;
         height: 22px;
     }
     .style14
     {
         color: #FF0000;
         font-size: small;
     }
     .style15
     {
         height: 18px;
     }
     .style16
     {
         border-right: 1px solid #006633;
         border-left: 1px solid #006633;
         height: 18px;
     }
     .style17
     {
         height: 20px;
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
      <div id="dvContent" runat="server" 
        
        style="width:1228px; padding-left:80px; padding-right:100px; height: 499px;" >
      <asp:UpdatePanel ID="HolderInfoUpdatePanel" runat="server">
      <ContentTemplate>
          <div style=" float:left" >
          <table width="600" align="center" cellpadding="0" cellspacing="0" border="0" >
    <colgroup width="120"></colgroup>
    <colgroup width="320"></colgroup>
        <tr>
           <td align="left" class="style17" ><b style="text-align: right">Sale No:</b></td>
            <td align="left" colspan="2" class="style17">
               <asp:TextBox ID="saleNumberTextBox" runat="server"  MaxLength="6"
                    CssClass= "textInputStyleDate" 
                    meta:resourcekey="saleNumberTextBoxResource1" 
                    onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
                                    <span class="star">*<asp:Button ID="findButton" 
                    runat="server" AccessKey="f" CssClass="buttonmid" 
                    meta:resourcekey="findButtonResource1" onclick="findButton_Click" 
                    onclientclick="return fnFindChek();" TabIndex="2" Text="Find" />
                </span></td>
           
        </tr>
        <tr>
           <td align="left" >Money Receipt No:</td>
            <td align="left" colspan="2">
               <asp:TextBox ID="MoneyReceiptNoTextBox" runat="server" 
                    CssClass= "textInputStyleDate" 
                    meta:resourcekey="saleNumberTextBoxResource1" 
                    onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
                                    </td>
           
        </tr>
        <tr>
            <td align="left" >Registration No:</td>
            <td align="left">
                <asp:TextBox ID="fundCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" 
                    meta:resourcekey="fundCodeTextBoxResource1"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="branchCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" 
                    meta:resourcekey="branchCodeTextBoxResource1"></asp:TextBox>
                &nbsp;
                                    <asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="8"   
                    CssClass= "TextInputStyleSmall" TabIndex="1" 
                     onkeypress= "fncInputNumericValuesOnly()"
                    meta:resourcekey="regNoTextBoxResource1" Width="95px" ReadOnly="True"></asp:TextBox>
                                    <span class="star">*</span>&nbsp;</td>
            
            <%--<td align="center" class="style8">
                Denomination</td>--%> 
           
        </tr>
         
        <tr>
            <td align="left" >Payment Type:</td>
            <td align="left">
            <table>
            <tr>
                <td  align="center">
                     <asp:RadioButton ID="ChqRadioButton" runat="server" AutoPostBack="True" 
                    Checked="True" GroupName="payType" Text="CHQ" 
                    oncheckedchanged="ChqRadioButton_CheckedChanged" TabIndex="2" />
                </td>
                 <td  align="center">
                 <asp:DropDownList ID="ChequeTypeDropDownList" runat="server" 
                    CssClass="DropDownList" 
                    meta:resourcekey="saleTypeDropDownListResource1" Height="27px">
                <asp:ListItem Value="CHQ" Selected="True">CHQ</asp:ListItem>
                <asp:ListItem Value="DD">DD</asp:ListItem>
                <asp:ListItem Value="PO">PO</asp:ListItem>
                </asp:DropDownList>
                </td>
                 <td  align="center">
                   <asp:RadioButton ID="CashRadioButton" runat="server" AutoPostBack="True" 
                    GroupName="payType" Text="CASH" 
                    oncheckedchanged="CashRadioButton_CheckedChanged" TabIndex="3" />
                </td>
                 <td  align="center">
                  <asp:RadioButton ID="BothRadioButton" runat="server" AutoPostBack="True" 
                    GroupName="payType" Text="BOTH" 
                    oncheckedchanged="BothRadioButton_CheckedChanged" TabIndex="4" />
                </td>
                 <td  align="center">
                 <asp:RadioButton ID="MultiRadioButton" runat="server" Text="MULTIPLE" 
                    GroupName="payType" AutoPostBack="True" 
                    oncheckedchanged="MultiRadioButton_CheckedChanged" TabIndex="5" />
                </td>
            </tr>
            </table>
                                                                                             
            </td>
            <%--<td align="center" class="style9"> 
                   <b>A:1x</b>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; 
                   <asp:TextBox ID="aTextBox" runat="server" 
                    CssClass= "TextInputStyleDenomination" meta:resourcekey="aTextBoxResource1" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>                     
                </td>--%>
               
           
           
        </tr>
        <tr>
            <td align="left" >Cheque/DD/PO No:</td>
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
                <asp:TextBox ID="chequeDateTextBox" runat="server" CssClass="textInputStyleDate" 
                    TabIndex="6" meta:resourcekey="saleDateTextBoxResource1"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                    TargetControlID="chequeDateTextBox" PopupButtonID="chequeDateImageButton" 
                    Format="dd-MMM-yyyy" Enabled="True"/>
                <asp:ImageButton ID="chequeDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                    TabIndex="10" />
                <span class="star">* </span></td>
            </tr>
            </table>
                
            </td>
            <%--<td align="center" class="style9"> 
                   <b>B:5x&nbsp;</b>&nbsp; &nbsp;&nbsp; &nbsp;<asp:TextBox ID="bTextBox" runat="server" 
                    CssClass= "TextInputStyleDenomination" meta:resourcekey="bTextBoxResource1" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>                     
                </td>--%>
            
            
        </tr>
        <tr>
           <td align="left" >Bank Name:</td>
            <td align="left">
                <asp:DropDownList ID="bankNameDropDownList" runat="server" AutoPostBack="True" meta:resourcekey="bankNameDropDownListResource1" 
                    onselectedindexchanged="bankNameDropDownList_SelectedIndexChanged" 
                    TabIndex="7">
                </asp:DropDownList>
            </td>
           <%--<td align="center" class="style9"> 
                   <b>C:10x&nbsp;</b>&nbsp;&nbsp; &nbsp;<asp:TextBox ID="cTextBox" runat="server" 
                    CssClass= "TextInputStyleDenomination" meta:resourcekey="cTextBoxResource1" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>                     
                </td>--%>
            </tr>
        <tr>
            <td align="left" class="style15" >Branch Name:</td>
            <td align="left" class="style15" >
                <asp:DropDownList ID="branchNameDropDownList" runat="server" 
                    meta:resourcekey="branchNameDropDownListResource1" TabIndex="8">
                </asp:DropDownList>
            </td>
            <%-- <td align="center" class="style16"> 
                   <b>D:20x</b>&nbsp;&nbsp;&nbsp;&nbsp; 
                   <asp:TextBox ID="dTextBox" runat="server" 
                    CssClass= "TextInputStyleDenomination" meta:resourcekey="dTextBoxResource1" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>                     
                </td>
            --%>
        </tr>   
        <tr>
                  <td align="left">Cash Amount:</td>
                  <td align="left">
                      <asp:TextBox ID="CashAmountTextBox" runat="server" 
                          CssClass="TextInputStyleLarge" MaxLength="55" 
                          meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="9"></asp:TextBox>
                  </td>
           <%-- <td align="center" class="style9"> 
                   <b>E:50x</b>&nbsp;&nbsp;&nbsp; &nbsp; 
                   <asp:TextBox ID="eTextBox" runat="server" 
                    CssClass= "TextInputStyleDenomination" meta:resourcekey="eTextBoxResource2" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>                     
                </td>--%>
         </tr>
          <tr>
            <td align="left">Multiple Payment:</td>
            <td align="left"  runat="server" class="style7" >
                <asp:TextBox ID="MultiplePayTypeTextBox" runat="server" 
                    CssClass="TextInputStyleLarge" MaxLength="256" 
                    meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="10" Width="280px"></asp:TextBox>
            </td>
                 <%--  <td align="center" class="style9"> 
                     <b>F:100x</b>&nbsp;&nbsp;&nbsp; 
                     <asp:TextBox ID="fTextBox" runat="server" 
                    CssClass= "TextInputStyleDenomination" meta:resourcekey="fTextBoxResource1" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>                     
                </td>--%>
        </tr>     
       
          <tr>
            <td align="left" class="style11" >Sale Date:</td>
            <td align="left" class="style11">
                <asp:TextBox ID="saleDateTextBox" runat="server" CssClass="textInputStyleDate" 
                    TabIndex="11" meta:resourcekey="saleDateTextBoxResource1"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                    TargetControlID="saleDateTextBox" PopupButtonID="RegDateImageButton" 
                    Format="dd-MMM-yyyy" Enabled="True"/>
                <asp:ImageButton ID="RegDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                    TabIndex="10" meta:resourcekey="RegDateImageButtonResource1" />
                <span class="star">* </span></td>
          <%-- <td align="center" class="style9"> 
                   <b>G:250x </b>&nbsp; &nbsp;<asp:TextBox ID="gTextBox" runat="server" 
                    CssClass= "TextInputStyleDenomination" meta:resourcekey="gTextBoxResource1" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>                     
                </td>--%>
       
           
        </tr>
        <tr>
            <td align="left" class="style11" >Sale Type:</td>
            <td align="left" >
             <asp:DropDownList ID="saleTypeDropDownList" runat="server" 
                    CssClass="DropDownList" TabIndex="12" 
                    meta:resourcekey="saleTypeDropDownListResource1">
                <asp:ListItem Value="SL" Selected="True" meta:resourcekey="ListItemResource1">SALE</asp:ListItem>
                <asp:ListItem Value="CIP" meta:resourcekey="ListItemResource2">CIP</asp:ListItem>
                </asp:DropDownList><span class="star">*</span>
               </td>
                <%--<td align="center" class="style12"> 
                    <b>H:500x</b>&nbsp;&nbsp; &nbsp;<asp:TextBox ID="hTextBox" runat="server" 
                    CssClass= "TextInputStyleDenomination" meta:resourcekey="hTextBoxResource1" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>                     
                </td>--%>
        </tr>
        <tr>
            <td align="left" >Sale Rate:</td>
            <td align="left">
               <asp:TextBox ID="saleRateTextBox" runat="server"  MaxLength="6"
                    CssClass= "textInputStyleDate" TabIndex="13" 
                    meta:resourcekey="saleRateTextBoxResource1" ></asp:TextBox>
                                    <span class="star">*</span></td>
              <%--<td align="center" class="style9"> 
                   <b>I:1000x&nbsp; </b>&nbsp;<asp:TextBox ID="iTextBox" runat="server" 
                    CssClass= "TextInputStyleDenomination" meta:resourcekey="iTextBoxResource1" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>                     
                </td>--%>
            
                    
        </tr>
              <tr>
            <td align="left" >Unit Quantity:</td>
            <td align="left">
               <asp:TextBox ID="unitQtyTextBox" runat="server"  MaxLength="10"
                    CssClass= "textInputStyleDate" TabIndex="14" 
                    meta:resourcekey="unitQtyTextBoxResource1" 
                    onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
                                    <span class="star">*</span></td>
          <%--  <td align="center" class="style10"> 
                  <b>J:5000x&nbsp; </b>&nbsp;<asp:TextBox ID="jTextBox" runat="server" 
                    CssClass= "TextInputStyleDenomination" meta:resourcekey="jTextBoxResource1" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>                     
                </td>--%>
       
           
        </tr>
              <tr>
            <td align="left" >Remarks:</td>
            <td align="left">
                 <asp:TextBox ID="saleRemarksTextBox" runat="server" 
                     CssClass="TextInputStyleLarge" MaxLength="55" 
                     meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="9"></asp:TextBox>
                  </td>
             <%-- <td align="center" class="style10"> 
                  <b>K:10000x</b>&nbsp;<asp:TextBox ID="kTextBox" runat="server" 
                    CssClass= "TextInputStyleDenomination" meta:resourcekey="jTextBoxResource1" 
                      onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>                     
                </td>--%>
       
           
        </tr> 
        <tr>
                  <td align="left" >&nbsp;</td>
            <td align="left">
                &nbsp;</td>
                  <%--<td align="center"> 
                <asp:Button ID="DinoResetButton" runat="server" Text="Reset" 
                CssClass="buttonmid"  OnClientClick="return fnDinoReset();" AccessKey="r" 
                    meta:resourcekey="DinoResetButtonResource1" /></td>--%>
        </tr>   
         <%--<tr>
            <td align="center" colspan="3" >
                <asp:Button ID="addListButton" runat="server" 
                    Text="Add to List" CssClass="buttoncommon" onclick="addListButton_Click" 
                    OnClientClick="return fnCheqQty();" AccessKey="l" 
                    meta:resourcekey="addListButtonResource1" TabIndex="16"/></td>--%>
           
        </tr> 
        <tr>
            <td colspan="3">&nbsp;</td>
        </tr>  
         <tr>
            <td colspan="3"> 
            <div style="text-align: center; display: block; overflow: auto; width:577px; height:173px;">
                <asp:DataGrid ID="dinoGridView" runat="server" AutoGenerateColumns="False" 
                    meta:resourcekey="dinoGridViewResource1"  >
                <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                <ItemStyle CssClass="TableText"></ItemStyle>
                <HeaderStyle CssClass="TableHeader2"></HeaderStyle>
                <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                <Columns>
                
                <asp:TemplateColumn>
                 <HeaderTemplate>
                                    <input id="chkAllWarrant" type="checkbox" onclick="CheckAllDataGridWarrantNo(this.checked)"> 
                   </HeaderTemplate>
                    <ItemTemplate> 
                         <asp:CheckBox ID="leftCheckBox" runat="server" onclick="fnSelectedTotalUnit();" ></asp:CheckBox> 
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
                                Text='<%# DataBinder.Eval(Container.DataItem,"cert_no") %>' Width="80px" onkeypress= "fncInputNumericValuesOnly()"   onBlur="Javascript:fncheqBox(this);"
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
                                Text='<%# DataBinder.Eval(Container.DataItem,"cert_weight") %>' Width="70px" onkeypress= "fncInputNumericValuesOnly()"  
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
                                Text='<%# DataBinder.Eval(Container.DataItem,"status_flag") %>' Width="20px" onBlur="Javascript:fncheqBoxStatus(this);" ></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Font-Bold="True" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderText="RowID"  Visible="false" >
                        <ItemTemplate>
                            <asp:TextBox ID="RowIDTextBox" runat="server"  
                                Text='<%# DataBinder.Eval(Container.DataItem,"ROWID") %>'  Visible="false" Enabled="false" ></asp:TextBox>&nbsp;
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
        </tr>     
            
        </table>
       
        </div>
        <div>
           <div style="border-style: solid; border-color: inherit; border-width: 1px; width:222px; float:none; height:187px;">
                <table align="left" width="164"  cellpadding="0" cellspacing="0" 
                    style="height: 180px">
                <tr style=" height:15px;">
                   <td align="center" ><span class="style6"  style="border:1px solid">Signature    and 
                       Photo    </span></td></tr>
                   <tr style=" height:102px;">
                    <td align="left">
                  <asp:Image ID="SignImage" runat="server" Height="170px" Width="217px" 
                            meta:resourcekey="SignImageResource1"  />
                    </td>    
                </tr>
                
               
                </table>
                
           </div>
           <table align="left" cellpadding="0" cellspacing="0" style="width: 448px">
               <colgroup width="160">
               </colgroup>
               <tr>
               <td colspan="2">
                &nbsp;
               </td>
               </tr>
           <tr>
             <td align="right" >Name of Holder :</td>
            <td align="left">
                <asp:TextBox ID="holderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="3" 
                    meta:resourcekey="holderNameTextBoxResource1"></asp:TextBox>
            </td>
           </tr>
           <tr>
               <td align="right" >Name of Joint Holder :</td>
                <td align="left">
                <asp:TextBox ID="jHolderTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                    meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
            </td>
           </tr>
            <tr>
               <td align="right" >Address1 :</td>
                <td align="left">
                <asp:TextBox ID="holderAddress1TextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                    meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
            </td>
           </tr>
            <tr>
               <td align="right" >Address2 :</td>
                <td align="left">
                <asp:TextBox ID="holderAddress2TextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                    meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
            </td>
           </tr>
            <tr>
               <td align="right" >Telephone/Mobile :</td>
                <td align="left">
                <asp:TextBox ID="holderTelphoneTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                    meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
            </td>
           </tr>
            <tr>
               <td align="right" ><span class="style14"><b>CIP :&nbsp; </b></span></td>
                <td align="left" id="tdCIP" runat="server" class="style7">
                    &nbsp;</td>
           </tr>
           </table>
        </div>
      </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="regNoTextBox" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="findButton" EventName="Click" />
            
        </Triggers>
        </asp:UpdatePanel>
    </div>
    <br />
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

