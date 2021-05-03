
var Items = new Object();
Items.Salarios = new Array();
var gridSalarios = document.getElementById("gridSalarios");



function AddSalario() {

    var id_empleado = document.getElementById("cboEmpleado").value;

    var anyo = document.getElementById("txtAnyo").value;
    var mes = document.getElementById("cboMes").value;
    var salarioBase = document.getElementById("txtSalarioBase").value;
    var bonoProduccion = document.getElementById("txtBonoProduccion").value;
    var bonoCompensacion = document.getElementById("txtBonoCompensacion").value;
    var comision = document.getElementById("txtComision").value;
    var contribucion = document.getElementById("txtContribucion").value;
    //var SubTotal = document.getElementById("txtSubTotal").value;
    var SalarioTotal = 0;


    var sel = document.getElementById("cboMes");
    var mesDesc = sel.options[sel.selectedIndex].text;

    var fvalid = true;

    $.each(Items.Salarios, function (index, value) {

        if (value.Year == anyo && value.Month == mes) {
            fvalid = false;
            return;
        }

    });

    if (fvalid == false) {
        alert("Un salario para el mes ya esta en la lista");
        return;
    }


    var regSal = {
        "EmployeeId": id_empleado,
        "Year": anyo,
        "Month": mes,
        "BaseSalary": salarioBase,
        "ProductionBonus": bonoProduccion == "" ? 0 : bonoProduccion,
        "CompensationBonus": bonoCompensacion == "" ? 0 : bonoCompensacion,
        "Commission": comision == "" ? 0 : comision,
        "Contributions": contribucion == "" ? 0 : contribucion,
    };


    $.ajax({
        type: "POST",
        url: "/Salarios/Validar",
        data: regSal,
        success: function (response) {

            var mensaje = response.mensaje;

            if (mensaje.startsWith("Success")) {

                var res_total = mensaje.replace("Success:", "");
                SalarioTotal = res_total * 1;

                Items.Salarios.push(regSal);

                document.getElementById("txtJsonSalarios").value = JSON.stringify(Items.Salarios);

                var lineGrid =
                    "<tr id='tr_" + mes + "'>" +
                    "<td>" + anyo + "</td>" +
                    "<td>" + mesDesc + "</td>" +
                    "<td>" + salarioBase + "</td>" +
                    "<td>" + bonoProduccion + "</td>" +
                    "<td>" + bonoCompensacion + "</td>" +
                    "<td>" + comision + "</td>" +
                    "<td>" + contribucion + "</td>" +
                    "<td> $ " + SalarioTotal + "</td>" +
                    "</tr>";

                gridSalarios.innerHTML += lineGrid;

                //var total = Number(document.getElementById("txtTotal").value.toString().replace(",", ".")) + Number(SalarioTotal);
                //document.getElementById("txtTotal").value = fixaDuasCasasDecimais(total).replace(".", ",");

                document.getElementById("txtAnyo").value = "";
                document.getElementById("txtSalarioBase").value = "";
                document.getElementById("txtBonoProduccion").value = "";
                document.getElementById("txtBonoCompensacion").value = "";
                document.getElementById("txtComision").value = "";
                document.getElementById("txtContribucion").value = "";
                document.getElementById("cboMes").selectedIndex = -1;
            }
            else
            {
                alert(mensaje);
            }


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });


    
}



function validar_salario(salario) {

   

}


function Listado() {
    Request("");
}

function Request(request) {
    window.location = window.origin + "\\Salarios\\" + request;
}