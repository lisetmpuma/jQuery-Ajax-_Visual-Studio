<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mywebform1.aspx.cs" Inherits="FormularioWeb6.mywebform1" %>

<!DOCTYPE html>

<html >
<head >

    
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v6.0.0-beta1/css/all.css">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script defer src="/Scripts/bootstrap.js"></script>
    <script src="./Scripts/jquery-3.4.1.js"></script>

     <style>
        
        .form-container {       /*  centrar el formulario */
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        #requerimiento {
            height: 120px;           /* Estilo personalizado para hacer el campo de Requerimiento más grande */
        }
    
        .half-width {
            width: 45%;                /* Estilo personalizado para ajustar el tamaño de los campos Nombre, Apellido, Email y Dirección */
        }
       
        .full-width {
            width: 90%;                 /* Estilo personalizado para ajustar el tamaño de Dirección y Ciudad */
        }
        
        .btn-custom {                   /* Estilo personalizado para los botones */
            padding: 10px 20px;
            font-size: 16px;
            font-weight: bold;
        }
      
        .custom-form {                   /* Estilo personalizado para el formulario */
            background-color: #f5f5f5;
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        
        .form-title {                   /* Estilo personalizado para el título del formulario */
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
            text-align: center;
        }
       
        .label-ciudad {                 /* Estilo personalizado para el campo de Ciudad */
            display: block;
            margin-bottom: 5px;
        }
       
        .form-check-label {                /* Estilo personalizado para el campo de Sexo */
            margin-right: 10px;
        }
    </style>



</head>
     
<body>

     <div class="container form-container">
       <form runat="server" class="col-lg-6 custom-form">
            <div class="form-title">Formulario de Registro</div>

            <!-- 1ra línea -->
            <div class="form-group">
                <label for="Nombre">Nombre:</label>
                <asp:TextBox ID="Nombre" runat="server" name="nombre" class="form-control half-width"   placeholder="Nombre" ClientIDMode="Static" ></asp:TextBox>
            </div>

            <!-- 2da línea -->
            <div class="form-group">
                <label for="Apellidos">Apellidos:</label>
                <asp:TextBox ID="Apellidos" runat="server" name="apellidos" class="form-control half-width"  type="text" placeholder="Apellidos" ClientIDMode="Static"> </asp:TextBox>
            </div>


                <div class="col-sm-6 text-white p-2 rounded" id="boxRegisterError" style="display: none">
                    <div class="mb-1" id="text"></div>
                </div>

             
             

            <!-- 3ra línea (Sexo) -->
            <div class="form-group">
                <label>Sexo:</label>
                <br>
                <div class="form-check form-check-inline">
                    <asp:RadioButton ID="Masculino" runat="server" GroupName="sexo" CssClass="form-check-input" />
                    <label class="form-check-label" for="masculino">Masculino</label>
                </div>
                <div class="form-check form-check-inline">
                    <asp:RadioButton ID="Femenino" runat="server" GroupName="sexo" CssClass="form-check-input" />
                    <label class="form-check-label" for="femenino">Femenino</label>
                </div>
            </div>

            <!-- 4ta línea -->
            <div class="form-group">
                <label>Email:</label>
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text"><i class="fa fa-at"></i></span>
                    </div>
                    <asp:TextBox ID="Correo" runat="server" class="form-control half-width"></asp:TextBox>
                </div>
            </div>

            <!-- 5ta línea (Dirección y Ciudad en la misma línea) -->
            <div class="form-row">
                <div class="form-group col-md-6">
                    <label for="Direccion">Dirección:</label>
                    <asp:TextBox ID="Direccion" runat="server" name="direccion" class="form-control full-width"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <label class="label-ciudad">Ciudad:</label>
                    <asp:DropDownList ID="Ciudad" runat="server" class="form-control full-width">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            

            <!-- 6ta línea -->
            <div class="form-group">
                <label for="Requerimiento">Requerimiento:</label>
                <asp:TextBox ID="Requerimiento" runat="server" name="requerimiento" class="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>

            <!-- 7ma línea -->
            <div class="form-group text-center">
                <asp:Button ID="Enviar" runat="server" Text="Enviar"  data-bs-toggle="modal" data-bs-target="#staticBackdrop" OnClientClick="validar();" OnClick="EnviarClick" class="btn btn-success" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="Eliminar_Click" type="button" class="btn btn-danger btn-custom ml-2" />
            </div>

           
            <div class="container">
            <section class="row">
                <asp:Panel ID="datosDiv" runat="server" class="mt-4 border border-2 col-md-6"></asp:Panel>
            </section>
        </div>


      

  </form>
 </div>

    <!-- referencia a Bootstrap JS -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    
    <script>

        let Nombre = document.querySelector('#Nombre');
        let Apellidos = document.querySelector('#Apellidos');
        let Email = document.querySelector('#Email');
        let Masculino = document.getElementById('Masculino');
        let Femeninno = document.getElementById('Femenino');
        let Direccion = document.querySelector('#Direccion');
        let Ciudad = document.querySelector('#Ciudad');
        let Requerimiento = document.querySelector('#Requerimiento');
       

        

        function validar() {

            let Nombre = document.getElementById("Nombre").value;       
            let Apellidos = document.getElementById("Apellidos").value;

            let Masculino = document.getElementById("Masculino").checked;
            let Femenino = document.getElementById("Femenino").checked;

            let Ciudad = document.getElementById("Ciudad").selectedIndex;
            let Correo = document.getElementById("Correo").value;

            let validarNombreApellido = /^[a-zA-Z\s]+$/;
            let Dominio = Correo.substring(Correo.lastIndexOf('@') + 1);



            if (!validarNombreApellido.test(Nombre.trim())) {                      //telimina espacios en blanco al inicio y final (trim:) ,despues evalua  si el cnteniddo de nombre ocoincide con el patron validarnombreapellido
                alert("Error en Nombre");
                return false;
            }

            if (!validarNombreApellido.test(Apellidos.trim())) {
                alert("Error en Apellidos");
                return false;
            }

            if (!Masculino && !Femenino) {
                alert("ERROR seleccionar Sexo");
                return false;
            }

            if (Dominio !== 'unsa.edu.pe') {
                alert("Error en correo");
                return false;
            }


            if (Ciudad === 0) {                                 //1er intento funcioa cambiando "" por 0
                alert("ERROR seleccionar Ciudad");
                return false;
            }


            alert(Date());


            return false;
        }


        function verificarAjax(Nombre, Apellidos) {

            var jsonData = {
                Nombre: Nombre,
                Apellidos: Apellidos,
            };
            console.log(JSON.stringify(jsonData));
            $.ajax({
                url: 'mywebform1.aspx/getInfo',
                type: 'POST',
                async: true,
                data: JSON.stringify(jsonData),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: exito,
            })
            return false;
        }

        function exito(data) {
            let verify = data.d;
            $('#boxRegisterError').css("display", "block");
            $('#text').text(data.d);

            var palabraBuscada = "words";
            var palabraEncontrada = verify.includes(palabraBuscada);
            $('#boxRegisterError').removeClass('bg-danger bg-success');
            if (palabraEncontrada) {
                $('#boxRegisterError').addClass('bg-danger');
            } else {
                $('#boxRegisterError').addClass('bg-success');
            }
            return false; }

        function verificarApellidoEnServidor() {
           let Nombre = document.getElementById("<%=Nombre.ClientID%>").value;
           let  Apellidos = document.getElementById("<%=Apellidos.ClientID%>").value;
            verificarAjax(Nombre, Apellidos);
            console.log("Nombre:", Nombre);
            console.log("Apellidos:", Apellidos);
        }
        document.getElementById("<%=Apellidos.ClientID%>").addEventListener('input', verificarApellidoEnServidor);





    </script>


</body>
</html>
