function FillGaps(Number, Char) {
    var Space = "";
    for (var i = 0; i < Number; i++) {
        Space += Char;
    }
    return Space;
}