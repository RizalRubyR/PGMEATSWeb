var surveyID;

$(document).ready(function myfunction() {
    
    getUrlVars();

    // Load google charts
    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);
    google.charts.setOnLoadCallback(getchartByDepartment);
    google.charts.setOnLoadCallback(getchartByShift);

});




// Draw the chart and set the chart values
function drawChart() {
    var htmlsList = "";
    var udata = { SurveyID: surveyID };
    $.ajax({
        type: "POST",
        //contentType: "application/json; charset=UTF-8",
        url: "/SurveyPollsList/getchartheaderbyemployee/",
        dataType: 'json',
        data: JSON.stringify(udata),
        success: function (data) {
            $('#fill-by-employee').empty();
            var pData = data[0].Contents;
            if (data[0].ID == '1') {
                toastr.warning(data[0].Message, 'Warning', { timeOut: 3000, closeButton: true, positionClass: "toast-top-full-width" });
            }
            else {
                // Optional; add a title and set the width and height of the chart
                var pdata;
                var options;
                var dp = [];
                $.each(data[0].Contents, function (i, dt) {
                    //dp = [];
                    pdata = null;
                    options = null;
                    htmlsList = '<div id="piechart_' + dt.QuestionID + '" style="margin-bottom: -90px;"></div>';
                    $('#fill-by-employee').append(htmlsList);
                    var xdata = { SurveyID: dt.SurveyID, QuestionID: dt.QuestionID };
                    $.ajax({
                        type: "POST",
                        //contentType: "application/json; charset=UTF-8",
                        url: "/SurveyPollsList/getchartbyemployee/",
                        dataType: 'json',
                        data: JSON.stringify(xdata),
                        success: function (data) {
                            var o = 0;
                            dp = []
                            $.each(data[0].Contents, function (i, dt) {
                                var p =[]
                                if (o == 0) {
                                    p = ['Survey', 'Population']
                                    dp.push(p)
                                }
                                var x = ['' + dt.AnswerDesc + '', dt.Sub_total]
                                dp.push(x);
                                o += 1;
                            });
                        },
                        error: function (ex) {
                            toastr.error('Failed to retrieve this Data. ' + ex, 'Error', { timeOut: 3000, closeButton: true, positionClass: "toast-top-full-width" });
                           
                        }
                    }).done(function () {
                        var dp1 = [['Task', 'Hours per Day'], ['Work', 8], ['Eat', 2], ['TV', 4], ['Gym', 2], ['Sleep', 8]]
                        pdata = google.visualization.arrayToDataTable(dp);
                        options = { 'title': dt.QuestionDesc, 'width': 400, 'height': 400 };
                        var id = "piechart_" + dt.QuestionID;
                        var chart = new google.visualization.PieChart(document.getElementById(id));
                        chart.draw(pdata, options);
                        
                    });
                    
                });
            }
        },
        error: function (ex) {
            toastr.error('Failed to retrieve this Data. ' + ex, 'Error', { timeOut: 3000, closeButton: true, positionClass: "toast-top-full-width" });
            
        }
    }).done(function () {
       
    });
}

