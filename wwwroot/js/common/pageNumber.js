function getPageNumHtml(count, iPageSize, iCurrentPage) {
    var liItem = "";

    //count = 800;
    //获取总的页数
    var pagernum1 = 0;
    if (count % iPageSize == 0) {
        pagernum1 = count / iPageSize;
    }
    else {
        pagernum1 = parseInt(count / iPageSize) + 1;
    }
    if (iCurrentPage > pagernum1) {
        iCurrentPage = 1;
    }



    else if (pagernum1 == 1) { liItem += "  <li class='page-item'><span class='page-link'>1</span></li>"; }
    else if (pagernum1 <= 10) {
        if (iCurrentPage == 1) {

            for (var i = 1; i <= pagernum1; i++) {
                if (iCurrentPage == i) {
                    liItem += " <li class='page-item'><a class='page-link' href='javascript:void(0)'>" + i + "</a></li>";
                }
                else {
                    liItem += " <li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + i + ")'>" + i + "</a></li>";
                }
            }
            liItem += "<li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + (iCurrentPage*1 + 1) + ")'>Next</a></li> ";


        }
        else if (iCurrentPage < pagernum1) {
            liItem += "<li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + (iCurrentPage - 1) + ")'>Previous</a></li> ";
            for (var i = 1; i <= pagernum1; i++) {
                if (iCurrentPage == i) {
                    liItem += "  <li class='page-item'><a class='page-link' href='javascript:void(0)'>" + i + "</a></li>";
                }
                else {
                    liItem += " <li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + i + ")'>" + i + "</a></li>";
                }
            }

            liItem += "<li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + (iCurrentPage*1 + 1) + ")'>Next</a></li> ";
        }
        else if (iCurrentPage == pagernum1) {
            liItem += "<li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + (iCurrentPage - 1) + ")'>Previous</a></li> ";
            for (var i = 1; i <= pagernum1; i++) {
                if (iCurrentPage == i) {
                    liItem += "  <li class='page-item'><a class='page-link' href='javascript:void(0)'>" + i + "</a></li>";
                }
                else {
                    liItem += " <li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + i + ")'>" + i + "</a></li>";
                }
            }
            liItem += "";
        }
    }
    else if (pagernum1 > 10) {
        if (iCurrentPage == 1) {
            liItem += "";
            for (var i = 1; i <= 11; i++) {
                if (i == 11) {
                    liItem += " <li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + i + ")'>...</a></li>";
                }
                else {
                    if (iCurrentPage == i) {
                        liItem += "  <li class='page-item'><a class='page-link' href='javascript:void(0)'>" + i + "</a></li>";
                    }
                    else {
                        liItem += " <li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + i + ")'>" + i + "</a></li>";
                    }
                }
            }
            liItem += "<li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + (iCurrentPage*1 + 1) + ")'>Next</a></li> ";
        }
        else if (iCurrentPage <= pagernum1) {
            var j = parseInt(iCurrentPage / 10);
            if (iCurrentPage % 10 == 0) {
                j = j - 1;
            }
            if (pagernum1 > (j * 10 + 11)) {

                liItem += "<li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + (iCurrentPage - 1) + ")'>Previous</a></li>";
                if (j>0) {
                    liItem += "<li class='page-item'><a  class='page-link' href='javascript:void(0)' onclick='queryList(" + (j * 10 == 0 ? 1 : j * 10) + ")'>...</a></li>";
                }
                for (var i = j * 10 + 1; i <= j * 10 + 10; i++) {
                    if (iCurrentPage == i) {
                        liItem += "  <li class='page-item'><a class='page-link' href='javascript:void(0)'>" + i + "</a></li>";
                    }
                    else {
                        liItem += " <li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + i + ")'>" + i + "</a></li>";
                    }
                }
                liItem += "<li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + ((j + 1) * 10 + 1) + ")'>...</a></li> ";
            }
            else {

                liItem += "<li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + (iCurrentPage - 1) + ")'>Previous</a></li> <li><a  href='javascript:void(0)' onclick='queryList(" + (j * 10 == 0 ? 1 : j * 10) + ")'>...</a></li>";
                for (var i = j * 10 + 1; i <= pagernum1; i++) {
                    if (iCurrentPage == i) {
                        liItem += "  <li class='page-item'><a class='page-link' href='javascript:void(0)'>" + i + "</a></li>";
                    }
                    else {
                        liItem += " <li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + i + ")'>" + i + "</a></li>";
                    }
                }

            }
            if (iCurrentPage == pagernum1) {
                liItem += "<li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + (iCurrentPage * 1) + ")'>Next</a></li> ";
            } else {
                liItem += "<li class='page-item'><a class='page-link' href='javascript:void(0)' onclick='queryList(" + (iCurrentPage * 1 + 1) + ")'>Next</a></li> ";
            }
           
        }
        
    }
    return "<ul class=\"pagination justify-content-center mb-0\">" + liItem + " </ul>";
}