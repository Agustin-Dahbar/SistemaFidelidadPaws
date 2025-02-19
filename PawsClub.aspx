﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PawsClub.aspx.cs" Inherits="SistemaFidelidadPaws._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<!DOCTYPE html>
    <html lang="es">
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Paws Club</title>
            <!-- Bootstrap CSS -->
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
            <link rel="stylesheet" href="MediaQuerys/PawsClub.css" />
        </head>
        <body>
            <!--Div container, padre de todos los elementos del body.-->
            <div class="container mt-5"> <!--style="width:100vw; height:100vh; display:flex; position:relative--> <!--Esto fue un error, ya está el de la master page-->
                <!--Introducción del sistema-->
                <div id="introduccion">
                   <div>
                        <!--Título-->
                        <h1 id="titulo" class="text-start mb-4">¡Bienvenido a  <strong>Paws Club</strong>, nuestro sistema de fidelidad para nuestros <strong> mejores clientes! </strong>
                        </h1>
                        <!--Subtítulo-->
                        <h4 id="subtitulo" class="text-end">Por cada compra realizada obtendrás puntos que podrás cambiar por productos.
                        </h4>
                    </div>
                </div>

                <!--TARJETA USUARIO-->
                <div>
                    <!--Tarjeta de usuario con su nombre y puntos.-->
                    <div id="datosUsuario" class="card text-center">
                        <div class="card-body">
                            <h4 runat="server" id="bienvenidaUsuario" class="card-title bienvenidaUsuario" style="color: #ff6a00; font-family: Calibri; font-weight: 800">
                                <!-- ¡Bienvenido, <%# Eval("nombreUsuario") %>! No es necesario ya que lo asignamos en el backend-->
                            </h4>
                            <p runat="server" id="puntosUsuario" class="card-text puntosUsuario" style="color: #ff6a00; font-family: Calibri; font-weight: 800">
                                <!--Asignado en backend. Tus puntos: <%# Eval("puntos") %>-->
                            </p>
                        </div>
                    </div>

                    
                    <!--INVENTARIO-->

                    <!-- Botón estilizado con un ícono -->
                    <asp:Button ID="btnVerProductos" class="btnVerProductos" runat="server" Text="Ver productos adquiridos" OnClick="btnVerProductos_Click"/>

                    <!-- Modal para mostrar los productos adquiridos -->
                    <div class="modal fade" id="productosModal" tabindex="-1" role="dialog" aria-labelledby="productosModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document" style="border:5px solid #ff6a00">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="productosModalLabel">Productos adquiridos</h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                        <!--<span aria-hidden="true">&times;</span>-->
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <!-- Aquí se mostrará la lista de productos -->
                                    <asp:Repeater ID="rptProductosAdquiridos" runat="server">
                                        <ItemTemplate>
                                            <div>
                                                <p style="color:#ff6a00;"><strong><%# Eval("Nombre") %></strong></p>
                                                <p style="color:#ff6a00;margin-left:74%;margin-bottom:6%;">x<%#Eval("Cantidad") %></p>
                                                <img src="<%# Eval("imagenUrl") %>" style="height:20vh;margin-left:40%;margin-top:-30%;" ></img>
                                            </div>
                                            <hr />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--FIN MODAL-->



                <!--Buscador de productos-->
                    <input id="txtBuscarProducto" placeholder="Buscar producto..." />
                    <button runat="server" id="button" type="submit" OnClick="btnBuscarProducto_OnClick" class="btn btn-desktop-search d-flex button" value="Buscar" aria-label="Buscar">
                        <svg id="svgLupa" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                            <path d="M508.5 468.9L387.1 347.5c-2.3-2.3-5.3-3.5-8.5-3.5h-13.2c31.5-36.5 50.6-84 50.6-136C416 93.1 322.9 0 208 0S0 93.1 0 208s93.1 208 208 208c52 0 99.5-19.1 136-50.6v13.2c0 3.2 1.3 6.2 3.5 8.5l121.4 121.4c4.7 4.7 12.3 4.7 17 0l22.6-22.6c4.7-4.7 4.7-12.3 0-17zM208 368c-88.4 0-160-71.6-160-160S119.6 48 208 48s160 71.6 160 160-71.6 160-160 160z"></path>
                        </svg>                            
                    </button> 
                </div>
                <!--Fin contenedor tarjeta y buscador-->

                <!--TODOS LOS PRODUCTOS DE PAWS.-->
                <div id="contenedorRpt">
                <asp:Repeater ID="rptProductos" runat="server">
                    <HeaderTemplate>
                        <div class="row">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div id="colRptProd" class="col-md-3 colRptProd">
                            <div id="cardRptProd" class="card">
                                <!-- Imagen del producto -->
                                <img src='<%# Eval("ImagenUrl") %>' class="card-img-top" alt="Imagen del producto">
                                <div class="card-body text-center">
                                    <!-- Nombre del producto -->
                                    <h5 id="nombreProducto" class="card-title"><%# Eval("Nombre") %></h5>
                                    <!-- Precio del producto -->
                                    <!--<p id="precioProducto" class="card-text">Precio: $<%# Eval("Precio") %></p>-->
                                    <!-- Puntos del producto -->
                                    <p id="puntosProducto" class="card-text">Puntos: <%# Eval("Puntos") %></p>
                                    <!-- Botón para realizar el canje-->
                                    <asp:Button ID="btnCanjear" runat="server" Text="Canjear" CssClass="btn btn-primary" OnCommand="btnCanjear_Command" CommandArgument='<%# Eval("Nombre") %>' style="background-color: #ff6a00; margin-left: 2%; margin-top: 10%;"/>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>

                <!--PRODUCTOS FILTRADOS POR BÚSQUEDA-->
                <asp:Repeater ID="rptProductosBusqueda" runat="server" Visible="false">
                    <HeaderTemplate>
                        <div class="row">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="col-md-3" style="margin-top:3%;">
                            <div class="card" style="border: 2px solid #ff6a00;">
                                <img src='<%# Eval("ImagenUrl") %>' class="card-img-top" alt="Imagen del producto">
                                <div class="card-body text-center">
                                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                    <p class="card-text">Puntos: <%# Eval("Puntos") %></p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
                </div>
                <!--Ubicamos al label con el msj del canje fuera del rpt porque dentro nos seguia tirando el error de que no existía en el contexto actual-->
                <asp:Label ID="mensajeCanje" runat="server" CssClass="mt-3" Text="" EnableViewState="false"></asp:Label>
            <!--Fin repeater productos-->

            </div>
            <!--Fin div cotainer padre de todos.-->

            <!-- Bootstrap JS Bundle -->
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js">
            </script>

            <script type="text/javascript">
                //Función que esconderá el mensajeCanje a los 2 segundos de ser mostrado.
                function mostrarMensajeYOcultar()
                {
                    var mensaje = document.getElementById('<%= mensajeCanje.ClientID %>');
                    mensaje.style.display = 'block';  // Muestra el mensaje
                    setTimeout(function () {
                        mensaje.style.display = 'none';  // Oculta el mensaje después de 2 segundos
                    }, 1000); // 2000 milisegundos = 2 segundos
                }
            </script>


            <!--CSS-->
            <style>

                * 
                {
                    box-sizing:border-box;
                }

                #titulo
                {
                    font-family: Calibri;
                }

                #subtitulo
                {
                    font-family: Calibri;

                }

                #nombreProducto
                {
                    font-family:'Calibri';
                    color:#ff6a00;
                }

                #precioProducto
                {
                    font-family: 'Malgun Gothic';
                    color:#3589bf;
                    margin-top:16%;
                }

                #puntosProducto
                {
                    font-family: 'Malgun Gothic';
                    color:#ff6a00;
                    margin-top:0%;
                }

                #txtBuscarProducto:focus 
                {
                    outline: none; 
                    border-top: 2px solid #ff6a00;
                    border-bottom: 2px solid #ff6a00;
                    border-left: none;
                    border-right: none;
                    box-shadow: 3px 3px 3px #ff6a00;
                }

                 /* Cambiar el color del placeholder */
                #txtBuscarProducto::placeholder 
                {
                    color: #3589bf; /* Color deseado */
                    opacity: 1; /* Opcional: Para asegurar que el color se vea completamente */
                }


                .btn-primary:hover {
                    background-color: #45a049; /* Verde oscuro */
                }

                .modal-header {
                    background-color: #f8f9fa;
                    border-bottom: 1px solid #dee2e6;
                }

                .modal-body 
                {
                    max-height: 400px;
                    overflow-y: auto;
                }

            </style>
        </body>
    </html>

</asp:Content>