function getchartByDepartment() {
    var htmlsList = "";
    var udata = { SurveyID: surveyID };
    $.ajax({
        type: "POST",
        //contentType: "application/json; charset=UTF-8",
        url: "/SurveyPollsList/getchartByDepartment/",
        dataType: 'json',
        data: JSON.stringify(udata),
        success: function (data) {
           
            var pData = data[0].Contents;
            var lastCol;
            if (data[0].ID == '1') {
                toastr.warning(data[0].Message, 'Warning', { timeOut: 3000, closeButton: true, positionClass: "toast-top-full-width" });
            }
            else {
                //var data = google.visualization.arrayToDataTable(p);
                var datd = new google.visualization.DataTable();
                var deptbefore = '';
                var pHeader = ['Department']
                var pContent = []
                var pData = []
                //data.addColumn('string', 'Department');
                $.ajax({
                    type: "POST",
                    //contentType: "application/json; charset=UTF-8",
                    url: "/SurveyPollsList/Getlabel/",
                    dataType: 'json',
                    data: JSON.stringify(udata),
                    success: function (data) {
                        $.each(data[0].Contents, function (i, dt) {
                            //data.addColumn('number', '' + dt.AnswerDesc + '');
                            pHeader.push(dt.AnswerDesc)
                            lastCol = dt.LastCol;
                        })
                    },
                    error: function (ex) {
                        toastr.error('Failed to retrieve this Data. ' + ex, 'Error', { timeOut: 3000, closeButton: true, positionClass: "toast-top-full-width" });
                    }
                }).done(function (){
                    pData.push(pHeader)
                    var depart;
                    var val;
                    var pval = []
                    var o = 0
                    var oj = data[0].Contents
                    $.each(data[0].Contents, function (i, dt) {
                        if (deptbefore != dt.Department) {
                            if (pContent.length < (lastCol + 1) && deptbefore != "") {
                                for (var p = pContent.length; p < (lastCol +1); p++) {
                                    pContent.push(0)
                                } 
                            }
                            if (pContent.length > 0) {
                                pData.push(pContent)
                            }
                            pContent = []
                            //depart = dt.Department
                            //val = dt.Total + ','
                            pContent.push(dt.Department)
                            pContent.push(dt.Total)
                            /*o = 0;*/
                        } else {

                            pContent.push(dt.Total)
                            
                        }
                        if (i + 1 == data[0].Contents.length) {
                            if(pContent.length < (lastCol + 1) && deptbefore != "") {
                                for (var p = pContent.length; p < (lastCol + 1); p++) {
                                    pContent.push(0)
                                }
                            }
                        }
                        o += 1
                        if (o == oj.length) {
                            pData.push(pContent)
                        }
                        deptbefore = dt.Department
                    });
                    var pVal = google.visualization.arrayToDataTable(pData);
                    // Optional; add a title and set the width and height of the chart
                    var options = { 'title': '', 'width': 400, 'height': 400 };

                    // Display the chart inside the <div> element with id="piechart"
                    var chart = new google.visualization.BarChart(document.getElementById('by-department'));
                    chart.draw(pVal, options);
                });
                
            }
        },
        error: function (ex) {
            toastr.error('Failed to retrieve this Data. ' + ex, 'Error', { timeOut: 3000, closeButton: true, positionClass: "toast-top-full-width" });
            
        }
    });
}

function getchartByShift() {
    var htmlsList = "";
    var udata = { SurveyID: surveyID };
    $.ajax({
        type: "POST",
        //contentType: "application/json; charset=UTF-8",
        url: "/SurveyPollsList/getchartByShift/",
        dataType: 'json',
        data: JSON.stringify(udata),
        success: function (Result) {

            var pData = Result[0].Contents;
            var lastCol;
            if (Result[0].ID == '1') {
                toastr.warning(Result[0].Message, 'Warning', { timeOut: 3000, closeButton: true, positionClass: "toast-top-full-width" });
            }
            else {
                var datd = new google.visualization.DataTable();
                var Shiftbefore = '';
                var pHeader = ['Shift']
                var pContent = []
                var pData = []
                $.ajax({
                    type: "POST",
                    //contentType: "application/json; charset=UTF-8",
                    url: "/SurveyPollsList/Getlabel/",
                    dataType: 'json',
                    data: JSON.stringify(udata),
                    success: function (data) {
                        $.each(data[0].Contents, function (i, dt) {
                            pHeader.push(dt.AnswerDesc)
                            lastCol = dt.LastCol;
                        })
                    },
                    error: function (ex) {
                        toastr.error('Failed to retrieve this Data. ' + ex, 'Error', { timeOut: 3000, closeButton: true, positionClass: "toast-top-full-width" });
                    }
                }).done(function () {
                    pData.push(pHeader)
                    var depart;
                    var val;
                    var pval = []
                    var o = 0
                    var oj = Result[0].Contents
                    $.each(Result[0].Contents, function (i, dt) {
                        if (Shiftbefore != dt.Shift) {
                            if (pContent.length < (lastCol + 1) && Shiftbefore != "") {
                                for (var p = pContent.length; p < (lastCol + 1); p++) {
                                    pContent.push(0)
                                }
                            }
                            if (pContent.length > 2) {
                                pData.push(pContent)
                            } else if (pContent.length == 2) {
                                pContent.push(0)
                                pData.push(pContent)
                            }
                            pContent = []
                            pContent.push(dt.Shift)
                            pContent.push(dt.Total)
                        } else {
                            pContent.push(dt.Total)
                        }
                        o += 1
                        if (o == oj.length) {
                            pData.push(pContent)
                        }
                        Shiftbefore = dt.Shift
                    });

                    var pVal = google.visualization.arrayToDataTable(pData);
                    // Optional; add a title and set the width and height of the chart
                    var options = { 'title': '', 'width': 400, 'height': 400 };

                    // Display the chart inside the <div> element with id="piechart"
                    var chart = new google.visualization.BarChart(document.getElementById('by-Shift'));
                    chart.draw(pVal, options);
                });
            }
            
        },
        error: function (ex) {
            toastr.error('Failed to retrieve this Data. ' + ex, 'Error', { timeOut: 3000, closeButton: true, positionClass: "toast-top-full-width" });
            
        }
    });
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    surveyID = vars.id;
}