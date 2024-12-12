<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SistemaFidelidadPaws._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<!DOCTYPE html>
    <html lang="es">
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Carrusel Bootstrap</title>
            <!-- Bootstrap CSS -->
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
        </head>
        <body>
            <div class="container mt-5">
                <div id="introduccion">
                    <u style="color:#ff6a00">
                    <h1 id="titulo" class="text-center mb-4">¡Bienvenido a nuestro sistema de fidelidad para nuestros <strong> mejores clientes! </strong></h1>
                    </u>
                    <h4 id=subtitulo" style="color: #ff6a00">Por cada compra realizada obtendrás puntos que podrás cambiar por productos</h4>
                </div>
                <asp:Repeater ID="rptProductos" runat="server">
                    <HeaderTemplate>
                        <div class="row">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="col-md-3">
                            <div class="card mb-4">
                                <!-- Imagen del producto -->
                                <img src='<%# Eval("ImagenUrl") %>' class="card-img-top" alt="Imagen del producto">
                                <div class="card-body text-center">
                                    <!-- Nombre del producto -->
                                    <h5 id="nombreProducto" class="card-title"><%# Eval("Nombre") %></h5>
                                    <!-- Precio del producto -->
                                    <p id="precioProducto" class="card-text">Precio: $<%# Eval("Precio") %></p>
                                    <!-- Puntos del producto -->
                                    <p id="puntosProducto" class="card-text">Puntos: <%# Eval("Puntos") %></p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>

            </div>

            <!-- Bootstrap JS Bundle -->
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

            <style>
                #titulo
                {
                    font-family: Calibri;
                }

                #subtitulo
                {
                    font-family: 'Trebuchet MS';

                }

                #nombreProducto
                {
                    font-family:'Calibri';
                    color:#ff6a00;
                }

                #precioProducto
                {
                    font-family: 'Malgun Gothic';
                    color:#ff6a00;
                    margin-top:16%;
                }

                #puntosProducto
                {
                    font-family: 'Malgun Gothic';
                    color:#ff6a00;
                    margin-top:-5%;
                }
            </style>
        </body>
    </html>

</asp:Content>
