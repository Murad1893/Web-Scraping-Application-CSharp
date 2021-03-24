<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="k173722_Q4.Default" %>

<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 
    <!-- Bootstrap -->
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <style>
        body {
            width: 100%;
            margin: 5px;
        }

        .table-condensed tr th {
            border: 0px solid #6e7bd9;
            color: white;
            background-color: #6e7bd9;
        }

        .table-condensed, .table-condensed tr td {
            border: 0px solid #000;
        }

        tr:nth-child(even) {
            background: #f8f7ff
        }

        .GridPager a,
        .GridPager span {
            display: inline-block;
            padding: 0px 9px;
            margin-right: 4px;
            border-radius: 3px;
            border: solid 1px #c0c0c0;
            background: #e9e9e9;
            box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
            font-size: .875em;
            font-weight: bold;
            text-decoration: none;
            color: #717171;
            text-shadow: 0px 1px 0px rgba(255,255,255, 1);
        }

        .GridPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }

        .GridPager span {

            background: #6e7bd9;
            box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
            color: #f0f0f0;
            text-shadow: 0px 0px 3px rgba(0,0,0, .5);
            border: 1px solid #6e7bd9;
        }

    </style>

    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container">
            <div class="jumbotron text-center" style="background: #6e7bd9; color: azure">
                <h1>Pakistan Stock Exchange Market Summary</h1>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" class="row">
                  <ContentTemplate>
               
                <div class="col col-10">
                            <asp:GridView ID="GridView1" runat="server" UseAccessibleHeader="true" CssClass="table table-condensed" Width="100%"
                                AllowPaging="True" PagerStyle-CssClass="GridPager" 
                                OnPageIndexChanging="GridView_PageIndexChanging">
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center"></PagerStyle>
                            </asp:GridView>
                </div>
                <div class="col col-2">
                    <div class="row" style="justify-content: center">
                         <asp:Button ID="btnConfirm" CssClass="btn btn-primary" style="background: #6e7bd9;"
                                            runat="server" Text="Refresh" OnClick="btnConfirm_Click" 
                                            />
                    </div>
                    <br />
                    <div class="row" style="justify-content: center">
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="btn btn-secondary dropdown-toggle" style="background: #6e7bd9;" Width="100%" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"/>
                    </div>
                </div>
                 </ContentTemplate>
             </asp:UpdatePanel>
        </div>
    </form>
 
    <!-- jQuery and Bootstrap JS files -->
    <script src="bootstrap/js/jquery-3.3.1.min.js"></script>
    <script src="bootstrap/js/popper.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script> 

</body>
 
</html>
