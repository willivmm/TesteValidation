jQuery.validator.addMethod("validaintervalovalores", function (value, element, params) {
    var minvalue = document.getElementById(params.minvalue).value;
    var maxvalue = document.getElementById(params.maxvalue).value;
    if (value && minvalue && maxvalue) {
        var valor = value.replace(",", ".");
        valor = parseFloat(valor);
        var valorMinimo = minvalue.replace(",", ".");
        valorMinimo = parseFloat(valorMinimo).toFixed(2);
        var valorMaximo = maxvalue.replace(",", ".");
        valorMaximo = parseFloat(valorMaximo).toFixed(2);

        if (valor >= valorMinimo && valor <= valorMaximo) {
            return true;
        }
        return false;
    }
    return false;
}, '');
jQuery.validator.unobtrusive.adapters.add("validaintervalovalores", ['valorminimo', 'valormaximo'], function (options) {
    options.rules['validaintervalovalores'] = options.params;
    if (options.message) {
        options.messages['validaintervalovalores'] = options.message;
    }
});