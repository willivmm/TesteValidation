jQuery.validator.unobtrusive.adapters.add("validavencimento", ['outroatributo'], function (options) {
    options.rules['validavencimento'] = options.params;
    if (options.message) {
        options.messages['validavencimento'] = options.message;
    }
});

jQuery.validator.addMethod("validavencimento", function (value, element, params) {

    var hoje = new Date();
    var mesAtual = hoje.getMonth() + 1;
    var anoAtual = hoje.getFullYear();

    var outroAtributo = $('select[name="' + params.outroatributo + '"]').val();

    if (outroAtributo && value) {
        var valor = parseInt(value);
        var outroValor = parseInt(outroAtributo);

        if ($(element).attr('name').toLowerCase().indexOf('mes') >= 0) {
            if (parseInt(mesAtual) > valor && parseInt(anoAtual) >= outroValor) {
                $("[data-valmsg-for='" + params.outroatributo + "']").addClass('field-validation-valid');
                $("[data-valmsg-for='" + params.outroatributo + "']").removeClass('field-validation-error');
                $("[data-valmsg-for='" + params.outroatributo + "']").empty();
                return false;
            }
        } else {
            if (parseInt(mesAtual) > outroValor && parseInt(anoAtual) >= valor) {
                $("[data-valmsg-for='" + params.outroatributo + "']").addClass('field-validation-valid');
                $("[data-valmsg-for='" + params.outroatributo + "']").removeClass('field-validation-error');
                $("[data-valmsg-for='" + params.outroatributo + "']").empty();
                return false;
            };
        }

        return true;
    }
    else {
        return false;
    }
});