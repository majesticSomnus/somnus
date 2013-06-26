function HighlightTableRows(tableId) {
    var rows = $("#" + tableId + " tbody tr");
    var length = rows.length;
    if (length > 0) {
        $(rows[0]).addClass("head");
    }
    for (var i = 1; i < length; i++) {
        var row = $(rows[i]);
        if (i % 2 === 1) {
            row.addClass("odd");
        } else {
            row.addClass("even");
        }
        row.mouseover(function () { $(this).addClass("over"); });
        row.mouseout(function () { $(this).removeClass("over"); });
    }
}