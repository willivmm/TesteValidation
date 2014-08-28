function validaCartaoCredito(cardNumber, cardType) {
    var isValid = false;
    var ccCheckRegExp = /[^\d ]/;
    isValid = !ccCheckRegExp.test(cardNumber);
    if (isValid) {
        var cardNumbersOnly = cardNumber.replace(/ /g, "");
        var cardNumberLength = cardNumbersOnly.length;
        var lengthIsValid = false;
        var prefixIsValid = false;
        var prefixRegExp;
        switch (cardType) {
            case "mastercard":
                lengthIsValid = (cardNumberLength == 16);
                prefixRegExp = /^5[1-5]/;
                break;
            case "visa":
                lengthIsValid = (cardNumberLength == 16 || cardNumberLength == 13);
                prefixRegExp = /^4/;
                break;
            case "amex":
                lengthIsValid = (cardNumberLength == 15);
                prefixRegExp = /^3(4|7)/;
                break;

            case "hipercard":
                lengthIsValid = (cardNumberLength == 16 || cardNumberLength == 19);
                prefixRegExp = /^((384100)|(384140)|(384160)|(606282))+\d{10,13}/;
                break;

            default:
                prefixRegExp = /^$/;
        }

        prefixIsValid = prefixRegExp.test(cardNumbersOnly);
        isValid = prefixIsValid && lengthIsValid;
    }
    if (isValid) {
        if (cardType != "hipercard") {

            var numberProduct;
            var checkSumTotal = 0;
            for (digitCounter = cardNumberLength - 1; digitCounter >= 0; digitCounter--) {
                checkSumTotal += parseInt(cardNumbersOnly.charAt(digitCounter));
                digitCounter--;
                numberProduct = String((cardNumbersOnly.charAt(digitCounter) * 2));
                for (var productDigitCounter = 0; productDigitCounter < numberProduct.length; productDigitCounter++) {
                    checkSumTotal += parseInt(numberProduct.charAt(productDigitCounter));
                }
            }
            isValid = (checkSumTotal % 10 == 0);

        }
    }

    return isValid;
};
jQuery.validator.addMethod("validacartao", function (value, element, params) {
    if (value) {
        var bandeira = 0;

        if ($("input[name='" + params.bandeira + "']").length > 1) {
            bandeira = $("input[name='" + params.bandeira + "']:checked").val();
        }
        else {
            if ($("input[name='" + params.bandeira + "']").length == 1)
                bandeira = $("input[name='" + params.bandeira + "']").val();
        }

        var nomeBandeira = "";
        if (bandeira) {
            switch (bandeira) {
                case "1":
                    nomeBandeira = "visa";
                    break;
                case "2":
                    nomeBandeira = "mastercard";
                    break;
                case "5":
                    nomeBandeira = "amex";
                    break;
            }
        }

        value = value.replace(/[.//-]/g, "");
        return validaCartaoCredito(value, nomeBandeira);
    }
    else {
        return false;
    }
}, '');
jQuery.validator.unobtrusive.adapters.add("validacartao", ['bandeira'], function (options) {
    options.rules['validacartao'] = options.params;
    options.messages['validacartao'] = options.message;
});