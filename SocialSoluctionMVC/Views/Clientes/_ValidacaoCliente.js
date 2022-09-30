$('#btnSalvar').click(function () {
    var vCpfCnpj = $("#CPFCNPJ").val();
    var isOK = false;
    $.get("https://localhost:44326/Clientes/ValidaCPFCNPJ/" + vCpfCnpj,
        function (data, status) {
            if (!data) {
                $("#CPFCNPJInvalidos").html('Informe um CPF ou CNPJ válido.');
            }
            else {
                isOK = true;
            }
        }
    );

    $.post("https://localhost:44326/Clientes/ValidaCPFCNPJ/",
        {
            id: 0,
            cpfCnpj: vCpfCnpj
        },
        function (data, status) {
            if (!data) {
                $("#CPFCNPJInvalidos").html('CPF ou CNPJ já vinculado a outro cliente.');
            }
            else {
                isOK = true;
            }
        }
    );
    if (isOK) {
        $("form").submit();
    }
});