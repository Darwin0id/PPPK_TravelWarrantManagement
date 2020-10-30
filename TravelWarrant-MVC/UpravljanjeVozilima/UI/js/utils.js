//CHECK IF TAGLE IS EMPTY?
isTableEmpty = (tableName = ".table") => {
    const isEmpty = $(tableName+" tr").length <= 1;
    const numOfThEl = $(tableName).find("th").length;
    if(isEmpty) {
        $(tableName).find("tbody").append("<tr><td class='text-center' colspan='"+numOfThEl+"'>Tablica je prazna!</td></tr>")
    }
}

//CHECK TYPE OF INPUTS
$(document).ready(function () {
    $('[data-phone=true]').mask('(000)00000000');
    $('[data-licenseid=true]').mask('AAAAAAAA');
});