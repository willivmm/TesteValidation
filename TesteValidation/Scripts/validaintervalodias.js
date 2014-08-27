jQuery.validator.unobtrusive.adapters.add("validaintervalodias", ['diainicial', 'diafinal'], function (options) {
    options.rules['validaintervalodias'] = options.params;
    if (options.message) {
        options.messages['validaintervalodias'] = options.message;
    }
});

jQuery.validator.addMethod("validaintervalodias", function (value, element, params) {
    if (value) {
        var dd = value.substring(0, 2);
        var mm = value.substring(3, 5);
        var yyyy = value.substring(6);
        var dateValue = Date.parse(mm + "/" + dd + "/" + yyyy);
        var date = new Date(dateValue);
        var dateStart = new Date();
        var dateEnd = new Date();
        var firstDay = parseInt(params.firstday);
        var secondDay = parseInt(params.secondday);
        dateStart.setDate(dateStart.getDate() + firstDay);
        dateEnd.setDate(dateEnd.getDate() + secondDay);
        dateStart.setHours(0, 0, 0, 0);
        dateEnd.setHours(0, 0, 0, 0);

        if (date >= dateStart && date <= dateEnd) {
            return true;
        }
        return false;
    }
    return false;
});